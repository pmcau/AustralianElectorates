namespace AustralianElectorates
{
    public static class StateExtensions
    {
        public static string Name(this State state)
        {
            if (state == State.ACT)
            {
                return "Australian Capital Territory";
            }
            if (state == State.NSW)
            {
                return "New South Wales";
            }
            if (state == State.NT)
            {
                return "Northern Territory";
            }
            if (state == State.SA)
            {
                return "South Australia";
            }
            if (state == State.QLD)
            {
                return "Queensland";
            }
            if (state == State.TAS)
            {
                return "Tasmania";
            }
            if (state == State.VIC)
            {
                return "Victoria";
            }
            if (state == State.WA)
            {
                return "Western Australia";
            }
            throw new($"Unknown state: {state}");
        }
    }
}