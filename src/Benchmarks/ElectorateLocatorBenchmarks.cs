using System.IO.Compression;
using System.Reflection;
using BenchmarkDotNet.Attributes;

// Builds the location lookup from the full-detail 2025 map, the way the first TryLocateElectorate does.
//
// Note that Allocated understates the JsonDocument baseline on a first load. The 64+64+128MB of
// buffers it rents from ArrayPool.Shared are only allocated on the first iteration, then reused, so
// they drop out of the per operation figure. In a real process that first load is the only load,
// and the pool then holds on to those buffers.
[MemoryDiagnoser]
public class ElectorateLocatorBenchmarks
{
    Assembly assembly = typeof(DataLoader).Assembly;

    [Benchmark(Baseline = true)]
    public object JsonDocument()
    {
        using var stream = OpenGeoJson(out var archive);
        using (archive)
        {
            using var reader = new StreamReader(stream);
            return JsonDocumentLocator.Build(reader.ReadToEnd());
        }
    }

    [Benchmark]
    public object Streaming()
    {
        using var stream = OpenGeoJson(out var archive);
        using (archive)
        {
            return new ElectorateLocator(stream);
        }
    }

    Stream OpenGeoJson(out ZipArchive archive)
    {
        var resource = assembly.GetManifestResourceStream("AustraliaFull.zip")!;
        archive = new(resource);
        return archive.GetEntry("2025/australia.geojson")!.Open();
    }
}
