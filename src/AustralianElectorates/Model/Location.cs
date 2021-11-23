namespace AustralianElectorates
{
    public interface ILocation
    {
        int Postcode { get; }
        IElectorate Electorate { get; }
        IReadOnlyList<string> Localities { get; }
    }

    [DebuggerDisplay("Postcode={Postcode}")]
    class Location :
        ILocation
    {
        public int Postcode { get; set; }
        public IElectorate Electorate { get; set; } = null!;
        public IReadOnlyList<string> Localities { get; set; } = null!;
        public override string ToString()
        {
            return Postcode.ToString();
        }
    }
}