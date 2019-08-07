using System.IO;
using Xunit;
using Xunit.Abstractions;

public class PdfToPngTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public void Convert()
    {
        foreach (var file in Directory.EnumerateFiles(DataLocations.MapsDetail,"*.pdf"))
        {
            PdfToPng.Convert(file);
        }
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void ConvertSingle()
    {
        PdfToPng.Convert(Path.Combine(DataLocations.MapsDetail, "sydney.pdf"));
    }

    public PdfToPngTests(ITestOutputHelper output) :
        base(output)
    {
    }
}