using System.IO.Compression;
using BenchmarkDotNet.Attributes;

// A lookup once the map is loaded. The places cover a small inner city electorate (Sydney), ones
// whose bounding boxes overlap others (Canberra, Hobart), and large coastal electorates with long,
// detailed coastlines (Lancelin in Durack, and O'Connor).
[MemoryDiagnoser]
public class LocateBenchmarks
{
    static Dictionary<string, (double Latitude, double Longitude, int Postcode)> places = new()
    {
        ["Canberra"] = (-35.2809, 149.1300, 2601),
        ["Sydney"] = (-33.8688, 151.2093, 2000),
        ["Hobart"] = (-42.8821, 147.3272, 7000),
        ["Lancelin"] = (-31.0225285, 115.3301909, 6044),
        ["O'Connor"] = (-34.2527415, 118.2189916, 6330)
    };

    [Params("Canberra", "Sydney", "Hobart", "Lancelin", "O'Connor")]
    public string Place { get; set; } = null!;

    (double Latitude, double Longitude, int Postcode) place;
    RayCastLocator rayCast = null!;

    [GlobalSetup]
    public void Setup()
    {
        place = places[Place];
        using var stream = typeof(DataLoader).Assembly.GetManifestResourceStream("AustraliaFull.zip")!;
        using var archive = new ZipArchive(stream);
        using var entry = archive.GetEntry("2025/australia.geojson")!.Open();
        using var reader = new StreamReader(entry);
        rayCast = new(JsonDocumentLocator.Build(reader.ReadToEnd()));
        // load the library's map outside of the measurement
        DataLoader.TryLocateElectorate(place.Latitude, place.Longitude, out _);
    }

    [Benchmark(Baseline = true)]
    public IElectorate? RayCastEveryEdge() =>
        rayCast.Find(place.Latitude, place.Longitude);

    [Benchmark]
    public IElectorate? BandIndex()
    {
        DataLoader.TryLocateElectorate(place.Latitude, place.Longitude, out var electorate);
        return electorate;
    }

    [Benchmark]
    public IElectorate? BandIndexWithPostcode()
    {
        DataLoader.TryLocateElectorate(place.Latitude, place.Longitude, place.Postcode, out var electorate);
        return electorate;
    }
}
