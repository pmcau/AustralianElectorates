using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Member
    {
        internal Member()
        {
        }

        [DataMember]
        public string FamilyName { get; internal set; } = null!;
        [DataMember]
        public string GivenNames { get; internal set; } = null!;
        [DataMember]
        public ushort Begin { get; internal set; }
        [DataMember]
        public ushort? End { get; internal set; }
        [DataMember]
        public Electorate Electorate { get; internal set; } = null!;

        [DataMember(Name = nameof(PartyCodes), Order = 100)]
        internal List<string> partyCodes = new List<string>();
        public IReadOnlyList<string> PartyCodes
        {
            get => partyCodes;
        }

        [DataMember(Name = nameof(PartyIds),EmitDefaultValue = false, Order = 100)]
        internal List<ushort> partyIds = new List<ushort>();
        public IReadOnlyList<ushort> PartyIds
        {
            get => partyIds;
        }

        [DataMember(Name = nameof(Parties), Order = 100)]
        internal List<IParty> parties = new List<IParty>();
        public IReadOnlyList<IParty> Parties
        {
            get => parties;
        }

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}