using System.Text.RegularExpressions;

public class MetadataCleanerTests
{
    // Locks in the invariant MetadataCleaner now enforces: every electorateName
    // string written into a committed *.geojson must match a canonical
    // IElectorate.Name from electorates.json. This catches case drift like the
    // historical "Mcmahon" / "Mcewen" / "Mcpherson" leak even if a future
    // regeneration skips the generator or someone hand-edits a geojson.
    [Fact]
    public void Every_electorateName_in_committed_geojsons_matches_a_canonical_Name()
    {
        var canonicalNames = DataLoader
            .Electorates.Select(_ => _.Name)
            .ToHashSet(StringComparer.Ordinal);

        var pattern = new Regex("\"electorateName\":\"(?<name>[^\"]+)\"", RegexOptions.Compiled);

        var violations = new List<string>();
        foreach (var root in new[] { DataLocations.MapsPath, DataLocations.MapsCuratedPath })
        {
            foreach (var file in Directory.EnumerateFiles(root, "*.geojson", SearchOption.AllDirectories))
            {
                var text = File.ReadAllText(file);
                foreach (Match match in pattern.Matches(text))
                {
                    var name = match.Groups["name"].Value;
                    if (!canonicalNames.Contains(name))
                    {
                        violations.Add($"{Path.GetRelativePath(DataLocations.RootDir, file)}: \"{name}\"");
                    }
                }
            }
        }

        if (violations.Count != 0)
        {
            var distinct = violations
                .Distinct(StringComparer.Ordinal)
                .OrderBy(_ => _, StringComparer.Ordinal)
                .ToList();
            throw new(
                $"Found {violations.Count} electorateName value(s) ({distinct.Count} distinct) " +
                "that do not match any canonical IElectorate.Name. The geojson and electorates.json " +
                "have drifted; rerun the data sync or fix MetadataCleaner. Examples:" +
                Environment.NewLine +
                string.Join(Environment.NewLine, distinct.Take(20)));
        }
    }
}
