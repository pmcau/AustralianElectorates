using System.Collections.Generic;
using System.IO;

namespace AustralianElectorates
{
    public static class LandscapeDetailMaps
    {
        static LandscapeDetailMaps()
        {
            var assemblyDirectory = AssemblyLocation.DirectoryFor(typeof(LandscapeDetailMaps));
            Directory = Path.Combine(assemblyDirectory, "ElectorateMaps");
        }

        public static string MapForElectorate(string name)
        {
            Guard.AgainstNullWhiteSpace(nameof(name),name);
            return MapForElectorate(DataLoader.FindElectorate(name));
        }

        public static string MapForElectorate(IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            return Path.Combine(Directory, $"{electorate.ShortName}_landscape.png");
        }

        public static IEnumerable<string> Files(IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            return System.IO.Directory.EnumerateFiles(Directory, "*_landscape.png");
        }

        public static readonly string Directory;
    }
}