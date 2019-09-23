namespace AustralianElectorates
{
    public interface IAddress
    {
        string Line1 { get; }
        string? Line2 { get; }
        string? Line3 { get; }
        string Suburb { get; }
        State State { get; }
        int Postcode { get; }
    }

    class Address :
        IAddress
    {
        public string Line1 { get; set; } = null!;
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string Suburb { get; set; } = null!;
        public State State { get; set; }
        public int Postcode { get; set; }
    }
}