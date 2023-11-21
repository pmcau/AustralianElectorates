using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

static class GeoJsonExtensions
{
    public static FeatureCollection ToCollection(this Feature feature) =>
        new(new()
        {
            feature
        });

    public static FeatureCollection FeaturesCollectionForState(this FeatureCollection featureCollection, List<string> electorates)
    {
        var features = featureCollection.Features
            .Where(_ =>
            {
                var electorate  = (string) _.Properties["electorateShortName"];
                return electorates.Contains(electorate);
            })
            .ToList();
        var collection = new FeatureCollection(features);
        collection.FixBoundingBox();
        return collection;
    }

    public static double[] CalculateBoundingBox(this IEnumerable<Feature> features) =>
        BoundingBox(features.SelectMany(_ => _.AllPositions()));

    public static double[] CalculateBoundingBox(this Feature feature) =>
        BoundingBox(feature.AllPositions());

    public static void FixBoundingBox(this FeatureCollection featureCollection)
    {
        featureCollection.BoundingBoxes = featureCollection.Features.CalculateBoundingBox();
        foreach (var feature in featureCollection.Features)
        {
            feature.BoundingBoxes = feature.CalculateBoundingBox();
        }
    }

    public static IEnumerable<IPosition> AllPositions(this Feature features)
    {
        if (features.Geometry is MultiPolygon multiPolygon)
        {
            return multiPolygon.Coordinates.SelectMany(_ => _.Coordinates)
                .SelectMany(_ => _.Coordinates);
        }

        var polygon = (Polygon) features.Geometry;
        return polygon.Coordinates.SelectMany(_ => _.Coordinates);
    }

    static double[] BoundingBox(IEnumerable<IPosition> points)
    {
        var xmin = double.MaxValue;
        var xmax = double.MinValue;
        var ymin = double.MaxValue;
        var ymax = double.MinValue;
        foreach (var position in points)
        {
            xmax = Math.Max(position.Longitude, xmax);
            xmin = Math.Min(position.Longitude, xmin);
            ymax = Math.Max(position.Latitude, ymax);
            ymin = Math.Min(position.Latitude, ymin);
        }

        return new[] {xmin, ymin, xmax, ymax};
    }
}