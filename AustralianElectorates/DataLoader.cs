using System.Collections.Generic;

namespace AustralianElectorates
{
    public static class DataLoader
    {
        static DataLoader()
        {
            var assembly = typeof(DataLoader).Assembly;
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
    }
}