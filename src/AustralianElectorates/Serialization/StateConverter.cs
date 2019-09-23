using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AustralianElectorates;

class StateConverter :
    JsonConverter<State>
{
    public override bool CanConvert(Type type)
    {
        return type == typeof(State);
    }
    public override State Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (State) Enum.Parse(typeof(State), reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, State value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}