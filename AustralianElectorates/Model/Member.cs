namespace AustralianElectorates
{
    public class Member
    {
        public string Name { get; set; }
        public ushort Begin { get; set; }
        public ushort? End { get; set; }
        public string Party { get; set; }
        public Electorate Electorate { get; set; }
    }
}