using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AustralianElectorates;
using HtmlAgilityPack;

public static class ElectoratesScraper
{
    public static async Task<Electorate> ScrapeElectorate(string electorateName, State state)
    {
        var tempElectorateHtmlPath = Path.Combine(DataLocations.TempPath, $"{electorateName}.html");
        await Downloader.DownloadFile(tempElectorateHtmlPath, $"https://www.aec.gov.au/profiles/{state}/{electorateName}.htm");
        var document = new HtmlDocument();
        document.Load(tempElectorateHtmlPath);
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
            Name = electorateName,
            State = state
        };
        if (values.TryGetValue("Date this name and boundary was gazetted", out var gazettedHtml))
        {
            electorate.DateGazetted = DateTime.ParseExact(gazettedHtml.InnerText, "d MMMM yyyy", null);
        }

        electorate.Members = values["Members"].TrimmedInnerHtml();
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

    static HtmlNode FindProfileTable(HtmlDocument document)
    {
        return document.DocumentNode.SelectSingleNode("//comment()[contains(., ' InstanceBeginEditable name=\"Content\" ')]/following-sibling::dl");
    }
}