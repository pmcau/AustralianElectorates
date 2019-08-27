using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Member
    {
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public ushort Begin { get; set; }
        public ushort? End { get; set; }
        public List<string> PartyCodes { get; set; } = new List<string>();
        public Electorate Electorate { get; set; }
        public List<ushort> PartyIds { get; set; } = new List<ushort>();
        public List<IParty> Parties { get; set; } = new List<IParty>();

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}