using System.Diagnostics;
using System.Linq;
using AustralianElectorates;
using AustralianElectorates.Bogus;
using Bogus;
using Xunit;
// ReSharper disable UnusedVariable

public class Samples
{
    [Fact]
    public void Foo()
    {
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
        var currentFennerGeoJson = DataLoader.Fenner.GetCurrentMap();
        Debug.WriteLine(currentFennerGeoJson);
        var futureFennerGeoJson = DataLoader.Fenner.GetFutureMap();
        Debug.WriteLine(futureFennerGeoJson);

        // get an electorates maps (geojson) by string
        var currentCanberraGeoJson = DataLoader.CurrentMaps.GetElectorate("Canberra");
        Debug.WriteLine(currentCanberraGeoJson);
        var futureCanberraGeoJson = DataLoader.FutureMaps.GetElectorate("Canberra");
        Debug.WriteLine(futureCanberraGeoJson);
    }

    [Fact]
    public void Bogus()
    {
        var faker = new Faker<Target>()
            .RuleFor(u => u.RandomElectorate, (f, u) => f.Electorates().Electorate())
            .RuleFor(u => u.RandomElectorateName, (f, u) => f.Electorates().Name())
            .RuleFor(u => u.RandomMember, (f, u) => f.Electorates().Member())
            .RuleFor(u => u.RandomMemberName, (f, u) => f.Electorates().MemberName());
        var targetInstance = faker.Generate();
    }
}

public class Target
{
    public string RandomElectorateName;
    public Member RandomMember;
    public string RandomMemberName;
    public Electorate RandomElectorate;
}