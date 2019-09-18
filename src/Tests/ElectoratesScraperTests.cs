using System.Threading.Tasks;
using AustralianElectorates;
using Xunit;
using Xunit.Abstractions;

public class ElectoratesScraperTests :
    XunitApprovalBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var parties = await PartyScraper.Run();
        var banks = await ElectoratesScraper.ScrapeCurrentElectorate("banks", State.NSW, parties);
        var denison = await ElectoratesScraper.Scrape2016Electorate("denison", State.TAS, parties);
        var bass = await ElectoratesScraper.ScrapeCurrentElectorate("clark", State.TAS, parties);
        var fenner = await ElectoratesScraper.ScrapeCurrentElectorate("fenner", State.ACT, parties);
        var hunter = await ElectoratesScraper.ScrapeCurrentElectorate("hunter", State.NSW, parties);
        var spence = await ElectoratesScraper.ScrapeCurrentElectorate("spence", State.SA, parties);
        var cook = await ElectoratesScraper.ScrapeCurrentElectorate("cook", State.NSW, parties);
        var canberra = await ElectoratesScraper.ScrapeCurrentElectorate("canberra", State.ACT, parties);
        var bean = await ElectoratesScraper.ScrapeCurrentElectorate("bean", State.ACT, parties);
        var batman = await ElectoratesScraper.ScrapeCurrentElectorate("batman", State.VIC, parties);
        var melbourne = await ElectoratesScraper.ScrapeCurrentElectorate("melbourne", State.VIC, parties);
        ObjectApprover.Verify(new {denison, melbourne,banks, hunter, batman, spence, cook, bean, fenner, canberra, bass});
    }

    public ElectoratesScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}