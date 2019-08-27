using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AecModels;
using AustralianElectorates;
using HtmlAgilityPack;
using Officer = AustralianElectorates.Officer;
using Address = AustralianElectorates.Address;

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

    private static Party DetailToParty(Detail detail)
    {
        var officer = detail.Officer;
        var officerAddress = officer.Address;
        var party = new Party
        {
            Name = detail.NameOfParty,
            Abbreviation = detail.Abbreviation,
            RegisterDate = detail.PartyRegisterDate,
            AmendmentDate = detail.PartyRegisterDate,
            Address = detail.PostalAddress,
            Officer = new Officer
            {
                Capacity = officer.Capacity,
                FamilyName = officer.Surname,
                GivenNames = officer.FirstName,
                Title = officer.Title,
                Address = new Address
                {
                    State = (State) Enum.Parse(typeof(State), officerAddress.State),
                    Line1 = officerAddress.Line1,
                    Line2 = officerAddress.Line2,
                    Line3 = officerAddress.Line3,
                    Postcode = Convert.ToInt32(officerAddress.Postcode),
                    Suburb = officerAddress.Suburb,
                },
            },
        };
        if (detail.DeputyOfficers != null)
        {
            party.DeputyOfficers = new List<Officer>();
            foreach (var deputyOfficer in detail.DeputyOfficers)
            {
                var deputyOfficerAddress = deputyOfficer.Address;
                party.DeputyOfficers.Add(
                    new Officer
                    {
                        Capacity = deputyOfficer.Capacity,
                        FamilyName = deputyOfficer.Surname,
                        GivenNames = deputyOfficer.FirstName,
                        Title = deputyOfficer.Title,
                        Address = new Address
                        {
                            State = (State) Enum.Parse(typeof(State), deputyOfficerAddress.State),
                            Line1 = deputyOfficerAddress.Line1,
                            Line2 = deputyOfficerAddress.Line2,
                            Line3 = deputyOfficerAddress.Line3,
                            Postcode = Convert.ToInt32(deputyOfficerAddress.Postcode),
                            Suburb = deputyOfficerAddress.Suburb,
                        },
                    });
            }

        }
            return party;
    }
}