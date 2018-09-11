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
        return new FeatureCollection(featureCollection.Features.Select(x => x.FirstGeometryFeature()).ToList());
    }

    public static FeatureCollection FeaturesCollectionForState(this FeatureCollection featureCollection, State state)
    {
        return new FeatureCollection(featureCollection.Features.Where(x => string.Equals((string) x.Properties["state"], state.ToString(), StringComparison.OrdinalIgnoreCase)).ToList());
    }

    public static Feature FirstGeometryFeature(this Feature feature)
    {
        if (feature.Geometry is MultiPolygon multiPolygon)
        {
            var items = multiPolygon.Coordinates.Skip(1).Take(2).Where(x => x.Size() > 350).ToList();
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