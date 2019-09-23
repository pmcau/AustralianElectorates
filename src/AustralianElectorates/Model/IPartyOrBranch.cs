using System.Collections.Generic;

namespace AustralianElectorates
{
    public interface IPartyOrBranch
    {
        ushort Id { get; }
        string Name { get; }
        string Abbreviation { get; }
        string RegisterDate { get; }
        string AmendmentDate { get; }
        string Address { get; }
        Officer Officer { get; }
        IReadOnlyList<Officer> DeputyOfficers { get; }
        string Code { get; }
    }
}