public class PdfToPngTests
{
    [Fact(Explicit = true)]
    public Task ConvertSingle()
    {
        var fullPath = ProjectFiles.sample_electorate_map_pdf.FullPath;
        return PdfToPng.Convert(fullPath);
    }
}
