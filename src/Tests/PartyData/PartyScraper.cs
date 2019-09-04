using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AecModels;
using AustralianElectorates;
using Officer = AustralianElectorates.Officer;
using Address = AustralianElectorates.Address;
using Branch = AustralianElectorates.Branch;

public static class PartyScraper
{
    public static async Task<List<Party>> Run()
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

            var jsonUrl = File.ReadAllLines(htmlPath)
                .Single(x => x.Contains("/Parties_and_Representatives/Party_Registration/Registered_parties/files/register"))
                .Split('"')[1];
            await Downloader.DownloadFile(partyRegisterPath, $"https://www.aec.gov.au{jsonUrl}");
            var aecParties = JsonSerializerService.Deserialize<PartyData>(partyRegisterPath);
            var parties = new List<Party>();
            foreach (var detail in aecParties.Details)
            {
                var party = DetailToParty(detail, codes);
                parties.Add(party);
            }

            var combine = Path.Combine(DataLocations.DataPath, "parties.json");
            File.Delete(combine);
            JsonSerializerService.Serialize(parties, combine);
            return parties;
        }
        catch (Exception exception)
        {
            throw new Exception($"Failed to parse {htmlPath} {htmlPath}", exception);
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
        var code = GetCode(detail.NameOfParty, abbreviation, null,codes);
        return new Party
        {
            Id = detail.Id,
            Name = detail.NameOfParty,
            Code = code,
            Abbreviation = abbreviation ?? detail.NameOfParty,
            RegisterDate = detail.PartyRegisterDate,
            AmendmentDate = detail.PartyRegisterDate,
            Address = detail.PostalAddress,
            Officer = ToOfficer(detail.Officer),
            deputyOfficers = ToOfficers(detail.DeputyOfficers),
            branches = ToBranches(detail.Branches, code,codes),
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

    static List<Branch> ToBranches(AecModels.Branch[] branches, string partyCode, Dictionary<string, string> codes)
    {
        var list = new List<Branch>();
        if (branches == null)
        {
            return list;
        }

        foreach (var branch in branches)
        {
            var item = ToBranch(branch, partyCode,codes);
            list.Add(item);
        }

        return list;
    }

    static Branch ToBranch(AecModels.Branch branch, string partyCode, Dictionary<string, string> codes)
    {
        var abbreviation = branch.Abbreviation?.Replace(".", "");
        return new Branch
        {
            Id = branch.Id,
            Name = branch.NameOfParty,
            Abbreviation = abbreviation ?? branch.NameOfParty,
            Code = GetCode(branch.NameOfParty, abbreviation, partyCode,codes),
            RegisterDate = branch.PartyRegisterDate,
            AmendmentDate = branch.PartyRegisterDate,
            Address = branch.PostalAddress,
            Officer = ToOfficer(branch.Officer),
            deputyOfficers = ToOfficers(branch.DeputyOfficers)
        };
    }

    static List<Officer> ToOfficers(AecModels.Officer[] detail)
    {
        var officers = new List<Officer>();
        if (detail == null)
        {
            return officers;
        }

        foreach (var deputyOfficer in detail)
        {
            var item = ToOfficer(deputyOfficer);
            officers.Add(item);
        }

        return officers;
    }

    static Officer ToOfficer(AecModels.Officer deputyOfficer)
    {
        return new Officer
        {
            Capacity = deputyOfficer.Capacity,
            FamilyName = deputyOfficer.Surname,
            GivenNames = deputyOfficer.FirstName,
            Title = deputyOfficer.Title,
            Address = ToAddress(deputyOfficer.Address),
        };
    }

    static Address ToAddress(AecModels.Address deputyOfficerAddress)
    {
        var line1 = deputyOfficerAddress.Line1;
        if (string.IsNullOrWhiteSpace(line1))
        {

            throw new Exception();
            // line1 = null;
        }

        var line2 = deputyOfficerAddress.Line2;
        if (string.IsNullOrWhiteSpace(line2))
        {

            throw new Exception();
            //    line2 = null;
        }

        var line3 = deputyOfficerAddress.Line3;
        if (string.IsNullOrWhiteSpace(line3))
        {
            throw new Exception();
            //line3 = null;
        }

        return new Address
        {
            State = (State) Enum.Parse(typeof(State), deputyOfficerAddress.State),
            Line1 = line1,
            Line2 = line2,
            Line3 = line3,
            Postcode = Convert.ToInt32(deputyOfficerAddress.Postcode),
            Suburb = deputyOfficerAddress.Suburb,
        };
    }
}