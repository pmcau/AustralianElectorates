using VerifyXunit;
using Xunit;

[UsesVerify]
public class PostcodeScraperTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var data = await PostcodeScraper.Run();
        File.Delete(DataLocations.LocalitiesPath);
        JsonSerializerService.Serialize(data, DataLocations.LocalitiesPath);
        await Verifier.Verify(data.Take(10));
    }

    [Fact]
    public Task Specific()
    {
        var place = AustraliaData.PostCodes.Single(x=>x.Key == "2612");
        return Verifier.Verify(PostcodeScraper.GetAECDataForPostcode(place));
    }
}