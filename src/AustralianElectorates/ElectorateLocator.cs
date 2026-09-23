class ElectorateLocator
{
    Area[] areas;
    FrozenDictionary<int, Area[]> areasByPostcode;

    // Reads the geojson a token at a time as it streams in, keeping only the vertices and an index
    // of their edges. Parsing the whole text instead (a string, then a JsonDocument over it) peaked
    // at over 500MB, and ArrayPool.Shared then held on to most of that.
    public ElectorateLocator(Stream australiaGeoJson)
    {
        var reader = new JsonStreamReader(australiaGeoJson);
        var items = new List<Area>();
        // reused for every feature, so they grow to the largest once rather than per feature
        var points = new List<(double, double)>();
        var ringEnds = new List<int>();
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
                items.Add(ReadFeature(ref reader, points, ringEnds));
            }
        }

        areas = [.. items];
        areasByPostcode = areas
            .SelectMany(area => area.Electorate.Locations.Select(_ => (_.Postcode, area)))
            .GroupBy(_ => _.Postcode, _ => _.area)
            .ToFrozenDictionary(_ => _.Key, _ => _.Distinct().ToArray());
    }

    static Area ReadFeature(ref JsonStreamReader reader, List<(double, double)> points, List<int> ringEnds)
    {
        string? name = null;
        string? type = null;
        points.Clear();
        ringEnds.Clear();
        while (reader.Read() == JsonTokenType.PropertyName)
        {
            if (reader.ValueTextEquals("properties"u8))
            {
                name = ReadElectorateName(ref reader);
            }
            else if (reader.ValueTextEquals("geometry"u8))
            {
                type = ReadGeometry(ref reader, points, ringEnds);
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

        return new(DataLoader.FindElectorate(name!), points, ringEnds);
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

    static string? ReadGeometry(ref JsonStreamReader reader, List<(double, double)> points, List<int> ringEnds)
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
                ReadCoordinates(ref reader, points, ringEnds);
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
    static void ReadCoordinates(ref JsonStreamReader reader, List<(double, double)> points, List<int> ringEnds)
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
            else if (token == JsonTokenType.EndArray)
            {
                EndRing(points, ringEnds);
            }
        } while (reader.CurrentDepth > depth);
    }

    // Rings are stored one after the other in points. Geojson rings repeat their first point last,
    // and one that does not is closed here, so every edge of a ring is a point and the one before it.
    static void EndRing(List<(double, double)> points, List<int> ringEnds)
    {
        var start = ringEnds.Count == 0 ? 0 : ringEnds[^1];
        if (points.Count == start)
        {
            return;
        }

        if (points[start] != points[^1])
        {
            points.Add(points[start]);
        }

        ringEnds.Add(points.Count);
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

    // Postcode is a hint: the electorates that include the postcode are tested first, then all of
    // them, so a wrong postcode still gives the same result.
    public IElectorate? Find(double latitude, double longitude, int postcode)
    {
        if (areasByPostcode.TryGetValue(postcode, out var candidates))
        {
            foreach (var area in candidates)
            {
                if (area.Contains(longitude, latitude))
                {
                    return area.Electorate;
                }
            }
        }

        return Find(latitude, longitude);
    }

    class Area
    {
        // Fewer edges per band is faster, at the cost of edges spanning more bands being listed in
        // each of them. 16 gives lookups of well under a microsecond for about a third more memory.
        const int edgesPerBand = 16;

        public IElectorate Electorate { get; }
        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;
        (double X, double Y)[] points;

        // The edges, each as the index of its second point in points, grouped by the horizontal
        // bands of the bounding box they span: the edges of band n are edges[bandStarts[n]..bandStarts[n + 1]].
        // A ray from a point can only cross an edge that spans the point's y, and every such edge is
        // listed in the point's band, so only that band needs testing.
        int[] bandStarts;
        int[] edges;
        double bandScale;

        public Area(IElectorate electorate, List<(double, double)> points, List<int> ringEnds)
        {
            Electorate = electorate;
            this.points = [.. points];
            foreach (var (x, y) in this.points)
            {
                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
            }

            var bandCount = Math.Max(1, (points.Count - ringEnds.Count) / edgesPerBand);
            if (maxY > minY)
            {
                bandScale = bandCount / (maxY - minY);
            }

            bandStarts = new int[bandCount + 1];
            // count the edges in each band, offset by one so a running total turns the counts into starts
            foreach (var (from, to, _) in Edges(ringEnds))
            {
                for (var band = from; band <= to; band++)
                {
                    bandStarts[band + 1]++;
                }
            }

            for (var band = 0; band < bandCount; band++)
            {
                bandStarts[band + 1] += bandStarts[band];
            }

            edges = new int[bandStarts[bandCount]];
            var next = bandStarts[..^1];
            foreach (var (from, to, index) in Edges(ringEnds))
            {
                for (var band = from; band <= to; band++)
                {
                    edges[next[band]++] = index;
                }
            }
        }

        // Each edge as the bands it spans and the index of its second point. Horizontal edges are left
        // out, since a horizontal ray never crosses one.
        IEnumerable<(int From, int To, int Index)> Edges(List<int> ringEnds)
        {
            var start = 0;
            foreach (var end in ringEnds)
            {
                for (var index = start + 1; index < end; index++)
                {
                    var y1 = points[index - 1].Y;
                    var y2 = points[index].Y;
                    if (y1 == y2)
                    {
                        continue;
                    }

                    yield return (Band(Math.Min(y1, y2)), Band(Math.Max(y1, y2)), index);
                }

                start = end;
            }
        }

        int Band(double y) =>
            Math.Clamp((int) ((y - minY) * bandScale), 0, bandStarts.Length - 2);

        // Bounding-box reject, then an even-odd ray cast across the edges in the point's band
        // (holes and multi-polygons fall out of the parity test for non-overlapping areas).
        public bool Contains(double x, double y)
        {
            if (x < minX || x > maxX || y < minY || y > maxY)
            {
                return false;
            }

            var band = Band(y);
            var inside = false;
            for (var edge = bandStarts[band]; edge < bandStarts[band + 1]; edge++)
            {
                var index = edges[edge];
                var (xi, yi) = points[index];
                var (xj, yj) = points[index - 1];
                if (yi > y != yj > y &&
                    x < (xj - xi) * (y - yi) / (yj - yi) + xi)
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
