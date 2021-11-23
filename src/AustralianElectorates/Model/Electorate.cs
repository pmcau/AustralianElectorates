namespace AustralianElectorates
{
    public interface IElectorate
    {
        string Name { get; }
        string ShortName { get; }
        State State { get; }
        double Area { get; }
        bool Exist2016 { get; }
        bool Exist2019 { get; }
        bool ExistInFuture { get; }
        DateTime? DateGazetted { get; }
        string Description { get; }
        string DemographicRating { get; }
        string NameDerivation { get; }
        uint? Enrollment { get; }
        ITwoCandidatePreferred? TwoCandidatePreferred { get; }
        IPartyOrBranch? CurrentParty { get; set; }
        IReadOnlyList<ILocation> Locations { get; set; }
        IElectorateMap Get2016Map();
        IElectorateMap Get2019Map();
        IElectorateMap GetFutureMap();
    }

    [DebuggerDisplay("Name={Name}, State={State}")]
    class Electorate :
        IElectorate
    {
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public State State { get; set; }
        public double Area { get; set; }
        public bool Exist2016 { get; set; }
        public bool Exist2019 { get; set; }
        public bool ExistInFuture { get; set; }
        public DateTime? DateGazetted { get; set; }
        public string Description { get; set; } = null!;
        public string DemographicRating { get; set; } = null!;
        public string NameDerivation { get; set; } = null!;
        public uint? Enrollment { get; set; }
        public ITwoCandidatePreferred? TwoCandidatePreferred { get; set; }
        public IPartyOrBranch? CurrentParty { get; set; } = null!;
        public IReadOnlyList<ILocation> Locations { get; set; } = Array.Empty<ILocation>();

        public static string GetShortName(string name)
        {
            Guard.AgainstWhiteSpace(name, nameof(name));
            return name.Replace(" ", "-")
                .Replace("'", "")
                .ToLowerInvariant();
        }

        public override string ToString()
        {
            return Name;
        }

        public IElectorateMap Get2016Map()
        {
            return DataLoader.Get2016Map(this);
        }

        public IElectorateMap Get2019Map()
        {
            return DataLoader.Get2019Map(this);
        }

        public IElectorateMap GetMap()
        {
            return DataLoader.GetMap(this);
        }

        public IElectorateMap GetFutureMap()
        {
            return DataLoader.GetFutureMap(this);
        }
    }
}