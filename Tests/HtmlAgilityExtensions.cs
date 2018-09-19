using System.Text.RegularExpressions;
using HtmlAgilityPack;

static class HtmlAgilityExtensions
{
    public static string TrimmedInnerHtml(this HtmlNode node)
    {
        var newlinesTrimmed = node.InnerHtml.Replace("\r\n", " ");
        var duplicateWhiteSpaceTrimmed = Regex.Replace(newlinesTrimmed, @"\s+", " ");
        return duplicateWhiteSpaceTrimmed
            .Trim()
            .Replace("> <", "><")
            .Replace("â€“","-")
            .Replace("&ndash;", "-");
    }
}