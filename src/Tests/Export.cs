public class Export
{
    public static void ExportElectorates()
    {
        IoHelpers.PurgeDirectoryRecursive(DataLocations.MapsCuratedPath);
        foreach (var sourceYear in Directory.EnumerateDirectories(DataLocations.MapsPath))
        {
            var targetYear = Path.Combine(DataLocations.MapsCuratedPath, Path.GetFileName(sourceYear));
            Directory.CreateDirectory(targetYear);
            CopyGeoJson(sourceYear, targetYear);

            var sourceElectorates = Path.Combine(sourceYear, "Electorates");
            var targetElectorates = Path.Combine(targetYear, "Electorates");
            Directory.CreateDirectory(targetElectorates);
            CopyGeoJson(sourceElectorates, targetElectorates);
        }
    }

    static void CopyGeoJson(string source, string target)
    {
        foreach (var file in Directory.EnumerateFiles(source, "*.geojson"))
        {
            File.Copy(file, Path.Combine(target, Path.GetFileName(file)), true);
        }
    }
}
