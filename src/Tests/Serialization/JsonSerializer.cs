using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

static class JsonSerializerService
{
    static JsonSerializer jsonSerializer;

    static JsonSerializerService()
    {
        jsonSerializer = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            ContractResolver = new CustomContractResolver()
        };
        jsonSerializer.Converters.Insert(0, new CustomDateTimeConverter());
        jsonSerializer.Converters.Add(new StringEnumConverter());
    }

    static JsonSerializerSettings geoSettings = new()
    {
        ContractResolver = new CustomContractResolver()
    };

    public static void SerializeGeo(FeatureCollection value, string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        var serializeObject = JsonConvert.SerializeObject(value, geoSettings);
        serializeObject = serializeObject.Replace("{\"properties\":", "\r\n{\"properties\":");
        File.WriteAllText(path, serializeObject);
    }

    public static void Serialize(object value, string path)
    {
        using var fileStream = File.OpenWrite(path);
        using StreamWriter textWriter = new(fileStream);
        using JsonTextWriter jsonTextWriter = new(textWriter)
        {
            Indentation = 2
        };
        jsonSerializer.Serialize(jsonTextWriter, value);
    }

    public static FeatureCollection DeserializeGeo(string path) =>
        Deserialize<FeatureCollection>(path);

    public static T Deserialize<T>(string path)
    {
        using var fileStream = File.OpenRead(path);
        using StreamReader textReader = new(fileStream);
        using JsonTextReader jsonTextReader = new(textReader);
        return jsonSerializer.Deserialize<T>(jsonTextReader)!;
    }
}