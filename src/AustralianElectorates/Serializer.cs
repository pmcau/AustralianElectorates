using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

static class Serializer
{
    public static T Deserialize<T>(Stream stream)
    {
        var serializerSettings = new DataContractJsonSerializerSettings
        {
            DateTimeFormat = new DateTimeFormat("yyyy-MM-dd")
        };
        var jsonSerializer = new DataContractJsonSerializer(typeof(T), serializerSettings);
        var readToEnd = ReadToEnd(stream);
        readToEnd = readToEnd
                .Replace("\"State\": \"ACT\",", "\"State\": 0,")
                .Replace("\"State\": \"NSW\",", "\"State\": 1,")
                .Replace("\"State\": \"NT\",", "\"State\": 2,")
                .Replace("\"State\": \"SA\",", "\"State\": 3,")
                .Replace("\"State\": \"QLD\",", "\"State\": 4,")
                .Replace("\"State\": \"TAS\",", "\"State\": 5,")
                .Replace("\"State\": \"VIC\",", "\"State\": 6,")
                .Replace("\"State\": \"WA\",", "\"State\": 7,")
            ;
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(readToEnd)) {Position = 0};
        return (T) jsonSerializer.ReadObject(memoryStream);
    }

    static string ReadToEnd(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}