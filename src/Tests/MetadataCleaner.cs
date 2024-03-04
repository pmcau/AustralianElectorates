using AustralianElectorates;
using GeoJSON.Net.Feature;

static class MetadataCleaner
{
    public static void CleanMetadata(FeatureCollection featureCollection, State? state = null)
    {
        if (!featureCollection
                .Features.First()
                .Properties.ContainsKey("Elect_div"))
        {
            return;
        }

        foreach (var feature in featureCollection.Features)
        {
            var properties = feature.Properties;
            var electorate = (string) properties["Elect_div"];
            var stateFromProperties = GetState(feature, state);
            var area = (double) properties["Area_SqKm"];

            var shortName = Electorate.GetShortName(electorate);
            properties.Clear();
            properties["electorateName"] = electorate;
            properties["electorateShortName"] = shortName;
            properties["area"] = Math.Round(area, 6);
            properties["state"] = stateFromProperties;
        }
    }

    static string? GetState(Feature feature, State? state)
    {
        if (feature.Properties.TryGetValue("State", out var stateFromProperties))
        {
            return (string) stateFromProperties;
        }

        return state?.ToString();
    }
}