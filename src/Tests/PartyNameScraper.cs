using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

public static class PartyNameScraper
{
    public static async Task Run()
    {
        var htmlPath = Path.Combine(DataLocations.TempPath, "partycodes.html");
        var url = "https://www.aec.gov.au/Electorates/party-codes.htm";
        try
        {
            await Downloader.DownloadFile(htmlPath, url);

            if (!File.Exists(htmlPath))
            {
                throw new Exception($"Could not download {url}");
            }

            var document = new HtmlDocument();
            document.Load(htmlPath);
            var selectSingleNode = document.DocumentNode.SelectSingleNode("//caption");
            var table = selectSingleNode.ParentNode;
            var dictionary = new Dictionary<string,string>();
            foreach (var node in table.SelectNodes("//tr").Skip(1))
            {
                var nodes = node.ChildNodes.Where(x=>x.NodeType != HtmlNodeType.Text).ToList();
                var abbreviation = nodes[0].InnerHtml;
                var name = nodes[1].InnerHtml;
                dictionary.Add(abbreviation, name);
            }
            var combine = Path.Combine(DataLocations.DataPath, "parties.json");
            File.Delete(combine);
            JsonSerializer.Serialize(dictionary, combine);
        }
        catch (Exception exception)
        {
            throw new Exception($"Failed to parse {htmlPath} {htmlPath}", exception);
        }
    }
}