namespace AustralianElectorates;

public class ElectionNotFoundException : Exception
{
    public int Parliament { get; }

    public ElectionNotFoundException(int parliament)
    {
        Parliament = parliament;
    }

    public override string Message => $"Unable to find election for Parliament '{Parliament}'.";
}