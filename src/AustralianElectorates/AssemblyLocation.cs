using System;
using System.IO;

static class AssemblyTimestamp
{
    static AssemblyTimestamp()
    {
        var assembly = typeof(AssemblyTimestamp).Assembly;

        var assemblyPath = assembly.CodeBase
            .Replace("file:///", "")
            .Replace("file://", "")
            .Replace(@"file:\\\", "")
            .Replace(@"file:\\", "");
        Value = File.GetCreationTimeUtc(assemblyPath);
    }

    public static DateTime Value;
}