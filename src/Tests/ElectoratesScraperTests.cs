﻿using AustralianElectorates;

[UsesVerify]
public class ElectoratesScraperTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task<Task> Run()
    {
        var lingiari = await ElectoratesScraper.ScrapeCurrentElectorate("lingiari", State.NT);
        var bass = await ElectoratesScraper.ScrapeCurrentElectorate("clark", State.TAS);
        var banks = await ElectoratesScraper.ScrapeCurrentElectorate("banks", State.NSW);
        var northernTerritory = await ElectoratesScraper.ScrapeCurrentElectorate("northern-territory", State.NT);
        var denison = await ElectoratesScraper.Scrape2016Electorate("denison", State.TAS);
        var fenner = await ElectoratesScraper.ScrapeCurrentElectorate("fenner", State.ACT);
        var hunter = await ElectoratesScraper.ScrapeCurrentElectorate("hunter", State.NSW);
        var spence = await ElectoratesScraper.ScrapeCurrentElectorate("spence", State.SA);
        var cook = await ElectoratesScraper.ScrapeCurrentElectorate("cook", State.NSW);
        var canberra = await ElectoratesScraper.ScrapeCurrentElectorate("canberra", State.ACT);
        var bean = await ElectoratesScraper.ScrapeCurrentElectorate("bean", State.ACT);
        var batman = await ElectoratesScraper.ScrapeCurrentElectorate("batman", State.VIC);
        var melbourne = await ElectoratesScraper.ScrapeCurrentElectorate("melbourne", State.VIC);
        return Verify(new {northernTerritory, lingiari, denison, melbourne, banks, hunter, batman, spence, cook, bean, fenner, canberra, bass});
    }
}