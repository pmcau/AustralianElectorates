using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Location
    {
        internal Location()
        {
        }

        [DataMember]
        public int Postcode { get; internal set; }
        [DataMember]
        public Electorate Electorate { get; internal set; } = null!;

        [DataMember(Name = nameof(Localities), Order = 100)]
        internal List<string> localities = null!;
        public IReadOnlyList<string> Localities
        {
            get => localities;
        }
    }
}