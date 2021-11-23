﻿namespace AustralianElectorates
{
    public interface IParty:
        IPartyOrBranch
    {
        IReadOnlyList<IBranch> Branches { get; }
    }

    [DebuggerDisplay("Name={Name}, Code={Code}")]
    class Party :
        IParty
    {
        public ushort Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public string RegisterDate { get; set; } = null!;
        public string AmendmentDate { get; set; } = null!;
        public string Address { get; set; } = null!;
        public IOfficer Officer { get; set; } = null!;
        public IReadOnlyList<IOfficer> DeputyOfficers { get; set; } = Array.Empty<IOfficer>();
        public IReadOnlyList<IBranch> Branches { get; set; } = Array.Empty<IBranch>();
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}