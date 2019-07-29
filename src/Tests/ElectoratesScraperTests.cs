using System.Collections.Generic;
using System.Threading.Tasks;
using AustralianElectorates;
using ObjectApproval;
using Xunit;
using Xunit.Abstractions;

public class ElectoratesScraperTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var denison =await ElectoratesScraper.Scrape2016Electorate("denison", State.TAS);
        var bass = await ElectoratesScraper.ScrapeCurrentElectorate("clark", State.TAS, new List<Elected>());
        var fenner = await ElectoratesScraper.ScrapeCurrentElectorate("fenner", State.ACT, new List<Elected>());
        var hunter = await ElectoratesScraper.ScrapeCurrentElectorate("hunter", State.NSW, new List<Elected>());
        var spence = await ElectoratesScraper.ScrapeCurrentElectorate("spence", State.SA, new List<Elected>());
        var cook = await ElectoratesScraper.ScrapeCurrentElectorate("cook", State.NSW, new List<Elected>());
        var canberra = await ElectoratesScraper.ScrapeCurrentElectorate("canberra", State.ACT, new List<Elected>());
        var bean = await ElectoratesScraper.ScrapeCurrentElectorate("bean", State.ACT, new List<Elected>());
        var batman = await ElectoratesScraper.ScrapeCurrentElectorate("batman", State.VIC, new List<Elected>());
        var melbourne = await ElectoratesScraper.ScrapeCurrentElectorate("melbourne", State.VIC, new List<Elected>());
        ObjectApprover.VerifyWithJson(new {denison, melbourne, hunter, batman, spence, cook, bean, fenner , canberra , bass });
    }

    public ElectoratesScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}