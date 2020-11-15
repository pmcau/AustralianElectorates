using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace AustralianElectorates
{
    public static partial class DataLoader
    {
        static object exportLocker = new object();
        static Assembly assembly;

        static DataLoader()
        {
            assembly = typeof(DataLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("electorates.json"))
            {
                Electorates = Serializer.Deserialize<List<Electorate>>(stream);
            }

            using (var stream = assembly.GetManifestResourceStream("parties.json"))
            {
                Parties = Serializer.Deserialize<List<Party>>(stream);
            }

            var partiesAndBranches = new List<IPartyOrBranch>();
            foreach (var party in Parties)
            {
                partiesAndBranches.Add(party);
                if (party.Branches != null)
                {
                    foreach (Branch branch in party.Branches)
                    {
                        partiesAndBranches.Add(branch);
                        branch.Party = party;
                    }
                }
            }

            PartiesAndBranches = partiesAndBranches;

            foreach (var electorate in Electorates)
            {
                var preferred = electorate.TwoCandidatePreferred;
                if (preferred != null)
                {
                    var elected = (Candidate) preferred.Elected;
                    elected.Party = PartiesAndBranches.SingleOrDefault(x => x.Id == elected.PartyId);
                    electorate.CurrentParty = elected.Party;

                    var other = (Candidate) preferred.Other;
                    other.Party = PartiesAndBranches.SingleOrDefault(x => x.Id == other.PartyId);
                }
            }

            InitNamed();
            Elections = BuildElections();

            foreach (var electorate in Electorates)
            {
                if (electorate.Locations == null)
                {
                    continue;
                }

                foreach (var location in electorate.Locations.Cast<Location>())
                {
                    location.Electorate = electorate;
                }
            }
        }

        public static IReadOnlyList<IElectorate> Electorates { get; }

        public static IReadOnlyList<IElection> Elections { get; }

        static List<Election> BuildElections()
        {
            //TODO: scrape from here instead, will need to change from the electorate.ExistNNNN pattern: https://www.aec.gov.au/Elections/Federal_Elections/

            #region elections

            return new List<Election>
            {
                new Election
                {
                    Parliament = 45,
                    Year = 2016,
                    Date = new DateTime(2016, 07, 02, 0, 0, 0),
                    Electorates = Electorates.Where(_ => _.Exist2016).ToList()
                },
                new Election
                {
                    Parliament = 46,
                    Year = 2019,
                    Date = new DateTime(2019, 05, 18, 0, 0, 0),
                    Electorates = Electorates.Where(_ => _.Exist2019).ToList()
                }
            };

            #endregion
        }

        public static IReadOnlyList<IParty> Parties { get; }
        public static IReadOnlyList<IPartyOrBranch> PartiesAndBranches { get; }
        public static MapCollection Maps2016 { get; } = new("2016");
        public static MapCollection Maps2019 { get; } = new("2019");
        public static MapCollection MapsFuture { get; } = new("Future");

        public static IElection FindElection(int parliament)
        {
            if (TryFindElection(parliament, out var election))
            {
                return election;
            }

            throw new ElectionNotFoundException(parliament);
        }

        public static bool TryFindElection(int parliament, [NotNullWhen(true)] out IElection? election)
        {
            election = Elections.SingleOrDefault(x => x.Parliament == parliament);
            if (election != null)
            {
                return true;
            }

            return false;
        }

        public static IElectorate FindElectorate(string name)
        {
            Guard.AgainstNullWhiteSpace(nameof(name), name);
            if (TryFindElectorate(name, out var electorate))
            {
                return electorate;
            }

            throw new ElectorateNotFoundException(name);
        }

        public static bool TryFindElectorate(string name, [NotNullWhen(true)] out IElectorate? electorate)
        {
            Guard.AgainstNullWhiteSpace(nameof(name), name);
            electorate = Electorates.SingleOrDefault(x => MatchName(name, x));
            if (electorate != null)
            {
                return true;
            }

            return false;
        }

        static bool MatchName(string name, IElectorate x)
        {
            return string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(x.ShortName, name, StringComparison.OrdinalIgnoreCase);
        }

        public static void ValidateElectorates(params string[] names)
        {
            ValidateElectorates((IEnumerable<string>) names);
        }

        public static void ValidateElectorates(IEnumerable<string> names)
        {
            var missing = FindInvalidateElectorates(names).ToList();
            if (missing.Any())
            {
                throw new ElectoratesNotFoundException(missing);
            }
        }

        public static bool TryFindInvalidateElectorates(IEnumerable<string> names, out List<string> invalid)
        {
            Guard.AgainstNull(names, nameof(names));
            invalid = FindInvalidateElectorates(names).ToList();
            return invalid.Any();
        }

        public static IEnumerable<string> FindInvalidateElectorates(params string[] names)
        {
            return FindInvalidateElectorates((IEnumerable<string>) names);
        }

        public static IEnumerable<string> FindInvalidateElectorates(IEnumerable<string> names)
        {
            Guard.AgainstNull(names, nameof(names));
            return names.Where(name => !Electorates.Any(x => MatchName(name, x)));
        }

        public static void LoadAll()
        {
            MapsFuture.LoadAll();
            Maps2016.LoadAll();
            Maps2019.LoadAll();
        }

        public static void Export(string directory)
        {
            Guard.AgainstNullWhiteSpace(nameof(directory), directory);
            lock (exportLocker)
            {
                ExportInLock(directory);
            }
        }

        static void ExportInLock(string directory)
        {
            WriteElectoratesJson(directory);

            using var stream = assembly.GetManifestResourceStream("Maps.zip");
            using var archive = new ZipArchive(stream);
            archive.ExtractToDirectory(directory);
        }

        static void WriteElectoratesJson(string directory)
        {
            var electoratesJsonPath = Path.Combine(directory, "electorates.json");
            if (File.Exists(electoratesJsonPath))
            {
                var existingCreationTime = File.GetCreationTimeUtc(electoratesJsonPath);
                if (AssemblyTimestamp.Value == existingCreationTime)
                {
                    return;
                }
            }

            WriteElectoratesJsonInner(electoratesJsonPath);

            File.SetCreationTimeUtc(electoratesJsonPath, AssemblyTimestamp.Value);
        }

        static void WriteElectoratesJsonInner(string electoratesJsonPath)
        {
            using var stream = assembly.GetManifestResourceStream("electorates.json");
            using var target = File.Create(electoratesJsonPath);
            stream.CopyTo(target);
        }

        public static IElectorateMap Get2016Map(this IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (!electorate.Exist2016)
            {
                throw new Exception($"Electorate '{electorate.Name}' does not have a 2016 map");
            }

            return Maps2016.GetElectorate(electorate.ShortName);
        }

        public static IElectorateMap GetMap(this IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (electorate.Exist2019)
            {
                return Maps2019.GetElectorate(electorate.ShortName);
            }

            if (electorate.Exist2016)
            {
                return Maps2016.GetElectorate(electorate.ShortName);
            }

            return MapsFuture.GetElectorate(electorate.ShortName);
        }

        public static IElectorateMap Get2019Map(this IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (!electorate.Exist2019)
            {
                throw new Exception($"Electorate '{electorate.Name}' does not have a 2019 map");
            }

            return Maps2019.GetElectorate(electorate.ShortName);
        }

        public static IElectorateMap GetFutureMap(this IElectorate electorate)
        {
            Guard.AgainstNull(electorate, nameof(electorate));
            if (!electorate.ExistInFuture)
            {
                throw new Exception($"Electorate '{electorate.Name}' does not have a future map");
            }

            return MapsFuture.GetElectorate(electorate.ShortName);
        }
    }
}