using System.IO;

namespace AustralianElectorates
{
    public static class DetailMaps
    {
        static DetailMaps()
        {
            var assembly = typeof(DetailMaps).Assembly;

            var path = assembly.CodeBase
                .Replace("file:///", "")
                .Replace("file://", "")
                .Replace(@"file:\\\", "")
                .Replace(@"file:\\", "");

            Directory = Path.Combine(path, "ElectorateMaps");
        }

        public static string Directory;
    }
}