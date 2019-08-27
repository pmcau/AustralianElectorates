using System.Collections.Generic;

namespace AustralianElectorates
{
    public class Branch: IParty
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string RegisterDate { get; set; }
        public string AmendmentDate { get; set; }
        public string Address { get; set; }
        public Officer Officer { get; set; }
        public List<Officer> DeputyOfficers { get; set; }
        public Party Party { get; set; }
    }
    public interface IParty
    {
        ushort Id { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        string RegisterDate { get; set; }
        string AmendmentDate { get; set; }
        string Address { get; set; }
        Officer Officer { get; set; }
        List<Officer> DeputyOfficers { get; set; }
    }
}