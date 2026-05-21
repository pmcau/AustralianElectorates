using System.IO.Compression;
using System.Text.Json.Nodes;
using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Index.Strtree;
using NetTopologySuite.IO;
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

    public static async Task SmoothCoastline(string geoJsonPath)
    {
        var year = Path.GetFileName(Path.GetDirectoryName(geoJsonPath)!);
        var topoPath = Path.Combine(DataLocations.TempPath, $"{year}-australia-topo.json");

        // build arc topology (shared borders deduplicated), simplify only coast arcs, convert back
        await MapToGeoJson.ToTopoJson(topoPath, geoJsonPath);
        SimplifyCoastArcs(topoPath);
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
    // partitioned by nearest land (a generalised Voronoi over every electorate's boundary points), so
    // the offshore border between two electorates follows the medial line between their coasts - i.e.
    // it continues the direction of their shared internal border (e.g. the vertical WA/NT border in the
    // Joseph Bonaparte Gulf stays vertical out to sea). Each skirt is clipped to its electorate's
    // Voronoi region, so skirts never overlap and an offshore point resolves to exactly one electorate.
    // Excluded electorates are not expanded, but still take part in the partition so they claim (and
    // thus block) the ocean in front of their own coast - neighbours can't reach across it.
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
        DissolveSlivers(geometries, regions, factory);

        // local-land lookup, rebuilt from the now gap-free land: each skirt is clipped to ocean by
        // subtracting nearby land.
        var tree = new STRtree<Geometry>();
        foreach (var geometry in geometries)
        {
            tree.Insert(geometry.EnvelopeInternal, geometry);
        }

        // Expand each electorate's coast into the open ocean (skipped for excluded electorates). The
        // skirt is clipped to the electorate's Voronoi region, so skirts never overlap.
        var result = new FeatureCollection();
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

            result.Add(new Feature(geometry, features[i].Attributes));
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
    static void DissolveSlivers(Geometry[] geometries, Dictionary<int, Geometry> regions, GeometryFactory factory)
    {
        var landUnion = factory.BuildGeometry(geometries).Union();

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

    static void SimplifyCoastArcs(string topoPath)
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

            var simplified = DouglasPeucker(points);
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

    static List<(double x, double y)> DouglasPeucker(List<(double x, double y)> points)
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

            var maxDistance = coastTolerance;
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
