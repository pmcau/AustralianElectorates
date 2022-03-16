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
        {State.VIC, "https://www.aec.gov.au/Electorates/gis/files/vic-july-2018-esri.zip"},
        {State.QLD, "https://www.aec.gov.au/Electorates/gis/files/qld-march-2018-esri.zip"},
        {State.NT, "https://www.aec.gov.au/Electorates/gis/files/nt-july-2020-esri.zip"},
        {State.NSW, "https://www.aec.gov.au/Electorates/gis/files/nsw-esri-06042016.zip"},
        {State.WA, "https://www.aec.gov.au/Electorates/gis/files/wa-esri-19012016.zip"},
    };

    public static Task RunFuture() =>
        Run(stateUrls, DataLocations.Australia2019JsonPath, DataLocations.FutureAustraliaJsonPath);

    static async Task Run(Dictionary<State, string> dictionary, string sourceJsonPath, string targetJsonPath)
    {
        var features = JsonSerializerService.DeserializeGeo(sourceJsonPath);

        foreach (var stateUrl in dictionary)
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
        JsonSerializerService.SerializeGeo(features, targetJsonPath);
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