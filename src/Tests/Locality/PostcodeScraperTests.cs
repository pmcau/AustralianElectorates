using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        JsonSerializer.Serialize(data, DataLocations.LocalitiesPath);
        ObjectApprover.Verify(data.Take(10));
    }

    [Fact]
    public async Task Specific()
    {
        ObjectApprover.Verify(await PostcodeScraper.GetAECDataForPostcode("2612"));
    }

    public PostcodeScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}