using System.Collections.Frozen;
using System.Text.Json;
using NetTopologySuite.Algorithm.Locate;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using NtsLocation = NetTopologySuite.Geometries.Location;

namespace AustralianElectorates;

class ElectorateLocator
{
    static JsonSerializerOptions options;

    static ElectorateLocator()
    {
        options = new();
        options.Converters.Add(new GeoJsonConverterFactory());
    }

    FrozenDictionary<string, IndexedPointInAreaLocator> locators;

    public ElectorateLocator(string australiaGeoJson)
    {
        var features = JsonSerializer.Deserialize<FeatureCollection>(australiaGeoJson, options)!;

        var items = new Dictionary<string, IndexedPointInAreaLocator>();
        foreach (var feature in features)
        {
            var name = (string) feature.Attributes["electorateName"];
            items.Add(name, new(feature.Geometry));
        }

        locators = items.ToFrozenDictionary();
    }

    public IElectorate? Find(double latitude, double longitude, int? postcode)
    {
        var name = FindName(latitude, longitude, postcode);
        if (name == null)
        {
            return null;
        }

        return DataLoader.FindElectorate(name);
    }

    string? FindName(double latitude, double longitude, int? postcode)
    {
        // No postcode provided, search all electorates.
        if (postcode == null)
        {
            return Locate(latitude, longitude, locators.Keys);
        }

        var forPostcode = DataLoader.ElectoratesForPostcode(postcode.Value)
            .Select(_ => _.Name)
            .Where(locators.ContainsKey)
            .ToList();

        if (forPostcode.Count == 1)
        {
            return forPostcode[0];
        }

        return Locate(latitude, longitude, forPostcode) ??
               // Occasionally, postcode-narrowing removes the correct electorate from the pool.
               // In this case, searching all electorates acts as a backstop.
               Locate(latitude, longitude, locators.Keys);
    }

    string? Locate(double latitude, double longitude, IEnumerable<string> names)
    {
        var coordinate = new Coordinate(longitude, latitude);
        foreach (var name in names)
        {
            var location = locators[name].Locate(coordinate);
            if (!location.HasFlag(NtsLocation.Exterior))
            {
                return name;
            }
        }

        return null;
    }
}
