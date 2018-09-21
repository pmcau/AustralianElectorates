using System.IO;
using System.IO.Compression;

static class ZipExtensions
{
    public static void ExtractToDirectory(this ZipArchive archive, string directory, bool overwrite)
    {
        if (!overwrite)
        {
            archive.ExtractToDirectory(directory);
            return;
        }

        foreach (var file in archive.Entries)
        {
            var completeFileName = Path.Combine(directory, file.FullName);
            var fileDirectory = Path.GetDirectoryName(completeFileName);
            Directory.CreateDirectory(fileDirectory);

            if (file.Name == "")
            {
                continue;
            }
            file.ExtractToFile(completeFileName, true);
        }
    }
}