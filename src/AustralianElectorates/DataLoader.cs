using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace AustralianElectorates
{
    public static partial class DataLoader
    {
        static Assembly assembly;

        static DataLoader()
        {
            assembly = typeof(DataLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            {
                Electorates = Serializer.Deserialize<List<Electorate>>(stream);
            }

            foreach (var electorate in Electorates)
            {
                foreach (var member in electorate.Members)
                {
                    member.Electorate = electorate;
                }
            }

            AllMembers = Electorates
                .SelectMany(x => x.Members)
                .ToList();
            AllCurrentMembers = Electorates
                .Where(x => x.Members.Any())
                .Select(x => x.Members.First())
                .ToList();
            InitNamed();
        }

        public static IReadOnlyList<Member> AllMembers { get; }
        public static IReadOnlyList<Member> AllCurrentMembers { get; }

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

        public static ElectorateMap GetCurrentMap(this Electorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (!electorate.ExistInCurrent)
            {
                throw new Exception($"Electorate '{electorate.Name}' does not have a current map");
            }

            return CurrentMaps.GetElectorate(electorate.ShortName);
        }

        public static ElectorateMap GetFutureMap(this Electorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (!electorate.ExistInFuture)
            {
                throw new Exception($"Electorate '{electorate.Name}' does not have a future map");
            }

            return CurrentMaps.GetElectorate(electorate.ShortName);
        }
    }
}