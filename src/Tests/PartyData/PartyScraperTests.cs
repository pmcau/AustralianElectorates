public class PartyScraperTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public Task Run() =>
        PartyScraper.Run();
}