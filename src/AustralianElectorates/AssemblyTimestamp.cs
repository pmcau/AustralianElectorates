static class AssemblyTimestamp
{
    static AssemblyTimestamp() =>
        Value = File.GetCreationTimeUtc(AssemblyLocation.File);

    public static DateTime Value;
}