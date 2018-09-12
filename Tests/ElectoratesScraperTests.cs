using System.Threading.Tasks;
using AustralianElectorates;
using ObjectApproval;
using Xunit;

public class ElectoratesScraperTests
{
    [Fact]
    public async Task Run()
    {
        var bass = await ElectoratesScraper.ScrapeElectorate("bass", State.TAS);
        var fenner = await ElectoratesScraper.ScrapeElectorate("fenner", State.ACT);
        var hunter = await ElectoratesScraper.ScrapeElectorate("hunter", State.NSW);
        var spence = await ElectoratesScraper.ScrapeElectorate("spence", State.NSW);
        var cook = await ElectoratesScraper.ScrapeElectorate("cook", State.NSW);
        var canberra = await ElectoratesScraper.ScrapeElectorate("canberra", State.ACT);
        var bean = await ElectoratesScraper.ScrapeElectorate("bean", State.ACT);
        var batman = await ElectoratesScraper.ScrapeElectorate("batman", State.VIC);
        ObjectApprover.VerifyWithJson(new { hunter, batman, spence, cook, bean, fenner , canberra , bass });
    }
}