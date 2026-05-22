using System.IO.Compression;
using System.Text.Json.Nodes;
using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Index.Strtree;
using NetTopologySuite.IO;
using NetTopologySuite.Operation.Distance;
using NetTopologySuite.Operation.Polygonize;
using NetTopologySuite.Triangulate;

// Coastline smoothing for the electorate maps.
//
// We don't care about strict coastlines for electorate lookup/rendering, but we DO care about the
// internal borders between electorates. So this smooths only the coastline while keeping internal
// borders at full precision.
//
// It works on the arc topology: a boundary arc shared by two electorates (degree 2) is an internal
// border; an arc belonging to one electorate (degree 1) is coastline. Only degree-1 arcs are
// simplified (Douglas-Peucker). Because simplification preserves arc endpoints (the junction nodes),
// the smoothed coast re-stitches exactly to the untouched internal borders - no slivers, no
// electorate loss.
//
// Applied to australia.geojson during sync (before it is split into states/electorates), so every
// derived geojson inherits the smoothed coast.
static class LocatorMapBuilder
{
    // ~100m. Coast only; large enough to drop coastline detail, small enough that coastal towns
    // stay inside their electorate (no outward buffer needed).
    const double coastTolerance = 0.001;

    // ~10km in degrees. The locator map's coastline is expanded outward into the ocean so that
    // coastal/near-shore points resolve. Only the coast is expanded (offshore "skirt" = buffer minus
    // land), so internal borders stay exact. The expanded ocean is partitioned by nearest land so
    // skirts never overlap and an offshore point resolves to exactly one electorate.
    const double expandDistance = 0.09;

    // ~1km. Spacing for the boundary points fed into the nearest-land Voronoi partition. Smaller is
    // more accurate (smoother offshore borders) but slower; ~1km is plenty for a 10km skirt.
    const double siteSpacing = 0.01;

    // Large Queensland reef electorates excluded from coast expansion.
    static readonly HashSet<string> excludeFromExpansion = new(StringComparer.OrdinalIgnoreCase)
    {
        "Kennedy", "Maranoa", "Leichhardt", "Dawson", "Herbert",
        "Capricornia", "Flynn", "Hinkler", "Wide Bay"
    };

    // Simplifies the coastline (only the coast - internal borders between electorates are kept exact).
    public static Task SmoothCoastline(string geoJsonPath) =>
        Smooth(geoJsonPath, coastTolerance);

    static async Task Smooth(string geoJsonPath, double tolerance)
    {
        var year = Path.GetFileName(Path.GetDirectoryName(geoJsonPath)!);
        var topoPath = Path.Combine(DataLocations.TempPath, $"{year}-australia-topo.json");

        // build arc topology (shared borders deduplicated), simplify only single-electorate arcs, convert back
        await MapToGeoJson.ToTopoJson(topoPath, geoJsonPath);
        SimplifyCoastArcs(topoPath, tolerance);
        await MapToGeoJson.ToGeoJson(geoJsonPath, topoPath);

        // normalise to the repo's geojson format (bbox etc.)
        var featureCollection = JsonSerializerService.DeserializeGeo(geoJsonPath);
        featureCollection.FixBoundingBox();
        JsonSerializerService.SerializeGeo(featureCollection, geoJsonPath);
    }

    // Embeds the (already coast-smoothed and -expanded) current map for DataLoader.LocateElectorate.
    public static void BuildLocatorZip()
    {
        File.Delete(DataLocations.AustraliaFullZipPath);
        using var zip = ZipFile.Open(DataLocations.AustraliaFullZipPath, ZipArchiveMode.Create);
        var australia = Path.Combine(DataLocations.Maps2025Path, "australia.geojson");
        zip.CreateEntryFromFile(australia, "2025/australia.geojson", CompressionLevel.Optimal);
    }

    // Pushes each electorate's coastline out to sea by expandDistance, in place. The expanded ocean is
    // partitioned by nearest land (a generalised Voronoi over every electorate's boundary points), so the
    // offshore border between two electorates is the smooth medial line between their coasts. Each skirt is
    // clipped to its electorate's Voronoi region, so skirts never overlap and an offshore point resolves to
    // exactly one electorate. Excluded electorates are not expanded, but still take part in the partition
    // so they claim (and thus block) the ocean in front of their own coast - neighbours can't reach across it.
    // Before expanding, the thin slivers of no-man's-land between electorates (state-border misalignment
    // in the AEC source) are dissolved into the electorates so the land tiles with no gaps - that way the
    // expansion can never create an inland gap.
    // Applied to australia.geojson during sync (before it is split into states/electorates) so every
    // derived geojson inherits the smoothed, gap-free, expanded coast.
    public static void ExpandCoastline(string geoJsonPath)
    {
        // OverlayNG: robust union/difference/intersection. The legacy overlay throws a non-noded
        // intersection error on the full-resolution map.
        NtsGeometryServices.Instance = new(GeometryOverlay.NG);

        var features = new GeoJsonReader()
            .Read<FeatureCollection>(File.ReadAllText(geoJsonPath))
            .ToList();
        var geometries = features
            .Select(_ => GeometryFixer.Fix(_.Geometry))
            .ToArray();
        var factory = geometries[0].Factory;
        var names = features
            .Select(_ => (string) _.Attributes["electorateName"])
            .ToArray();

        // Nearest-land partition. Every electorate - including excluded ones - contributes boundary
        // points (downsampled to siteSpacing) tagged with its index, so the ocean is split by whichever
        // coast is nearest.
        var sites = new List<Coordinate>();
        var owner = new Dictionary<Coordinate, int>();
        for (var i = 0; i < geometries.Length; i++)
        {
            Coordinate? previous = null;
            foreach (var coordinate in geometries[i].Coordinates)
            {
                if (previous != null &&
                    coordinate.Distance(previous) < siteSpacing)
                {
                    continue;
                }

                previous = coordinate;
                owner[coordinate] = i;
                sites.Add(coordinate);
            }
        }

        var landUnion = factory.BuildGeometry(geometries).Union();

        var envelope = new Envelope();
        foreach (var geometry in geometries)
        {
            envelope.ExpandToInclude(geometry.EnvelopeInternal);
        }

        envelope.ExpandBy(expandDistance * 2);

        var voronoi = new VoronoiDiagramBuilder();
        voronoi.SetSites(sites);
        voronoi.ClipEnvelope = envelope;
        var diagram = voronoi.GetDiagram(factory);

        // union each owner's Voronoi cells into one "nearest-to-me" region
        var cellsByOwner = new Dictionary<int, List<Geometry>>();
        for (var i = 0; i < diagram.NumGeometries; i++)
        {
            var cell = diagram.GetGeometryN(i);
            if (cell.UserData is not Coordinate site ||
                !owner.TryGetValue(site, out var index))
            {
                continue;
            }

            if (!cellsByOwner.TryGetValue(index, out var list))
            {
                cellsByOwner[index] = list = [];
            }

            list.Add(cell);
        }

        var regions = cellsByOwner.ToDictionary(
            _ => _.Key,
            _ => factory.BuildGeometry(_.Value).Union());

        // Keep the original (pre-dissolve) land per electorate, so after expanding we can tell a real
        // landmass/island from a stray offshore skirt fragment (a band cell the polygonize left detached).
        // DissolveSlivers reassigns geometries[i], but this shallow copy keeps the original references.
        var rawGeometries = geometries.ToArray();

        // Dissolve inland no-man's-land into the electorates BEFORE expanding, so the electorates tile the
        // landmass with no gaps - then the coast expansion can't create any. The AEC source has thin
        // slivers between electorates from different states (their borders don't perfectly align); these
        // are filled and assigned to the nearest electorate (its Voronoi region).
        DissolveSlivers(geometries, regions, factory, landUnion);

        // Offshore expansion. Build a band around the (gap-free) land, cut it at each coastal junction
        // with a straight line continuing the internal border out to sea, polygonize the band into cells,
        // and give each cell to the electorate whose coast it fronts. Straight cuts give straight offshore
        // borders (no nearest-land "wrap" around headlands), and a single line per junction means no
        // sawtooth.
        var mergedLand = factory.BuildGeometry(geometries).Union();
        var expandedLand = mergedLand.Buffer(expandDistance);
        var band = expandedLand.Difference(mergedLand);
        var cuts = OffshoreCuts(geometries, mergedLand, band, factory);

        var dividers = cuts.IsEmpty ? band.Boundary : band.Boundary.Union(cuts);
        var polygonizer = new Polygonizer();
        polygonizer.Add(dividers);

        var boundaries = geometries.Select(_ => _.Boundary).ToArray();
        var indexTree = new STRtree<int>();
        for (var i = 0; i < geometries.Length; i++)
        {
            indexTree.Insert(geometries[i].EnvelopeInternal, i);
        }

        // assign each band cell to the (non-excluded) electorate whose coast it fronts (longest shared edge)
        var preparedBand = PreparedGeometryFactory.Prepare(band);
        var skirts = new Dictionary<int, List<Geometry>>();
        foreach (var cell in polygonizer.GetPolygons())
        {
            if (!preparedBand.Contains(cell.InteriorPoint))
            {
                continue; // a land cell, not an offshore skirt cell
            }

            var best = -1;
            var bestShared = 0d;
            foreach (var i in indexTree.Query(cell.EnvelopeInternal))
            {
                var shared = cell.Intersection(boundaries[i]).Length;
                if (shared > bestShared)
                {
                    bestShared = shared;
                    best = i;
                }
            }

            if (best < 0 || excludeFromExpansion.Contains(names[best]))
            {
                continue;
            }

            if (!skirts.TryGetValue(best, out var list))
            {
                skirts[best] = list = [];
            }

            list.Add(cell);
        }

        var expanded = new Geometry[features.Count];
        for (var i = 0; i < features.Count; i++)
        {
            var merged = geometries[i];
            if (skirts.TryGetValue(i, out var cells))
            {
                merged = merged.Union(factory.BuildGeometry(cells).Union());
            }

            // Drop stray disconnected fragments - parts with no real land of their own that sit out over
            // the water (a band cell the polygonize left detached, or a dissolved sliver assigned to an
            // electorate it doesn't touch). These render as disconnected shapes. Real islands, and the
            // enclosed inland gap-fill slivers that keep the landmass seamless, are kept.
            expanded[i] = WithoutStrayFragments(merged, rawGeometries[i], expandedLand, factory);
        }

        var result = new FeatureCollection();
        for (var i = 0; i < features.Count; i++)
        {
            // drop stray line/point fragments AND degenerate sliver holes left by the overlays, so every
            // feature is a clean Polygon/MultiPolygon with no zero-area "disconnected line" rings.
            result.Add(new Feature(CleanGeometry(expanded[i], factory), features[i].Attributes));
        }

        // Post-condition: no feature may keep a stray disconnected fragment (see IsStray). On the final map
        // such a fragment is indistinguishable from a small real island - the only reliable signal is
        // whether it overlaps the raw coastline - so the check has to run here, where the raw land is still
        // available. WithoutStrayFragments removes these; this guard fails the build if one slips through.
        AssertNoStrayFragments(result, rawGeometries, expandedLand, names);

        File.WriteAllText(geoJsonPath, new GeoJsonWriter().Write(result));

        // normalise to the repo's geojson format (bbox etc.)
        var featureCollection = JsonSerializerService.DeserializeGeo(geoJsonPath);
        featureCollection.FixBoundingBox();
        JsonSerializerService.SerializeGeo(featureCollection, geoJsonPath);
    }

    // ~440m. Morphological-closing radius used to find the thin slivers of no-man's-land between
    // electorates (state borders in the AEC source don't perfectly align). Channels/gaps narrower than
    // 2x this are dissolved; real bays/gulfs are wider and are left open.
    const double slatRadius = 0.004;

    // Dissolves thin slivers of no-man's-land between electorates by absorbing each into a single
    // electorate, in place, so the electorates tile the landmass with no gaps. Each sliver is given
    // WHOLE to the electorate whose Voronoi region covers most of it (not split along the medial line),
    // so the resulting border is the neighbouring electorate's existing edge - a clean line that leaves
    // that border unchanged, rather than a wiggly new dividing line.
    static void DissolveSlivers(Geometry[] geometries, Dictionary<int, Geometry> regions, GeometryFactory factory, Geometry landUnion)
    {
        // closing (dilate then erode) fills gaps/channels narrower than 2x slatRadius - i.e. the slivers,
        // whether they are enclosed in the source or open at the coast.
        var closed = landUnion.Buffer(slatRadius).Buffer(-slatRadius);
        var slivers = closed.Difference(landUnion);
        if (slivers.IsEmpty)
        {
            return;
        }

        var regionTree = new STRtree<int>();
        foreach (var (index, region) in regions)
        {
            regionTree.Insert(region.EnvelopeInternal, index);
        }

        // group each sliver under its owning electorate
        var byOwner = new Dictionary<int, List<Geometry>>();
        for (var s = 0; s < slivers.NumGeometries; s++)
        {
            var sliver = slivers.GetGeometryN(s);
            if (sliver.IsEmpty)
            {
                continue;
            }

            var owner = -1;
            var ownerArea = 0d;
            foreach (var index in regionTree.Query(sliver.EnvelopeInternal))
            {
                var area = sliver.Intersection(regions[index]).Area;
                if (area > ownerArea)
                {
                    ownerArea = area;
                    owner = index;
                }
            }

            if (owner < 0)
            {
                continue;
            }

            if (!byOwner.TryGetValue(owner, out var list))
            {
                byOwner[owner] = list = [];
            }

            list.Add(sliver);
        }

        foreach (var (index, list) in byOwner)
        {
            geometries[index] = geometries[index].Union(factory.BuildGeometry(list).Union());
        }
    }

    // ~12,000 m². Interior rings smaller than this are overlay artifacts (degenerate slivers that render
    // as disconnected lines), not real bays/gulfs, so they are dropped (the area is filled into the
    // electorate). Real coastal bays kept as holes are far larger than this.
    const double holeMinArea = 1e-6;

    // Produces a clean Polygon/MultiPolygon: drops line/point fragments and degenerate (sub-threshold)
    // interior rings, keeping each polygon's shell and its real holes.
    static Geometry CleanGeometry(Geometry geometry, GeometryFactory factory)
    {
        var polygons = new List<Geometry>();
        for (var i = 0; i < geometry.NumGeometries; i++)
        {
            if (geometry.GetGeometryN(i) is not Polygon polygon)
            {
                continue;
            }

            var holes = new List<LinearRing>();
            for (var h = 0; h < polygon.NumInteriorRings; h++)
            {
                var ring = (LinearRing) polygon.GetInteriorRingN(h);
                if (factory.CreatePolygon(ring).Area >= holeMinArea)
                {
                    holes.Add(ring);
                }
            }

            polygons.Add(factory.CreatePolygon((LinearRing) polygon.ExteriorRing, holes.ToArray()));
        }

        return factory.BuildGeometry(polygons);
    }

    // Keeps every polygon part of a (land + skirt) union except stray disconnected fragments (see IsStray).
    static Geometry WithoutStrayFragments(Geometry unioned, Geometry rawLand, Geometry coverage, GeometryFactory factory)
    {
        var parts = new List<Geometry>();
        for (var i = 0; i < unioned.NumGeometries; i++)
        {
            var part = unioned.GetGeometryN(i);
            if (!IsStray(part, rawLand, coverage))
            {
                parts.Add(part);
            }
        }

        return factory.BuildGeometry(parts);
    }

    // ~12km. Reach used to decide whether a land-less fragment is an enclosed inland gap (keep) or an
    // ocean-adjacent stray (drop). Matches the LocateElectorate_no_inland_gaps test so the two agree:
    // anything that test would treat as "open to the sea" is dropped, and only sea-locked gap-fills stay.
    const double enclosedReach = 0.11;

    // A part is a stray disconnected fragment if it has no real (pre-dissolve) land of its own AND it is
    // not an enclosed inland gap-fill. Real islands (which contain raw land - including the reef islands
    // of excluded electorates) are never stray. A land-less fragment is kept only when it is sea-locked
    // (a dissolved sliver plugging an inland gap); a land-less fragment open to the ocean (a detached
    // skirt cell, or a dissolved coastal notch) is stray and would render as a disconnected shape.
    // Enclosure is tested against the land-plus-skirt coverage (mergedLand buffered by expandDistance).
    // That region contains the final expanded union, so this stays consistent with the inland-gaps test:
    // a fragment it keeps can never leave a hole that test would later flag.
    static bool IsStray(Geometry part, Geometry rawLand, Geometry coverage)
    {
        if (part.Intersection(rawLand).Area > 0)
        {
            return false; // real land (mainland, island, or a gap-fill merged into the coast)
        }

        // no land of its own: stray unless the ring around it stays within the land+skirt coverage
        return part.Buffer(enclosedReach).Difference(coverage).Area > 1e-6;
    }

    // Fails if any expanded feature kept a stray disconnected fragment (see IsStray) - an ocean-adjacent
    // part with no land of its own, which would render as a disconnected shape.
    static void AssertNoStrayFragments(FeatureCollection result, Geometry[] rawGeometries, Geometry coverage, string[] names)
    {
        var strays = new List<string>();
        for (var i = 0; i < result.Count; i++)
        {
            var geometry = result[i].Geometry;
            for (var g = 0; g < geometry.NumGeometries; g++)
            {
                var part = geometry.GetGeometryN(g);
                if (part is Polygon &&
                    IsStray(part, rawGeometries[i], coverage))
                {
                    var point = part.InteriorPoint;
                    strays.Add($"{names[i]} at {point.X:F4},{point.Y:F4} (area {part.Area:E2})");
                }
            }
        }

        if (strays.Count > 0)
        {
            throw new(
                $"Expanded map has {strays.Count} stray disconnected fragment(s) with no land of their own:" +
                Environment.NewLine +
                string.Join(Environment.NewLine, strays));
        }
    }

    // The polygonal parts of a geometry, dropping any line/point components.
    static Geometry Polygonal(Geometry geometry, GeometryFactory factory)
    {
        if (geometry is Polygon or MultiPolygon)
        {
            return geometry;
        }

        var polygons = new List<Geometry>();
        for (var i = 0; i < geometry.NumGeometries; i++)
        {
            if (geometry.GetGeometryN(i) is Polygon polygon)
            {
                polygons.Add(polygon);
            }
        }

        return factory.BuildGeometry(polygons);
    }

    // Builds the straight border-extension cuts that divide the offshore band. Where an internal land
    // border meets the coast, a straight line continuing that border out to sea (clipped to the band)
    // separates the two electorates' frontage cells - so the offshore boundary is a straight continuation
    // of the land border, not the nearest-land medial (which wraps around headlands). One line per
    // junction means no sawtooth.
    static Geometry OffshoreCuts(Geometry[] land, Geometry mergedLand, Geometry band, GeometryFactory factory)
    {
        var coast = mergedLand.Boundary;
        var coastDistance = new IndexedFacetDistance(coast);
        var preparedCoast = PreparedGeometryFactory.Prepare(coast);
        var preparedLand = PreparedGeometryFactory.Prepare(mergedLand);
        var boundaries = land.Select(_ => _.Boundary).ToArray();

        // only coastal electorates can have a coastal junction
        var coastal = new List<int>();
        var tree = new STRtree<int>();
        for (var i = 0; i < land.Length; i++)
        {
            if (preparedCoast.Intersects(boundaries[i]))
            {
                coastal.Add(i);
                tree.Insert(land[i].EnvelopeInternal, i);
            }
        }

        const double coastEps = 0.0005; // ~55m: endpoint counts as "on the coast" (vs an inland tripoint)
        const double sideEps = 0.001;   // ~110m: tiny inland start so the cut connects to the coast
        var cutLength = expandDistance * 2; // must reach across the ~10km band

        var cuts = new List<Geometry>();
        foreach (var i in coastal)
        {
            foreach (var j in tree.Query(land[i].EnvelopeInternal))
            {
                if (j <= i)
                {
                    continue;
                }

                foreach (var line in LineStrings(boundaries[i].Intersection(boundaries[j])))
                {
                    var coords = line.Coordinates;
                    foreach (var (end, inward) in new[] { (coords[0], coords[1]), (coords[^1], coords[^2]) })
                    {
                        if (coastDistance.Distance(factory.CreatePoint(end)) > coastEps)
                        {
                            continue; // inland endpoint (a tripoint), not a coastal junction
                        }

                        // continue the land border's own direction (from the last inland vertex out through
                        // the coastal junction) seaward - this aligns the cut with the actual border so the
                        // band splits into clean per-electorate frontage cells.
                        var dx = end.X - inward.X;
                        var dy = end.Y - inward.Y;
                        var length = Math.Sqrt(dx * dx + dy * dy);
                        if (length < 1e-9)
                        {
                            continue;
                        }

                        dx /= length;
                        dy /= length;

                        // the cut must head into the ocean, not back into land
                        if (preparedLand.Contains(factory.CreatePoint(new Coordinate(end.X + dx * sideEps, end.Y + dy * sideEps))))
                        {
                            continue;
                        }

                        // straight ray from just inland of the junction out to sea, clipped to the band
                        var ray = factory.CreateLineString(
                        [
                            new Coordinate(end.X - dx * sideEps, end.Y - dy * sideEps),
                            new Coordinate(end.X + dx * cutLength, end.Y + dy * cutLength)
                        ]);
                        // keep only the line parts (the clip can also yield touch points)
                        foreach (var segment in LineStrings(ray.Intersection(band)))
                        {
                            cuts.Add(segment);
                        }
                    }
                }
            }
        }

        return cuts.Count == 0 ? factory.BuildGeometry(cuts) : factory.BuildGeometry(cuts).Union();
    }

    // The LineString components of a geometry (drops points).
    static IEnumerable<LineString> LineStrings(Geometry geometry)
    {
        switch (geometry)
        {
            case LineString line when line.NumPoints >= 2:
                yield return line;
                break;
            case GeometryCollection collection:
                for (var i = 0; i < collection.NumGeometries; i++)
                {
                    foreach (var line in LineStrings(collection.GetGeometryN(i)))
                    {
                        yield return line;
                    }
                }

                break;
        }
    }

    static void SimplifyCoastArcs(string topoPath, double tolerance)
    {
        var root = JsonNode.Parse(File.ReadAllText(topoPath))!.AsObject();
        var arcs = root["arcs"]!.AsArray();

        // count how many geometries reference each arc
        var degree = new int[arcs.Count];
        foreach (var obj in root["objects"]!.AsObject())
        {
            var geometries = obj.Value?["geometries"]?.AsArray();
            if (geometries == null)
            {
                continue;
            }

            foreach (var geometry in geometries)
            {
                var geometryArcs = geometry?["arcs"];
                if (geometryArcs != null)
                {
                    CountArcs(geometryArcs, degree);
                }
            }
        }

        // simplify only coastline arcs (referenced by a single electorate)
        for (var i = 0; i < arcs.Count; i++)
        {
            if (degree[i] != 1)
            {
                continue;
            }

            var arc = arcs[i]!.AsArray();
            var points = new List<(double x, double y)>(arc.Count);
            foreach (var point in arc)
            {
                points.Add((point![0]!.GetValue<double>(), point[1]!.GetValue<double>()));
            }

            var simplified = DouglasPeucker(points, tolerance);
            if (simplified.Count == points.Count)
            {
                continue;
            }

            var newArc = new JsonArray();
            foreach (var (x, y) in simplified)
            {
                newArc.Add(new JsonArray(JsonValue.Create(x), JsonValue.Create(y)));
            }

            arcs[i] = newArc;
        }

        File.WriteAllText(topoPath, root.ToJsonString());
    }

    static void CountArcs(JsonNode node, int[] degree)
    {
        if (node is JsonValue value)
        {
            var index = value.GetValue<int>();
            degree[index < 0 ? ~index : index]++;
            return;
        }

        foreach (var child in node.AsArray())
        {
            CountArcs(child!, degree);
        }
    }

    static List<(double x, double y)> DouglasPeucker(List<(double x, double y)> points, double tolerance)
    {
        var count = points.Count;
        if (count < 3)
        {
            return points;
        }

        var keep = new bool[count];
        keep[0] = true;
        keep[count - 1] = true;
        var stack = new Stack<(int first, int last)>();
        stack.Push((0, count - 1));
        while (stack.Count > 0)
        {
            var (first, last) = stack.Pop();
            var (ax, ay) = points[first];
            var (bx, by) = points[last];
            var dx = bx - ax;
            var dy = by - ay;
            var lengthSquared = dx * dx + dy * dy;

            var maxDistance = tolerance;
            var index = -1;
            for (var k = first + 1; k < last; k++)
            {
                var (px, py) = points[k];
                double distance;
                if (lengthSquared == 0)
                {
                    distance = Math.Sqrt((px - ax) * (px - ax) + (py - ay) * (py - ay));
                }
                else
                {
                    var t = ((px - ax) * dx + (py - ay) * dy) / lengthSquared;
                    var cx = ax + t * dx;
                    var cy = ay + t * dy;
                    distance = Math.Sqrt((px - cx) * (px - cx) + (py - cy) * (py - cy));
                }

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    index = k;
                }
            }

            if (index == -1)
            {
                continue;
            }

            keep[index] = true;
            stack.Push((first, index));
            stack.Push((index, last));
        }

        var result = new List<(double x, double y)>();
        for (var i = 0; i < count; i++)
        {
            if (keep[i])
            {
                result.Add(points[i]);
            }
        }

        return result;
    }
}
