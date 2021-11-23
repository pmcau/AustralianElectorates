static class AssemblyTimestamp
{
    static AssemblyTimestamp()
    {
        var assemblyPath = AssemblyLocation.PathFor(typeof(AssemblyTimestamp));
        Value = File.GetCreationTimeUtc(assemblyPath);
    }

    public static DateTime Value;
}