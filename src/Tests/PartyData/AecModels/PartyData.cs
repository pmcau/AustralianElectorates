#nullable disable
using Newtonsoft.Json;

namespace AecModels;

public class PartyData
{
    [JsonProperty("Details")]
    public Detail[] Details { get; set; }
}