using System.IO;
using Xunit;
using Xunit.Abstractions;

public class PdfCompressTests :
    XunitLoggingBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public void CompressPdf()
    {
        foreach (var file in Directory.EnumerateFiles(DataLocations.MapsPdfPath))
        {
            PdfCompress.CompressPdf(file);
        }
    }

    public PdfCompressTests(ITestOutputHelper output) :
        base(output)
    {
    }
}