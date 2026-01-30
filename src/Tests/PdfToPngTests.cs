public class PdfToPngTests
{
    [Fact(Explicit = true)]
    public Task ConvertSingle()
    {
        var fullPath = Path.GetFullPath("sample_electorate_map.pdf");
        return PdfToPng.Convert(fullPath);
    }
}
