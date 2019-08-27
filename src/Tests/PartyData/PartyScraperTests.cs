using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public class PartyScraperTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public Task Run()
    {
        return PartyScraper.Run();
    }

    public PartyScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}