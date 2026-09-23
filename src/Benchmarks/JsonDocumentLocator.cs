using System.Text.Json;

// The ElectorateLocator parse from 12.10.0, before it streamed: the whole geojson as a string,
// then a JsonDocument over it. Kept as the baseline for ElectorateLocatorBenchmarks.
static class JsonDocumentLocator
{
    public static List<(IElectorate electorate, (double X, double Y)[][] rings)> Build(string australiaGeoJson)
    {
        var items = new List<(IElectorate, (double X, double Y)[][])>();
        using var document = JsonDocument.Parse(australiaGeoJson);
        foreach (var feature in document.RootElement.GetProperty("features").EnumerateArray())
        {
            var name = feature.GetProperty("properties")
                .GetProperty("electorateName")
                .GetString()!;
            var electorate = DataLoader.FindElectorate(name);
            var rings = new List<(double, double)[]>();
            ReadGeometry(feature.GetProperty("geometry"), rings);
            items.Add((electorate, [.. rings]));
        }

        return items;
    }

    static void ReadGeometry(JsonElement geometry, List<(double, double)[]> rings)
    {
        var type = geometry.GetProperty("type").GetString();
        var coordinates = geometry.GetProperty("coordinates");
        switch (type)
        {
            case "Polygon":
                ReadPolygon(coordinates, rings);
                break;
            case "MultiPolygon":
                foreach (var polygon in coordinates.EnumerateArray())
                {
                    ReadPolygon(polygon, rings);
                }

                break;
            default:
                throw new($"Unsupported geometry type: {type}");
        }
    }

    static void ReadPolygon(JsonElement polygon, List<(double, double)[]> rings)
    {
        foreach (var ring in polygon.EnumerateArray())
        {
            var points = new List<(double, double)>();
            foreach (var position in ring.EnumerateArray())
            {
                points.Add((position[0].GetDouble(), position[1].GetDouble()));
            }

            rings.Add(points.ToArray());
        }
    }
}
