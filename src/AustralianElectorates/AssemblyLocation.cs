static class AssemblyLocation
{
    static AssemblyLocation()
    {
        File = typeof(AssemblyLocation).Assembly.Location;
        Directory = Path.GetDirectoryName(File)!;
    }

    public static string File { get; }
    public static string Directory { get; }
}