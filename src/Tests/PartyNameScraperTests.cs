using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public class PartyNameScraperTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public Task Run()
    {
        return PartyNameScraper.Run();
    }

    public PartyNameScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}