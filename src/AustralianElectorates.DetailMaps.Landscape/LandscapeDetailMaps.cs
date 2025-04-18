﻿namespace AustralianElectorates;

public static class LandscapeDetailMaps
{
    static LandscapeDetailMaps() =>
        Directory = Path.Combine(AssemblyLocation.Directory, "ElectorateMaps");

    public static string MapForElectorate(string name)
    {
        Guard.AgainstWhiteSpace(nameof(name), name);
        return MapForElectorate(DataLoader.FindElectorate(name));
    }

    public static string MapForElectorate(IElectorate electorate) =>
        Path.Combine(Directory, $"{electorate.ShortName}_landscape.png");

    public static IEnumerable<string> Files(IElectorate electorate) =>
        System.IO.Directory.EnumerateFiles(Directory, "*_landscape.png");

    public static readonly string Directory;
}