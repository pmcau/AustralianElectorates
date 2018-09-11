using System;
using System.Diagnostics;

public class ShapeToGeoJson
{
    public static void Convert(string targetFile, string shpFile, int? percent = null)
    {
        //mapshaper C:\Code\AustralianElectorates\Data\ElectoratesByState\act.json -simplify dp 20% -o format=geojson C:\Code\AustralianElectorates\Data\ElectoratesByState\temp.json
        var arguments = $@"/C mapshaper ""{shpFile}"" ";
        if (percent != null)
        {
            arguments += $" -simplify {percent}%";
        }
        arguments += $@" -o format=geojson ""{targetFile}""";
        var startInfo = new ProcessStartInfo("cmd.exe", arguments)
        {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };
        using (var process = Process.Start(startInfo))
        {
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                var readToEnd = process.StandardError.ReadToEnd();
                if (readToEnd.Contains("Error"))
                {
                    throw new Exception($"Failed to run: mapshaper {arguments}. Output: {readToEnd}");
                }
            }
        }
    }
}