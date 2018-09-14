using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

public class Export
{
    private int size = 200 * 1024;

    [Fact]
    [Trait("Category", "Integration")]
    public void ExportElectorates()
    {
        var target = @"C:\Code\ProgramsWebApi\src\Api\Maps";

        foreach (var sourceYear in Directory.EnumerateDirectories(DataLocations.MapsPath))
        {
            var targetYear = Path.Combine(target, Path.GetFileName(sourceYear));
            foreach (var fileInfo in FileInfos(sourceYear))
            {
                var destFileName = Path.Combine(targetYear, "Electorates", $"{ElectorateName(fileInfo.FullName)}.geojson");
                fileInfo.CopyTo(destFileName);
            }
        }
    }

    IEnumerable<FileInfo> FileInfos(string directory)
    {
        foreach (var state in Directory.EnumerateDirectories(directory))
        {
            foreach (var group in Directory.EnumerateFiles(state).GroupBy(ElectorateName))
            {
                var fileInfos = @group.Select(x => new FileInfo(x)).OrderByDescending(x => x.Length).ToList();
                var firstOrDefault = fileInfos.SkipWhile(x => x.Length > size).FirstOrDefault();
                if (firstOrDefault == null)
                {
                    yield return fileInfos.First();
                }
                else
                {
                    yield return firstOrDefault;
                }
            }
        }
    }

    static string ElectorateName(string x)
    {
        return Path.GetFileNameWithoutExtension(x).Split('_')[0];
    }
}