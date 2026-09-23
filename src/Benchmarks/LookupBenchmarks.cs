using BenchmarkDotNet.Attributes;

// Finding electorates by name. FindElectorateScan is the 12.10.0 implementation, which went
// through every electorate on each call.
[MemoryDiagnoser]
public class NameLookupBenchmarks
{
    [Params("Wills", "eden-monaro")]
    public string Name { get; set; } = null!;

    [Benchmark(Baseline = true)]
    public IElectorate? FindElectorateScan() =>
        DataLoader.Electorates.SingleOrDefault(_ =>
            string.Equals(_.Name, Name, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(_.ShortName, Name, StringComparison.OrdinalIgnoreCase));

    [Benchmark]
    public IElectorate FindElectorate() =>
        DataLoader.FindElectorate(Name);
}

// Finding electorates by postcode. ElectoratesForPostcodeScan is the 12.10.0 implementation,
// which went through every location of every electorate on each call.
[MemoryDiagnoser]
public class PostcodeLookupBenchmarks
{
    [Params(2000)]
    public int Postcode { get; set; }

    [Benchmark(Baseline = true)]
    public int ElectoratesForPostcodeScan() =>
        DataLoader.Electorates
            .Count(_ => _.Exist2025 &&
                        _.ContainsPostcode(Postcode));

    [Benchmark]
    public int ElectoratesForPostcode() =>
        DataLoader.ElectoratesForPostcode(Postcode).Count();
}
