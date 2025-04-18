﻿class StateConverter :
    JsonConverter<State>
{
    public override bool CanConvert(Type type) =>
        type == typeof(State);

    public override State Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<State>(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, State value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}