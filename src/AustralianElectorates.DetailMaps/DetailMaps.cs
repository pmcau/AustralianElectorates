﻿namespace AustralianElectorates;

public static class DetailMaps
{
    static DetailMaps() =>
        Directory = Path.Combine(AssemblyLocation.Directory, "ElectorateMaps");

    public static string MapForElectorate(string name)
    {
        Guard.AgainstWhiteSpace(nameof(name), name);
        return MapForElectorate(DataLoader.FindElectorate(name));
    }

    public static string MapForElectorate(IElectorate electorate) =>
        Path.Combine(Directory, $"{electorate.ShortName}.png");

    public static IEnumerable<string> Files(IElectorate electorate)
    {
        static bool Predicate(string x) =>
            !x.Contains(".landscape.") &&
            !x.Contains(".portrait.");

        return System
            .IO.Directory.EnumerateFiles(Directory)
            .Where(Predicate);
    }

    public static readonly string Directory;
}