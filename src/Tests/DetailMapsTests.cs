using System.Diagnostics;
using AustralianElectorates;
using Xunit;

public class DetailMapsTests
{
    [Fact]
    public void Simple()
    {
        Trace.WriteLine(DetailMaps.MapForElectorate("swan"));
    }
}