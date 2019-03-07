using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

static class Extensions
{
    public static List<string> Headings(this HtmlDocument document)
    {
        return document.DocumentNode.Descendants("h1")
            .Select(x => x.InnerText).ToList();
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
        stringBuilder.Append(str.Substring(previousIndex));

        return stringBuilder.ToString();
    }
}