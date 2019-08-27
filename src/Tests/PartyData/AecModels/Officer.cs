using Newtonsoft.Json;

namespace AecModels
{
    public class Officer
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}