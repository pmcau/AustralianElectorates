using System.Diagnostics;
using AustralianElectorates;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class DetailMapsTests
{
    [Fact]
    public void Simple()
    {
        Trace.WriteLine(DetailMaps.MapForElectorate("swan"));
    }

    public DetailMapsTests(ITestOutputHelper output) :
        base(output)
    {
    }
}