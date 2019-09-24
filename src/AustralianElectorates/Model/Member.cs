using System.Collections.Generic;

namespace AustralianElectorates
{
    public interface IMember
    {
        string FamilyName { get; }
        string GivenNames { get; }
        ushort Begin { get; }
        ushort? End { get; }
        IElectorate Electorate { get; }
        IReadOnlyList<string> PartyCodes { get; }
        IReadOnlyList<ushort> PartyIds { get; }
        IReadOnlyList<IPartyOrBranch> Parties { get; set; }
        string FullName();
    }

    class Member :
        IMember
    {
        public string FamilyName { get; set; } = null!;
        public string GivenNames { get; set; } = null!;
        public ushort Begin { get; set; }
        public ushort? End { get; set; }
        public IElectorate Electorate { get; set; } = null!;
        public IReadOnlyList<string> PartyCodes { get; set; } = null!;
        public IReadOnlyList<ushort> PartyIds { get; set; } = null!;
        public IReadOnlyList<IPartyOrBranch> Parties { get; set; } = null!;
        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}