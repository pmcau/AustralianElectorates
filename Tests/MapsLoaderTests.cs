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
        var dataCurrent = MapsLoader.Current.LoadAustralia();
        Assert.NotEmpty(dataCurrent);
        Assert.NotNull(dataCurrent);
        var dataFuture = MapsLoader.Future.LoadAustralia();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void MapsCurrentLoadStateMap()
    {
        var data = MapsLoader.Current.LoadState(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void FutureLoadState()
    {
        var data = MapsLoader.Future.LoadState(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void CurrentLoadElectorate()
    {
        var data = MapsLoader.Current.LoadElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void FutureLoadElectorate()
    {
        var data = MapsLoader.Future.LoadElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void LoadAll()
    {
        MapsLoader.LoadAll();
        ObjectApprover.VerifyWithJson(new
        {
            FutureLoadedElectorateMaps = MapsLoader.Future.LoadedElectorates.Count,
            FutureLoadedStateMaps = MapsLoader.Future.LoadedStates.Count,
            CurrentLoadedElectorateMaps = MapsLoader.Current.LoadedElectorates.Count,
            CurrentLoadedStateMaps = MapsLoader.Current.LoadedStates.Count,
        });
    }
}