namespace AustralianElectorates;

public interface IBranch:
    IPartyOrBranch
{
    IParty Party { get; }
}

[DebuggerDisplay("Name={Name}, Code={Code}")]
class Branch :
    IBranch
{
    public ushort Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public string RegisterDate { get; set; } = null!;
    public string AmendmentDate { get; set; } = null!;
    public string Address { get; set; } = null!;
    public IOfficer Officer { get; set; } = null!;
    public IReadOnlyList<IOfficer> DeputyOfficers { get; set; } = null!;
    public IParty Party { get; set; } = null!;
    public override string ToString()
    {
        return $"{Name} ({Code})";
    }
}