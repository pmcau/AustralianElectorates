using System.Diagnostics;
using System.IO;

public static class PdfToPng
{
    public static void Convert(string pdf)
    {
        var tempPng = pdf.Replace(".pdf", "_temp.png");
        var png = Path.ChangeExtension(pdf, "png");
        File.Delete(png);
        File.Delete(tempPng);
        CallGhostScript(pdf, tempPng);
        CallPngquant(tempPng, png);
    }

    static void CallPngquant(string tempPng, string png)
    {
        var pngquant = new ProcessStartInfo
        {
            FileName = "pngquant.exe",
            Arguments = $"--force --verbose --ordered --speed=1 --skip-if-larger --quality=50-70 {tempPng} --output {png}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        using var process = Process.Start(pngquant);
        process.WaitForExit();
        //skip-if-larger can result in 98 "not saved"
        if (process.ExitCode == 98)
        {
            File.Move(tempPng, png);
        }
        else
        {
            File.Delete(tempPng);
        }
    }

    static void CallGhostScript(string pdf, string tempPng)
    {
        var gswin64 = new ProcessStartInfo
        {
            FileName = "gswin64c.exe",
            Arguments = $"-dNoCancel -sDEVICE=png16m -dBATCH -r300 -dNOPAUSE -q -sOutputFile={tempPng} {pdf}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        using var process = Process.Start(gswin64);
        process.WaitForExit();
    }
}