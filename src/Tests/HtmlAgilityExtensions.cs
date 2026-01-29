static class HtmlAgilityExtensions
{
    public static string TrimmedInnerHtml(this HtmlNode node)
    {
        var builder = new StringBuilder(node.InnerHtml.Length);
        var lastWasWhitespace = false;

        foreach (var c in node.InnerHtml)
        {
            if (c == '\r' || c == '\n' || char.IsWhiteSpace(c))
            {
                if (!lastWasWhitespace)
                {
                    builder.Append(' ');
                    lastWasWhitespace = true;
                }
            }
            else
            {
                builder.Append(c);
                lastWasWhitespace = false;
            }
        }

        var result = builder.ToString().Trim();

        return result
            .Replace("> <", "><")
            .Replace("â€“", "-")
                .Replace("â€™", "'")
                .Replace("&ndash;", "-");
    }
}
