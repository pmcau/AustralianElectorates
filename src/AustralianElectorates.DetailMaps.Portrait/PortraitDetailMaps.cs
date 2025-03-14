﻿namespace AustralianElectorates;

public static class PortraitDetailMaps
{
    static PortraitDetailMaps() =>
        Directory = Path.Combine(AssemblyLocation.Directory, "ElectorateMaps");

    public static string MapForElectorate(string name)
    {
        Guard.AgainstWhiteSpace(nameof(name), name);
        return MapForElectorate(DataLoader.FindElectorate(name));
    }

    public static string MapForElectorate(IElectorate electorate) =>
        Path.Combine(Directory, $"{electorate.ShortName}.portrait.png");

    public static IEnumerable<string> Files(IElectorate electorate) =>
        System.IO.Directory.EnumerateFiles(Directory, "*.portrait.png");

    public static readonly string Directory;
}