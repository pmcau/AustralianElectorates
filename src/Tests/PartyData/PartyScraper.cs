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
            var list = new List<Party>();
            foreach (var detail in aecParties.Details)
            {
                var party = DetailToParty(detail);
                list.Add(party);
            }

            var combine = Path.Combine(DataLocations.DataPath, "parties.json");
            File.Delete(combine);
            JsonSerializerService.Serialize(list, combine);
        }
        catch (Exception exception)
        {
            throw new Exception($"Failed to parse {htmlPath} {htmlPath}", exception);
        }
    }

    static Party DetailToParty(Detail detail)
    {
        var party = new Party
        {
            Id = detail.Id,
            Name = detail.NameOfParty,
            Code = detail.Abbreviation?.Replace(".", ""),
            RegisterDate = detail.PartyRegisterDate,
            AmendmentDate = detail.PartyRegisterDate,
            Address = detail.PostalAddress,
            Officer = ToOfficer(detail.Officer),
            DeputyOfficers = ToOfficers(detail.DeputyOfficers),
            Branches = ToBranches(detail.Branches),
        };

        return party;
    }

    static List<Branch> ToBranches(AecModels.Branch[] branches)
    {
        if (branches == null)
        {
            return null;
        }

        var list = new List<Branch>();
        foreach (var branch in branches)
        {
            var item = ToBranch(branch);
            list.Add(item);
        }
        return list;
    }

    static Branch ToBranch(AecModels.Branch branch)
    {
        return new Branch
        {
            Id = branch.Id,
            Name = branch.NameOfParty,
            Code = branch.Abbreviation?.Replace(".", ""),
            RegisterDate = branch.PartyRegisterDate,
            AmendmentDate = branch.PartyRegisterDate,
            Address = branch.PostalAddress,
            Officer = ToOfficer(branch.Officer),
            DeputyOfficers = ToOfficers(branch.DeputyOfficers)
        };
    }
    static List<Officer> ToOfficers(AecModels.Officer[] detail)
    {
        if (detail == null)
        {
            return null;
        }

        var officers = new List<Officer>();
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
        return new Address
        {
            State = (State) Enum.Parse(typeof(State), deputyOfficerAddress.State),
            Line1 = deputyOfficerAddress.Line1,
            Line2 = deputyOfficerAddress.Line2,
            Line3 = deputyOfficerAddress.Line3,
            Postcode = Convert.ToInt32(deputyOfficerAddress.Postcode),
            Suburb = deputyOfficerAddress.Suburb,
        };
    }
}