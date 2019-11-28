using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class PartyScraperTests :
    VerifyBase
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