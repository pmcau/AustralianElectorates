public class MapToGeoJson
{
    // Change as needed if not in local path
    const string ogr2ogrPath = @"C:\OSGeo4W64\bin";

    public static Task ConvertShape(string targetFile, string shpFile, int? percent = null)
    {
        //mapshaper C:\Code\AustralianElectorates\Data\ElectoratesByState\act.geojson -simplify dp 20% -o format=geojson C:\Code\AustralianElectorates\Data\ElectoratesByState\temp.json
        var arguments = new List<string>
        {
            "/C",
            "mapshaper",
            shpFile
        };
        if (percent != null)
        {
            arguments.Add("-simplify");
            arguments.Add($"{percent}%");
        }

        arguments.Add("-o");
        arguments.Add("format=geojson");
        arguments.Add(targetFile);
        return Run("cmd.exe", arguments.ToArray());
    }

    public static Task ConvertTab(string targetFile, string tabFile) =>
        Run("ogr2ogr", "-f", "GeoJSON", targetFile, tabFile);

    static async Task Run(string fileName, params string[] arguments)
    {
        var startInfo = new ProcessStartInfo(fileName)
        {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        startInfo.AppendArguments(arguments);

        EnvironmentHelpers.AppendToPath(ogr2ogrPath);
        using var process = Process.Start(startInfo)!;
        await process.WaitForExitAsync();
        if (process.ExitCode != 0)
        {
            var readToEnd = await process.StandardError.ReadToEndAsync();
            if (readToEnd.Contains("Error"))
            {
                throw new($"Failed to run: {arguments}. Output: {readToEnd}");
            }
        }
    }
}