using System.Text.Json;

public class MetadataCleanerTests
{
    [Fact]
    public void Every_electorateName_in_committed_geojsons_matches_a_canonical_Name()
    {
        var canonicalNames = DataLoader
            .Electorates.Select(_ => _.Name)
            .ToHashSet(StringComparer.Ordinal);

        var violations = new List<string>();
        foreach (var root in new[] { DataLocations.MapsPath, DataLocations.MapsCuratedPath })
        {
            foreach (var file in Directory.EnumerateFiles(root, "*.geojson", SearchOption.AllDirectories))
            {
                foreach (var name in ReadElectorateNames(file))
                {
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

    static List<string> ReadElectorateNames(string path)
    {
        var bytes = File.ReadAllBytes(path);
        var reader = new Utf8JsonReader(bytes);
        var names = new List<string>();

        while (reader.Read())
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }

            if (reader.ValueTextEquals("geometry"))
            {
                reader.Read();
                reader.Skip();
                continue;
            }

            if (reader.ValueTextEquals("electorateName"))
            {
                reader.Read();
                names.Add(reader.GetString()!);
            }
        }

        return names;
    }
}
