﻿using HtmlAgilityPack;

static class ElectoratesScraper
{
    // public static Task<ElectorateEx> ScrapeCurrentElectorate(string shortName, State state)
    // {
    //     //TODO: split this out to a ScrapeCurrentElectorate and a Scrape2019Electorate when aec archives 2019
    //     // and also remove the NT hacks
    //     var requestUri = $"https://www.aec.gov.au/profiles/{state}/{shortName}.htm";
    //     if (shortName is "lingiari" or "solomon")
    //     {
    //         return Scrape2016Electorate(shortName, state);
    //     }
    //
    //     return ScrapeElectorate(shortName, state, requestUri, "Profile of the electoral division of ");
    // }

    public static async Task<ElectorateEx> ScrapeElectorate(int year, string shortName, State state)
    {
        var tempElectorateHtmlPath = Path.Combine(DataLocations.TempPath, $"{shortName}.html");

        var requestUri = $"https://www.aec.gov.au/{shortName}";
        try
        {
            requestUri = await Downloader.DownloadFile(tempElectorateHtmlPath, requestUri);

            // because AEC does not return a 404 for missing electorates
            var textAsync = await File.ReadAllTextAsync(tempElectorateHtmlPath);
            if (textAsync.Contains("Sauce not found"))
            {
                requestUri = $"https://www.aec.gov.au/Elections/federal_elections/{year}/profiles/{state.ToString().ToLowerInvariant()}/{shortName}.htm";
                //requestUri = $"https://www.aec.gov.au/profiles/{state}/{shortName}.htm";
                requestUri = await Downloader.DownloadFile(tempElectorateHtmlPath, requestUri);
            }

            var document = new HtmlDocument();
            document.Load(tempElectorateHtmlPath);
            var fullName = GetFullName(document, year);
            var values = new Dictionary<string, HtmlNode>(StringComparer.OrdinalIgnoreCase);
            var profileId = FindProfileTable(document);
            var htmlNodeCollection = profileId.SelectNodes("dt")!;
            foreach (var keyNode in htmlNodeCollection)
            {
                var valueNode = keyNode.NextSibling.NextSibling;
                values[keyNode
                    .InnerText.Trim()
                    .Trim(':')
                    .Replace("  ", " ")] = valueNode;
            }

            var electorate = new ElectorateEx
            {
                Name = fullName,
                ShortName = shortName,
                State = state
            };

            if (values.TryGetValue("Date this name and boundary was gazetted", out var gazettedHtml))
            {
                electorate.DateGazetted = Date.ParseExact(gazettedHtml.InnerText, "d MMMM yyyy", null);
            }

            var contest = MediaFeedService.HouseOfReps.Contests.SingleOrDefault(_ => _.ContestIdentifier.ContestName == fullName);
            if (contest != null)
            {
                electorate.Enrollment = contest.Enrolment.Value;
                var candidatePreferred = contest.TwoCandidatePreferred;
                var electedCandidate = candidatePreferred.Candidate.Single(_ => _.Elected.Value);
                var electedCandidateName = SplitName(electedCandidate.CandidateIdentifier.CandidateName);

                var other = candidatePreferred.Candidate.Single(_ => !_.Elected.Value);
                var otherName = SplitName(other.CandidateIdentifier.CandidateName);

                var affiliationIdentifier = electedCandidate.AffiliationIdentifier;
                electorate.TwoCandidatePreferred = new TwoCandidatePreferred
                {
                    Elected = new Candidate
                    {
                        FamilyName = electedCandidateName.familyName,
                        GivenNames = electedCandidateName.givenNames,
                        PartyCode = affiliationIdentifier?.ShortCode,
                        PartyId = affiliationIdentifier?.Id,
                        Votes = electedCandidate.Votes.Value,
                        Swing = electedCandidate.Votes.Swing
                    },
                    Other = new Candidate
                    {
                        FamilyName = otherName.familyName,
                        GivenNames = otherName.givenNames,
                        PartyCode = other.AffiliationIdentifier?.ShortCode,
                        PartyId = other.AffiliationIdentifier?.Id,
                        Votes = other.Votes.Value,
                        Swing = other.Votes.Swing
                    }
                };
            }

            electorate.DemographicRating = values["Demographic Rating"]
                .TrimmedInnerHtml();

            var uri = new Uri(new(requestUri), FindMapUrl(values));
            electorate.MapUrl = uri.AbsoluteUri;

            electorate.NameDerivation = values["Name derivation"]
                .TrimmedInnerHtml();
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
                electorate.Area = double.Parse(area
                    .InnerHtml.Trim()
                    .Replace("&nbsp;", " ")
                    .Replace(" ", "")
                    .Replace("sqkm", "")
                    .Replace(",", ""));
            }

            return electorate;
        }
        catch (Exception exception)
        {
            throw new($"Failed to parse {shortName} {tempElectorateHtmlPath}. Uri: {requestUri}", exception);
        }
    }

    static string FindMapUrl(Dictionary<string, HtmlNode> values)
    {
        var mapsNode = FindMapsNode(values);
        return mapsNode
            .ChildNodes
            .FindFirst("a")
            .Attributes["href"]
            .Value
            .Replace("http://", "https://");
    }

    static HtmlNode FindMapsNode(Dictionary<string, HtmlNode> values)
    {
        if (values.TryGetValue("Map of Division", out var node))
        {
            return node;
        }

        if (values.TryGetValue("Maps of Division", out node))
        {
            return node;
        }

        if (values.TryGetValue("Map and data", out node))
        {
            return node;
        }

        if (values.TryGetValue("Maps and data", out node))
        {
            return node;
        }

        if (values.TryGetValue("Maps &amp; GIS data", out node))
        {
            return node;
        }

        throw new("FindMapsNode");
    }

    static string GetFullName(HtmlDocument document, int year)
    {
        var prefix1 = $"{year} federal election: profile of the electoral division of ";
        var prefix2 = "Profile of the electoral division of ";
        var headings = document.Headings();
        var caseless = headings
            .Single(_ => _.StartsWith(prefix1) || _.StartsWith(prefix2))
            .ReplaceCaseless(prefix1, "")
            .ReplaceCaseless(prefix2, "");
        return TrimState(caseless);
    }

    static string TrimState(string caseless)
    {
        var strings = caseless
            .Split([" ("], StringSplitOptions.None);
        var fullName = strings[0];
        Assert.NotEmpty(fullName);
        return fullName;
    }

    static IEnumerable<ushort> FindPartyIds(string[] partyIds, List<Party> parties)
    {
        foreach (var id in partyIds)
        {
            var findPartyId = PartyScraper.FindPartyId(id, parties);
            if (findPartyId != null)
            {
                yield return findPartyId.Value;
            }
        }
    }

    static (string familyName, string givenNames) SplitName(string member)
    {
        var memberSplit = member.Split(',');
        var familyName = memberSplit[0]
            .ToTitleCase();
        var givenNames = memberSplit[1]
            .Trim();
        return (familyName, givenNames);
    }

    static HtmlNode FindProfileTable(HtmlDocument document) =>
        document.DocumentNode
            .SelectSingleNode("//comment()[contains(., ' InstanceBeginEditable name=\"Content\" ')]/following-sibling::dl")!;
}