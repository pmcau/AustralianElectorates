using System.Text;

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
