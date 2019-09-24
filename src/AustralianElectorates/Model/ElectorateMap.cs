namespace AustralianElectorates
{
    public interface IElectorateMap
    {
        IElectorate Electorate { get; }
        string GeoJson { get; }
    }

    class ElectorateMap :
        IElectorateMap
    {
        public IElectorate Electorate { get; set; } = null!;
        public string GeoJson { get; set; } = null!;
    }
}