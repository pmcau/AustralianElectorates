using System.Collections.Generic;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Xunit;

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
        Assert.Equal(new double[] {0, 0,1,1}, simpleBoundingBox);
    }

    public List<Feature> BuildFeatures(params Position[] positions)
    {
        return new List<Feature>
        {
            new Feature(
                new Polygon(
                    new List<LineString>
                    {
                        new LineString(positions)
                    }))
        };
    }
}