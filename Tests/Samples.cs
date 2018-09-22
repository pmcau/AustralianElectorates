using System.Diagnostics;
using System.Linq;
using AustralianElectorates;
using Xunit;

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
}