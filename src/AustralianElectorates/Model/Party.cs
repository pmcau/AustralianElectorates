using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Party
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string RegisterDate { get; set; }
        public string AmendmentDate { get; set; }
        public string Address { get; set; }
        public Officer Officer { get; set; }
        public List<Officer> DeputyOfficers { get; set; }
    }
}