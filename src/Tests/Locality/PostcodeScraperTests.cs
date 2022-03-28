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
        await Verify(data.Take(10));
    }

    [Fact]
    [Trait("Category", "Integration")]
    public Task Specific()
    {
        var place = AustraliaData.PostCodes.Single(x=>x.Key == "2612");
        return Verify(PostcodeScraper.GetAECDataForPostcode(place));
    }
}