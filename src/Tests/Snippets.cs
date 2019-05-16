using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AustralianElectorates;
using AustralianElectorates.Bogus;
using Bogus;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable UnusedVariable

public class Snippets :
    XunitLoggingBase
{
    [Fact]
    public void Foo()
    {
        #region usage
        // get an electorate by name
        var fenner = DataLoader.Fenner;
        Debug.WriteLine(fenner.Description);

        // get an electorate by string
        var canberra = DataLoader.Electorates.Single(x => x.Name == "Canberra");
        Debug.WriteLine(canberra.Description);

        // access the current member
        var currentMember = canberra.Members.First();
        Debug.WriteLine(currentMember.Name);
        Debug.WriteLine(currentMember.Party);

        // get an electorates maps (geojson) by string
        var currentFennerGeoJson = DataLoader.Fenner.Get2016Map();
        Debug.WriteLine(currentFennerGeoJson);
        var futureFennerGeoJson = DataLoader.Fenner.GetFutureMap();
        Debug.WriteLine(futureFennerGeoJson);

        // get an electorates maps (geojson) by string
        var canberraGeoJson2016 = DataLoader.Maps2016.GetElectorate("Canberra");
        Debug.WriteLine(canberraGeoJson2016);
        var futureCanberraGeoJson = DataLoader.MapsFuture.GetElectorate("Canberra");
        Debug.WriteLine(futureCanberraGeoJson);

        // export all data to a directory
        // structure will be
        // /electorates.json
        // /2016 (2016 states and australia geojson files)
        // /2016/Electorates (2016 electorate geojson files)
        // /Future (future states and australia geojson files)
        // /Future/Electorates (future electorate geojson files)
        var directory = Path.Combine(Environment.CurrentDirectory, "Maps");
        Directory.CreateDirectory(directory);
        DataLoader.Export(directory);
        #endregion
    }

    [Fact]
    public void Bogus()
    {
        #region usagebogus
        var faker = new Faker<Target>()
            .RuleFor(u => u.RandomElectorate, (f, u) => f.AustralianElectorates().Electorate())
            .RuleFor(u => u.RandomElectorateName, (f, u) => f.AustralianElectorates().Name())
            .RuleFor(u => u.RandomCurrentMember, (f, u) => f.AustralianElectorates().CurrentMember())
            .RuleFor(u => u.RandomCurrentMemberName, (f, u) => f.AustralianElectorates().CurrentMemberName())
            .RuleFor(u => u.RandomMember, (f, u) => f.AustralianElectorates().Member())
            .RuleFor(u => u.RandomMemberName, (f, u) => f.AustralianElectorates().MemberName());
        var targetInstance = faker.Generate();
        #endregion
    }

    public class Target
    {
        public string RandomElectorateName;
        public Member RandomMember;
        public string RandomMemberName;
        public Electorate RandomElectorate;
        public Member RandomCurrentMember;
        public string RandomCurrentMemberName;
    }

    public Snippets(ITestOutputHelper output) :
        base(output)
    {
    }
}