namespace AustralianElectorates
{
    public interface IOfficer
    {
        string Title { get; }
        string FamilyName { get; }
        string GivenNames { get; }
        string Capacity { get; }
        IAddress? Address { get; }
        string FullName();
    }

    [DebuggerDisplay("FamilyName={FamilyName}, GivenNames={GivenNames}")]
    class Officer :
        IOfficer
    {
        public string Title { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public string GivenNames { get; set; } = null!;
        public string Capacity { get; set; } = null!;
        public IAddress? Address { get; set; } = null!;

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
        public override string ToString()
        {
            return FullName();
        }
    }
}