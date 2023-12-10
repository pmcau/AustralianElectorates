static class IoHelpers
{
    public static void PurgeDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            return;
        }

        var root = new DirectoryInfo(directory);

        foreach (var file in root.GetFiles("*.*", SearchOption.AllDirectories))
        {
            file.Delete();
        }
    }

    public static void PurgeDirectoryRecursive(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            return;
        }

        var root = new DirectoryInfo(directory);

        foreach (var file in root.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in root.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    public static string FindFile(string directory, string extension) =>
        Directory
            .EnumerateFiles(directory, $"*.{extension}", SearchOption.AllDirectories)
            .Single();
}