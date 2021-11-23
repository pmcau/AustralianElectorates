public static class StatisticalAreaCleaner
{
    public static void DeleteStatisticalAreaFiles(string extract)
    {
        foreach (var saFile in Directory.EnumerateFiles(extract, "*_SA1s_*", SearchOption.AllDirectories))
        {
            File.Delete(saFile);
        }
    }
}