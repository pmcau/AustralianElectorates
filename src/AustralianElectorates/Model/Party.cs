using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Party:IParty
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string RegisterDate { get; set; }
        public string AmendmentDate { get; set; }
        public string Address { get; set; }
        public Officer Officer { get; set; }
        public List<Officer> DeputyOfficers { get; set; } = new List<Officer>();
        public List<Branch> Branches { get; set; } = new List<Branch>();
    }
}