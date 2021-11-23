static class AssemblyLocation
{
    public static string PathFor(Type type)
    {
        var assembly = type.Assembly;

        return assembly.CodeBase
            .Replace("file:///", "")
            .Replace("file://", "")
            .Replace(@"file:\\\", "")
            .Replace(@"file:\\", "");
    }
    public static string DirectoryFor(Type type)
    {
        return Path.GetDirectoryName(PathFor(type))!;
    }
}