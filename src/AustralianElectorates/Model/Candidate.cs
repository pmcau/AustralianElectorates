namespace AustralianElectorates;

public interface ICandidate
{
    string FamilyName { get; }
    string GivenNames { get; }
    string? PartyCode { get; }
    uint Votes { get; }
    decimal Swing { get; }
    ushort? PartyId { get; }
    IPartyOrBranch? Party { get; }
    string FullName();
}

[DebuggerDisplay("FamilyName={FamilyName}, GivenNames={GivenNames}, PartyCode={PartyCode}")]
class Candidate :
    ICandidate
{
    public string FamilyName { get; set; } = null!;
    public string GivenNames { get; set; } = null!;
    public string? PartyCode { get; set; }
    public uint Votes { get; set; }
    public decimal Swing { get; set; }
    public ushort? PartyId { get; set; }
    public IPartyOrBranch? Party { get; set; }

    public string FullName() =>
        $"{FamilyName}, {GivenNames}";

    public override string ToString() =>
        FullName();
}