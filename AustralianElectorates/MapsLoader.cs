using System.Collections.Generic;

namespace AustralianElectorates
{
    public static class MapsLoader
    {
        static MapsLoader()
        {
            var assembly = typeof(MapsLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            {
                Electorates = Serializer.Deserialize<List<Electorate>>(stream);
            }
        }

        public static IReadOnlyList<Electorate> Electorates { get; }
        public static MapCollection Maps2016 { get; } = new MapCollection("2016");
        public static MapCollection MapsFuture { get; } = new MapCollection("Future");

        public static void LoadAll()
        {
            MapsFuture.LoadAll();
            Maps2016.LoadAll();
        }
    }
}