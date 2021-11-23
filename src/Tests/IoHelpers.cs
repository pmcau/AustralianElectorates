﻿static class IoHelpers
{
    public static void PurgeDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            return;
        }

        DirectoryInfo root = new(directory);

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

        DirectoryInfo root = new(directory);

        foreach (var file in root.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in root.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    public static string FindFile(string directory, string extension)
    {
        return Directory.EnumerateFiles(directory, $"*.{extension}", SearchOption.AllDirectories).Single();
    }
}