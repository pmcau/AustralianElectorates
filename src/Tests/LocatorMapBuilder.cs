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

        // Dissolve inland no-man's-land into the electorates BEFORE expanding, so the electorates tile the
        // landmass with no gaps - then the coast expansion can't create any. The AEC source has thin
        // slivers between electorates from different states (their borders don't perfectly align); these
        // are filled and assigned to the nearest electorate (its Voronoi region).
        DissolveSlivers(geometries, regions, factory, landUnion);

        // local-land lookup, rebuilt from the now gap-free land: each skirt is clipped to ocean by
        // subtracting nearby land.
        var tree = new STRtree<Geometry>();
        foreach (var geometry in geometries)
        {
            tree.Insert(geometry.EnvelopeInternal, geometry);
        }

        // Expand each electorate's coast into the open ocean (skipped for excluded electorates). The
        // skirt is clipped to the electorate's Voronoi region, so skirts never overlap.
        var expanded = new Geometry[features.Count];
        for (var i = 0; i < features.Count; i++)
        {
            var geometry = geometries[i];
            if (!excludeFromExpansion.Contains(names[i]) &&
                regions.TryGetValue(i, out var region))
            {
                var buffered = geometry.Buffer(expandDistance);
                var land = factory
                    .BuildGeometry(tree.Query(buffered.EnvelopeInternal))
                    .Union();
                // Intersection can yield a GeometryCollection (polygons plus stray boundary
                // lines/points); keep only the polygonal parts so Union accepts it.
                var skirt = Polygonal(buffered.Difference(land).Intersection(region), factory);
                if (!skirt.IsEmpty)
                {
                    geometry = geometry.Union(skirt);
                }
            }

            expanded[i] = geometry;
        }

        var result = new FeatureCollection();
        for (var i = 0; i < features.Count; i++)
        {
            // drop stray line/point fragments AND degenerate sliver holes left by the overlays, so every
            // feature is a clean Polygon/MultiPolygon with no zero-area "disconnected line" rings.
            result.Add(new Feature(CleanGeometry(expanded[i], factory), features[i].Attributes));
        }

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

    // ~13.5km. Length of the straight border-extension cut (must reach across the ~10km skirt).
    const double offshoreCutLength = expandDistance * 1.5;

    // At each coastal junction (where an internal land border meets the coast), the nearest-land partition
    // lets one electorate's skirt wrap around the headland in front of its neighbour. This replaces that
    // wrap with a single STRAIGHT cut that continues the land border out to sea: the ocean wedge each
    // electorate holds on the wrong side of the cut is swapped to the other. A single straight line per
    // junction (not paired Voronoi sites) means the offshore border is clean - no sawtooth or spikes.
    static void StraightenOffshoreBorders(Geometry[] expanded, Geometry[] land, Geometry landUnion, GeometryFactory factory)
    {
        var coastDistance = new IndexedFacetDistance(landUnion.Boundary);
        var preparedCoast = PreparedGeometryFactory.Prepare(landUnion.Boundary);
        var preparedLand = PreparedGeometryFactory.Prepare(landUnion);
        var boundaries = land.Select(_ => _.Boundary).ToArray();
        var prepared = land.Select(_ => PreparedGeometryFactory.Prepare(_)).ToArray();

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
        const double sideEps = 0.001;   // ~110m: probe distance to decide direction and which side is which

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

                        var perpX = -dy;
                        var perpY = dx;

                        // which electorate sits on the +perp side (sampled just inland of the junction)
                        var sideProbe = factory.CreatePoint(
                            new Coordinate(end.X - dx * sideEps + perpX * sideEps, end.Y - dy * sideEps + perpY * sideEps));
                        int a;
                        int b;
                        if (prepared[i].Contains(sideProbe))
                        {
                            a = i;
                            b = j;
                        }
                        else if (prepared[j].Contains(sideProbe))
                        {
                            a = j;
                            b = i;
                        }
                        else
                        {
                            continue;
                        }

                        try
                        {
                            SwapWedge(expanded, a, b, end, dx, dy, perpX, perpY, landUnion, factory);
                        }
                        catch
                        {
                            // a degenerate overlay at this junction - leave it on the nearest-land partition
                        }
                    }
                }
            }
        }
    }

    // Swaps the ocean each electorate holds on the wrong side of the straight cut through the junction.
    // Electorate 'a' is on the +perp side, 'b' on the -perp side.
    static void SwapWedge(Geometry[] expanded, int a, int b, Coordinate end, double dx, double dy, double perpX, double perpY, Geometry landUnion, GeometryFactory factory)
    {
        var l = offshoreCutLength;
        var p0 = new Coordinate(end.X - dx * l, end.Y - dy * l); // inland end of the cut
        var p1 = new Coordinate(end.X + dx * l, end.Y + dy * l); // seaward end of the cut

        Geometry HalfPlane(double sign) =>
            factory.CreatePolygon(
            [
                p0,
                p1,
                new Coordinate(p1.X + perpX * l * sign, p1.Y + perpY * l * sign),
                new Coordinate(p0.X + perpX * l * sign, p0.Y + perpY * l * sign),
                p0
            ]);

        var halfA = HalfPlane(1);  // +perp side (a)
        var halfB = HalfPlane(-1); // -perp side (b)
        var vicinity = factory.CreatePoint(end).Buffer(l); // limit the swap to near the junction

        // a's ocean on b's side, and b's ocean on a's side (land excluded - only the skirt moves)
        var aOnB = Polygonal(expanded[a].Intersection(halfB).Intersection(vicinity).Difference(landUnion), factory);
        var bOnA = Polygonal(expanded[b].Intersection(halfA).Intersection(vicinity).Difference(landUnion), factory);

        // keep each result polygonal - Difference/Union can emit a GeometryCollection, which the next
        // overlay would reject.
        if (!aOnB.IsEmpty)
        {
            expanded[a] = Polygonal(expanded[a].Difference(aOnB), factory);
            expanded[b] = Polygonal(expanded[b].Union(aOnB), factory);
        }

        if (!bOnA.IsEmpty)
        {
            expanded[b] = Polygonal(expanded[b].Difference(bOnA), factory);
            expanded[a] = Polygonal(expanded[a].Union(bOnA), factory);
        }
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
