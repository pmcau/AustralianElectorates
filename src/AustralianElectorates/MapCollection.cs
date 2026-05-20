using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace AustralianElectorates;

public class MapCollection
{
    string prefix;
    ConcurrentDictionary<string, IElectorateMap> electoratesCache = new(StringComparer.OrdinalIgnoreCase);
    ConcurrentDictionary<State, IStateMap> statesCache = [];
    string? australia;
    Lazy<ElectorateLocator> locator;
    static Assembly assembly;

    public IReadOnlyDictionary<string, IElectorateMap> LoadedElectorates => electoratesCache;
    public IReadOnlyDictionary<State, IStateMap> LoadedStates => statesCache;

    internal MapCollection(string prefix)
    {
        this.prefix = prefix;
        locator = new(() => new(GetAustralia()));
    }

    static MapCollection() =>
        assembly = typeof(DataLoader).Assembly;

    public IElectorateMap GetElectorate(string electorateName)
    {
        Guard.AgainstWhiteSpace(electorateName, nameof(electorateName));
        return GetElectorateInner(Electorate.GetShortName(electorateName), electorateName);
    }

    public IElectorateMap GetElectorate(IElectorate electorate) =>
        GetElectorateInner(electorate.ShortName, electorate.Name);

    IElectorateMap GetElectorateInner(string electorateShortName, string electorateName) =>
        electoratesCache.GetOrAdd($@"{prefix}\Electorates\{electorateShortName}",
            s =>
            {
                var geoJson = GetMap(s);
                var electorate = DataLoader.Electorates.SingleOrDefault(_ => _.ShortName == electorateShortName);
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

    public string GetAustralia() =>
        australia ??= GetMap($@"{prefix}\australia");

    public IElectorate LocateElectorate(double latitude, double longitude)
    {
        if (TryLocateElectorate(latitude, longitude, out var electorate))
        {
            return electorate;
        }

        throw new($"Unable to find electorate for location: latitude '{latitude}', longitude '{longitude}'.");
    }

    public bool TryLocateElectorate(double latitude, double longitude, [NotNullWhen(true)] out IElectorate? electorate)
    {
        electorate = locator.Value.Find(latitude, longitude);
        return electorate != null;
    }

    static string GetMap(string path)
    {
        using var stream = assembly.GetManifestResourceStream("Maps.zip")!;
        using var archive = new ZipArchive(stream);
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
        using var archive = new ZipArchive(stream);
        foreach (var entry in archive.Entries.Where(_ => _.FullName.StartsWith(prefix)))
        {
            var key = entry
                .FullName.Split('.')
                .First();
            var mapString = entry.ReadString();

            if (key.Contains("Electorates"))
            {
                var shortName = Path.GetFileName(key);
                var electorate = DataLoader.Electorates.Single(_ => _.ShortName == shortName);
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

    static State ParseState(string key) =>
        (State) Enum.Parse(typeof(State), key
            .Split('\\')[1], true);
}
