using System.Linq;
using AustralianElectorates;
using ObjectApproval;
using Xunit;

public class MapsLoaderTests
{
    [Fact]
    public void Electorates()
    {
        ObjectApprover.VerifyWithJson(MapsLoader.Electorates.Select(x=>x.Name));
    }

    [Fact]
    public void LoadAustraliaMaps()
    {
        var data2016 = MapsLoader.Maps2016.LoadAustraliaMap();
        Assert.NotEmpty(data2016);
        Assert.NotNull(data2016);
        var dataFuture = MapsLoader.MapsFuture.LoadAustraliaMap();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void Maps2016LoadStateMap()
    {
        var data = MapsLoader.Maps2016.LoadStateMap(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void MapsFutureLoadStateMap()
    {
        var data = MapsLoader.MapsFuture.LoadStateMap(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void Maps2016LoadElectorateMap()
    {
        var data = MapsLoader.Maps2016.LoadElectorateMap("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void MapsFutureLoadElectorateMap()
    {
        var data = MapsLoader.MapsFuture.LoadElectorateMap("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void LoadAll()
    {
        MapsLoader.LoadAll();
        ObjectApprover.VerifyWithJson(new
        {
            MapsFutureLoadedElectorateMaps = MapsLoader.MapsFuture.LoadedElectorateMaps.Count,
            MapsFutureLoadedStateMaps = MapsLoader.MapsFuture.LoadedStateMaps.Count,
            Maps2016LoadedElectorateMaps = MapsLoader.Maps2016.LoadedElectorateMaps.Count,
            Maps2016LoadedStateMaps = MapsLoader.Maps2016.LoadedStateMaps.Count,
        });
    }
}