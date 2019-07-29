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
        var denison =await ElectoratesScraper.ScrapeElectorate("denison", State.TAS, new List<Elected>());
        var bass = await ElectoratesScraper.ScrapeElectorate("bass", State.TAS, new List<Elected>());
        var fenner = await ElectoratesScraper.ScrapeElectorate("fenner", State.ACT, new List<Elected>());
        var hunter = await ElectoratesScraper.ScrapeElectorate("hunter", State.NSW, new List<Elected>());
        var spence = await ElectoratesScraper.ScrapeElectorate("spence", State.SA, new List<Elected>());
        var cook = await ElectoratesScraper.ScrapeElectorate("cook", State.NSW, new List<Elected>());
        var canberra = await ElectoratesScraper.ScrapeElectorate("canberra", State.ACT, new List<Elected>());
        var bean = await ElectoratesScraper.ScrapeElectorate("bean", State.ACT, new List<Elected>());
        var batman = await ElectoratesScraper.ScrapeElectorate("batman", State.VIC, new List<Elected>());
        var melbourne = await ElectoratesScraper.ScrapeElectorate("melbourne", State.VIC, new List<Elected>());
        ObjectApprover.VerifyWithJson(new {denison, melbourne, hunter, batman, spence, cook, bean, fenner , canberra , bass });
    }

    public ElectoratesScraperTests(ITestOutputHelper output) :
        base(output)
    {
    }
}