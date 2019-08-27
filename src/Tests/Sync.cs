using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AustralianElectorates;
using GeoJSON.Net.Feature;
using Xunit;
using Xunit.Abstractions;

public class Sync :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task SyncData()
    {
        IoHelpers.PurgeDirectory(DataLocations.MapsPath);
        IoHelpers.PurgeDirectory(DataLocations.TempPath);

        await Get2016();
        await Get2019();


        File.Copy(DataLocations.Australia2019JsonPath, DataLocations.FutureAustraliaJsonPath);
        await StatesToCountryDownloader.RunFuture();

        ProcessYear(DataLocations.Maps2016Path, electorates2016);
        ProcessYear(DataLocations.Maps2019Path, electorates2019);
        ProcessYear(DataLocations.MapsFuturePath, electoratesFuture);

        var electorates = await WriteElectoratesMetaData();

        IoHelpers.PurgeDirectoryRecursive(DataLocations.MapsDetail);
        foreach (var electorate in electorates)
        {
            var targetPath = Path.Combine(DataLocations.MapsDetail, $"{electorate.ShortName}.pdf");
            await Downloader.DownloadFile(targetPath, electorate.MapUrl);
        }

        foreach (var file in Directory.EnumerateFiles(DataLocations.MapsDetail))
        {
            PdfToPng.Convert(file);
            File.Delete(file);
        }

        WriteNamedCs(electorates);
        Export.ExportElectorates();
        Zipper.ZipDir(DataLocations.MapsCuratedZipPath, DataLocations.MapsCuratedPath);
    }

    static List<int> percents;

    static List<string> electoratesFuture = new List<string>();
    static List<string> electorates2016 = new List<string>();
    static List<string> electorates2019 = new List<string>();

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
        percents = new List<int> { 20, 10, 5, 1 };
    }

    static void ProcessYear(string yearPath, List<string> electorates)
    {
        WriteOptimised(yearPath);

        foreach (var australiaPath in Directory.EnumerateFiles(yearPath, "australia*"))
        {
            var australiaFeatures = JsonSerializerService.DeserializeGeo(australiaPath);

            var electoratesDirectory = Path.Combine(yearPath, "Electorates");
            Directory.CreateDirectory(electoratesDirectory);
            foreach (var state in states)
            {
                var lower = state.ToString().ToLower();
                var featureCollectionForState = australiaFeatures.FeaturesCollectionForState(state);
                var suffix = Path.GetFileName(australiaPath).Replace("australia", "");
                var stateJson = Path.Combine(yearPath, $"{lower}{suffix}");
                JsonSerializerService.SerializeGeo(featureCollectionForState, stateJson);

                foreach (var electorateFeature in featureCollectionForState.Features)
                {
                    var electorate = (string) electorateFeature.Properties["electorateShortName"];
                    var electorateNameList = electorateNames[state];
                    electorateNameList.Add(electorate);
                    electorates.Add(electorate);

                    var electorateJsonPath = Path.Combine(electoratesDirectory, $"{electorate}{suffix}");
                    JsonSerializerService.SerializeGeo(electorateFeature.ToCollection(), electorateJsonPath);
                }
            }
        }
    }

    static Task Get2016()
    {
        // elected https://results.aec.gov.au/20499/Website/Downloads/HouseMembersElectedDownload-20499.csv
        // 2 party pref https://results.aec.gov.au/20499/Website/Downloads/HouseTppByDivisionDownload-20499.csv
        return GetCountry(2016, "https://www.aec.gov.au/Electorates/gis/files/national-midmif-09052016.zip", DataLocations.Maps2016Path);
    }

    static Task Get2019()
    {
        // elected https://tallyroom.aec.gov.au/Downloads/HouseMembersElectedDownload-24310.csv
        // 2 party pref https://tallyroom.aec.gov.au/Downloads/HouseTppByDivisionDownload-24310.csv
        return GetCountry(2019, "https://www.aec.gov.au/Electorates/gis/files/national-mapinfo-fe2019.zip", DataLocations.Maps2019Path);
    }

    static async Task GetCountry(int year, string url, string mapsPath)
    {
        var zip = Path.Combine(DataLocations.TempPath, year + ".zip");
        await Downloader.DownloadFile(zip, url);
        var targetPath = Path.Combine(mapsPath, "australia.geojson");

        var extractDirectory = Path.Combine(DataLocations.TempPath, $"australia{year}_extract");
        ZipFile.ExtractToDirectory(zip, extractDirectory);

        MapToGeoJson.ConvertTab(targetPath, Path.Combine(extractDirectory, "COM_ELB.tab"));

        var featureCollection = JsonSerializerService.Deserialize<FeatureCollection>(targetPath);
        featureCollection.FixBoundingBox();
        MetadataCleaner.CleanMetadata(featureCollection);
        JsonSerializerService.SerializeGeo(featureCollection, targetPath);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void AssertAllContainBbox()
    {
        foreach (var file in Directory.EnumerateFiles(DataLocations.MapsPath, "*.geojson", SearchOption.AllDirectories))
        {
            if (!File.ReadAllText(file).Contains("bbox"))
            {
                throw new Exception(file);
            }
        }
    }

    static void WriteOptimised(string directory)
    {
        var jsonPath = Path.Combine(directory, "australia.geojson");
        var raw = JsonSerializerService.DeserializeGeo(jsonPath);
        raw.FixBoundingBox();
        foreach (var percent in percents)
        {
            var percentJsonPath = Path.Combine(directory, $"australia_{percent:D2}.geojson");
            MapToGeoJson.ConvertShape(percentJsonPath, jsonPath, percent);
            var featureCollection = JsonSerializerService.DeserializeGeo(percentJsonPath);
            featureCollection.FixBoundingBox();

            JsonSerializerService.SerializeGeo(featureCollection, percentJsonPath);
        }
    }

    static async Task<List<ElectorateEx>> WriteElectoratesMetaData()
    {
        var localityData = JsonSerializerService.Deserialize<List<AecLocalityData>>(DataLocations.LocalitiesPath);
        var electorates = new List<ElectorateEx>();
        foreach (var electoratePair in electorateNames)
        {
            foreach (var electorateName in electoratePair.Value)
            {
                var existIn2016 = electorates2016.Contains(electorateName);
                var existIn2019 = electorates2019.Contains(electorateName);
                var existInFuture = electoratesFuture.Contains(electorateName);

                ElectorateEx electorate;
                if (existIn2019)
                {
                    electorate = await ElectoratesScraper.ScrapeCurrentElectorate(electorateName, electoratePair.Key);
                }
                else
                {
                    electorate = await ElectoratesScraper.Scrape2016Electorate(electorateName, electoratePair.Key);
                }
                electorate.Exist2016 = existIn2016;
                electorate.Exist2019 = existIn2019;
                electorate.ExistInFuture = existInFuture;
                electorate.Locations = SelectLocations(electorateName,localityData).ToList();
                electorates.Add(electorate);
            }
        }

        var combine = Path.Combine(DataLocations.DataPath, "electorates.json");
        File.Delete(combine);
        JsonSerializerService.Serialize(electorates, combine);
        return electorates;
    }

    static List<Location> SelectLocations(string electorateName, List<AecLocalityData> localityData)
    {
        return localityData
            .Where(x => string.Equals(x.Electorate, electorateName, StringComparison.OrdinalIgnoreCase))
            .GroupBy(x => x.Postcode)
            .Select(group =>
                new Location
                {
                    Postcode = group.Key,
                    Localities = group.Select(x => x.Place).ToList()
                })
            .ToList();
    }

    static void WriteNamedCs(List<ElectorateEx> electorates)
    {
        var namedData = Path.Combine(DataLocations.AustralianElectoratesProjectPath, "DataLoader_named.cs");
        File.Delete(namedData);

        using (var writer = File.CreateText(namedData))
        {
            writer.WriteLine(@"
// ReSharper disable IdentifierTypo
using System.Linq;

namespace AustralianElectorates
{
    public static partial class DataLoader
    {");

            writer.WriteLine(@"
        static void InitNamed()
        {");
            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine($@"
            {name} = Electorates.Single(x => x.Name == ""{electorate.Name}"");");
            }
            writer.WriteLine("        }");

            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine($@"
        public static Electorate {name} {{ get; private set;}}");
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
        }


        var namedBogusData = Path.Combine(DataLocations.BogusProjectPath, "ElectorateDataSet_named.cs");
        File.Delete(namedBogusData);
        using (var writer = File.CreateText(namedBogusData))
        {
            writer.WriteLine(@"
// ReSharper disable IdentifierTypo
using Bogus;

namespace AustralianElectorates.Bogus
{
    public partial class ElectorateDataSet : DataSet
    {");
            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine($@"
        public Electorate {name}()
        {{
            return DataLoader.{name};
        }}");
            }
            writer.WriteLine("    }");
            writer.WriteLine("}");
        }
    }

    static string GetCSharpName(Electorate electorate)
    {
        return electorate.Name.Replace(" ", "").Replace("-", "").Replace("'", "");
    }

    public Sync(ITestOutputHelper output) :
        base(output)
    {
    }
}