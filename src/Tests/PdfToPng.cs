using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

public static class PdfToPng
{
    public static async Task<string> Convert(string pdf)
    {
        var tempPng1 = pdf.Replace(".pdf", "_temp1.png");
        var tempPng2 = pdf.Replace(".pdf", "_temp2.png");
        var png = Path.ChangeExtension(pdf, "png");
        File.Delete(png);
        File.Delete(tempPng1);
        File.Delete(tempPng2);
        await CallGhostScript(pdf, tempPng1);

        var cropSize = 60;
        using (var image = await Image.LoadAsync<Rgba32>(tempPng1))
        {
            var cropRect = new Rectangle(cropSize, cropSize, image.Width - 2 * cropSize, image.Height - 2 * cropSize);
            image.Mutate(_ => _.Crop(cropRect));
            DrawBorder(image);
            await image.SaveAsync(tempPng2);
        }

        File.Delete(tempPng1);
        await Quantize(tempPng2, png);
        return png;
    }

    static void DrawBorder(Image<Rgba32> image)
    {
        const int thickness = 3;
        var black = new Rgba32(0, 0, 0);
        image.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var row = accessor.GetRowSpan(y);
                if (y < thickness ||
                    y >= accessor.Height - thickness)
                {
                    row.Fill(black);
                }
                else
                {
                    for (var x = 0; x < thickness; x++)
                    {
                        row[x] = black;
                        row[accessor.Width - 1 - x] = black;
                    }
                }
            }
        });
    }

    static async Task Quantize(string source, string png)
    {
        using (var image = await Image.LoadAsync(source))
        await using (var stream = File.Create(png))
        {
            var encoder = new PngEncoder
            {
                ColorType = PngColorType.Palette,
                BitDepth = PngBitDepth.Bit8,
                Quantizer = new WuQuantizer()
            };
            await image.SaveAsync(stream, encoder);
        }

        // keep the original if quantization did not shrink it
        if (new FileInfo(png).Length < new FileInfo(source).Length)
        {
            File.Delete(source);
        }
        else
        {
            File.Delete(png);
            File.Move(source, png);
        }
    }

    static async Task CallGhostScript(string pdf, string tempPng)
    {
        var gswin64 = new ProcessStartInfo
        {
            FileName = "gswin64c.exe",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        gswin64.AppendArguments("-dNoCancel", "-sDEVICE=png16m", "-dBATCH", "-r300", "-dNOPAUSE", "-dDownScaleFactor=2", "-q", $"-sOutputFile={tempPng}", pdf);
        using var process = Process.Start(gswin64)!;
        await process.WaitForExitAsync();
        if (process.ExitCode != 0)
        {
            throw new(
                $"""
                 Failed to execute ghostscript.
                 gswin64c {string.Join(" ", gswin64.ArgumentList)}
                 """);
        }
    }
}