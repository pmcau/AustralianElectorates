using AustralianElectorates;

public class ElectoratesScraperTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var northSydney = await ElectoratesScraper.ScrapeElectorate(2022, "north-sydney", State.NSW);
        var bean = await ElectoratesScraper.ScrapeElectorate(2022, "bean", State.ACT);
        var lingiari = await ElectoratesScraper.ScrapeElectorate(2016, "lingiari", State.NT);
        var bass = await ElectoratesScraper.ScrapeElectorate(2022, "clark", State.TAS);
        var banks = await ElectoratesScraper.ScrapeElectorate(2022, "banks", State.NSW);
        var northernTerritory = await ElectoratesScraper.ScrapeElectorate(2022, "northern-territory", State.NT);
        var denison = await ElectoratesScraper.ScrapeElectorate(2016, "denison", State.TAS);
        var fenner = await ElectoratesScraper.ScrapeElectorate(2022, "fenner", State.ACT);
        var hunter = await ElectoratesScraper.ScrapeElectorate(2022, "hunter", State.NSW);
        var spence = await ElectoratesScraper.ScrapeElectorate(2022, "spence", State.SA);
        var cook = await ElectoratesScraper.ScrapeElectorate(2022, "cook", State.NSW);
        var canberra = await ElectoratesScraper.ScrapeElectorate(2022, "canberra", State.ACT);
        var batman = await ElectoratesScraper.ScrapeElectorate(2022, "batman", State.VIC);
        var melbourne = await ElectoratesScraper.ScrapeElectorate(2022, "melbourne", State.VIC);
        await Verify(new
        {
            northSydney,
            northernTerritory,
            lingiari,
            denison,
            melbourne,
            banks,
            hunter,
            batman,
            spence,
            cook,
            bean,
            fenner,
            canberra,
            bass
        });
    }
}