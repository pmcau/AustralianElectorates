public class DataLoaderTests
{
    // [Fact]
    // public async Task Electorates()
    // {
    //     File.Delete(DataLocations.PostcodeToElectorateJsonPath);
    //     await using var writer = File.CreateText(DataLocations.PostcodeToElectorateJsonPath);
    //     await writer.WriteLineAsync("{");
    //     foreach (var electorate in DataLoader.Electorates)
    //     {
    //         foreach (var location in electorate.Locations)
    //         {
    //             await writer.WriteLineAsync($"  {location.Postcode}:\"{electorate.ShortName}\",");
    //         }
    //     }
    //     await writer.WriteLineAsync("}");
    // }

    [Fact]
    public void GetAustralia()
    {
        var data2019 = DataLoader.Maps2019.GetAustralia();
        Assert.NotEmpty(data2019);
        Assert.NotNull(data2019);
        var data2022 = DataLoader.Maps2022.GetAustralia();
        Assert.NotEmpty(data2022);
        Assert.NotNull(data2022);
        var data2025 = DataLoader.Maps2025.GetAustralia();
        Assert.NotEmpty(data2025);
        Assert.NotNull(data2025);
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
        var electorates2022 = DataLoader
            .Electorates.Where(_ => _.Exist2022)
            .ToArray();
        var electorates2025 = DataLoader
            .Electorates.Where(_ => _.Exist2025)
            .ToArray();
        var removed = electorates2022
            .Where(_ => !electorates2025.Contains(_))
            .Select(_ => _.Name);
        var added = electorates2025
            .Where(_ => !electorates2022.Contains(_))
            .Select(_ => _.Name);
        return Verify(new
        {
            added,
            removed
        });
    }

    [Fact]
    public Task ValidateElectorates() =>
        Throws(() => DataLoader.ValidateElectorates("not Found", "Bass"));

    [Fact]
    public Task FindInvalidateElectorates() =>
        Verify(DataLoader.FindInvalidateElectorates("not Found", "Bass"));

    [Fact]
    public Task FindInvalidateElectorates_by_short_name() =>
        Verify(DataLoader.FindInvalidateElectorates("not Found", "bass"));

    [Fact]
    public void TryFindElectorate()
    {
        Assert.True(DataLoader.TryFindElectorate("Bass", out var electorate));
        Assert.NotNull(electorate);
        Assert.NotNull(DataLoader.FindElectorate("Bass"));
        Assert.True(DataLoader.TryFindElectorate("bass", out electorate));
        Assert.NotNull(electorate);
        Assert.NotNull(DataLoader.FindElectorate("bass"));
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
            await Verify(Directory
                .EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Count());
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public Task Get2019State()
    {
        var data = DataLoader.Maps2019.GetState(State.ACT);
        return Verify(data.GeoJson[..200]);
    }

    [Fact]
    public Task Get2022State()
    {
        var data = DataLoader.Maps2022.GetState(State.ACT);
        return Verify(data.GeoJson[..200]);
    }

    [Fact]
    public Task Get2025State()
    {
        var data = DataLoader.Maps2025.GetState(State.ACT);
        return Verify(data.GeoJson[..200]);
    }

    // [Fact]
    // public Task GetFutureState()
    // {
    //     var data = DataLoader.MapsFuture.GetState(State.ACT);
    //     return Verify(data.GeoJson.Substring(0, 200));
    // }

    [Fact]
    public Task Get2019Electorate()
    {
        var data = DataLoader.Maps2019.GetElectorate("fenner");
        return Verify(data.GeoJson[..200]);
    }

    [Fact]
    public void GetElectorateFull()
    {
        var data = DataLoader.Maps2022.GetElectorate("O'Connor");
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
            LoadedElectorateMaps2019 = DataLoader.Maps2019.LoadedElectorates.Count,
            LoadedStateMaps2019 = DataLoader.Maps2019.LoadedStates.Count,
            LoadedElectorateMaps2022 = DataLoader.Maps2022.LoadedElectorates.Count,
            LoadedStateMaps2022 = DataLoader.Maps2022.LoadedStates.Count,
            LoadedElectorateMaps2025 = DataLoader.Maps2025.LoadedElectorates.Count,
            LoadedStateMaps2025 = DataLoader.Maps2025.LoadedStates.Count
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
        var election = DataLoader.FindElection(47);
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