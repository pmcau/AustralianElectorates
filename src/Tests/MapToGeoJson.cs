using System.Diagnostics;
using System.Threading.Tasks;

public class MapToGeoJson
{
    // Change as needed if not in local path
    const string ogr2ogrPath = @"C:\OSGeo4W64\bin";

    public static Task ConvertShape(string targetFile, string shpFile, int? percent = null)
    {
        //mapshaper C:\Code\AustralianElectorates\Data\ElectoratesByState\act.geojson -simplify dp 20% -o format=geojson C:\Code\AustralianElectorates\Data\ElectoratesByState\temp.json
        var arguments = $@"/C mapshaper ""{shpFile}"" ";
        if (percent != null)
        {
            arguments += $" -simplify {percent}%";
        }
        arguments += $@" -o format=geojson ""{targetFile}"" ";
        return Run("cmd.exe", arguments);
    }

    public static Task ConvertTab(string targetFile, string tabFile)
    {
        var arguments = $"-f GeoJSON {targetFile} {tabFile}";
        return Run("ogr2ogr", arguments);
    }

    static async Task Run(string fileName, string arguments)
    {
        ProcessStartInfo startInfo = new(fileName, arguments)
        {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        EnvironmentHelpers.AppendToPath(ogr2ogrPath);
        using var process = Process.Start(startInfo)!;
        await process.WaitForExitAsync();
        if (process.ExitCode != 0)
        {
            var readToEnd = process.StandardError.ReadToEnd();
            if (readToEnd.Contains("Error"))
            {
                throw new($"Failed to run: {arguments}. Output: {readToEnd}");
            }
        }
    }
}