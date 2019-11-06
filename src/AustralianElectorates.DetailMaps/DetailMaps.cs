using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AustralianElectorates
{
    public static class DetailMaps
    {
        static DetailMaps()
        {
            var assemblyDirectory = AssemblyLocation.DirectoryFor(typeof(DetailMaps));
            Directory = Path.Combine(assemblyDirectory, "ElectorateMaps");
        }

        public static string MapForElectorate(string name)
        {
            Guard.AgainstNullWhiteSpace(nameof(name), name);
            return MapForElectorate(DataLoader.FindElectorate(name));
        }

        public static string MapForElectorate(IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            return Path.Combine(Directory, $"{electorate.ShortName}.png");
        }

        public static IEnumerable<string> Files(IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));

            static bool Predicate(string x) =>
                !x.Contains(".landscape.") &&
                !x.Contains(".portrait.");

            return System.IO.Directory.EnumerateFiles(Directory)
                .Where(Predicate);
        }

        public static readonly string Directory;
    }
}