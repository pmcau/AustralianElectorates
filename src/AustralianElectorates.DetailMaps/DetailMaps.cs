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
            var assemblyDirectory = Path.GetDirectoryName(path);
            Directory = Path.Combine(assemblyDirectory, "ElectorateMaps");
        }

        public static string MapForElectorate(string name)
        {
            Guard.AgainstNullWhiteSpace(nameof(name),name);
            return MapForElectorate(DataLoader.FindElectorate(name));
        }

        public static string MapForElectorate(Electorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            return Path.Combine(Directory, $"{electorate.ShortName}.png");
        }

        public static readonly string Directory;
    }
}