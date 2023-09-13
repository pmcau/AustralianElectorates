using System.Globalization;
using AecModels;
using AustralianElectorates;
using Officer = AustralianElectorates.Officer;
using Address = AustralianElectorates.Address;
using Branch = AustralianElectorates.Branch;
using State = AustralianElectorates.State;

static class PartyScraper
{
    public static async Task Run()
    {
        var codes = await PartyCodeScraper.Run();
        var htmlPath = Path.Combine(DataLocations.TempPath, "partycodes.html");
        var partyRegisterPath = Path.Combine(DataLocations.TempPath, "partyRegister.json");
        File.Delete(htmlPath);
        File.Delete(partyRegisterPath);
        var url = "https://www.aec.gov.au/parties_and_representatives/party_registration/Registered_parties/";
        try
        {
            await Downloader.DownloadFile(htmlPath, url);

            var jsonUrl = (await File.ReadAllLinesAsync(htmlPath))
                .Single(_ => _.Contains("/Parties_and_Representatives/Party_Registration/Registered_parties/files/register"))
                .Split('"')[1];
            await Downloader.DownloadFile(partyRegisterPath, $"https://www.aec.gov.au{jsonUrl}");
            var aecParties = JsonSerializerService.Deserialize<PartyData>(partyRegisterPath);
            var parties = new List<Party>();
            foreach (var detail in aecParties.Details)
            {
                var party = DetailToParty(detail, codes);
                parties.Add(party);
            }

            File.Delete(DataLocations.PartiesJsonPath);
            JsonSerializerService.Serialize(parties, DataLocations.PartiesJsonPath);
        }
        catch (Exception exception)
        {
            throw new($"Failed to parse {htmlPath} {htmlPath}", exception);
        }
    }

    public static ushort? FindPartyId(string code, List<Party> parties)
    {
        foreach (var party in parties)
        {
            if (party.Code == code)
            {
                return party.Id;
            }

            if (party.Abbreviation == code)
            {
                return party.Id;
            }
        }

        foreach (var party in parties)
        {
            foreach (var branch in party.Branches)
            {
                if (branch.Code == code)
                {
                    return branch.Id;
                }

                if (branch.Abbreviation == code)
                {
                    return branch.Id;
                }
            }
        }

        return null;
    }

    static Party DetailToParty(Detail detail, Dictionary<string, string> codes)
    {
        var abbreviation = detail.Abbreviation?.Replace(".", "");
        var code = GetCode(detail.NameOfParty, abbreviation, null, codes);
        return new()
        {
            Id = detail.Id,
            Name = detail.NameOfParty,
            Code = code,
            Abbreviation = abbreviation ?? detail.NameOfParty,
            RegisterDate = detail.PartyRegisterDate,
            AmendmentDate = detail.PartyRegisterDate,
            Address = detail.PostalAddress,
            Officer = ToOfficer(detail.Officer),
            DeputyOfficers = ToOfficers(detail.DeputyOfficers),
            Branches = ToBranches(detail.Branches, code, codes)
        };
    }

    static string GetCode(string name, string? abbreviation, string? parentCode, Dictionary<string, string> codes)
    {
        if (codes.TryGetKey(name, out var key))
        {
            return key;
        }

        if (parentCode != null)
        {
            return parentCode;
        }

        if (abbreviation != null)
        {
            return abbreviation;
        }

        return name;
    }

    static List<Branch> ToBranches(AecModels.Branch[] branches, string partyCode, Dictionary<string, string> codes) =>
        branches
            .Select(branch => ToBranch(branch, partyCode, codes))
            .ToList();

    static Branch ToBranch(AecModels.Branch branch, string partyCode, Dictionary<string, string> codes)
    {
        var abbreviation = branch.Abbreviation?.Replace(".", "");
        return new()
        {
            Id = branch.Id,
            Name = branch.NameOfParty,
            Abbreviation = abbreviation ?? branch.NameOfParty,
            Code = GetCode(branch.NameOfParty, abbreviation, partyCode,codes),
            RegisterDate = branch.PartyRegisterDate,
            AmendmentDate = branch.PartyRegisterDate,
            Address = branch.PostalAddress,
            Officer = ToOfficer(branch.Officer),
            DeputyOfficers = ToOfficers(branch.DeputyOfficers)
        };
    }

    static List<Officer> ToOfficers(AecModels.Officer[]? detail)
    {
        if (detail == null)
        {
            return new();
        }
        return detail.Select(ToOfficer).ToList();
    }

    static Officer ToOfficer(AecModels.Officer deputyOfficer) =>
        new()
        {
            Capacity = deputyOfficer.Capacity,
            FamilyName = deputyOfficer.Surname,
            GivenNames = deputyOfficer.FirstName,
            Title = deputyOfficer.Title,
            Address = ToAddress(deputyOfficer.Address)
        };

    static Address? ToAddress(AecModels.Address address)
    {
        if (address.Line1 == null &&
            address.Line2 == null  &&
            address.Line3 == null  &&
            address.State == null  &&
            address.Postcode == null  &&
            address.Suburb == null)
        {
            return null;
        }
        var line1 = address.Line1;
        if (string.IsNullOrWhiteSpace(line1))
        {
            throw new();
        }

        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        string? line2 = address.Line2;
        if (string.IsNullOrWhiteSpace(line2))
        {
            line2 = null;
        }

        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        string? line3 = address.Line3;
        if (string.IsNullOrWhiteSpace(line3))
        {
            line3 = null;
        }

        return new()
        {
            State = (State) Enum.Parse(typeof(State), address.State!),
            Line1 = line1,
            Line2 = line2,
            Line3 = line3,
            Postcode =  Convert.ToInt32(address.Postcode),
            Suburb = FixSuburbCase(address,address.Postcode!),
        };
    }

    static string FixSuburbCase(AecModels.Address deputyOfficerAddress, string postcode)
    {
        var suburbs = AustraliaData.PostCodes.Single(_ => _.Key == postcode).Value;
        var suburb = deputyOfficerAddress.Suburb.Trim();
        var place = suburbs.SingleOrDefault(_ => string.Equals(_.Name, suburb, StringComparison.OrdinalIgnoreCase));
        if (place == null)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(suburb);
        }
        return place.Name!;
    }
}