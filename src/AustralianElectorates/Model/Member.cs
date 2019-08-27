namespace AustralianElectorates
{
    public class Member
    {
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public ushort Begin { get; set; }
        public ushort? End { get; set; }
        public string PartyCode { get; set; }
        public Electorate Electorate { get; set; }
        public ushort? PartyId { get; set; }
        public IParty Party { get; set; }

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}