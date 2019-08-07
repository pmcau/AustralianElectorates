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
        MapsPdfPath = Path.GetFullPath(Path.Combine(DataPath, "PdfMaps"));
        Directory.CreateDirectory(MapsPdfPath);
        Maps2016Path = Path.GetFullPath(Path.Combine(MapsPath, "2016"));
        Directory.CreateDirectory(Maps2016Path);
        Australia2016JsonPath = Path.GetFullPath(Path.Combine(Maps2016Path, "australia.geojson"));
        Maps2019Path = Path.GetFullPath(Path.Combine(MapsPath, "2019"));
        Directory.CreateDirectory(Maps2019Path);
        Australia2019JsonPath = Path.GetFullPath(Path.Combine(Maps2019Path, "australia.geojson"));
        MapsCuratedPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated"));
        Directory.CreateDirectory(MapsCuratedPath);
        AustralianElectoratesProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates.Bogus"));
        MapsCuratedZipPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated.zip"));
        FutureAustraliaJsonPath = Path.GetFullPath(Path.Combine(MapsFuturePath, "australia.geojson"));
    }

    public static string AustralianElectoratesProjectPath;
    public static string BogusProjectPath;
    public static string MapsPath;
    public static string MapsPdfPath;
    public static string Maps2016Path;
    public static string Maps2019Path;
    public static string MapsFuturePath;
    public static string MapsCuratedPath;
    public static string MapsCuratedZipPath;
    public static string FutureAustraliaJsonPath;
    public static string Australia2016JsonPath;
    public static string Australia2019JsonPath;
    public static string RootDir;
    public static string DataPath;
    public static string TempPath;
}