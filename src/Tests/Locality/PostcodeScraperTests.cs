public class PostcodeScraperTests(ITestOutputHelper outputHelper)
{
    [Fact(Explicit = true)]
    public async Task Run()
    {
        var data = await PostcodeScraper.Run(outputHelper);
        File.Delete(DataLocations.LocalitiesPath);
        JsonSerializerService.Serialize(data, DataLocations.LocalitiesPath);
        await Verify(data.Take(10));
    }

    [Fact(Explicit = true)]
    public Task Specific() =>
        Verify(PostcodeScraper.GetAECDataForPostcode("6062"));
}
