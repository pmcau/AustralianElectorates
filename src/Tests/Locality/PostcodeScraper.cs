using Xunit.Abstractions;

static class PostcodeScraper
{
    static HttpClient client = new();

    public static async Task<List<AecLocalityData>> Run(ITestOutputHelper outputHelper)
    {
        var items = new List<AecLocalityData>();
        var postcodes = AustraliaData.PostCodes.Keys.ToList();
        for (var index = 0; index < postcodes.Count; index++)
        {
            var postcode = postcodes[index];

            if (index % 250 == 0)
            {
                outputHelper.WriteLine($"{index} of {postcodes.Count}");
            }

            items.AddRange(await GetAECDataForPostcode(postcode));
        }

        return items;
    }

    static IEnumerable<AecLocalityData> GetLocalityData(HtmlDocument doc, int postcode)
    {
        var tablePath = "//table[@id='ContentPlaceHolderBody_gridViewLocalities']";
        var table = doc.DocumentNode.SelectSingleNode(tablePath);
        if (table == null)
        {
            throw new(
                $"""
                 Could not find table: {tablePath}
                 {doc}
                 """);
        }

        foreach (var tr in table
                     .SelectNodes("tr")
                     .Where(p => !p.HasAttributes))
        {
            var tds = tr.SelectNodes("td");
            if (tds is not { Count: > 3 })
            {
                continue;
            }

            var place = tds[1]
                .InnerText.ToUpper()
                .Trim()
                .ToTitleCase();

            // Select all <a> tags within the electorate cell
            var electorateLinks = tds[3].SelectNodes(".//a");
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (electorateLinks != null)
            {
                foreach (var link in electorateLinks)
                {
                    yield return new(
                        place: place,
                        postcode: postcode,
                        electorate: link.InnerText.ToUpper().Trim()
                    );
                }
            }
        }
    }

    static Dictionary<string, string> ParseFormForParameters(HtmlDocument doc)
    {
        var parameters = new Dictionary<string, string>();
        var form = doc.DocumentNode.SelectSingleNode("//form[@id='formMaster']");

        var inputs = form.SelectNodes("input[@type='hidden']");

        foreach (var input in inputs)
        {
            parameters.Add(input.Attributes["name"].Value, input.Attributes["value"].Value);
        }

        return parameters;
    }

    static int GetPageCount(HtmlDocument doc)
    {
        var table = doc.DocumentNode.SelectSingleNode("//table[@id='ContentPlaceHolderBody_gridViewLocalities']");
        var nodes = table.SelectNodes("tr[@class='pagingLink']//a");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (nodes != null)
        {
            return nodes.Count + 1;
        }

        return 1;
    }

    public static async Task<List<AecLocalityData>> GetAECDataForPostcode(string postcode)
    {
        var doc = new HtmlDocument();
        if (!int.TryParse(postcode, out var result))
        {
            throw new("Invalid Postcode");
        }

        if (result is < 0 or > 9999)
        {
            throw new("Invalid Postcode");
        }

        var url = $"https://electorate.aec.gov.au/LocalitySearchResults.aspx?filter={result:D4}&filterby=Postcode";

        using var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        try
        {
            doc.LoadHtml(content);

            var data = new List<AecLocalityData>();

            data.AddRange(GetLocalityData(doc, result));
            var lastPage = GetPageCount(doc);

            var page = 1;
            while (page != lastPage)
            {
                page++;

                var parameters = ParseFormForParameters(doc);
                parameters["__EVENTTARGET"] = "ctl00$ContentPlaceHolderBody$gridViewLocalities";
                parameters["__EVENTARGUMENT"] = $"Page${page}";

                var encodedContent = new FormUrlEncodedContent(parameters);

                using var pageResponse = await client.PostAsync(url, encodedContent);
                pageResponse.EnsureSuccessStatusCode();
                var pageContent = await pageResponse.Content.ReadAsStringAsync();

                var pageHtmlDoc = new HtmlDocument();
                pageHtmlDoc.LoadHtml(pageContent);
                data.AddRange(GetLocalityData(pageHtmlDoc, result));
            }

            return data;
        }
        catch (Exception exception)
        {
            throw new($"""
                       Failed.
                       {url}
                       {content}
                       """,
                exception);
        }
    }
}
