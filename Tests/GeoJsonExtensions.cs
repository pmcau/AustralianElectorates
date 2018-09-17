using System;
using System.Collections.Generic;
using System.Linq;
using AustralianElectorates;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

static class GeoJsonExtensions
{
    public static FeatureCollection ToCollection(this Feature feature)
    {
        return new FeatureCollection(
            new List<Feature>
            {
                feature
            });
    }

    public static FeatureCollection Trim(this FeatureCollection featureCollection)
    {
        var features = featureCollection.Features.Select(x => x.FirstGeometryFeature())
            .ToList();
        return new FeatureCollection(features);
    }

    public static FeatureCollection FeaturesCollectionForState(this FeatureCollection featureCollection, State state)
    {
        var features = featureCollection.Features
            .Where(x => string.Equals((string) x.Properties["state"], state.ToString(), StringComparison.OrdinalIgnoreCase))
            .ToList();
        return new FeatureCollection(features);
    }

    public static double[] CalculateBoundingBox(this IEnumerable<Feature> features)
    {
        return BoundingBox(features.SelectMany(x => x.AllPositions()));
    }
    public static double[] CalculateBoundingBox(this Feature feature)
    {
        return BoundingBox(feature.AllPositions());
    }
    public static void FixBoundingBox(this FeatureCollection featureCollection)
    {
        foreach (var feature in featureCollection.Features)
        {
            feature.BoundingBoxes = feature.CalculateBoundingBox();
        }
    }

public static IEnumerable<IPosition> AllPositions(this Feature features)
    {
        if (features.Geometry is MultiPolygon multiPolygon)
        {
            return multiPolygon.Coordinates.SelectMany(x => x.Coordinates)
                .SelectMany(x=>x.Coordinates);
        }

        var polygon = (Polygon)features.Geometry;
        return polygon.Coordinates.SelectMany(x => x.Coordinates);
    }

    static double[] BoundingBox(IEnumerable<IPosition> points)
    {
        var xmin=double.MaxValue;
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

        return new[]{xmin, ymin, xmax, ymax};
    }

    public static Feature FirstGeometryFeature(this Feature feature)
    {
        if (feature.Geometry is MultiPolygon multiPolygon)
        {
            var items = multiPolygon.Coordinates.Skip(1).Where(x => x.Size() > 300).Take(2).ToList();
            items.Insert(0, multiPolygon.Coordinates.First());
            return new Feature(new MultiPolygon(items), feature.Properties);
        }

        return feature;
    }

    static double Size(this Polygon polygon)
    {
        var points = polygon.Coordinates.SelectMany(x => x.Coordinates).ToList();
        return Math.Abs(points.Take(points.Count - 1)
                            .Select((p, i) => (points[i + 1].Latitude - p.Latitude) * (points[i + 1].Longitude + p.Longitude) * 111 * 111)
                            .Sum() / 2);
    }
}