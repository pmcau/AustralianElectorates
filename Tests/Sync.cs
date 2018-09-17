using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AustralianElectorates;
using Xunit;

public class Sync
{
    static List<int> percents;

    static Dictionary<State, HashSet<string>> electorateNames = new Dictionary<State, HashSet<string>>
    {
        {State.ACT, new HashSet<string>()},
        {State.TAS, new HashSet<string>()},
        {State.SA, new HashSet<string>()},
        {State.VIC, new HashSet<string>()},
        {State.QLD, new HashSet<string>()},
        {State.NT, new HashSet<string>()},
        {State.NSW, new HashSet<string>()},
        {State.WA, new HashSet<string>()},
    };

    static List<State> states = new List<State>
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
        percents = new List<int> {20, 10, 5, 1};
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task SyncData()
    {
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
                        electorateNameList.Add(electorate);

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
        foreach (var feature in raw.Features)
        {
            feature.BoundingBoxes = feature.CalculateBoundingBox();
        }
        var rawTrimmedPath = Path.Combine(directory, "australia_trimmed.geojson");
        JsonSerializer.SerializeGeo(raw.Trim(), rawTrimmedPath);
        foreach (var percent in percents)
        {
            var percentJsonPath = Path.Combine(directory, $"australia_{percent:D2}.geojson");
            ShapeToGeoJson.Convert(percentJsonPath, jsonPath, percent);
            var featureCollection = JsonSerializer.DeserializeGeo(percentJsonPath);
            foreach (var feature in featureCollection.Features)
            {
                feature.BoundingBoxes = feature.CalculateBoundingBox();
            }
            var trimmedPath = Path.Combine(directory, $"australia_trimmed{percent:D2}.geojson");
            JsonSerializer.SerializeGeo(featureCollection.Trim(), trimmedPath);
        }
    }

    static async Task WriteElectoratesMetaData()
    {
        var electorates = new List<Electorate>();
        foreach (var electoratePair in electorateNames)
        {
            foreach (var electorateName in electoratePair.Value)
            {
                electorates.Add(await ElectoratesScraper.ScrapeElectorate(electorateName, electoratePair.Key));
            }
        }

        JsonSerializer.Serialize(electorates, Path.Combine(DataLocations.DataPath, "electorates.json"));
    }
}