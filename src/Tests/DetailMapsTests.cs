using System.Diagnostics;
using AustralianElectorates;
using Xunit;
using Xunit.Abstractions;

public class DetailMapsTests :
    XunitLoggingBase
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