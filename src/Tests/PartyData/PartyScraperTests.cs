public class PartyScraperTests
{
    [Fact(Explicit = true)]
    public Task Run() =>
        PartyScraper.Run();
}
