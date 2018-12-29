using System.IO;
using CaptureSnippets;

public static class DataLocations
{
    static DataLocations()
    {
        RootDir = GitRepoDirectoryFinder.Find();
        TempPath = Path.GetFullPath(Path.Combine(RootDir, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(RootDir, "Data"));
        Directory.CreateDirectory(DataPath);
        MapsPath = Path.GetFullPath(Path.Combine(DataPath, "Maps"));
        Directory.CreateDirectory(MapsPath);
        MapsFuturePath = Path.GetFullPath(Path.Combine(MapsPath, "Future"));
        Directory.CreateDirectory(MapsFuturePath);
        MapsCurrentPath = Path.GetFullPath(Path.Combine(MapsPath, "Current"));
        Directory.CreateDirectory(MapsCurrentPath);
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
    public static string MapsCurrentPath;
    public static string MapsFuturePath;
    public static string MapsCuratedPath;
    public static string MapsCuratedZipPath;
    public static string FutureAustraliaJsonPath;

    public static string RootDir;

    public static string DataPath;

    public static string TempPath;
}