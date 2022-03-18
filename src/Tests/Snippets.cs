using AustralianElectorates;
using AustralianElectorates.Bogus;
using Bogus;

// ReSharper disable UnusedVariable
[UsesVerify]
public class Snippets
{
    [Fact]
    public async Task Foo()
    {
        #region usage
        // get an electorate by name
        var fenner = DataLoader.Fenner;
        Debug.WriteLine(fenner.Description);

        // get an electorate by string
        var canberra = DataLoader.Electorates.Single(x => x.Name == "Canberra");
        Debug.WriteLine(canberra.Description);

        // get an electorates maps (geojson) by string
        var fennerGeoJson2016 = DataLoader.Fenner.Get2016Map();
        Debug.WriteLine(fennerGeoJson2016);
        var fennerGeoJson2019 = DataLoader.Fenner.Get2019Map();
        Debug.WriteLine(fennerGeoJson2019);
        var fennerGeoJson2022 = DataLoader.Fenner.Get2022Map();
        Debug.WriteLine(fennerGeoJson2022);

        // get an electorates maps (geojson) by string
        var canberraGeoJson2016 = DataLoader.Maps2016.GetElectorate("Canberra");
        Debug.WriteLine(canberraGeoJson2016);
        var canberraGeoJson2019 = DataLoader.Maps2019.GetElectorate("Canberra");
        Debug.WriteLine(canberraGeoJson2019);
        var canberraGeoJson2022 = DataLoader.Maps2022.GetElectorate("Canberra");
        Debug.WriteLine(canberraGeoJson2022);

        // export all data to a directory
        // structure will be
        // /electorates.json
        // /2016 (2016 states and australia geojson files)
        // /2016/Electorates (2016 electorate geojson files)
        // /2019 (2019 states and australia geojson files)
        // /2019/Electorates (2019 electorate geojson files)
        // /2022 (202 states and australia geojson files)
        // /2022/Electorates (2022 electorate geojson files)
        var directory = Path.Combine(Environment.CurrentDirectory, "Maps");
        Directory.CreateDirectory(directory);
        await DataLoader.Export(directory);
        #endregion
    }

    [Fact]
    public void DetailMapsUsage()
    {
        #region usageDetailMaps
        var pathToPng = DetailMaps.MapForElectorate("Bass");
        #endregion
    }

    [Fact]
    public Task ElectoratesSampleJson() =>
        Verify(string.Join(Environment.NewLine, File.ReadAllLines(DataLocations.ElectoratesJsonPath).Take(50)));

    [Fact]
    public Task LocalitiesSampleJson() =>
        Verify(string.Join(Environment.NewLine, File.ReadAllLines(DataLocations.LocalitiesPath).Take(21)));

    [Fact]
    public Task PartiesSampleJson() =>
        Verify(string.Join(Environment.NewLine, File.ReadAllLines(DataLocations.PartiesJsonPath).Take(34)));

    [Fact]
    public void Bogus()
    {
        #region usagebogus

        var faker = new Faker<Target>()
            .RuleFor(
                property: u => u.RandomElectorate,
                setter: (f, _) => f.AustralianElectorates().Electorate())
            .RuleFor(
                property: u => u.RandomElectorateName,
                setter: (f, _) => f.AustralianElectorates().Name());
        var targetInstance = faker.Generate();
        #endregion
    }

    public class Target
    {
        public string RandomElectorateName = null!;
        public IElectorate RandomElectorate = null!;
    }
}