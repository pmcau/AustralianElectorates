using System.Collections.Generic;

namespace AustralianElectorates
{
    public interface IParty
    {
        ushort Id { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        string RegisterDate { get; set; }
        string AmendmentDate { get; set; }
        string Address { get; set; }
        Officer Officer { get; set; }
        List<Officer> DeputyOfficers { get; set; }
        string Code { get; set; }
    }
}