using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Electorate
    {
        internal Electorate()
        {
        }

        [DataMember]
        public string Name { get; internal set; } = null!;

        [DataMember]
        public string ShortName { get; internal set; } = null!;

        [DataMember]
        public State State { get; internal set; }

        [DataMember]
        public double Area { get; internal set; }

        [DataMember]
        public bool Exist2016 { get; internal set; }

        [DataMember]
        public bool Exist2019 { get; internal set; }

        [DataMember]
        public bool ExistInFuture { get; internal set; }

        [DataMember]
        public DateTime? DateGazetted { get; internal set; }

        [DataMember]
        public string Description { get; internal set; } = null!;

        [DataMember]
        public string DemographicRating { get; internal set; } = null!;

        [DataMember]
        public string ProductsAndIndustry { get; internal set; } = null!;

        [DataMember]
        public string NameDerivation { get; internal set; } = null!;

        [DataMember]
        public List<Member> Members { get; internal set; } = null!;

        [DataMember]
        public List<Location> Locations { get; internal set; } = null!;

        [DataMember]
        public uint? Enrollment { get; internal set; }

        [DataMember]
        public TwoCandidatePreferred? TwoCandidatePreferred { get; internal set; }

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