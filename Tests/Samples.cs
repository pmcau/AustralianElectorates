using System.Diagnostics;
using System.Linq;
using AustralianElectorates;
using Xunit;

public class Samples
{
    [Fact]
    public void Foo()
    {
        var canberraInfo = DataLoader.Electorates.Single(x => x.Name == "Canberra");
        Debug.WriteLine(canberraInfo.Description);
        var currentMember = canberraInfo.Members.First();
        Debug.WriteLine(currentMember.Name);
        Debug.WriteLine(currentMember.Party);
        var canberraGeoJson = DataLoader.CurrentMaps.GetElectorate("Canberra");
    }
}