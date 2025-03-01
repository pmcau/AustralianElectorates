﻿// ReSharper disable UnusedVariable
public class Snippets
{
    [Fact]
    public Task Foo()
    {
        #region usage

        // get an electorate by name
        var fenner = DataLoader.Fenner;
        Trace.WriteLine(fenner.Description);

        // get an electorate by string
        var canberra = DataLoader.Electorates.Single(_ => _.Name == "Canberra");
        Trace.WriteLine(canberra.Description);

        // get an electorates maps (geojson) by string
        var fennerGeoJson2019 = DataLoader.Fenner.Get2019Map();
        Trace.WriteLine(fennerGeoJson2019);
        var fennerGeoJson2022 = DataLoader.Fenner.Get2022Map();
        Trace.WriteLine(fennerGeoJson2022);
        var fennerGeoJson2025 = DataLoader.Fenner.Get2025Map();
        Trace.WriteLine(fennerGeoJson2025);

        // get an electorates maps (geojson) by string
        var canberraGeoJson2019 = DataLoader.Maps2019.GetElectorate("Canberra");
        Trace.WriteLine(canberraGeoJson2019);
        var canberraGeoJson2022 = DataLoader.Maps2022.GetElectorate("Canberra");
        Trace.WriteLine(canberraGeoJson2022);
        var canberraGeoJson2025 = DataLoader.Maps2022.GetElectorate("Canberra");
        Trace.WriteLine(canberraGeoJson2025);

        // export all data to a directory
        // structure will be
        // /electorates.json
        // /2019 (2019 states and australia geojson files)
        // /2019/Electorates (2019 electorate geojson files)
        // /2022 (202 states and australia geojson files)
        // /2022/Electorates (2022 electorate geojson files)
        var directory = Path.Combine(Environment.CurrentDirectory, "Maps");
        Directory.CreateDirectory(directory);
        return DataLoader.Export(directory);

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
        Verify(string.Join(Environment.NewLine, File
            .ReadAllLines(DataLocations.ElectoratesJsonPath)
            .Take(50)));

    [Fact]
    public Task LocalitiesSampleJson() =>
        Verify(string.Join(Environment.NewLine, File
            .ReadAllLines(DataLocations.LocalitiesPath)
            .Take(21)));

    [Fact]
    public Task PartiesSampleJson() =>
        Verify(string.Join(Environment.NewLine, File
            .ReadAllLines(DataLocations.PartiesJsonPath)
            .Take(34)));

    [Fact]
    public void Bogus()
    {
        #region usagebogus

        var faker = new Faker<Target>()
            .RuleFor(
                property: u => u.RandomElectorate,
                setter: (f, _) => f
                    .AustralianElectorates()
                    .Electorate())
            .RuleFor(
                property: u => u.RandomElectorateName,
                setter: (f, _) => f
                    .AustralianElectorates()
                    .Name());
        var targetInstance = faker.Generate();

        #endregion
    }

    public class Target
    {
        public string RandomElectorateName = null!;
        public IElectorate RandomElectorate = null!;
    }
}