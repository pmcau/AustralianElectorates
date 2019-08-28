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
    public static async Task Run()
    {
        await PartyCodeScraper.Run();
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
            Parties = new List<Party>();
            foreach (var detail in aecParties.Details)
            {
                var party = DetailToParty(detail);
                Parties.Add(party);
            }

            var combine = Path.Combine(DataLocations.DataPath, "parties.json");
            File.Delete(combine);
            JsonSerializerService.Serialize(Parties, combine);
        }
        catch (Exception exception)
        {
            throw new Exception($"Failed to parse {htmlPath} {htmlPath}", exception);
        }
    }

    public static ushort? FindPartyId(string code)
    {
        foreach (var party in Parties)
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

        foreach (var party in Parties)
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

    public static List<Party> Parties;

    static Party DetailToParty(Detail detail)
    {
        var abbreviation = detail.Abbreviation?.Replace(".", "");
        var code = GetCode(detail.NameOfParty, abbreviation, null);
        var party = new Party
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
            Branches = ToBranches(detail.Branches, code),
        };

        return party;
    }

    static string GetCode(string name, string abbreviation, string parentCode)
    {
        if (PartyCodeScraper.Codes.TryGetKey(name, out var key))
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

    static List<Branch> ToBranches(AecModels.Branch[] branches, string partyCode)
    {
        var list = new List<Branch>();
        if (branches == null)
        {
            return list;
        }

        foreach (var branch in branches)
        {
            var item = ToBranch(branch, partyCode);
            list.Add(item);
        }

        return list;
    }

    static Branch ToBranch(AecModels.Branch branch, string partyCode)
    {
        var abbreviation = branch.Abbreviation?.Replace(".", "");
        return new Branch
        {
            Id = branch.Id,
            Name = branch.NameOfParty,
            Abbreviation = abbreviation ?? branch.NameOfParty,
            Code = GetCode(branch.NameOfParty, abbreviation, partyCode),
            RegisterDate = branch.PartyRegisterDate,
            AmendmentDate = branch.PartyRegisterDate,
            Address = branch.PostalAddress,
            Officer = ToOfficer(branch.Officer),
            DeputyOfficers = ToOfficers(branch.DeputyOfficers)
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
            line1 = null;
        }

        var line2 = deputyOfficerAddress.Line2;
        if (string.IsNullOrWhiteSpace(line2))
        {
            line2 = null;
        }

        var line3 = deputyOfficerAddress.Line3;
        if (string.IsNullOrWhiteSpace(line3))
        {
            line3 = null;
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