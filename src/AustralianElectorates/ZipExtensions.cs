using System.IO.Compression;

static class ZipExtensions
{
    public static void ExtractToDirectory(this ZipArchive archive, string directory)
    {
        foreach (var file in archive.Entries)
        {
            var completeFileName = Path.Combine(directory, file.FullName);
            if (File.Exists(completeFileName))
            {
                var existingCreationTime = File.GetCreationTimeUtc(completeFileName);
                if (AssemblyTimestamp.Value == existingCreationTime)
                {
                    continue;
                }
            }

            var fileDirectory = Path.GetDirectoryName(completeFileName)!;
            Directory.CreateDirectory(fileDirectory);

            if (file.Name.Length == 0)
            {
                continue;
            }

            file.ExtractToFile(completeFileName, true);
            File.SetCreationTimeUtc(completeFileName, AssemblyTimestamp.Value);
        }
    }

    public static string ReadString(this ZipArchiveEntry entry)
    {
        using var entryStream = entry.Open();
        using var reader = new StreamReader(entryStream);
        return reader.ReadToEnd();
    }
}