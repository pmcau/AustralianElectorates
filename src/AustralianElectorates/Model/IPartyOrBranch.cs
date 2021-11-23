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
        IOfficer Officer { get; }
        IReadOnlyList<IOfficer> DeputyOfficers { get; }
        string Code { get; }
    }
}