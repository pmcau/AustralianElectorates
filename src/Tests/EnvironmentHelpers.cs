static class EnvironmentHelpers
{
    public static void AppendToPath(string path)
    {
        var key = "path";
        var envPath = Environment.GetEnvironmentVariable(key);
        if (envPath != null && envPath.Contains(path))
        {
            return;
        }

        Environment.SetEnvironmentVariable(key, $"{envPath};{path}");
    }
}