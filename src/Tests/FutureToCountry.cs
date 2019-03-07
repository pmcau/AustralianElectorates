using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using AustralianElectorates;
using GeoJSON.Net.Feature;

public static class FutureToCountry
{
    static Dictionary<State, string> stateUrls = new Dictionary<State, string>
    {
        {State.ACT, "https://www.aec.gov.au/Electorates/gis/files/act-july-2018-esri.zip"},
        {State.TAS, "https://www.aec.gov.au/Electorates/gis/files/tas-november-2017-esri.zip"},
        {State.SA, "https://www.aec.gov.au/Electorates/gis/files/sa-july-2018-esri.zip"},
        {State.VIC, "https://www.aec.gov.au/Electorates/gis/files/vic-july-2018-esri.zip"},
        {State.QLD, "https://www.aec.gov.au/Electorates/gis/files/qld-march-2018-esri.zip"},
        {State.NT, "https://www.aec.gov.au/Electorates/gis/files/nt-esri-07022017.zip"},
        {State.NSW, "https://www.aec.gov.au/Electorates/gis/files/nsw-esri-06042016.zip"},
        {State.WA, "https://www.aec.gov.au/Electorates/gis/files/wa-esri-19012016.zip"},
    };

    public static async Task Run()
    {
        var features = new List<Feature>();
        foreach (var stateUrl in stateUrls)
        {
            var state = stateUrl.Key;
            var targetPath = Path.Combine(DataLocations.TempPath, $"2016_{state}.zip");
            await Downloader.DownloadFile(targetPath, stateUrl.Value);
            var extractDirectory = Path.Combine(DataLocations.TempPath, $"2016_{state}_extract");
            ZipFile.ExtractToDirectory(targetPath, extractDirectory);
            DeleteStatisticalAreaFiles(extractDirectory);
            var featureCollection = WriteState(state, IoHelpers.FindFile(extractDirectory, "shp"));
            features.AddRange(featureCollection.Features);
        }

        var collection = new FeatureCollection(features);
        collection.FixBoundingBox();
        JsonSerializer.SerializeGeo(collection, DataLocations.FutureAustraliaJsonPath);
    }

    static FeatureCollection WriteState(State state, string shpFile)
    {
        var stateJsonPath = Path.Combine(DataLocations.TempPath, $"{state}.geojson");
        MapToGeoJson.ConvertShape(stateJsonPath, shpFile);

        var stateCollection = JsonSerializer.Deserialize<FeatureCollection>(stateJsonPath);

        MetadataCleaner.CleanMetadata(stateCollection, state);
        return stateCollection;
    }

    static void DeleteStatisticalAreaFiles(string extract)
    {
        foreach (var saFile in Directory.EnumerateFiles(extract, "*_SA1s_*", SearchOption.AllDirectories))
        {
            File.Delete(saFile);
        }
    }
}