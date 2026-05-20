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
            var rawElectorate = (string)properties["Elect_div"];
            var stateFromProperties = GetState(feature, state);
            var area = properties["Area_SqKm"];

            // Resolve the AEC shapefile's electorate string to the curated
            // canonical name from electorates.json. The AEC ships
            // single-capitalised names like "Mcmahon" / "Mcewen" / "Mcpherson"
            // and "Oconnor" (no apostrophe); the curated list has the proper
            // "McMahon" / "McEwen" / "McPherson" / "O'Connor".  Looking up by
            // ShortName keeps the geojson aligned with IElectorate.Name by
            // construction, so consumers that join the two by name don't drop
            // these electorates.
            var shortName = Electorate.GetShortName(rawElectorate);
            var canonical = DataLoader.Electorates.SingleOrDefault(_ => _.ShortName == shortName)
                ?? throw new($"No curated electorate in electorates.json matches ShortName '{shortName}' (from Elect_div '{rawElectorate}').");
            properties.Clear();
            properties["electorateShortName"] = shortName;
            properties["electorateName"] = canonical.Name;

            if (area is double doubleArea)
            {
                properties["area"] = Math.Round(doubleArea, 6);
            }
            else
            {
                properties["area"] = (long)area;
            }

            properties["state"] = stateFromProperties;
        }
    }

    static string? GetState(Feature feature, State? state)
    {
        if (feature.Properties.TryGetValue("State", out var stateFromProperties))
        {
            return (string)stateFromProperties;
        }

        return state?.ToString();
    }
}