using System;
using System.IO;
using System.Linq;
using ApprovalTests;
using AustralianElectorates;
using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class DataLoaderTests :
    XunitLoggingBase
{
    [Fact]
    public void Electorates()
    {
        ObjectApprover.VerifyWithJson(DataLoader.Electorates.Select(x=>x.Name));
    }

    [Fact]
    public void GetAustralia()
    {
        var data2016 = DataLoader.Maps2016.GetAustralia();
        Assert.NotEmpty(data2016);
        Assert.NotNull(data2016);
        var dataFuture = DataLoader.MapsFuture.GetAustralia();
        Assert.NotEmpty(dataFuture);
        Assert.NotNull(dataFuture);
    }

    [Fact]
    public void TryFindElectorate_not_found()
    {
        Assert.False(DataLoader.TryFindElectorate("not Found", out _));
        var exception = Assert.Throws<ElectorateNotFoundException>(() => DataLoader.FindElectorate("not Found"));
        ObjectApprover.VerifyWithJson(new {exception.Name, exception.Message});
    }

    [Fact]
    public void ValidateElectorates()
    {
        var exception = Assert.Throws<ElectoratesNotFoundException>(() => DataLoader.ValidateElectorates("not Found", "Bass"));
        ObjectApprover.VerifyWithJson(new {exception.Names, exception.Message});
    }

    [Fact]
    public void FindInvalidateElectorates()
    {
        ObjectApprover.VerifyWithJson(DataLoader.FindInvalidateElectorates("not Found", "Bass"));
    }

    [Fact]
    public void TryFindElectorate()
    {
        Assert.True(DataLoader.TryFindElectorate("Bass", out var electorate));
        Assert.NotNull(electorate);
        Assert.NotNull(DataLoader.FindElectorate("Bass"));
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
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetFutureState()
    {
        var data = DataLoader.MapsFuture.GetState(State.ACT);
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void Get2016Electorate()
    {
        var data = DataLoader.Maps2016.GetElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
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
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetFutureElectorateExtension()
    {
        var data = DataLoader.Fenner.GetFutureMap();
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetCurrentElectorateExtension()
    {
        var data = DataLoader.Fenner.GetFutureMap();
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void LoadAll()
    {
        DataLoader.LoadAll();
        ObjectApprover.VerifyWithJson(new
        {
            FutureLoadedElectorateMaps = DataLoader.MapsFuture.LoadedElectorates.Count,
            FutureLoadedStateMaps = DataLoader.MapsFuture.LoadedStates.Count,
            LoadedElectorateMaps2016 = DataLoader.Maps2016.LoadedElectorates.Count,
            LoadedStateMaps2016 = DataLoader.Maps2016.LoadedStates.Count,
        });
    }

    public DataLoaderTests(ITestOutputHelper output) :
        base(output)
    {
    }
}