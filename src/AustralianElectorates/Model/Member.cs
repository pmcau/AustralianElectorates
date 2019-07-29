namespace AustralianElectorates
{
    public class Member
    {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string FullName => $"{FamilyName}, {GivenName}";
        public ushort Begin { get; set; }
        public ushort? End { get; set; }
        public string Party { get; set; }
        public Electorate Electorate { get; set; }
    }
}