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
        public Electorate Electorate { get; internal set; }

        [DataMember(Name = nameof(Localities))]
        internal List<string> localities;
        public IReadOnlyList<string> Localities
        {
            get => localities;
        }
    }
}