using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Location
    {
        public int Postcode { get; set; }
        public Electorate Electorate { get; set; }
        public List<string> Localities { get; set; }
    }
}