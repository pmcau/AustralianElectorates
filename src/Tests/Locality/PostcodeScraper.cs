using CountryData;
using HtmlAgilityPack;

static class PostcodeScraper
{
    static HttpClient client = new();
    static HtmlDocument doc = new();

    public static async Task<List<AecLocalityData>> Run()
    {
        List<AecLocalityData> items = new();
        foreach (var postcode in AustraliaData.PostCodes)
        {
            items.AddRange(await GetAECDataForPostcode(postcode));
        }

        return items;
    }

    static IEnumerable<AecLocalityData> GetLocalityData(HtmlDocument doc, int postcode)
    {
        var table = doc.DocumentNode.SelectSingleNode("//table[@id='ContentPlaceHolderBody_gridViewLocalities']");

        foreach (var tr in table.SelectNodes("tr").Where(p => !p.HasAttributes))
        {
            var tds = tr.SelectNodes("td");
            if (tds != null && tds.Count > 3)
            {
                yield return new(
                    //State = tds[0].InnerText.ToUpper().Trim(),
                    place: tds[1].InnerText.ToUpper().Trim().ToTitleCase(),
                    postcode: postcode,
                    //Postcode = tds[2].InnerText.ToUpper().Trim(),
                    electorate: tds[3].InnerText.ToUpper().Trim()
                );
            }
        }
    }

    static Dictionary<string, string> ParseFormForParameters(HtmlDocument doc)
    {
        Dictionary<string, string> parameters = new();
        var form = doc.DocumentNode.SelectSingleNode("//form[@id='formMaster']");

        var inputs = form.SelectNodes("input[@type='hidden']");

        if (inputs != null)
        {
            foreach (var input in inputs)
            {
                parameters.Add(input.Attributes["name"].Value, input.Attributes["value"].Value);
            }
        }

        return parameters;
    }

    static int GetPageCount(HtmlDocument doc)
    {
        var table = doc.DocumentNode.SelectSingleNode("//table[@id='ContentPlaceHolderBody_gridViewLocalities']");
        var nodes = table.SelectNodes("tr[@class='pagingLink']//a");
        var nodeCount = nodes?.Count ?? 0;
        return nodeCount + 1;
    }

    public static async Task<List<AecLocalityData>> GetAECDataForPostcode(KeyValuePair<string, IReadOnlyList<IPlace>> postcode)
    {
        if (!int.TryParse(postcode.Key, out var result))
        {
            throw new("Invalid Postcode");
        }

        if (result < 0 || result > 9999)
        {
            throw new("Invalid Postcode");
        }

        if (result % 50 == 0)
        {
            Trace.WriteLine(postcode.Key);
        }

        var url = $"https://electorate.aec.gov.au/LocalitySearchResults.aspx?filter={result:D4}&filterby=Postcode";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        doc.LoadHtml(content);

        List<AecLocalityData> data = new();

        data.AddRange(GetLocalityData(doc, result));
        var lastPage = GetPageCount(doc);

        var page = 1;
        while (page != lastPage)
        {
            page++;

            var parameters = ParseFormForParameters(doc);
            parameters["__EVENTTARGET"] = "ctl00$ContentPlaceHolderBody$gridViewLocalities";
            parameters["__EVENTARGUMENT"] = $"Page${page}";

#pragma warning disable 8620
            FormUrlEncodedContent encodedContent = new(parameters);
#pragma warning restore 8620

            response = await client.PostAsync(url, encodedContent);
            response.EnsureSuccessStatusCode();
            doc.LoadHtml(await response.Content.ReadAsStringAsync());
            data.AddRange(GetLocalityData(doc, result));
        }

        return data;
    }
}