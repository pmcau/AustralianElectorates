namespace AustralianElectorates
{
    public interface IStateMap
    {
        State State { get; }
        string GeoJson { get; }
    }

    class StateMap :
        IStateMap
    {
        public State State { get; set; }
        public string GeoJson { get; set; } = null!;
        public override string ToString()
        {
            return $"{State} Map";
        }
    }
}