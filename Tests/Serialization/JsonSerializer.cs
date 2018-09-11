using System.IO;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

static class JsonSerializer
{
    static Newtonsoft.Json.JsonSerializer jsonSerializer;

    static JsonSerializer()
    {
        jsonSerializer = new Newtonsoft.Json.JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            ContractResolver = new CustomContractResolver()
        };
        jsonSerializer.Converters.Add(new StringEnumConverter());
    }

    static JsonSerializerSettings geoSettings = new JsonSerializerSettings
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
        using (var fileStream = File.OpenWrite(path))
        using (var textWriter = new StreamWriter(fileStream))
        using (var jsonTextWriter = new JsonTextWriter(textWriter))
        {
            jsonTextWriter.Indentation = 0;
            jsonSerializer.Serialize(jsonTextWriter, value);
        }
    }

    public static FeatureCollection DeserializeGeo(string path)
    {
        return Deserialize<FeatureCollection>(path);
    }

    public static T Deserialize<T>(string path)
    {
        using (var fileStream = File.OpenRead(path))
        using (var textReader = new StreamReader(fileStream))
        using (var jsonTextReader = new JsonTextReader(textReader))
        {
            return jsonSerializer.Deserialize<T>(jsonTextReader);
        }
    }
}