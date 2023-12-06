﻿using HtmlAgilityPack;

public static class PartyCodeScraper
{
    public static async Task<Dictionary<string, string>> Run()
    {
        var htmlPath = Path.Combine(DataLocations.TempPath, "partycodes.html");
        var url = "https://www.aec.gov.au/Electorates/party-codes.htm";
        File.Delete(htmlPath);
        try
        {
            await Downloader.DownloadFile(htmlPath, url);

            if (!File.Exists(htmlPath))
            {
                throw new($"Could not download {url}");
            }

            var document = new HtmlDocument();
            document.Load(htmlPath);
            var selectSingleNode = document.DocumentNode.SelectSingleNode("//caption");
            var table = selectSingleNode.ParentNode;
            var codes = new Dictionary<string, string>();
            foreach (var node in table
                         .SelectNodes("//tr")
                         .Skip(1))
            {
                var nodes = node
                    .ChildNodes.Where(_ => _.NodeType != HtmlNodeType.Text)
                    .ToList();
                var abbreviation = nodes[0].InnerHtml;
                var name = nodes[1]
                    .InnerHtml.Split('(')[0]
                    .Trim();
                codes.Add(abbreviation, name);
            }

            return codes;
        }
        catch (Exception exception)
        {
            throw new($"Failed to parse {htmlPath} {htmlPath}", exception);
        }
    }
}