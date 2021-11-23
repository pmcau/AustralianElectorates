namespace AustralianElectorates;

public static class PortraitDetailMaps
{
    static PortraitDetailMaps()
    {
        var assemblyDirectory = AssemblyLocation.DirectoryFor(typeof(PortraitDetailMaps));
        Directory = Path.Combine(assemblyDirectory, "ElectorateMaps");
    }

    public static string MapForElectorate(string name)
    {
        Guard.AgainstWhiteSpace(nameof(name),name);
        return MapForElectorate(DataLoader.FindElectorate(name));
    }

    public static string MapForElectorate(IElectorate electorate)
    {
        return Path.Combine(Directory, $"{electorate.ShortName}.portrait.png");
    }

    public static IEnumerable<string> Files(IElectorate electorate)
    {
        return System.IO.Directory.EnumerateFiles(Directory, "*.portrait.png");
    }

    public static readonly string Directory;
}