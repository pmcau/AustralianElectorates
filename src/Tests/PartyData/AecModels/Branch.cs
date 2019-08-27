using Newtonsoft.Json;

namespace AecModels
{
    public class Branch
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nameOfParty")]
        public string NameOfParty { get; set; }

        [JsonProperty("partyRegisterDate")]
        public string PartyRegisterDate { get; set; }

        [JsonProperty("ammendmentDate")]
        public string AmmendmentDate { get; set; }

        [JsonProperty("logoDataUri")]
        public string LogoDataUri { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("isParliamentaryParty")]
        public bool IsParliamentaryParty { get; set; }

        [JsonProperty("electionFundingPayments")]
        public bool ElectionFundingPayments { get; set; }

        [JsonProperty("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("officerName")]
        public string OfficerName { get; set; }

        [JsonProperty("officerAddress")]
        public string OfficerAddress { get; set; }

        [JsonProperty("officer")]
        public Officer Officer { get; set; }

        [JsonProperty("deputyOfficerNames")]
        public string DeputyOfficerNames { get; set; }

        [JsonProperty("deputyOfficers")]
        public Officer[] DeputyOfficers { get; set; }
    }
}