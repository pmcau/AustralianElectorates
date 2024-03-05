using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

public class GeoJsonExtensionsTests
{
    [Fact]
    public void BoundingBox()
    {
        var simple = BuildFeatures(
            new Position(0, 0),
            new Position(0, 1),
            new Position(1, 1),
            new Position(1, 0),
            new Position(0, 0)
        );
        var simpleBoundingBox = simple.CalculateBoundingBox();
        Assert.Equal(
            [
                0,
                0,
                1,
                1
            ],
            simpleBoundingBox);
    }
#pragma warning disable CA1822
    // ReSharper disable once MemberCanBeMadeStatic.Local
    List<Feature> BuildFeatures(params Position[] positions) =>
    [
        new(
            new Polygon(
                new List<LineString>
                {
                    new(positions)
                }))
    ];
}