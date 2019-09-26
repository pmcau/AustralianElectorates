using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AustralianElectorates;
using HtmlAgilityPack;
using Xunit;

static class ElectoratesScraper
{
    public static Task<ElectorateEx> ScrapeCurrentElectorate(string shortName, State state, List<Party> parties)
    {
        var requestUri = $"https://www.aec.gov.au/profiles/{state}/{shortName}.htm";
        return ScrapeElectorate(shortName, state, requestUri, "Profile of the electoral division of ", parties);
    }

    public static Task<ElectorateEx> Scrape2016Electorate(string shortName, State state, List<Party> parties)
    {
        var requestUri = $"https://www.aec.gov.au/Elections/federal_elections/2016/profiles/{state}/{shortName}.htm";
        return ScrapeElectorate(shortName, state, requestUri, "2016 federal election: profile of the electoral division of ", parties);
    }

    static async Task<ElectorateEx> ScrapeElectorate(string shortName, State state, string requestUri, string prefix, List<Party> parties)
    {
        requestUri = requestUri.ToLowerInvariant();
        var tempElectorateHtmlPath = Path.Combine(DataLocations.TempPath, $"{shortName}.html");
        try
        {
            await Downloader.DownloadFile(tempElectorateHtmlPath, requestUri);

            if (!File.Exists(tempElectorateHtmlPath))
            {
                throw new Exception($"Could not download {shortName}");
            }

            var document = new HtmlDocument();
            document.Load(tempElectorateHtmlPath);

            var fullName = GetFullName(document, prefix);
            var values = new Dictionary<string, HtmlNode>(StringComparer.OrdinalIgnoreCase);
            var profileId = FindProfileTable(document);
            var htmlNodeCollection = profileId.SelectNodes("dt");
            foreach (var keyNode in htmlNodeCollection)
            {
                var valueNode = keyNode.NextSibling.NextSibling;
                values[keyNode.InnerText.Trim().Trim(':').Replace("  ", " ")] = valueNode;
            }

            var electorate = new ElectorateEx
            {
                Name = fullName,
                ShortName = shortName,
                State = state
            };

            if (values.TryGetValue("Date this name and boundary was gazetted", out var gazettedHtml))
            {
                electorate.DateGazetted = DateTime.ParseExact(gazettedHtml.InnerText, "d MMMM yyyy", null);
            }

            var electorateMembers = ElectorateMembers(values, parties).ToList();
            var first = electorateMembers.FirstOrDefault();
            if (first != null && first.End == null)
            {
                first.End = 2019;
            }

            var contest = MediaFeedService.HouseOfReps.Contests.SingleOrDefault(x => x.ContestIdentifier.ContestName == fullName);
            if (contest != null)
            {
                electorate.Enrollment = contest.Enrolment.Value;
                var candidatePreferred = contest.TwoCandidatePreferred;
                var electedCandidate = candidatePreferred.Candidate.Single(x => x.Elected.Value);
                var electedCandidateName = SplitName(electedCandidate.CandidateIdentifier.CandidateName);

                var other = candidatePreferred.Candidate.Single(x => !x.Elected.Value);
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
                        Swing = electedCandidate.Votes.Swing,
                    },
                    Other = new Candidate
                    {
                        FamilyName = otherName.familyName,
                        GivenNames = otherName.givenNames,
                        PartyCode = other.AffiliationIdentifier?.ShortCode,
                        PartyId = other.AffiliationIdentifier?.Id,
                        Votes = other.Votes.Value,
                        Swing = other.Votes.Swing,
                    }
                };

                var familyName = electedCandidateName.familyName.ToTitleCase();

                if (first != null &&
                    first.FamilyName == familyName &&
                    first.GivenNames[0] == electedCandidateName.givenNames[0]
                    )
                {
                    first.End = null;
                    first.GivenNames = electedCandidateName.givenNames;
                }
                else
                {
                    var member = new Member
                    {
                        GivenNames = electedCandidateName.givenNames,
                        FamilyName = familyName,
                        Begin = 2019
                    };
                    if (affiliationIdentifier != null)
                    {
                        member.PartyIds = new List<ushort> { affiliationIdentifier.Id};
                        member.PartyCodes =new List<string> { affiliationIdentifier.ShortCode};
                    }

                    electorateMembers.Insert(0,
                    member);
                }
            }

            electorate.Members = electorateMembers;
            electorate.DemographicRating = values["Demographic Rating"].TrimmedInnerHtml();
            electorate.ProductsAndIndustry = values["Products/Industries of the Area"].TrimmedInnerHtml();


            var uri = new Uri(new Uri(requestUri),FindMapUrl(values));
            electorate.MapUrl = uri.AbsoluteUri;

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
            throw new Exception($"Failed to parse {shortName} {tempElectorateHtmlPath}", exception);
        }
    }

    static string FindMapUrl(Dictionary<string, HtmlNode> values)
    {
        var mapsNode = FindMapsNode(values);
        return mapsNode.ChildNodes.FindFirst("a").Attributes["href"].Value;
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

        throw new Exception("FindMapsNode");
    }

    static string GetFullName(HtmlDocument document, string prefix)
    {
        var headings = document.Headings();
        var caseless = headings.Single(x => x.StartsWith(prefix))
            .ReplaceCaseless(prefix, "");
        return TrimState(caseless);
    }

    static string TrimState(string caseless)
    {
        var strings = caseless
            .Split(new[] {" ("}, StringSplitOptions.None);
        var fullName = strings[0];
        Assert.NotEmpty(fullName);
        return fullName;
    }

    static IEnumerable<Member> ElectorateMembers(Dictionary<string, HtmlNode> values, List<Party> parties)
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
            var cleaned = text.TrimEnd('(').TrimmedInnerHtml();
            if (cleaned.Length == 0)
            {
                continue;
            }

            var split = cleaned.Split(new[] {" ("}, 2, StringSplitOptions.None);
            var member = split[0];
            split = split[1].Split(new[] {") "}, 2, StringSplitOptions.None);
            var partyIds = split[0].Split('/');

            split = split[1].Split(new[] {"-","–"}, 2, StringSplitOptions.RemoveEmptyEntries);
            var trim = split[0].Trim();
            var begin = ushort.Parse(trim);
            ushort? end = null;
            if (split.Length > 1)
            {
                end = ushort.Parse(split[1].Trim());
            }

            var (familyName, givenNames) = SplitName(member);
            yield return new Member
            {
                FamilyName = familyName,
                GivenNames = givenNames,
                PartyCodes = partyIds.ToList(),
                PartyIds = FindPartyIds(partyIds, parties).ToList(),
                Begin = begin,
                End = end,
            };
        }
    }

    static IEnumerable<ushort> FindPartyIds(string[] partyIds, List<Party> parties)
    {
        foreach (var id in partyIds)
        {
            var findPartyId = PartyScraper.FindPartyId(id, parties);
            if (findPartyId.HasValue)
            {
                yield return findPartyId.Value;
            }
        }
    }

    static (string familyName, string givenNames) SplitName(string member)
    {
        var memberSplit = member.Split(',');
        var familyName = memberSplit[0].ToTitleCase();
        var givenNames = memberSplit[1].Trim();
        return (familyName, givenNames);
    }

    static HtmlNode FindProfileTable(HtmlDocument document)
    {
        return document.DocumentNode.SelectSingleNode("//comment()[contains(., ' InstanceBeginEditable name=\"Content\" ')]/following-sibling::dl");
    }
}