using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Candidate
    {
        internal Candidate()
        {
        }

        [DataMember]
        public string FamilyName { get; internal set; }
        [DataMember]
        public string GivenNames { get; internal set; }
        [DataMember]
        public string PartyCode { get; internal set; }
        [DataMember]
        public uint Votes { get; internal set; }
        [DataMember]
        public decimal Swing { get; internal set; }
        [DataMember]
        public ushort? PartyId { get; internal set; }
        [DataMember]
        public IParty Party { get; internal set; }

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}