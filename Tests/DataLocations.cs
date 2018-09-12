using System;
using System.IO;

public static class DataLocations
{
    static DataLocations()
    {
        var currentDirectory = Environment.CurrentDirectory;
        SlnPath = Path.GetFullPath(Path.Combine(currentDirectory, "../../../../"));
        TempPath = Path.GetFullPath(Path.Combine(SlnPath, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(SlnPath, "Data"));
        ElectoratesPath = Path.GetFullPath(Path.Combine(DataPath, "ElectoratesMetadata"));
        MapsPath = Path.GetFullPath(Path.Combine(DataPath, "Maps"));
        FutureAustraliaJsonPath = Path.GetFullPath(Path.Combine(MapsPath, "Future/australia.geojson"));
        AustralianElectoratesProjectPath = Path.GetFullPath(Path.Combine(SlnPath, "AustralianElectorates"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(SlnPath, "AustralianElectorates.Bogus"));
    }

    public static string MapsPath;
    public static string FutureAustraliaJsonPath;
    public static string AustralianElectoratesProjectPath;

    public static string BogusProjectPath;

    public static string SlnPath;

    public static string DataPath;
    public static string ElectoratesPath;

    public static string TempPath;
}