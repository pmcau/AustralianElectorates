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
            new(0, 0),
            new(0, 1),
            new(1, 1),
            new(1, 0),
            new(0, 0)
            );
        var simpleBoundingBox = simple.CalculateBoundingBox();
        Assert.Equal(new double[] {0, 0,1,1}, simpleBoundingBox);
    }

    public List<Feature> BuildFeatures(params Position[] positions)
    {
        return new()
        {
            new(
                new Polygon(
                    new List<LineString>
                    {
                        new(positions)
                    }))
        };
    }
}