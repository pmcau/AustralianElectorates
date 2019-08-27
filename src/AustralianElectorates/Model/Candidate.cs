namespace AustralianElectorates
{
    public class Candidate
    {
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string PartyCode { get; set; }
        public uint Votes { get; set; }
        public decimal Swing { get; set; }
        public ushort? PartyId { get; set; }
        public IParty Party { get; set; }

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}