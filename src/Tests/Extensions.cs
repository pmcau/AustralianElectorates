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

    public static string ToTitleCase(this string value)
    {
        var words = value.Split(' ');
        for (var i = 0; i < words.Length; i++)
        {
            if (words[i].Length == 0)
            {
                continue;
            }

            var firstChar = char.ToUpper(words[i][0]);
            var rest = "";
            if (words[i].Length > 1)
            {
                rest = words[i].Substring(1).ToLower();
            }
            words[i] = firstChar + rest;
        }
        return string.Join(" ", words);
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