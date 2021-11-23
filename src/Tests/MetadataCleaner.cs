using AustralianElectorates;
using GeoJSON.Net.Feature;

static class MetadataCleaner
{
    public static void CleanMetadata(FeatureCollection featureCollection, State? state = null)
    {
        if (!featureCollection.Features.First().Properties.ContainsKey("Elect_div"))
        {
            return;
        }

        foreach (var feature in featureCollection.Features)
        {
            var electorate = (string) feature.Properties["Elect_div"];
            var stateFromProperties = GetState(feature, state);
            var area = (double) feature.Properties["Area_SqKm"];

            var shortName = Electorate.GetShortName(electorate);
            feature.Properties.Clear();
            feature.Properties["electorateName"] = electorate;
            feature.Properties["electorateShortName"] = shortName;
            feature.Properties["area"] = Math.Round(area, 6);
            feature.Properties["state"] = stateFromProperties;
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