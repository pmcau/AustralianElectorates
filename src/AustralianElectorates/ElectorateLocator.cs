using System.Collections.Frozen;

namespace AustralianElectorates;

class ElectorateLocator
{
    FrozenDictionary<string, Area> areas;

    public ElectorateLocator(string australiaGeoJson)
    {
        var items = new Dictionary<string, Area>();
        using var document = JsonDocument.Parse(australiaGeoJson);
        foreach (var feature in document.RootElement.GetProperty("features").EnumerateArray())
        {
            var name = feature.GetProperty("properties")
                .GetProperty("electorateName")
                .GetString()!;
            var rings = new List<(double, double)[]>();
            ReadGeometry(feature.GetProperty("geometry"), rings);
            items.Add(name, new(rings));
        }

        areas = items.ToFrozenDictionary();
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

    public IElectorate? Find(double latitude, double longitude, int? postcode)
    {
        var name = FindName(latitude, longitude, postcode);
        if (name == null)
        {
            return null;
        }

        return DataLoader.FindElectorate(name);
    }

    string? FindName(double latitude, double longitude, int? postcode)
    {
        // No postcode provided, search all electorates.
        if (postcode == null)
        {
            return Locate(latitude, longitude, areas.Keys);
        }

        var forPostcode = DataLoader.ElectoratesForPostcode(postcode.Value)
            .Select(_ => _.Name)
            .Where(areas.ContainsKey)
            .ToList();

        if (forPostcode.Count == 1)
        {
            return forPostcode[0];
        }

        return Locate(latitude, longitude, forPostcode) ??
               // Occasionally, postcode-narrowing removes the correct electorate from the pool.
               // In this case, searching all electorates acts as a backstop.
               Locate(latitude, longitude, areas.Keys);
    }

    string? Locate(double latitude, double longitude, IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            if (areas[name].Contains(longitude, latitude))
            {
                return name;
            }
        }

        return null;
    }

    class Area
    {
        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;
        (double X, double Y)[][] rings;

        public Area(List<(double, double)[]> rings)
        {
            this.rings = [.. rings];
            foreach (var ring in rings)
            {
                foreach (var (x, y) in ring)
                {
                    minX = Math.Min(minX, x);
                    minY = Math.Min(minY, y);
                    maxX = Math.Max(maxX, x);
                    maxY = Math.Max(maxY, y);
                }
            }
        }

        // Bounding-box reject, then even-odd ray cast across all rings
        // (holes and multi-polygons fall out of the parity test for non-overlapping areas).
        public bool Contains(double x, double y)
        {
            if (x < minX || x > maxX || y < minY || y > maxY)
            {
                return false;
            }

            var inside = false;
            foreach (var ring in rings)
            {
                for (int i = 0, j = ring.Length - 1; i < ring.Length; j = i++)
                {
                    var (xi, yi) = ring[i];
                    var (xj, yj) = ring[j];
                    if (((yi > y) != (yj > y)) &&
                        (x < (xj - xi) * (y - yi) / (yj - yi) + xi))
                    {
                        inside = !inside;
                    }
                }
            }

            return inside;
        }
    }
}
