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
        public static MapCollection Current { get; } = new MapCollection("Current");
        public static MapCollection Future { get; } = new MapCollection("Future");

        public static void LoadAll()
        {
            Future.LoadAll();
            Current.LoadAll();
        }
    }
}