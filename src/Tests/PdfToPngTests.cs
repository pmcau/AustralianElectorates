using Xunit;
using Xunit.Abstractions;

public class PdfToPngTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public void ConvertSingle()
    {
        PdfToPng.Convert(@"sample_electorate_map.pdf");
    }

    public PdfToPngTests(ITestOutputHelper output) :
        base(output)
    {
    }
}