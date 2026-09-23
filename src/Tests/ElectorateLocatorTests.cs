using System.IO.Compression;
using System.Text;
using System.Text.Json;

public class ElectorateLocatorTests
{
    // Bean is a square with a square hole. Canberra is two squares (a MultiPolygon, one with
    // altitudes) with its members in a different order. Both have members that need skipping.
    static byte[] geoJson =
        """
        {
          "type": "FeatureCollection",
          "features": [
            {
              "type": "Feature",
              "properties": {
                "electorateName": "Bean",
                "state": "ACT",
                "nested": {"values": [1, [2, 3], {"a": "b"}]}
              },
              "geometry": {
                "type": "Polygon",
                "coordinates": [
                  [[149, -36], [150, -36], [150, -35], [149, -35], [149, -36]],
                  [[149.4, -35.6], [149.6, -35.6], [149.6, -35.4], [149.4, -35.4], [149.4, -35.6]]
                ]
              },
              "bbox": [149, -36, 150, -35]
            },
            {
              "geometry": {
                "coordinates": [
                  [[[151, -36, 10], [152, -36, 10], [152, -35, 10], [151, -35, 10], [151, -36, 10]]],
                  [[[153, -36], [154, -36], [154, -35], [153, -35], [153, -36]]]
                ],
                "type": "MultiPolygon"
              },
              "type": "Feature",
              "properties": {
                "electorateName": "Canberra"
              }
            }
          ],
          "bbox": [149, -36, 154, -35]
        }
        """u8.ToArray();

    [Fact]
    public void Find() =>
        AssertFind(new(new MemoryStream(geoJson)));

    // every token is split across reads, so the reader state has to carry over each refill
    [Fact]
    public void Find_one_byte_per_read() =>
        AssertFind(new(new OneBytePerReadStream(geoJson)));

    static void AssertFind(ElectorateLocator locator)
    {
        Assert.Equal("Bean", locator.Find(-35.2, 149.2)?.Name);
        // in the hole
        Assert.Null(locator.Find(-35.5, 149.5));
        Assert.Equal("Canberra", locator.Find(-35.5, 151.5)?.Name);
        Assert.Equal("Canberra", locator.Find(-35.5, 153.5)?.Name);
        // between Canberra's two polygons
        Assert.Null(locator.Find(-35.5, 152.5));
    }

    [Fact]
    public void Token_larger_than_buffer()
    {
        var description = new string('a', 100_000);
        var json = $$$"""
                      {"type":"FeatureCollection","features":[{"type":"Feature","properties":{"electorateName":"Bean","description":"{{{description}}}"},"geometry":{"type":"Polygon","coordinates":[[[149,-36],[150,-36],[150,-35],[149,-35],[149,-36]]]}}]}
                      """;
        var locator = new ElectorateLocator(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        Assert.Equal("Bean", locator.Find(-35.5, 149.5)?.Name);
    }

    // the ring does not repeat its first point, so the right hand side is only there if the ring gets closed
    [Fact]
    public void Unclosed_ring()
    {
        var json = """{"type":"FeatureCollection","features":[{"type":"Feature","properties":{"electorateName":"Bean"},"geometry":{"type":"Polygon","coordinates":[[[150,-35],[149,-35],[149,-36],[150,-36]]]}}]}"""u8.ToArray();
        var locator = new ElectorateLocator(new MemoryStream(json));
        Assert.Equal("Bean", locator.Find(-35.5, 149.5)?.Name);
    }

    // The band index against a ray cast over every edge, at random points across the full map
    [Fact]
    public void Matches_ray_cast_over_every_edge()
    {
        var areas = ReadAreas();
        var random = new Random(0);
        var onLand = 0;
        for (var i = 0; i < 5_000; i++)
        {
            var latitude = -44 + random.NextDouble() * 35;
            var longitude = 112 + random.NextDouble() * 42;
            var expected = areas
                .FirstOrDefault(_ => _.Contains(longitude, latitude))
                ?.Electorate;
            DataLoader.TryLocateElectorate(latitude, longitude, out var actual);
            Assert.Same(expected, actual);
            if (actual != null)
            {
                onLand++;
            }
        }

        Assert.True(onLand > 1_000);
    }

    static List<RayCastArea> ReadAreas()
    {
        using var stream = typeof(DataLoader).Assembly.GetManifestResourceStream("AustraliaFull.zip")!;
        using var archive = new ZipArchive(stream);
        using var entry = archive.GetEntry("2025/australia.geojson")!.Open();
        using var document = JsonDocument.Parse(entry);
        var areas = new List<RayCastArea>();
        foreach (var feature in document.RootElement.GetProperty("features").EnumerateArray())
        {
            var name = feature.GetProperty("properties").GetProperty("electorateName").GetString()!;
            var geometry = feature.GetProperty("geometry");
            var coordinates = geometry.GetProperty("coordinates");
            var polygons = new List<JsonElement>();
            if (geometry.GetProperty("type").GetString() == "Polygon")
            {
                polygons.Add(coordinates);
            }
            else
            {
                polygons.AddRange(coordinates.EnumerateArray());
            }

            var rings = polygons
                .SelectMany(_ => _.EnumerateArray())
                .Select(ring => ring
                    .EnumerateArray()
                    .Select(_ => (_[0].GetDouble(), _[1].GetDouble()))
                    .ToArray())
                .ToArray();
            areas.Add(new(DataLoader.FindElectorate(name), rings));
        }

        return areas;
    }

    // The lookup as it was before the band index: every edge of every ring
    class RayCastArea
    {
        public IElectorate Electorate { get; }
        (double X, double Y)[][] rings;
        double minX;
        double maxX;
        double minY;
        double maxY;

        public RayCastArea(IElectorate electorate, (double X, double Y)[][] rings)
        {
            Electorate = electorate;
            this.rings = rings;
            var points = rings.SelectMany(_ => _).ToList();
            minX = points.Min(_ => _.X);
            maxX = points.Max(_ => _.X);
            minY = points.Min(_ => _.Y);
            maxY = points.Max(_ => _.Y);
        }

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

    [Fact]
    public void Unsupported_geometry()
    {
        var json = """{"type":"FeatureCollection","features":[{"properties":{"electorateName":"Bean"},"type":"Feature","geometry":{"type":"Point","coordinates":[0,0]}}]}"""u8.ToArray();
        Assert.Throws<Exception>(() => new ElectorateLocator(new MemoryStream(json)));
    }

    // hands back one byte per read, however many are asked for
    class OneBytePerReadStream(byte[] bytes) :
        MemoryStream(bytes)
    {
        public override int Read(byte[] buffer, int offset, int count) =>
            base.Read(buffer, offset, Math.Min(count, 1));

        public override int Read(Span<byte> buffer) =>
            base.Read(buffer[..Math.Min(buffer.Length, 1)]);
    }
}
