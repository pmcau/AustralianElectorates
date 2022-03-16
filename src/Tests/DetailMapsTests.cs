using AustralianElectorates;

public class DetailMapsTests
{
    [Fact]
    public void Simple() =>
        Trace.WriteLine(DetailMaps.MapForElectorate("swan"));
}