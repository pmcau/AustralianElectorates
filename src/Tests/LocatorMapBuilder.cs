using System.IO.Compression;
using System.Text.Json.Nodes;

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

    // Embeds the (already coast-smoothed) current map for DataLoader.LocateElectorate.
    public static void BuildLocatorZip()
    {
        File.Delete(DataLocations.AustraliaFullZipPath);
        using var zip = ZipFile.Open(DataLocations.AustraliaFullZipPath, ZipArchiveMode.Create);
        var australia = Path.Combine(DataLocations.Maps2025Path, "australia.geojson");
        zip.CreateEntryFromFile(australia, "2025/australia.geojson", CompressionLevel.Optimal);
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
