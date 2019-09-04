using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Address
    {
        internal Address()
        {
        }

        [DataMember]
        public string Line1 { get; internal set; } = null!;

        [DataMember]
        public string Line2 { get; internal set; } = null!;

        [DataMember]
        public string Line3 { get; internal set; } = null!;

        [DataMember]
        public string Suburb { get; internal set; } = null!;

        [DataMember]
        public State State { get; internal set; }

        [DataMember]
        public int Postcode { get; internal set; }
    }
}