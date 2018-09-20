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
        var data2016 = MapsLoader.LoadAustralia2016Map();
        Assert.NotEmpty(data2016);
        Assert.NotNull(data2016);
        var dataFuture = MapsLoader.LoadAustraliaFutureMap();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void Load2016State()
    {
        var data = MapsLoader.LoadState2016Map(State.ACT);
        ObjectApprover.VerifyWithJson(data);
    }

    [Fact]
    public void LoadFutureState()
    {
        var data = MapsLoader.LoadStateFutureMap(State.ACT);
        ObjectApprover.VerifyWithJson(data);
    }

    [Fact]
    public void Load2016Electorate()
    {
        var data = MapsLoader.LoadElectorateFutureMap("fenner");
        ObjectApprover.VerifyWithJson(data);
    }

    [Fact]
    public void LoadFutureElectorate()
    {
        var data = MapsLoader.LoadElectorateFutureMap("fenner");
        ObjectApprover.VerifyWithJson(data);
    }

    [Fact]
    public void LoadAll()
    {
        MapsLoader.LoadAll();
        ObjectApprover.VerifyWithJson(new
        {
            FutureElectorateMapsCount = MapsLoader.LoadedFutureElectorateMaps.Count,
            LoadedFutureStateMapsCount = MapsLoader.LoadedFutureStateMaps.Count,
            Loaded2016ElectorateMapsCount = MapsLoader.Loaded2016ElectorateMaps.Count,
            Loaded2016StateMapsCount = MapsLoader.Loaded2016StateMaps.Count,
        });
    }
}