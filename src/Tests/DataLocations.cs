using System.IO;

public static class DataLocations
{
    static DataLocations()
    {
        RootDir = GitRepoDirectoryFinder.FindForFilePath();
        TempPath = Path.GetFullPath(Path.Combine(RootDir, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(RootDir, "Data"));
        Directory.CreateDirectory(DataPath);
        MapsPath = Path.GetFullPath(Path.Combine(DataPath, "Maps"));
        Directory.CreateDirectory(MapsPath);
        MapsFuturePath = Path.GetFullPath(Path.Combine(MapsPath, "Future"));
        Directory.CreateDirectory(MapsFuturePath);
        Maps2016Path = Path.GetFullPath(Path.Combine(MapsPath, "2016"));
        Directory.CreateDirectory(Maps2016Path);
        MapsCuratedPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated"));
        Directory.CreateDirectory(MapsCuratedPath);
        AustralianElectoratesProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates.Bogus"));
        MapsCuratedZipPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated.zip"));
        FutureAustraliaJsonPath = Path.GetFullPath(Path.Combine(MapsPath, "Future/australia.geojson"));
    }

    public static string AustralianElectoratesProjectPath;
    public static string BogusProjectPath;
    public static string MapsPath;
    public static string Maps2016Path;
    public static string MapsFuturePath;
    public static string MapsCuratedPath;
    public static string MapsCuratedZipPath;
    public static string FutureAustraliaJsonPath;

    public static string RootDir;

    public static string DataPath;

    public static string TempPath;
}