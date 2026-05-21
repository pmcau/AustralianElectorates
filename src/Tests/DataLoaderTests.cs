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
    public void LocateElectorate()
    {
        // (-35.349, 149.09) is in Bean (the simplified map misplaced this into Canberra)
        Assert.Equal("Bean", DataLoader.LocateElectorate(-35.349, 149.09).Name);
        // a point in remote Western Australia
        Assert.Equal("O'Connor", DataLoader.LocateElectorate(-34.2527415, 118.2189916).Name);
        // Lancelin: a coastal town the simplified map dropped but the full map resolves
        Assert.Equal("Durack", DataLoader.LocateElectorate(-31.0225285, 115.3301909).Name);
    }

    [Fact]
    public void LocateElectorate_with_postcode()
    {
        // postcode narrows the candidates; result matches the no-postcode lookup
        Assert.Equal("Bean", DataLoader.LocateElectorate(-35.349, 149.09, 2903).Name);
        // a postcode whose electorates do not contain the point still resolves
        // via the full-scan backstop (2000 is Sydney; the point is in Bean)
        Assert.Equal("Bean", DataLoader.LocateElectorate(-35.349, 149.09, 2000).Name);
        // null postcode behaves like the no-postcode overload
        Assert.Equal("Bean", DataLoader.LocateElectorate(-35.349, 149.09, null).Name);
    }

    [Fact]
    public void TryLocateElectorate_with_postcode()
    {
        Assert.True(DataLoader.TryLocateElectorate(-35.349, 149.09, 2903, out var electorate));
        Assert.NotNull(electorate);
        Assert.Equal("Bean", electorate.Name);
    }

    [Fact]
    public void LocateElectorate_outside_australia() =>
        Assert.Throws<Exception>(() => DataLoader.LocateElectorate(0, 0));

    [Fact]
    public void TryLocateElectorate()
    {
        Assert.True(DataLoader.TryLocateElectorate(-35.349, 149.09, out var electorate));
        Assert.NotNull(electorate);
        Assert.Equal("Bean", electorate.Name);

        Assert.False(DataLoader.TryLocateElectorate(0, 0, out var missing));
        Assert.Null(missing);
    }

    [Fact]
    public void LocateElectorate_unsupported_geometry()
    {
        var geoJson = """{"type":"FeatureCollection","features":[{"properties":{"electorateName":"Bean"},"type":"Feature","geometry":{"type":"Point","coordinates":[0,0]}}]}""";
        Assert.Throws<Exception>(() => new ElectorateLocator(geoJson));
    }

    [Fact]
    public void LocateElectorate_single_at_gulf_border()
    {
        // -14.878297, 129.001284 is in the Joseph Bonaparte Gulf on the WA/NT border. The expanded
        // coastlines must not overlap there - the point must be inside exactly one electorate.
        var point = new NetTopologySuite.Geometries.Point(129.001284, -14.878297);
        using var stream = File.OpenRead(DataLocations.AustraliaFullZipPath);
        using var archive = new System.IO.Compression.ZipArchive(stream);
        using var reader = new StreamReader(archive.GetEntry("2025/australia.geojson")!.Open());
        var features = new NetTopologySuite.IO.GeoJsonReader()
            .Read<NetTopologySuite.Features.FeatureCollection>(reader.ReadToEnd());
        var matches = features
            .Where(_ => _.Geometry.Contains(point))
            .Select(_ => (string) _.Attributes["electorateName"])
            .ToList();
        Assert.Single(matches);
    }

    [Fact]
    public void LocateElectorate_no_inland_gaps()
    {
        // OverlayNG: robust union of the full map.
        NetTopologySuite.NtsGeometryServices.Instance =
            new(NetTopologySuite.Geometries.GeometryOverlay.NG);

        using var stream = File.OpenRead(DataLocations.AustraliaFullZipPath);
        using var archive = new System.IO.Compression.ZipArchive(stream);
        using var reader = new StreamReader(archive.GetEntry("2025/australia.geojson")!.Open());
        var features = new NetTopologySuite.IO.GeoJsonReader()
            .Read<NetTopologySuite.Features.FeatureCollection>(reader.ReadToEnd());
        var geometries = features
            .Select(_ => NetTopologySuite.Geometries.Utilities.GeometryFixer.Fix(_.Geometry))
            .ToArray();
        var factory = geometries[0].Factory;
        var union = factory.BuildGeometry(geometries).Union();

        // Adjacent electorates must meet with no gap. A hole in the merged map is an inland gap when it
        // is a thin sliver (state borders in the AEC source don't perfectly align); real bays/gulfs
        // beyond the ~10km coastline expansion are wide and are allowed.
        var gaps = new List<string>();
        for (var i = 0; i < union.NumGeometries; i++)
        {
            if (union.GetGeometryN(i) is not NetTopologySuite.Geometries.Polygon polygon)
            {
                continue;
            }

            for (var hole = 0; hole < polygon.NumInteriorRings; hole++)
            {
                var ring = factory.CreatePolygon(polygon.GetInteriorRingN(hole).Coordinates);
                if (ring.Area > 1e-8 &&
                    ring.Buffer(-0.004).IsEmpty)
                {
                    var point = ring.InteriorPoint;
                    gaps.Add($"lon={point.X:F4} lat={point.Y:F4} area={ring.Area:E2}");
                }
            }
        }

        Assert.Empty(gaps);
    }

    [Fact]
    public void ElectoratesForPostcode()
    {
        var electorates = DataLoader.ElectoratesForPostcode(2606)
            .ToList();
        Assert.All(electorates, _ => Assert.True(_.Exist2025));
        var names = electorates
            .Select(_ => _.Name)
            .ToList();
        Assert.Contains("Bean", names);
        Assert.Contains("Canberra", names);
    }

    [Fact]
    public Task TryFindElectorate_not_found()
    {
        Assert.False(DataLoader.TryFindElectorate("not Found", out _));
        return Throws(() => DataLoader.FindElectorate("not Found"))
            .IgnoreStackTrace();
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
        Throws(() => DataLoader.ValidateElectorates("not Found", "Bass"))
            .IgnoreStackTrace();

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
        using var directory = new TempDirectory();

        if (overwrite)
        {
            await DataLoader.Export(directory);
        }

        await DataLoader.Export(directory);
        await Verify(directory.Info.EnumerateFiles("*.*", SearchOption.AllDirectories).Count());
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
        return Throws(() => DataLoader.FindElection(parliament))
            .IgnoreStackTrace();
    }
}
