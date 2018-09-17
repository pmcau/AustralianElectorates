using GeoJSON.Net.Feature;
using Xunit;

public class CleanMetadata
{
    [Fact]
    [Trait("Category", "Integration")]
    public void Run()
    {
        var path = @"C:\Code\AustralianElectorates\Data\Maps\2016\australia.geojson";
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(path);
        foreach (var feature in featureCollection.Features)
        {
            feature.BoundingBoxes = feature.CalculateBoundingBox();
        }

        MetadataCleaner.CleanMetadata(featureCollection);
        JsonSerializer.SerializeGeo(featureCollection, path);
    }
}