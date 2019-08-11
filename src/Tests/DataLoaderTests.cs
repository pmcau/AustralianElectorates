using System;
using System.IO;
using System.Linq;
using ApprovalTests;
using AustralianElectorates;
using Xunit;
using Xunit.Abstractions;

public class DataLoaderTests :
    XunitLoggingBase
{
    [Fact]
    public void Electorates()
    {
        ObjectApprover.Verify(DataLoader.Electorates.Select(x=>x.Name));
    }

    [Fact]
    public void GetAustralia()
    {
        var data2016 = DataLoader.Maps2016.GetAustralia();
        Assert.NotEmpty(data2016);
        Assert.NotNull(data2016);
        var data2019 = DataLoader.Maps2019.GetAustralia();
        Assert.NotEmpty(data2019);
        Assert.NotNull(data2019);
        var dataFuture = DataLoader.MapsFuture.GetAustralia();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void TryFindElectorate_not_found()
    {
        Assert.False(DataLoader.TryFindElectorate("not Found", out _));
        var exception = Assert.Throws<ElectorateNotFoundException>(() => DataLoader.FindElectorate("not Found"));
        ObjectApprover.Verify(new {exception.Name, exception.Message});
    }

    [Fact]
    public void ValidateElectorates()
    {
        var exception = Assert.Throws<ElectoratesNotFoundException>(() => DataLoader.ValidateElectorates("not Found", "Bass"));
        ObjectApprover.Verify(new {exception.Names, exception.Message});
    }

    [Fact]
    public void FindInvalidateElectorates()
    {
        ObjectApprover.Verify(DataLoader.FindInvalidateElectorates("not Found", "Bass"));
    }

    [Fact]
    public void FindInvalidateElectorates_by_short_name()
    {
        ObjectApprover.Verify(DataLoader.FindInvalidateElectorates("not Found", "port-adelaide"));
    }

    [Fact]
    public void TryFindElectorate()
    {
        Assert.True(DataLoader.TryFindElectorate("Port Adelaide", out var electorate));
        Assert.NotNull(electorate);
        Assert.NotNull(DataLoader.FindElectorate("Port Adelaide"));
        Assert.True(DataLoader.TryFindElectorate("port-adelaide", out electorate));
        Assert.NotNull(electorate);
        Assert.NotNull(DataLoader.FindElectorate("port-adelaide"));
    }

    [Fact]
    public void Export()
    {
        InnerExport(false);
    }

    [Fact]
    public void Export_overwrite()
    {
        InnerExport(true);
    }

    private static void InnerExport(bool overwrite)
    {
        var directory = Path.Combine(Environment.CurrentDirectory, "export");
        Directory.CreateDirectory(directory);

        try
        {   if (overwrite)
            {
                DataLoader.Export(directory);
            }
            DataLoader.Export(directory);
            Approvals.Verify(Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories).Count());
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public void Get2016State()
    {
        var data = DataLoader.Maps2016.GetState(State.ACT);
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void Get2019State()
    {
        var data = DataLoader.Maps2019.GetState(State.ACT);
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetFutureState()
    {
        var data = DataLoader.MapsFuture.GetState(State.ACT);
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void Get2016Electorate()
    {
        var data = DataLoader.Maps2016.GetElectorate("fenner");
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void Get2019Electorate()
    {
        var data = DataLoader.Maps2019.GetElectorate("fenner");
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetElectorateFull()
    {
        var data = DataLoader.Maps2016.GetElectorate("O'Connor");
        Assert.NotNull(data);
    }

    [Fact]
    public void GetFutureElectorate()
    {
        var data = DataLoader.MapsFuture.GetElectorate("fenner");
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetFutureElectorateExtension()
    {
        var data = DataLoader.Fenner.GetFutureMap();
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetCurrentElectorateExtension()
    {
        var data = DataLoader.Fenner.GetFutureMap();
        ObjectApprover.Verify(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void LoadAll()
    {
        DataLoader.LoadAll();
        ObjectApprover.Verify(new
        {
            FutureLoadedElectorateMaps = DataLoader.MapsFuture.LoadedElectorates.Count,
            FutureLoadedStateMaps = DataLoader.MapsFuture.LoadedStates.Count,
            LoadedElectorateMaps2016 = DataLoader.Maps2016.LoadedElectorates.Count,
            LoadedStateMaps2016 = DataLoader.Maps2016.LoadedStates.Count,
            LoadedElectorateMaps2019 = DataLoader.Maps2019.LoadedElectorates.Count,
            LoadedStateMaps2019 = DataLoader.Maps2019.LoadedStates.Count,
        });
    }

    public DataLoaderTests(ITestOutputHelper output) :
        base(output)
    {
    }
}