using System.Linq;
using AustralianElectorates;
using ObjectApproval;
using Xunit;

public class DataLoaderTests
{
    [Fact]
    public void Electorates()
    {
        ObjectApprover.VerifyWithJson(DataLoader.Electorates.Select(x=>x.Name));
    }

    [Fact]
    public void GetAustralia()
    {
        var dataCurrent = DataLoader.CurrentMaps.GetAustralia();
        Assert.NotEmpty(dataCurrent);
        Assert.NotNull(dataCurrent);
        var dataFuture = DataLoader.FutureMaps.GetAustralia();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void GetCurrentState()
    {
        var data = DataLoader.CurrentMaps.GetState(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void GetFutureState()
    {
        var data = DataLoader.FutureMaps.GetState(State.ACT);
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void GetCurrentElectorate()
    {
        var data = DataLoader.CurrentMaps.GetElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void GetFutureElectorate()
    {
        var data = DataLoader.FutureMaps.GetElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.Substring(0,200));
    }

    [Fact]
    public void LoadAll()
    {
        DataLoader.LoadAll();
        ObjectApprover.VerifyWithJson(new
        {
            FutureLoadedElectorateMaps = DataLoader.FutureMaps.LoadedElectorates.Count,
            FutureLoadedStateMaps = DataLoader.FutureMaps.LoadedStates.Count,
            CurrentLoadedElectorateMaps = DataLoader.CurrentMaps.LoadedElectorates.Count,
            CurrentLoadedStateMaps = DataLoader.CurrentMaps.LoadedStates.Count,
        });
    }
}