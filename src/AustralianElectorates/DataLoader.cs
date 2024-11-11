using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace AustralianElectorates;

public static partial class DataLoader
{
    static Assembly assembly;

    static DataLoader()
    {
        assembly = typeof(DataLoader).Assembly;
        using (var stream = assembly.GetManifestResourceStream("electorates.json")!)
        {
            Electorates = Serializer.Deserialize<List<Electorate>>(stream);
        }

        using (var stream = assembly.GetManifestResourceStream("parties.json")!)
        {
            Parties = Serializer.Deserialize<List<Party>>(stream);
        }

        var partiesAndBranches = new List<IPartyOrBranch>();
        foreach (var party in Parties)
        {
            partiesAndBranches.Add(party);
            //if (party.Branches != null)
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
                elected.Party = PartiesAndBranches.SingleOrDefault(_ => _.Id == elected.PartyId);
                electorate.CurrentParty = elected.Party;

                var other = (Candidate) preferred.Other;
                other.Party = PartiesAndBranches.SingleOrDefault(_ => _.Id == other.PartyId);
            }
        }

        InitNamed();
        Elections = BuildElections();

        foreach (var electorate in Electorates)
        {
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

        return
        [
            new()
            {
                Parliament = 45,
                Year = 2016,
                Date = new(2016, 07, 02),
                Electorates = Electorates
                    .Where(_ => _.Exist2016)
                    .ToList()
            },

            new()
            {
                Parliament = 46,
                Year = 2019,
                Date = new(2019, 05, 18),
                Electorates = Electorates
                    .Where(_ => _.Exist2019)
                    .ToList()
            }
        ];

        #endregion
    }

    public static IReadOnlyList<IParty> Parties { get; }
    public static IReadOnlyList<IPartyOrBranch> PartiesAndBranches { get; }
    public static MapCollection Maps2016 { get; } = new("2016");
    public static MapCollection Maps2019 { get; } = new("2019");
    public static MapCollection Maps2022 { get; } = new("2022");
    public static MapCollection Maps2025 { get; } = new("2025");
    //public static MapCollection MapsFuture { get; } = new("Future");

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
        election = Elections.SingleOrDefault(_ => _.Parliament == parliament);
        if (election != null)
        {
            return true;
        }

        return false;
    }

    public static IElectorate FindElectorate(string name)
    {
        Guard.AgainstWhiteSpace(nameof(name), name);
        if (TryFindElectorate(name, out var electorate))
        {
            return electorate;
        }

        throw new ElectorateNotFoundException(name);
    }

    public static bool TryFindElectorate(string name, [NotNullWhen(true)] out IElectorate? electorate)
    {
        Guard.AgainstWhiteSpace(nameof(name), name);
        electorate = Electorates.SingleOrDefault(_ => MatchName(name, _));
        if (electorate != null)
        {
            return true;
        }

        return false;
    }

    static bool MatchName(string name, IElectorate x) =>
        string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(x.ShortName, name, StringComparison.OrdinalIgnoreCase);

    public static void ValidateElectorates(params string[] names) =>
        ValidateElectorates((IEnumerable<string>) names);

    public static void ValidateElectorates(IEnumerable<string> names)
    {
        var missing = FindInvalidateElectorates(names)
            .ToList();
        if (missing.Count != 0)
        {
            throw new ElectoratesNotFoundException(missing);
        }
    }

    public static IEnumerable<IElectorate> ElectoratesForPostcode(int postcode)
    {
        foreach (var electorate in Electorates)
        {
            if (electorate.ContainsPostcode(postcode))
            {
                yield return electorate;
            }
        }
    }

    public static bool TryFindInvalidateElectorates(IEnumerable<string> names, out List<string> invalid)
    {
        invalid = FindInvalidateElectorates(names)
            .ToList();
        return invalid.Count != 0;
    }

    public static IEnumerable<string> FindInvalidateElectorates(params string[] names) =>
        FindInvalidateElectorates((IEnumerable<string>) names);

    public static IEnumerable<string> FindInvalidateElectorates(IEnumerable<string> names) =>
        names.Where(name => !Electorates.Any(_ => MatchName(name, _)));

    public static void LoadAll()
    {
        //MapsFuture.LoadAll();
        Maps2016.LoadAll();
        Maps2019.LoadAll();
        Maps2022.LoadAll();
        Maps2025.LoadAll();
    }

    public static async Task Export(string directory)
    {
        Guard.AgainstWhiteSpace(nameof(directory), directory);
        await WriteElectoratesJson(directory);

        using var stream = assembly.GetManifestResourceStream("Maps.zip")!;
        using var archive = new ZipArchive(stream);
        archive.ExtractToDirectory(directory);
    }

    static async Task WriteElectoratesJson(string directory)
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

        await WriteElectoratesJsonInner(electoratesJsonPath);

        File.SetCreationTimeUtc(electoratesJsonPath, AssemblyTimestamp.Value);
    }

    static async Task WriteElectoratesJsonInner(string electoratesJsonPath)
    {
        using var stream = assembly.GetManifestResourceStream("electorates.json")!;
        using var target = File.Create(electoratesJsonPath);
        await stream.CopyToAsync(target);
    }

    public static IElectorateMap Get2016Map(this IElectorate electorate)
    {
        if (!electorate.Exist2016)
        {
            throw new($"Electorate '{electorate.Name}' does not have a 2016 map");
        }

        return Maps2016.GetElectorate(electorate.ShortName);
    }

    public static IElectorateMap GetMap(this IElectorate electorate)
    {
        var name = electorate.ShortName;
        if (electorate.Exist2025)
        {
            return Maps2025.GetElectorate(name);
        }

        if (electorate.Exist2022)
        {
            return Maps2022.GetElectorate(name);
        }

        if (electorate.Exist2019)
        {
            return Maps2019.GetElectorate(name);
        }

        if (electorate.Exist2016)
        {
            return Maps2016.GetElectorate(name);
        }

        throw new($"Map not found: {name}");
        //return MapsFuture.GetElectorate(electorate.ShortName);
    }

    public static IElectorateMap Get2019Map(this IElectorate electorate)
    {
        if (electorate.Exist2019)
        {
            return Maps2019.GetElectorate(electorate.ShortName);
        }

        throw new($"Electorate '{electorate.Name}' does not have a 2019 map");
    }

    public static IElectorateMap Get2022Map(this IElectorate electorate)
    {
        if (electorate.Exist2022)
        {
            return Maps2022.GetElectorate(electorate.ShortName);
        }

        throw new($"Electorate '{electorate.Name}' does not have a 2022 map");
    }

    public static IElectorateMap Get2025Map(this IElectorate electorate)
    {
        if (electorate.Exist2025)
        {
            return Maps2025.GetElectorate(electorate.ShortName);
        }

        throw new($"Electorate '{electorate.Name}' does not have a 2025 map");
    }
    //
    // public static IElectorateMap GetFutureMap(this IElectorate electorate)
    // {
    //     if (!electorate.ExistInFuture)
    //     {
    //         throw new($"Electorate '{electorate.Name}' does not have a future map");
    //     }
    //
    //     return MapsFuture.GetElectorate(electorate.ShortName);
    // }
}