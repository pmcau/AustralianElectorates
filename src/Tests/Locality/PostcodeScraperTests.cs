using Xunit.Abstractions;

public class PostcodeScraperTests(ITestOutputHelper outputHelper)
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Run()
    {
        var data = await PostcodeScraper.Run(outputHelper);
        File.Delete(DataLocations.LocalitiesPath);
        JsonSerializerService.Serialize(data, DataLocations.LocalitiesPath);
        await Verify(data.Take(10));
    }

    [Fact]
    [Trait("Category", "Integration")]
    public Task Specific() =>
        Verify(PostcodeScraper.GetAECDataForPostcode("3429"));
}