using System.Text.RegularExpressions;
using HtmlAgilityPack;

static class HtmlAgilityExtensions
{
    public static string TrimmedInnerHtml(this HtmlNode node)
    {
        var html = node.InnerHtml;
        return TrimmedInnerHtml(html);
    }

    public static string TrimmedInnerHtml(this string html)
    {
        var newlinesTrimmed = html.Replace("\r\n", " ");
        var duplicateWhiteSpaceTrimmed = Regex.Replace(newlinesTrimmed, @"\s+", " ");
        return duplicateWhiteSpaceTrimmed
            .Trim()
            .Replace("> <", "><")
            .Replace("â€“", "-")
            .Replace("â€™", "'")
            .Replace("&ndash;", "-");
    }
}