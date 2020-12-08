using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

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

            HtmlDocument document = new();
            document.Load(htmlPath);
            var selectSingleNode = document.DocumentNode.SelectSingleNode("//caption");
            var table = selectSingleNode.ParentNode;
            Dictionary<string,string> codes = new();
            foreach (var node in table.SelectNodes("//tr").Skip(1))
            {
                var nodes = node.ChildNodes.Where(x=>x.NodeType != HtmlNodeType.Text).ToList();
                var abbreviation = nodes[0].InnerHtml;
                var name = nodes[1].InnerHtml.Split('(')[0].Trim();
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