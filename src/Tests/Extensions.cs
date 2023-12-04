using HtmlAgilityPack;

static class Extensions
{
    public static void AppendArguments(this ProcessStartInfo info, IEnumerable<string> arguments)
    {
        foreach (var argument in arguments)
        {
            info.ArgumentList.Add(argument);
        }
    }
    public static void AppendArguments(this ProcessStartInfo info, params string[] arguments)
    {
        foreach (var argument in arguments)
        {
            info.ArgumentList.Add(argument);
        }
    }
    public static List<string> Headings(this HtmlDocument document) =>
        document.DocumentNode.Descendants("h1")
            .Select(_ => _.InnerText).ToList();

    public static bool TryGetKey(this Dictionary<string, string> document, string value, out string key)
    {
        foreach (var pair in document.Where(pair => pair.Value == value))
        {
            key = pair.Key;
            return true;
        }

        key = "";
        return false;
    }

    public static string ToTitleCase(this string value)
    {
        var builder = new StringBuilder(value.Length);

        var lowerNext = false;
        foreach (var ch in value)
        {
            if (lowerNext)
            {
                builder.Append(char.ToLower(ch));
            }
            else
            {
                builder.Append(ch);
            }

            if (!char.IsLetter(ch))
            {
                lowerNext = false;
                continue;
            }
            if (char.IsLower(ch))
            {
                lowerNext = false;
                continue;
            }
            lowerNext = true;
        }

        return builder.ToString();
    }

    public static string ReplaceCaseless(this string str, string oldValue, string newValue)
    {
        var stringBuilder = new StringBuilder();

        var previousIndex = 0;
        var index = str.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);
        while (index != -1)
        {
            stringBuilder.Append(str.Substring(previousIndex, index - previousIndex));
            stringBuilder.Append(newValue);
            index += oldValue.Length;

            previousIndex = index;
            index = str.IndexOf(oldValue, index, StringComparison.OrdinalIgnoreCase);
        }
        stringBuilder.Append(str[previousIndex..]);

        return stringBuilder.ToString();
    }
}