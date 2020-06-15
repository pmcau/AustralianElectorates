using System.Threading.Tasks;
using Xunit;

public class PartyScraperTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public Task Run()
    {
        return PartyScraper.Run();
    }
}