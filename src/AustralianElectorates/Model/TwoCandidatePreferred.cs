namespace AustralianElectorates;

public interface ITwoCandidatePreferred
{
    ICandidate Elected { get; }
    ICandidate Other { get; }
}

class TwoCandidatePreferred :
    ITwoCandidatePreferred
{
    public ICandidate Elected { get; set; } = null!;
    public ICandidate Other { get; set; } = null!;
    public override string ToString()
    {
        return $"Elected: {Elected.FullName()}. Other: {Other.FullName()}";
    }
}