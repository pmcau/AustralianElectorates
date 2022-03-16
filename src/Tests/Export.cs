public class Export
{
    static int electoratesSize = 200 * 1024;
    static int stateSize = 500 * 1024;

    public static void ExportElectorates()
    {
        IoHelpers.PurgeDirectoryRecursive(DataLocations.MapsCuratedPath);
        foreach (var sourceYear in Directory.EnumerateDirectories(DataLocations.MapsPath))
        {
            var targetYear = Path.Combine(DataLocations.MapsCuratedPath, Path.GetFileName(sourceYear));
            Directory.CreateDirectory(targetYear);
            foreach (var fileInfo in FileInfos(sourceYear, stateSize))
            {
                var destFileName = Path.Combine(targetYear, $"{Prefix(fileInfo.FullName)}.geojson");
                fileInfo.CopyTo(destFileName, true);
            }

            var sourceElectorates = Path.Combine(sourceYear, "Electorates");
            var targetElectorates = Path.Combine(targetYear, "Electorates");
            Directory.CreateDirectory(targetElectorates);
            foreach (var fileInfo in FileInfos(sourceElectorates, electoratesSize))
            {
                var destFileName = Path.Combine(targetElectorates, $"{Prefix(fileInfo.FullName)}.geojson");
                fileInfo.CopyTo(destFileName, true);
            }
        }
    }

    static IEnumerable<FileInfo> FileInfos(string directory, int electoratesSize)
    {
        foreach (var group in Directory.EnumerateFiles(directory).GroupBy(Prefix))
        {
            var fileInfos = @group.Select(x => new FileInfo(x)).OrderByDescending(x => x.Length).ToList();
            var firstOrDefault = fileInfos.SkipWhile(x => x.Length > electoratesSize).FirstOrDefault();
            if (firstOrDefault == null)
            {
                yield return fileInfos.Last();
            }
            else
            {
                yield return firstOrDefault;
            }
        }
    }

    static string Prefix(string x) =>
        Path.GetFileNameWithoutExtension(x).Split('_')[0];
}