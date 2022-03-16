namespace AustralianElectorates;

public class ElectoratesNotFoundException : Exception
{
    public IReadOnlyList<string> Names { get; }

    public ElectoratesNotFoundException(IEnumerable<string> names) =>
        Names = names.ToList();

    public override string Message => $"Unable to find electorates: '{string.Join("', '", Names)}'.";
}