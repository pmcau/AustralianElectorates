using System.IO;
using System.Threading.Tasks;
using Xunit;

public class PdfToPngTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public Task ConvertSingle()
    {
        var fullPath = Path.GetFullPath("sample_electorate_map.pdf");
        return PdfToPng.Convert(fullPath);
    }
}