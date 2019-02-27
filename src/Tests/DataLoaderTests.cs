using System;
using System.IO;
using System.Linq;
using ApprovalTests;
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
        //IoHelpers.PurgeDirectoryRecursive(directory);
        try
        {
            DataLoader.Export(directory,overwrite);
            Approvals.Verify(Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories).Count());
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public void GetCurrentState()
    {
        var data = DataLoader.CurrentMaps.GetState(State.ACT);
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetFutureState()
    {
        var data = DataLoader.FutureMaps.GetState(State.ACT);
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetCurrentElectorate()
    {
        var data = DataLoader.CurrentMaps.GetElectorate("fenner");
        ObjectApprover.VerifyWithJson(data.GeoJson.Substring(0,200));
    }

    [Fact]
    public void GetElectorateFull()
    {
        var data = DataLoader.CurrentMaps.GetElectorate("O'Connor");
        Assert.NotNull(data);
    }

    [Fact]
    public void GetFutureElectorate()
    {
        var data = DataLoader.FutureMaps.GetElectorate("fenner");
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
            FutureLoadedElectorateMaps = DataLoader.FutureMaps.LoadedElectorates.Count,
            FutureLoadedStateMaps = DataLoader.FutureMaps.LoadedStates.Count,
            CurrentLoadedElectorateMaps = DataLoader.CurrentMaps.LoadedElectorates.Count,
            CurrentLoadedStateMaps = DataLoader.CurrentMaps.LoadedStates.Count,
        });
    }
}