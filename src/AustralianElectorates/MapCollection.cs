using System.IO.Compression;

namespace AustralianElectorates;

public class MapCollection
{
    string prefix;
    ConcurrentDictionary<string, IElectorateMap> electoratesCache = new(StringComparer.OrdinalIgnoreCase);
    ConcurrentDictionary<State, IStateMap> statesCache = new();
    string? australia;
    static Assembly assembly;

    public IReadOnlyDictionary<string, IElectorateMap> LoadedElectorates => electoratesCache;
    public IReadOnlyDictionary<State, IStateMap> LoadedStates => statesCache;

    internal MapCollection(string prefix)
    {
        this.prefix = prefix;
    }

    static MapCollection()
    {
        assembly = typeof(DataLoader).Assembly;
    }

    public IElectorateMap GetElectorate(string electorateName)
    {
        Guard.AgainstWhiteSpace(electorateName, nameof(electorateName));
        return GetElectorateInner(Electorate.GetShortName(electorateName),electorateName);
    }

    public IElectorateMap GetElectorate(IElectorate electorate)
    {
        return GetElectorateInner(electorate.ShortName,electorate.Name);
    }

    IElectorateMap GetElectorateInner(string electorateShortName, string electorateName)
    {
        return electoratesCache.GetOrAdd($@"{prefix}\Electorates\{electorateShortName}",
            s =>
            {
                var geoJson = GetMap(s);
                var electorate = DataLoader.Electorates.SingleOrDefault(x => x.ShortName == electorateShortName);
                if (electorate == null)
                {
                    throw new($"Unable to find electorate named '{electorateName}'.");
                }
                return new ElectorateMap
                {
                    Electorate = electorate,
                    GeoJson = geoJson
                };
            });
    }

    public IStateMap GetState(State state)
    {
        var key = $@"{prefix}\{state.ToString().ToLowerInvariant()}";
        return statesCache.GetOrAdd(
            state,
            s =>
            {
                var geoJson = GetMap(key);
                return new StateMap
                {
                    State = s,
                    GeoJson = geoJson
                };
            });
    }

    public string GetAustralia()
    {
        if (australia == null)
        {
            australia = GetMap($@"{prefix}\australia");
        }

        return australia;
    }

    static string GetMap(string path)
    {
        using var stream = assembly.GetManifestResourceStream("Maps.zip")!;
        using ZipArchive archive = new(stream);
        var entry = archive.GetEntry($"{path}.geojson");
        if (entry == null)
        {
            throw new($"Could not find data for '{path}'.");
        }

        return entry.ReadString();
    }

    public void LoadAll()
    {
        using var stream = assembly.GetManifestResourceStream("Maps.zip")!;
        using ZipArchive archive = new(stream);
        foreach (var entry in archive.Entries.Where(x => x.FullName.StartsWith(prefix)))
        {
            var key = entry.FullName.Split('.').First();
            var mapString = entry.ReadString();

            if (key.Contains("Electorates"))
            {
                var shortName = Path.GetFileName(key);
                var electorate = DataLoader.Electorates.Single(x => x.ShortName == shortName);
                electoratesCache[key] = new ElectorateMap
                {
                    Electorate = electorate,
                    GeoJson = mapString
                };
                continue;
            }

            if (key.Contains("australia"))
            {
                australia = mapString;
                continue;
            }

            var state = ParseState(key);
            statesCache[state] = new StateMap
            {
                GeoJson = mapString,
                State = state
            };
        }
    }

    static State ParseState(string key)
    {
        return (State) Enum.Parse(typeof(State), key.Split('\\')[1], true);
    }
}