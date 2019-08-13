using System;
using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Electorate
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public State State { get; set; }
        public double Area { get; set; }
        public bool Exist2016 { get; set; }
        public bool Exist2019 { get; set; }
        public bool ExistInFuture { get; set; }
        public DateTime? DateGazetted { get; set; }
        public string Description { get; set; }
        [NonSerialized] internal string MapUrl;
        public string DemographicRating { get; set; }
        public string ProductsAndIndustry { get; set; }
        public string NameDerivation { get; set; }
        public List<Member> Members { get; set; }
        public List<Location> Locations { get; set; }
        public uint? Enrollment { get; set; }
        public TwoCandidatePreferred TwoCandidatePreferred { get; set; }

        public static string GetShortName(string name)
        {
            Guard.AgainstNullWhiteSpace(name, nameof(name));
            return name.Replace(" ", "-")
                .Replace("'", "")
                .ToLowerInvariant();
        }

        public ElectorateMap Get2016Map()
        {
            return DataLoader.Get2016Map(this);
        }

        public ElectorateMap Get2019Map()
        {
            return DataLoader.Get2019Map(this);
        }

        public ElectorateMap GetFutureMap()
        {
            return DataLoader.GetFutureMap(this);
        }
    }
}