using Xunit;

public class PdfToPngTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public void ConvertSingle()
    {
        PdfToPng.Convert(@"sample_electorate_map.pdf");
    }
}