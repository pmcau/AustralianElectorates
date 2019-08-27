using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CountryData;
using Xunit;
using Xunit.Abstractions;

public class PostcodeScraperTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var data = await PostcodeScraper.Run();
        File.Delete(DataLocations.LocalitiesPath);
        JsonSerializerService.Serialize(data, DataLocations.LocalitiesPath);
        ObjectApprover.Verify(data.Take(10));
    }

    [Fact]
    public async Task Specific()
    {
        var place = CountryLoader.LoadAustraliaLocationData().PostCodes().Single(x=>x.Key == "2612");
        ObjectApprover.Verify(await PostcodeScraper.GetAECDataForPostcode(place));
    }

    public PostcodeScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}