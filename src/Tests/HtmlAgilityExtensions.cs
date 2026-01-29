static class HtmlAgilityExtensions
{
    public static string TrimmedInnerHtml(this HtmlNode node)
    {
        var html = node.InnerHtml;
        var builder = new StringBuilder(html.Length);
        var lastWasWhitespace = false;
        var lastChar = '\0';

        for (var i = 0; i < html.Length; i++)
        {
            var c = html[i];

            if (c == '\r' || c == '\n' || char.IsWhiteSpace(c))
            {
                lastWasWhitespace = true;
                continue;
            }

            if (c == 'â' && i + 2 < html.Length)
            {
                if (html[i + 1] == '€' && html[i + 2] == '"')
                {
                    if (lastWasWhitespace)
                    {
                        builder.Append(' ');
                    }
                    builder.Append('-');
                    i += 2;
                    lastWasWhitespace = false;
                    lastChar = '-';
                    continue;
                }

                if (html[i + 1] == '€' && html[i + 2] == '™')
                {
                    if (lastWasWhitespace)
                    {
                        builder.Append(' ');
                    }
                    builder.Append('\'');
                    i += 2;
                    lastWasWhitespace = false;
                    lastChar = '\'';
                    continue;
                }
            }

            if (c == '&' && i + 6 < html.Length && html.AsSpan(i, 7) is "&ndash;")
            {
                if (lastWasWhitespace)
                {
                    builder.Append(' ');
                }
                builder.Append('-');
                i += 6;
                lastWasWhitespace = false;
                lastChar = '-';
                continue;
            }

            if (lastWasWhitespace)
            {
                if (!(lastChar == '>' && c == '<'))
                {
                    builder.Append(' ');
                }
            }

            builder.Append(c);
            lastWasWhitespace = false;
            lastChar = c;
        }

        return builder.ToString().Trim();
    }
}
