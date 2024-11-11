using System.Drawing;
using System.IO.Compression;
using AustralianElectorates;
using GeoJSON.Net.Feature;

public class Sync
{
    static List<int> percents;

    //static List<string> electoratesFuture = new();
    static List<string> electorates2016 = [];
    static List<string> electorates2019 = [];
    static List<string> electorates2022 = [];
    static List<string> electorates2025 = [];

    [Fact]
    [Trait("Category", "Integration")]
    public async Task SyncData()
    {
        var electorateToStateMap = GetElectorateToStateMap();
        // Hasher.Clear(DataLocations.DataPath);
        IoHelpers.PurgeDirectory(DataLocations.MapsPath);
        IoHelpers.PurgeDirectory(DataLocations.TempPath);

        await PartyScraper.Run();

        await Get2025();
        await Get2022();
        await Get2019();
        await Get2016();

        // File.Copy(DataLocations.Australia2019JsonPath, DataLocations.FutureAustraliaJsonPath);
        // await StatesToCountryDownloader.RunFuture();

        await ProcessYear(DataLocations.Maps2025Path, electorates2025, electorateToStateMap);
        await ProcessYear(DataLocations.Maps2022Path, electorates2022, electorateToStateMap);
        await ProcessYear(DataLocations.Maps2016Path, electorates2016, electorateToStateMap);
        await ProcessYear(DataLocations.Maps2019Path, electorates2019, electorateToStateMap);

        var electorates = await WriteElectoratesMetaData();

        IoHelpers.PurgeDirectoryRecursive(DataLocations.MapsDetail);
        foreach (var electorate in electorates)
        {
            var targetPath = Path.Combine(DataLocations.MapsDetail, $"{electorate.ShortName}.pdf");
            await Downloader.DownloadFile(targetPath, electorate.MapUrl);
        }

        foreach (var file in Directory.EnumerateFiles(DataLocations.MapsDetail))
        {
            var pngPath = await PdfToPng.Convert(file);
            File.Delete(file);

            CreatePortraitAndLandscape(pngPath);
        }

        WriteNamedCs(electorates);
        Export.ExportElectorates();
        // await Hasher.Create(DataLocations.DataPath);
        Zipper.ZipDir(DataLocations.MapsCuratedZipPath, DataLocations.MapsCuratedPath);
        WritePostcodeToElectorateJsonPathInner(electorates);
    }

    [Fact]
    public void WritePostcodeToElectorateJsonPath() =>
        WritePostcodeToElectorateJsonPathInner(DataLoader.Electorates);

    static void WritePostcodeToElectorateJsonPathInner(IReadOnlyList<IElectorate> electorates)
    {
        File.Delete(DataLocations.PostcodeToElectorateJsonPath);

        var dictionary = new Dictionary<int, List<string>>();
        foreach (var electorate in electorates)
        {
            foreach (var location in electorate.Locations)
            {
                if (!dictionary.TryGetValue(location.Postcode, out var list))
                {
                    dictionary[location.Postcode] = list = [];
                }

                list.Add(electorate.Name);
            }
        }

        JsonSerializerService.Serialize(dictionary, DataLocations.PostcodeToElectorateJsonPath);
    }

    static Dictionary<State, List<string>> GetElectorateToStateMap()
    {
        var electorateToStateMap = new Dictionary<State, List<string>>();
        foreach (var state in states)
        {
            electorateToStateMap[state] = [];
        }

        electorateToStateMap[State.VIC]
            .Add("hawke");
        var stateToElectorateFile = Path.Combine(DataLocations.DataPath, "state_to_electorate.txt");
        foreach (var line in File.ReadAllLines(stateToElectorateFile))
        {
            var indexOf = line.IndexOf(':');

            var statePart = line[..indexOf];
            var state = Enum.Parse<State>(statePart);
            var electorate = line.Substring(indexOf + 1, line.Length - indexOf - 1);
            var list = electorateToStateMap[state];
            list.Add(electorate);
        }

        return electorateToStateMap;
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreatePortraitAndLandscapeTest()
    {
        var pngPath = Path.Combine(DataLocations.MapsDetail, "banks.png");
        CreatePortraitAndLandscape(pngPath);
    }

    static void CreatePortraitAndLandscape(string pngPath)
    {
        var landscapePath = pngPath.Replace(".png", "_landscape.png");
        var portraitPath = pngPath.Replace(".png", "_portrait.png");
        using var image = Image.FromStream(File.OpenRead(pngPath));
        if (image.Height < image.Width)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipXY);
            image.Save(portraitPath);
            File.Copy(pngPath, landscapePath, true);
            return;
        }

        if (image.Height > image.Width)
        {
            image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            image.Save(landscapePath);
            File.Copy(pngPath, portraitPath, true);
            return;
        }

        File.Copy(pngPath, landscapePath, true);
        File.Copy(pngPath, portraitPath, true);
    }

    static Dictionary<State, HashSet<string>> electorateNames = new()
    {
        {
            State.ACT, new()
        },
        {
            State.TAS, new()
        },
        {
            State.SA, new()
        },
        {
            State.VIC, new()
        },
        {
            State.QLD, new()
        },
        {
            State.NT, new()
        },
        {
            State.NSW, new()
        },
        {
            State.WA, new()
        }
    };

    static List<State> states =
    [
        State.ACT,
        State.TAS,
        State.SA,
        State.VIC,
        State.QLD,
        State.NT,
        State.NSW,
        State.WA
    ];

    static Sync() => percents = [20, 10, 5, 1];

    static async Task ProcessYear(string yearPath, List<string> electorates, Dictionary<State, List<string>> electorateToStateMap)
    {
        await WriteOptimised(yearPath);

        foreach (var australiaPath in Directory.EnumerateFiles(yearPath, "australia*"))
        {
            var australiaFeatures = JsonSerializerService.DeserializeGeo(australiaPath);

            var electoratesDirectory = Path.Combine(yearPath, "Electorates");
            Directory.CreateDirectory(electoratesDirectory);
            foreach (var state in states)
            {
                var lower = state
                    .ToString()
                    .ToLower();
                var featureCollectionForState = australiaFeatures.FeaturesCollectionForState(electorateToStateMap[state]);
                var suffix = Path
                    .GetFileName(australiaPath)
                    .Replace("australia", "");
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

    static Task Get2016() =>
        // elected https://results.aec.gov.au/20499/Website/Downloads/HouseMembersElectedDownload-20499.csv
        // 2 party pref https://results.aec.gov.au/20499/Website/Downloads/HouseTppByDivisionDownload-20499.csv
        GetCountry(2016, "https://www.aec.gov.au/Electorates/gis/files/national-midmif-09052016.zip", DataLocations.Maps2016Path);

    static Task Get2019() =>
        // elected https://tallyroom.aec.gov.au/Downloads/HouseMembersElectedDownload-24310.csv
        // 2 party pref https://tallyroom.aec.gov.au/Downloads/HouseTppByDivisionDownload-24310.csv
        GetCountry(2019, "https://www.aec.gov.au/Electorates/gis/files/national-mapinfo-fe2019.zip", DataLocations.Maps2019Path);

    static Task Get2022() =>
        // elected https://tallyroom.aec.gov.au/Downloads/HouseMembersElectedDownload-24310.csv
        // 2 party pref https://tallyroom.aec.gov.au/Downloads/HouseTppByDivisionDownload-24310.csv
        GetCountry(2022, "https://www.aec.gov.au/Electorates/gis/files/2021-Cwlth_electoral_boundaries_TAB.zip", DataLocations.Maps2022Path);

    static Task Get2025() =>
        // elected ??
        // 2 party pref ??
        GetCountry(2025, "https://www.aec.gov.au/Electorates/gis/files/2021-Cwlth_electoral_boundaries_TAB.zip", DataLocations.Maps2025Path);

    static async Task GetCountry(int year, string url, string mapsPath)
    {
        var zip = Path.Combine(DataLocations.TempPath, year + ".zip");
        await Downloader.DownloadFile(zip, url);
        var targetPath = Path.Combine(mapsPath, "australia.geojson");

        var extractDirectory = Path.Combine(DataLocations.TempPath, $"australia{year}_extract");
        ZipFile.ExtractToDirectory(zip, extractDirectory);

        var tabFile = Directory
            .EnumerateFiles(extractDirectory, "*.tab")
            .Single();
        await MapToGeoJson.ConvertTab(targetPath, tabFile);

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
            if (!File
                    .ReadAllText(file)
                    .Contains("bbox"))
            {
                throw new(file);
            }
        }
    }

    static async Task WriteOptimised(string directory)
    {
        var jsonPath = Path.Combine(directory, "australia.geojson");
        var raw = JsonSerializerService.DeserializeGeo(jsonPath);
        raw.FixBoundingBox();
        foreach (var percent in percents)
        {
            var percentJsonPath = Path.Combine(directory, $"australia_{percent:D2}.geojson");
            await MapToGeoJson.ConvertShape(percentJsonPath, jsonPath, percent);
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
                var existIn2022 = electorates2022.Contains(electorateName);
                var existIn2025 = electorates2025.Contains(electorateName);
                //var existInFuture = electoratesFuture.Contains(electorateName);

                ElectorateEx electorate;
                if (existIn2025)
                {
                    electorate = await ElectoratesScraper.ScrapeElectorate(2025,electorateName, electoratePair.Key);
                }
                else if (existIn2022)
                {
                    electorate = await ElectoratesScraper.ScrapeElectorate(2022,electorateName, electoratePair.Key);
                }
                else if(existIn2019)
                {
                    electorate = await ElectoratesScraper.ScrapeElectorate(2019,electorateName, electoratePair.Key);
                }
                else if(existIn2016)
                {
                    electorate = await ElectoratesScraper.ScrapeElectorate(2016,electorateName, electoratePair.Key);
                }
                else
                {
                    throw new("Does not exist in any year");
                }

                electorate.Exist2016 = existIn2016;
                electorate.Exist2019 = existIn2019;
                electorate.Exist2022 = existIn2022;
                electorate.Exist2025 = existIn2025;
                //electorate.ExistInFuture = existInFuture;
                electorate.Locations = SelectLocations(electorateName, localityData)
                    .ToList();
                electorates.Add(electorate);
            }
        }

        File.Delete(DataLocations.ElectoratesJsonPath);
        JsonSerializerService.Serialize(electorates, DataLocations.ElectoratesJsonPath);
        return electorates;
    }

    static List<Location> SelectLocations(string electorateName, List<AecLocalityData> localityData) =>
        localityData
            .Where(_ => string.Equals(_.Electorate, electorateName, StringComparison.OrdinalIgnoreCase))
            .GroupBy(_ => _.Postcode)
            .Select(group =>
                new Location
                {
                    Postcode = group.Key,
                    Localities = group
                        .Select(_ => _.Place)
                        .ToList()
                })
            .ToList();

    static void WriteNamedCs(List<ElectorateEx> electorates)
    {
        var namedData = Path.Combine(DataLocations.AustralianElectoratesProjectPath, "DataLoader_named.cs");
        File.Delete(namedData);

        using (var writer = File.CreateText(namedData))
        {
            writer.WriteLine(
                """

                // ReSharper disable IdentifierTypo
                // ReSharper disable RedundantDefaultMemberInitializer

                namespace AustralianElectorates;

                public static partial class DataLoader
                {
                """);

            writer.WriteLine(
                """

                    static void InitNamed()
                    {
                """);
            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine(
                    $"""

                             {name} = Electorates.Single(_ => _.Name == "{electorate.Name}");
                     """);
            }

            writer.WriteLine("    }");

            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine(
                    $$"""

                          public static IElectorate {{name}} { get; private set; } = null!;
                      """);
            }

            writer.WriteLine("}");
        }

        var namedBogusData = Path.Combine(DataLocations.BogusProjectPath, "ElectorateDataSet_named.cs");
        File.Delete(namedBogusData);
        using (var writer = File.CreateText(namedBogusData))
        {
            writer.WriteLine(
                """

                // ReSharper disable IdentifierTypo
                // ReSharper disable RedundantDefaultMemberInitializer

                namespace AustralianElectorates.Bogus;

                public partial class ElectorateDataSet
                {
                """);
            foreach (var electorate in electorates)
            {
                var name = GetCSharpName(electorate);
                writer.WriteLine(
                    $"""

                         public static IElectorate {name}() =>
                             DataLoader.{name};
                     """);
            }

            writer.WriteLine("}");
        }
    }

    static string GetCSharpName(Electorate electorate) =>
        electorate
            .Name
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("'", "");
}