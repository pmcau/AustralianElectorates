using System.IO.Compression;

static class Zipper
{
    public static void ZipDir(string targetFile, string sourceDirectory)
    {
        File.Delete(targetFile);
        using var zipStream = File.Create(targetFile);
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Create);
        foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*.*", SearchOption.AllDirectories))
        {
            var entryName = file.Replace(sourceDirectory, "").Trim('\\');
            var entry = archive.CreateEntry(entryName);
            //To stop the zip changing from the perspective of git
            entry.LastWriteTime = new(2000, 1, 1, 1, 1, 1, TimeSpan.Zero);

            using var fileStream = File.OpenRead(file);
            using var entryStream = entry.Open();
            fileStream.CopyTo(entryStream);
        }
    }
}