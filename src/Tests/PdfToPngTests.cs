using System.IO;
using Xunit;

public class PdfToPngTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public void ConvertSingle()
    {
        var fullPath = Path.GetFullPath("sample_electorate_map.pdf");
        PdfToPng.Convert(fullPath);
    }
}