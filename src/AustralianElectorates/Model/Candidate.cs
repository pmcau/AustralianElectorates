namespace AustralianElectorates
{
    public class Candidate
    {
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string Party { get; set; }
        public uint Votes { get; set; }
        public decimal Swing { get; set; }
        public ushort? PartyId { get; set; }
    }
}