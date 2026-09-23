class ElectorateLocator
{
    Area[] areas;

    // Reads the geojson a token at a time as it streams in, keeping only the vertices: about 33MB for
    // the full-detail map. Parsing the whole text instead (a string, then a JsonDocument over it)
    // peaked at over 500MB, and ArrayPool.Shared then held on to most of that.
    public ElectorateLocator(Stream australiaGeoJson)
    {
        var reader = new JsonStreamReader(australiaGeoJson);
        var items = new List<Area>();
        // reused for every feature and ring, so they grow to the largest once rather than per ring
        var rings = new List<(double, double)[]>();
        var points = new List<(double, double)>();
        reader.Read();
        while (reader.Read() == JsonTokenType.PropertyName)
        {
            if (!reader.ValueTextEquals("features"u8))
            {
                reader.Skip();
                continue;
            }

            reader.Read();
            while (reader.Read() == JsonTokenType.StartObject)
            {
                items.Add(ReadFeature(ref reader, rings, points));
            }
        }

        areas = [.. items];
    }

    static Area ReadFeature(ref JsonStreamReader reader, List<(double, double)[]> rings, List<(double, double)> points)
    {
        string? name = null;
        string? type = null;
        rings.Clear();
        while (reader.Read() == JsonTokenType.PropertyName)
        {
            if (reader.ValueTextEquals("properties"u8))
            {
                name = ReadElectorateName(ref reader);
            }
            else if (reader.ValueTextEquals("geometry"u8))
            {
                type = ReadGeometry(ref reader, rings, points);
            }
            else
            {
                reader.Skip();
            }
        }

        if (type is not ("Polygon" or "MultiPolygon"))
        {
            throw new($"Unsupported geometry type: {type}");
        }

        return new(DataLoader.FindElectorate(name!), rings);
    }

    static string? ReadElectorateName(ref JsonStreamReader reader)
    {
        string? name = null;
        reader.Read();
        while (reader.Read() == JsonTokenType.PropertyName)
        {
            if (reader.ValueTextEquals("electorateName"u8))
            {
                reader.Read();
                name = reader.GetString();
            }
            else
            {
                reader.Skip();
            }
        }

        return name;
    }

    static string? ReadGeometry(ref JsonStreamReader reader, List<(double, double)[]> rings, List<(double, double)> points)
    {
        string? type = null;
        reader.Read();
        while (reader.Read() == JsonTokenType.PropertyName)
        {
            if (reader.ValueTextEquals("type"u8))
            {
                reader.Read();
                type = reader.GetString();
            }
            else if (reader.ValueTextEquals("coordinates"u8))
            {
                ReadCoordinates(ref reader, rings, points);
            }
            else
            {
                reader.Skip();
            }
        }

        return type;
    }

    // Polygon coordinates are rings of positions, and MultiPolygon coordinates are polygons of rings
    // of positions. Either way a ring is an array of positions, so the nesting can be followed
    // without relying on "type" having been read first. ReadPosition reads through the end of each
    // position, so an end of array with points pending is the end of a ring.
    static void ReadCoordinates(ref JsonStreamReader reader, List<(double, double)[]> rings, List<(double, double)> points)
    {
        reader.Read();
        var depth = reader.CurrentDepth;
        do
        {
            var token = reader.Read();
            if (token == JsonTokenType.Number)
            {
                points.Add(ReadPosition(ref reader));
            }
            else if (token == JsonTokenType.EndArray &&
                     points.Count > 0)
            {
                rings.Add(points.ToArray());
                points.Clear();
            }
        } while (reader.CurrentDepth > depth);
    }

    // [longitude, latitude, altitude?], with the reader on the longitude
    static (double, double) ReadPosition(ref JsonStreamReader reader)
    {
        var longitude = reader.GetDouble();
        reader.Read();
        var latitude = reader.GetDouble();
        // past any further ordinates to the end of the position
        while (reader.Read() == JsonTokenType.Number)
        {
        }

        return (longitude, latitude);
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

    public IElectorate? Find(double latitude, double longitude, int postcode)
    {
        // Postcode is a performance hint: test the electorates that include the
        // postcode first (avoids ray-casting large electorates that merely overlap
        // the point's bounding box), then fall back to a full scan for correctness.
        var candidates = DataLoader.ElectoratesForPostcode(postcode)
            .Select(_ => _.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var area in areas)
        {
            if (candidates.Contains(area.Electorate.Name) &&
                area.Contains(longitude, latitude))
            {
                return area.Electorate;
            }
        }

        return Find(latitude, longitude);
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
