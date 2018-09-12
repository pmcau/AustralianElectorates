using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AustralianElectorates;
using Xunit;

public class Sync
{
    static List<int> percents;

    static Dictionary<State, List<string>> electorateNames = new Dictionary<State, List<string>>
    {
        {State.ACT, new List<string>()},
        {State.TAS, new List<string>()},
        {State.SA, new List<string>()},
        {State.VIC, new List<string>()},
        {State.QLD, new List<string>()},
        {State.NT, new List<string>()},
        {State.NSW, new List<string>()},
        {State.WA, new List<string>()},
    };
    static  List<State> states = new List < State >
    {
        State.ACT,
        State.TAS,
        State.SA,
        State.VIC,
        State.QLD,
        State.NT,
        State.NSW,
        State.WA,
    };

    static Sync()
    {
        percents = new List<int> {20, 10, 5};
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task SyncData()
    {
        IoHelpers.PurgeDirectoryRecursive(DataLocations.ElectoratesPath);
        IoHelpers.PurgeDirectory(DataLocations.MapsPath, exclude: "australia.geojson");

        foreach (var directory in Directory.EnumerateDirectories(DataLocations.MapsPath))
        {
            WriteOptimised(directory);

            foreach (var australiaPath in Directory.EnumerateFiles(directory, "australia*"))
            {
                var australiaFeatures = JsonSerializer.DeserializeGeo(australiaPath);
                foreach (var state in states)
                {
                    var lower = state.ToString().ToLower();
                    var featureCollectionForState = australiaFeatures.FeaturesCollectionForState(state);
                    var suffix = Path.GetFileName(australiaPath).Replace("australia", "");
                    var stateJson = Path.Combine(directory, $"{lower}{suffix}");
                    JsonSerializer.SerializeGeo(featureCollectionForState, stateJson);

                    var stateDirectory = Path.Combine(directory, state.ToString());
                    Directory.CreateDirectory(stateDirectory);
                    foreach (var electorateFeature in featureCollectionForState.Features)
                    {
                        var electorate = (string) electorateFeature.Properties["electorate"];
                        var electorateNameList = electorateNames[state];
                        if (!electorateNameList.Contains(electorate))
                        {
                            electorateNameList.Add(electorate);
                        }

                        var electorateJson = Path.Combine(stateDirectory, $"{electorate}{suffix}");
                        JsonSerializer.SerializeGeo(electorateFeature.ToCollection(), electorateJson);
                    }
                }
            }
        }

        await WriteElectoratesMetaData();
    }

    static void WriteOptimised(string directory)
    {
        var jsonPath = Path.Combine(directory, "australia.geojson");
        var raw = JsonSerializer.DeserializeGeo(jsonPath);
        var rawTrimmedPath = Path.Combine(directory, "australia_trimmed.geojson");
        JsonSerializer.SerializeGeo(raw.Trim(), rawTrimmedPath);
        foreach (var percent in percents)
        {
            var percentJsonPath = Path.Combine(directory, $"australia_{percent}.geojson");
            ShapeToGeoJson.Convert(percentJsonPath, jsonPath, percent);
            var featureCollection = JsonSerializer.DeserializeGeo(percentJsonPath);
            var trimmedPath = Path.Combine(directory, $"australia_trimmed{percent:D2}.geojson");
            JsonSerializer.SerializeGeo(featureCollection.Trim(), trimmedPath);
        }
    }

    static async Task WriteElectoratesMetaData()
    {
        foreach (var electoratePair in electorateNames)
        {
            foreach (var electorateName in electoratePair.Value)
            {
                var electorate = await ElectoratesScraper.ScrapeElectorate(electorateName, electoratePair.Key);
                JsonSerializer.Serialize(electorate, Path.Combine(DataLocations.ElectoratesPath, electorateName + ".json"));
            }
        }
    }
}