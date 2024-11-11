using System.IO.Compression;
using AustralianElectorates;
using GeoJSON.Net.Feature;

public static class StatesToCountryDownloader
{
    //https://www.aec.gov.au/Electorates/gis/gis_datadownload.htm
    private static Dictionary<State, string> stateUrls = new()
    {
        {State.ACT, "https://www.aec.gov.au/Electorates/gis/files/act-july-2018-esri.zip"},
        {State.TAS, "https://www.aec.gov.au/Electorates/gis/files/tas-november-2017-esri.zip"},
        {State.SA, "https://www.aec.gov.au/Electorates/gis/files/sa-july-2018-esri.zip"},
        {State.VIC, "https://www.aec.gov.au/Electorates/gis/files/Vic-october-2024-esri.zip"},
        {State.QLD, "https://www.aec.gov.au/Electorates/gis/files/qld-march-2018-esri.zip"},
        {State.NT, "https://www.aec.gov.au/Electorates/gis/files/NT-Feb_2017-ESRI.zip"},
        {State.NSW, "https://www.aec.gov.au/Electorates/gis/files/NSW-october-2024-ESRI.zip"},
        {State.WA, "https://www.aec.gov.au/Electorates/gis/files/WA-september-2024-ESRI.zip"},
    };

    // Merges the current national with the state amendments to get the future
    public static async Task RunFuture()
    {
        var previousElectionJson = Path.Combine(DataLocations.Maps2022Path,"australia.geojson");
        var futureElectionJson = Path.Combine(DataLocations.Maps2025Path,"australia.geojson");
        var features = JsonSerializerService.DeserializeGeo(previousElectionJson);

        foreach (var stateUrl in stateUrls)
        {
            var state = stateUrl.Key;
            RemoveStateFromFeatures(features, state);

            var targetPath = Path.Combine(DataLocations.TempPath, $"{state}.zip");
            await Downloader.DownloadFile(targetPath, stateUrl.Value);
            var extractDirectory = Path.Combine(DataLocations.TempPath, $"{state}_extract");
            ZipFile.ExtractToDirectory(targetPath, extractDirectory);
            StatisticalAreaCleaner.DeleteStatisticalAreaFiles(extractDirectory);
            var featureCollection = await WriteState(state, IoHelpers.FindFile(extractDirectory, "shp"));
            features.Features.AddRange(featureCollection.Features);
            File.Delete(targetPath);
            Directory.Delete(extractDirectory, true);
        }

        features.FixBoundingBox();
        JsonSerializerService.SerializeGeo(features, futureElectionJson);
    }

    static void RemoveStateFromFeatures(FeatureCollection features, State state)
    {
        var stateString = state.ToString();
        foreach (var feature in features.Features.ToList())
        {
            var featureProperty = (string) feature.Properties["state"];
            if (string.Equals(featureProperty, stateString, StringComparison.OrdinalIgnoreCase))
            {
                features.Features.Remove(feature);
            }
        }
    }

    static async Task<FeatureCollection> WriteState(State state, string shpFile)
    {
        var stateJsonPath = Path.Combine(DataLocations.TempPath, $"{state}.geojson");
        await MapToGeoJson.ConvertShape(stateJsonPath, shpFile);

        var stateCollection = JsonSerializerService.Deserialize<FeatureCollection>(stateJsonPath);

        MetadataCleaner.CleanMetadata(stateCollection, state);
        return stateCollection;
    }
}

