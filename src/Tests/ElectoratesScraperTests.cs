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
        var banks = await ElectoratesScraper.ScrapeCurrentElectorate("banks", State.NSW);
        var denison = await ElectoratesScraper.Scrape2016Electorate("denison", State.TAS);
        var bass = await ElectoratesScraper.ScrapeCurrentElectorate("clark", State.TAS);
        var fenner = await ElectoratesScraper.ScrapeCurrentElectorate("fenner", State.ACT);
        var hunter = await ElectoratesScraper.ScrapeCurrentElectorate("hunter", State.NSW);
        var spence = await ElectoratesScraper.ScrapeCurrentElectorate("spence", State.SA);
        var cook = await ElectoratesScraper.ScrapeCurrentElectorate("cook", State.NSW);
        var lingiari = await ElectoratesScraper.ScrapeCurrentElectorate("lingiari", State.NT);
        var canberra = await ElectoratesScraper.ScrapeCurrentElectorate("canberra", State.ACT);
        var bean = await ElectoratesScraper.ScrapeCurrentElectorate("bean", State.ACT);
        var batman = await ElectoratesScraper.ScrapeCurrentElectorate("batman", State.VIC);
        var melbourne = await ElectoratesScraper.ScrapeCurrentElectorate("melbourne", State.VIC);
        ObjectApprover.Verify(new {denison, melbourne,banks, hunter, batman, spence, cook, bean, fenner, canberra, lingiari, bass});
    }

    public ElectoratesScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}