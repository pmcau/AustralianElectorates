using AustralianElectorates;

[UsesVerify]
public class DataLoaderTests
{
    [Fact]
    public Task Electorates() =>
        Verify(DataLoader.Electorates.Select(x => x.Name));

    [Fact]
    public void GetAustralia()
    {
        var data2016 = DataLoader.Maps2016.GetAustralia();
        Assert.NotEmpty(data2016);
        Assert.NotNull(data2016);
        var data2019 = DataLoader.Maps2019.GetAustralia();
        Assert.NotEmpty(data2019);
        Assert.NotNull(data2019);
        var data2022 = DataLoader.Maps2022.GetAustralia();
        Assert.NotEmpty(data2022);
        Assert.NotNull(data2022);
        // var dataFuture = DataLoader.MapsFuture.GetAustralia();
        // Assert.NotEmpty(dataFuture);
        // Assert.NotNull(dataFuture);
    }

    [Fact]
    public Task TryFindElectorate_not_found()
    {
        Assert.False(DataLoader.TryFindElectorate("not Found", out _));
        return Throws(() => DataLoader.FindElectorate("not Found"));
    }

    [Fact]
    public Task NewRemoved()
    {
        var electorates2019 = DataLoader.Electorates.Where(x=>x.Exist2019);
        var electorates2022 = DataLoader.Electorates.Where(x=>x.Exist2022);
        var removed = electorates2019.Where(x => !electorates2022.Contains(x)).Select(x=>x.Name);
        var added = electorates2022.Where(x => !electorates2019.Contains(x)).Select(x=>x.Name);
        return Verify(new{added, removed});
    }

    [Fact]
    public Task ValidateElectorates() =>
        Throws(() => DataLoader.ValidateElectorates("not Found", "Bass"));

    [Fact]
    public Task FindInvalidateElectorates() =>
        Verify(DataLoader.FindInvalidateElectorates("not Found", "Bass"));

    [Fact]
    public Task FindInvalidateElectorates_by_short_name() =>
        Verify(DataLoader.FindInvalidateElectorates("not Found", "port-adelaide"));

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
    public Task Export() =>
        InnerExport(false);

    [Fact]
    public Task Export_overwrite() =>
        InnerExport(true);

    static async Task InnerExport(bool overwrite)
    {
        var directory = Path.Combine(Environment.CurrentDirectory, $"export_overwrite-{overwrite}");
        Directory.CreateDirectory(directory);

        try
        {
            if (overwrite)
            {
                await DataLoader.Export(directory);
            }

            await DataLoader.Export(directory);
            await Verify(Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories).Count());
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public Task Get2016State()
    {
        var data = DataLoader.Maps2016.GetState(State.ACT);
        return Verify(data.GeoJson.Substring(0, 200));
    }

    [Fact]
    public Task Get2019State()
    {
        var data = DataLoader.Maps2019.GetState(State.ACT);
        return Verify(data.GeoJson.Substring(0, 200));
    }


    [Fact]
    public Task Get2022State()
    {
        var data = DataLoader.Maps2022.GetState(State.ACT);
        return Verify(data.GeoJson.Substring(0, 200));
    }

    // [Fact]
    // public Task GetFutureState()
    // {
    //     var data = DataLoader.MapsFuture.GetState(State.ACT);
    //     return Verify(data.GeoJson.Substring(0, 200));
    // }

    [Fact]
    public Task Get2016Electorate()
    {
        var data = DataLoader.Maps2016.GetElectorate("fenner");
        return Verify(data.GeoJson.Substring(0, 200));
    }

    [Fact]
    public Task Get2019Electorate()
    {
        var data = DataLoader.Maps2019.GetElectorate("fenner");
        return Verify(data.GeoJson.Substring(0, 200));
    }

    [Fact]
    public void GetElectorateFull()
    {
        var data = DataLoader.Maps2016.GetElectorate("O'Connor");
        Assert.NotNull(data);
    }

    // [Fact]
    // public Task GetFutureElectorate()
    // {
    //     var data = DataLoader.MapsFuture.GetElectorate("fenner");
    //     return Verify(data.GeoJson.Substring(0, 200));
    // }
    //
    // [Fact]
    // public Task GetFutureElectorateExtension()
    // {
    //     var data = DataLoader.Fenner.GetFutureMap();
    //     return Verify(data.GeoJson.Substring(0, 200));
    // }
    //
    // [Fact]
    // public Task GetCurrentElectorateExtension()
    // {
    //     var data = DataLoader.Fenner.GetFutureMap();
    //     return Verify(data.GeoJson.Substring(0, 200));
    // }

    [Fact]
    public Task LoadAll()
    {
        DataLoader.LoadAll();
        return Verify(new
        {
            // FutureLoadedElectorateMaps = DataLoader.MapsFuture.LoadedElectorates.Count,
            // FutureLoadedStateMaps = DataLoader.MapsFuture.LoadedStates.Count,
            LoadedElectorateMaps2016 = DataLoader.Maps2016.LoadedElectorates.Count,
            LoadedStateMaps2016 = DataLoader.Maps2016.LoadedStates.Count,
            LoadedElectorateMaps2019 = DataLoader.Maps2019.LoadedElectorates.Count,
            LoadedStateMaps2019 = DataLoader.Maps2019.LoadedStates.Count,
            LoadedElectorateMaps2022 = DataLoader.Maps2022.LoadedElectorates.Count,
            LoadedStateMaps2022 = DataLoader.Maps2022.LoadedStates.Count,
        });
    }

    [Fact]
    public void ElectorateData_CurrentParty() =>
        Assert.NotNull(DataLoader.Adelaide.CurrentParty);

    [Fact]
    public Task Elections() =>
        Verify(DataLoader.Elections.Select(election => new
        {
            election.Parliament,
            election.Year,
            election.Date,
            electorates = election.Electorates.Select(electorate => electorate.Name)
        }));

    [Fact]
    public void FindElection()
    {
        var election = DataLoader.FindElection(45);
        Assert.NotNull(election);
    }

    [Fact]
    public Task TryFindElection_not_found()
    {
        var parliament = 0;
        Assert.False(DataLoader.TryFindElection(parliament, out _));
        return Throws(() => DataLoader.FindElection(parliament));
    }
}