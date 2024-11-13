namespace AustralianElectorates;

public interface IElectorate
{
    string Name { get; }
    string ShortName { get; }
    State State { get; }
    double Area { get; }
    bool Exist2019 { get; }
    bool Exist2022 { get; }
    bool Exist2025 { get; }

    //bool ExistInFuture { get; }
    Date? DateGazetted { get; }
    string Description { get; }
    string DemographicRating { get; }
    string NameDerivation { get; }
    uint? Enrollment { get; }
    ITwoCandidatePreferred? TwoCandidatePreferred { get; }
    IPartyOrBranch? CurrentParty { get; set; }
    IReadOnlyList<ILocation> Locations { get; set; }
    IElectorateMap Get2019Map();
    IElectorateMap Get2022Map();
    IElectorateMap Get2025Map();
    //IElectorateMap GetFutureMap();

    bool ContainsPostcode(int postcode);
}

[DebuggerDisplay("Name={Name}, State={State}")]
class Electorate :
    IElectorate
{
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public State State { get; set; }
    public double Area { get; set; }
    public bool Exist2019 { get; set; }
    public bool Exist2022 { get; set; }
    public bool Exist2025 { get; set; }

    //public bool ExistInFuture { get; set; }
    public Date? DateGazetted { get; set; }
    public string Description { get; set; } = null!;
    public string DemographicRating { get; set; } = null!;
    public string NameDerivation { get; set; } = null!;
    public uint? Enrollment { get; set; }
    public ITwoCandidatePreferred? TwoCandidatePreferred { get; set; }
    public IPartyOrBranch? CurrentParty { get; set; } = null!;
    public IReadOnlyList<ILocation> Locations { get; set; } = [];

    public static string GetShortName(string name)
    {
        Guard.AgainstWhiteSpace(name, nameof(name));
        return name
            .Replace(' ', '-')
            .Replace("'", "")
            .ToLowerInvariant();
    }

    public override string ToString() =>
        Name;

    public IElectorateMap Get2019Map() =>
        DataLoader.Get2019Map(this);

    public IElectorateMap Get2022Map() =>
        DataLoader.Get2022Map(this);

    public IElectorateMap Get2025Map() =>
        DataLoader.Get2025Map(this);

    public IElectorateMap GetMap() =>
        DataLoader.GetMap(this);

    public bool ContainsPostcode(int postcode)
    {
        foreach (var location in Locations)
        {
            if (location.Postcode == postcode)
            {
                return true;
            }
        }

        return false;
    }

    // public IElectorateMap GetFutureMap() =>
    //     DataLoader.GetFutureMap(this);
}