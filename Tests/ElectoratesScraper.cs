using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AustralianElectorates;
using HtmlAgilityPack;
using Xunit;

public static class ElectoratesScraper
{
    public static async Task<Electorate> ScrapeElectorate(string shortName, State state)
    {
        try
        {
            var tempElectorateHtmlPath = Path.Combine(DataLocations.TempPath, $"{shortName}.html");
            await Downloader.DownloadFile(tempElectorateHtmlPath, $"https://www.aec.gov.au/profiles/{state}/{shortName}.htm");
            var document = new HtmlDocument();
            document.Load(tempElectorateHtmlPath);

            var fullName = GetFullName(document);
            var values = new Dictionary<string, HtmlNode>(StringComparer.OrdinalIgnoreCase);
            var profileId = FindProfileTable(document);
            var htmlNodeCollection = profileId.SelectNodes("dt");
            foreach (var keyNode in htmlNodeCollection)
            {
                var valueNode = keyNode.NextSibling.NextSibling;
                values[keyNode.InnerText.Trim().Trim(':').Replace("  ", " ")] = valueNode;
            }

            var electorate = new Electorate
            {
                Name = fullName,
                ShortName = shortName,
                State = state
            };
            if (values.TryGetValue("Date this name and boundary was gazetted", out var gazettedHtml))
            {
                electorate.DateGazetted = DateTime.ParseExact(gazettedHtml.InnerText, "d MMMM yyyy", null);
            }

            electorate.Members = ElectorateMembers(values).ToList();
            electorate.DemographicRating = values["Demographic Rating"].TrimmedInnerHtml();
            electorate.ProductsAndIndustry = values["Products/Industries of the Area"].TrimmedInnerHtml();
            electorate.NameDerivation = values["Name derivation"].TrimmedInnerHtml();
            if (values.TryGetValue("Location Description", out var description))
            {
                electorate.Description = description.TrimmedInnerHtml();
            }

            if (values.TryGetValue("Area and Location Description", out description))
            {
                electorate.Description = description.TrimmedInnerHtml();
            }

            if (values.TryGetValue("Area", out var area))
            {
                electorate.Area = double.Parse(area.InnerHtml.Trim().Replace("&nbsp;", " ").Replace(" ", "").Replace("sqkm", "").Replace(",", ""));
            }

            return electorate;
        }
        catch (Exception exception)
        {
            throw new Exception("Failed to parse " + shortName, exception);
        }
    }

    static string GetFullName(HtmlDocument document)
    {
        var headings = document.DocumentNode.Descendants("h1").Skip(1).ToList();
        var strings = headings.Single().InnerText.Replace("Profile of the electoral division of ", "").Split(new[] {" ("}, StringSplitOptions.None);
        var fullName = strings[0];
        Assert.NotEmpty(fullName);
        return fullName;
    }

    static IEnumerable<Member> ElectorateMembers(Dictionary<string, HtmlNode> values)
    {
        var members = values["Members"];
        if (members.InnerText.Contains("will be elected at the next federal general election."))
        {
            yield break;
        }
        var texts = members
            .Descendants("li")
            .Select(x => x.ChildNodes.First().InnerText)
            .ToList();
        if (texts.Count == 0)
        {
            texts = members.ChildNodes
                .Where(x => x.NodeType == HtmlNodeType.Text)
                .Select(x => x.InnerText)
                .ToList();
        }
        foreach (var text in texts)
        {
            var cleaned = text.TrimEnd('(').Trim();
            if (cleaned.Length == 0)
            {
                continue;
            }
            var split = cleaned.Split(new[] {" ("},2, StringSplitOptions.None);
            var member = split[0];
            split = split[1].Split(new[] { ") " }, 2, StringSplitOptions.None);
            var party = split[0];

            split = split[1].Split(new[] {"&ndash;", "â€“", "-" }, 2, StringSplitOptions.RemoveEmptyEntries);
            var begin = ushort.Parse(split[0].Trim());
            ushort? end = null;
            if (split.Length > 1)
            {
                end = ushort.Parse(split[1].Trim());
            }
            yield return new Member
            {
                Name = member,
                Party = party,
                Begin = begin,
                End = end,
            };
        }
    }

    static HtmlNode FindProfileTable(HtmlDocument document)
    {
        return document.DocumentNode.SelectSingleNode("//comment()[contains(., ' InstanceBeginEditable name=\"Content\" ')]/following-sibling::dl");
    }
}