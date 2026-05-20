class ElectorateLocator
{
    Area[] areas;

    public ElectorateLocator(string australiaGeoJson)
    {
        var items = new List<Area>();
        using var document = JsonDocument.Parse(australiaGeoJson);
        foreach (var feature in document.RootElement.GetProperty("features").EnumerateArray())
        {
            var name = feature.GetProperty("properties")
                .GetProperty("electorateName")
                .GetString()!;
            var electorate = DataLoader.FindElectorate(name);
            var rings = new List<(double, double)[]>();
            ReadGeometry(feature.GetProperty("geometry"), rings);
            items.Add(new(electorate, rings));
        }

        areas = [.. items];
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

    public IElectorate? Find(double latitude, double longitude)
    {
        foreach (var area in areas)
        {
            if (area.Contains(longitude, latitude))
            {
                return area.Electorate;
            }
        }

        return null;
    }

    class Area
    {
        public IElectorate Electorate { get; }
        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;
        (double X, double Y)[][] rings;

        public Area(IElectorate electorate, List<(double, double)[]> rings)
        {
            Electorate = electorate;
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
                    if (yi > y != yj > y &&
                        x < (xj - xi) * (y - yi) / (yj - yi) + xi)
                    {
                        inside = !inside;
                    }
                }
            }

            return inside;
        }
    }
}
