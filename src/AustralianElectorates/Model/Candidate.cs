namespace AustralianElectorates
{
    public interface ICandidate
    {
        string FamilyName { get; }
        string GivenNames { get; }
        string? PartyCode { get; }
        uint Votes { get; }
        decimal Swing { get; }
        ushort? PartyId { get; }
        IPartyOrBranch Party { get; }
        string FullName();
    }

    class Candidate :
        ICandidate
    {
        public string FamilyName { get; set; } = null!;
        public string GivenNames { get; set; } = null!;
        public string? PartyCode { get; set; }
        public uint Votes { get; set; }
        public decimal Swing { get; set; }
        public ushort? PartyId { get; set; }
        public IPartyOrBranch Party { get; set; } = null!;

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}