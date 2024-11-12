public static class DataLocations
{
    static DataLocations()
    {
        RootDir = GitRepoDirectoryFinder.FindForFilePath();
        TempPath = Path.GetFullPath(Path.Combine(RootDir, "temp"));
        Directory.CreateDirectory(TempPath);
        DataPath = Path.GetFullPath(Path.Combine(RootDir, "Data"));
        ElectoratesJsonPath = Path.Combine(DataPath, "electorates.json");

        PartiesJsonPath = Path.Combine(DataPath, "parties.json");
        PostcodeToElectorateJsonPath = Path.Combine(DataPath, "postcode_to_electorate.json");
        LgaToElectorateJsonPath = Path.Combine(DataPath, "lga_to_electorate.json");
        ElectorateToLgaJsonPath = Path.Combine(DataPath, "electorate_to_lga.json");
        LocalitiesPath = Path.GetFullPath(Path.Combine(DataPath, "Localities.json"));
        Directory.CreateDirectory(DataPath);
        MapsPath = Path.GetFullPath(Path.Combine(DataPath, "Maps"));
        Directory.CreateDirectory(MapsPath);
        //MapsFuturePath = Path.GetFullPath(Path.Combine(MapsPath, "Future"));
        //Directory.CreateDirectory(MapsFuturePath);
        MapsDetail = Path.GetFullPath(Path.Combine(DataPath, "DetailMaps"));
        Directory.CreateDirectory(MapsDetail);
        // Australia2016JsonPath = Path.GetFullPath(Path.Combine(Maps2016Path, "australia.geojson"));
        Maps2019Path = Path.GetFullPath(Path.Combine(MapsPath, "2019"));
        Directory.CreateDirectory(Maps2019Path);
        Maps2022Path = Path.GetFullPath(Path.Combine(MapsPath, "2022"));
        Directory.CreateDirectory(Maps2022Path);
        Maps2025Path = Path.GetFullPath(Path.Combine(MapsPath, "2025"));
        Directory.CreateDirectory(Maps2025Path);
        //Australia2019JsonPath = Path.GetFullPath(Path.Combine(Maps2019Path, "australia.geojson"));
        MapsCuratedPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated"));
        Directory.CreateDirectory(MapsCuratedPath);
        AustralianElectoratesProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates"));
        BogusProjectPath = Path.GetFullPath(Path.Combine(RootDir, "src/AustralianElectorates.Bogus"));
        MapsCuratedZipPath = Path.GetFullPath(Path.Combine(DataPath, "MapsCurated.zip"));
        //FutureAustraliaJsonPath = Path.GetFullPath(Path.Combine(MapsFuturePath, "australia.geojson"));
    }

    public static string PartiesJsonPath;
    public static string PostcodeToElectorateJsonPath;
    public static string LgaToElectorateJsonPath;
    public static string ElectorateToLgaJsonPath;

    public static string ElectoratesJsonPath;
    public static string LocalitiesPath;
    public static string AustralianElectoratesProjectPath;
    public static string BogusProjectPath;
    public static string MapsPath;
    public static string MapsDetail;
    public static string Maps2019Path;
    public static string Maps2022Path;
    public static string Maps2025Path;

    //public static string MapsFuturePath;
    public static string MapsCuratedPath;

    public static string MapsCuratedZipPath;

    //public static string FutureAustraliaJsonPath;
    public static string RootDir;
    public static string DataPath;
    public static string TempPath;
}