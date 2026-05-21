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
    // Applied to australia.geojson during sync (before it is split into states/electorates) so every
    // derived geojson inherits the expanded coast.
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

        // local-land lookup: each skirt is clipped to ocean by subtracting nearby land
        var tree = new STRtree<Geometry>();
        foreach (var geometry in geometries)
        {
            tree.Insert(geometry.EnvelopeInternal, geometry);
        }

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
                var skirt = buffered.Difference(land).Intersection(region);
                if (!skirt.IsEmpty)
                {
                    geometry = geometry.Union(skirt);
                }
            }

            expanded[i] = geometry;
        }

        // Close inland gaps. The AEC source has thin slivers of no-man's-land between electorates from
        // different states (their borders don't perfectly align); some are enclosed in the source, others
        // are open at the coast and only get sealed into holes by the ocean expansion. Assign each thin
        // hole to the nearest electorate (its Voronoi region) so adjacent electorates meet with no inland
        // gap - including across an excluded electorate's border (which gets no skirt). Wide holes are
        // real bays/gulfs beyond the skirt and are left alone.
        FillThinHoles(expanded, regions, factory);

        var result = new FeatureCollection();
        for (var i = 0; i < features.Count; i++)
        {
            result.Add(new Feature(expanded[i], features[i].Attributes));
        }

        File.WriteAllText(geoJsonPath, new GeoJsonWriter().Write(result));

        // normalise to the repo's geojson format (bbox etc.)
        var featureCollection = JsonSerializerService.DeserializeGeo(geoJsonPath);
        featureCollection.FixBoundingBox();
        JsonSerializerService.SerializeGeo(featureCollection, geoJsonPath);
    }

    // ~440m. A hole in the merged map narrower than this (max inscribed radius) is a border-misalignment
    // sliver and is filled; wider holes are real bays/gulfs beyond the coastline expansion and are kept.
    const double slatRadius = 0.004;

    // Finds thin holes (slivers) in the merged map and fills each by assigning it to the nearest
    // electorate (its Voronoi region), in place.
    static void FillThinHoles(Geometry[] geometries, Dictionary<int, Geometry> regions, GeometryFactory factory)
    {
        var union = factory.BuildGeometry(geometries).Union();

        var slivers = new List<Geometry>();
        for (var i = 0; i < union.NumGeometries; i++)
        {
            if (union.GetGeometryN(i) is not Polygon polygon)
            {
                continue;
            }

            for (var hole = 0; hole < polygon.NumInteriorRings; hole++)
            {
                var ring = factory.CreatePolygon(polygon.GetInteriorRingN(hole).Coordinates);
                if (ring.Buffer(-slatRadius).IsEmpty)
                {
                    slivers.Add(ring);
                }
            }
        }

        if (slivers.Count == 0)
        {
            return;
        }

        var sliver = factory.BuildGeometry(slivers).Union();
        for (var i = 0; i < geometries.Length; i++)
        {
            if (regions.TryGetValue(i, out var region))
            {
                // Intersection can yield a GeometryCollection (polygons plus stray boundary
                // lines/points); keep only the polygonal parts so Union accepts it.
                var fill = Polygonal(sliver.Intersection(region), factory);
                if (!fill.IsEmpty)
                {
                    geometries[i] = geometries[i].Union(fill);
                }
            }
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
