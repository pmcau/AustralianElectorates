using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace AustralianElectorates
{
    public static class DataLoader
    {
        static Assembly assembly;

        static DataLoader()
        {
            assembly = typeof(DataLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            {
                Electorates = Serializer.Deserialize<List<Electorate>>(stream);
            }
        }

        public static IReadOnlyList<Electorate> Electorates { get; }
        public static MapCollection CurrentMaps { get; } = new MapCollection("Current");
        public static MapCollection FutureMaps { get; } = new MapCollection("Future");

        public static void LoadAll()
        {
            FutureMaps.LoadAll();
            CurrentMaps.LoadAll();
        }

        public static void Export(string directory, bool overwrite = false)
        {
            Guard.AgainstNull(directory, nameof(directory));
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            using (var target = File.Create(Path.Combine(directory, "electorates.json")))
            {
                stream.CopyTo(target);
            }

            using (var stream = assembly.GetManifestResourceStream("Maps.zip"))
            using (var archive = new ZipArchive(stream))
            {
                archive.ExtractToDirectory(directory, overwrite);
            }
        }
    }
}