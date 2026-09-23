// A Utf8JsonReader over a Stream, so only a buffer's worth of the json is in memory at a time.
// When the reader runs out part way through a token, the unread tail is moved to the front of
// the buffer, the rest is refilled from the stream, and a new reader carries on from the state
// of the old one.
ref struct JsonStreamReader
{
    Stream stream;
    byte[] buffer = new byte[64 * 1024];
    int length;
    bool isFinalBlock;
    Utf8JsonReader reader;

    public JsonStreamReader(Stream stream)
    {
        this.stream = stream;
        Refill();
    }

    public JsonTokenType Read()
    {
        while (!reader.Read())
        {
            if (isFinalBlock)
            {
                throw new JsonException("Unexpected end of json.");
            }

            Refill();
        }

        return reader.TokenType;
    }

    void Refill()
    {
        var consumed = (int) reader.BytesConsumed;
        var leftover = length - consumed;
        buffer.AsSpan(consumed, leftover).CopyTo(buffer);
        // a single token bigger than the buffer
        if (leftover == buffer.Length)
        {
            Array.Resize(ref buffer, buffer.Length * 2);
        }

        var read = stream.Read(buffer, leftover, buffer.Length - leftover);
        length = leftover + read;
        isFinalBlock = read == 0;
        reader = new(buffer.AsSpan(0, length), isFinalBlock, reader.CurrentState);
    }

    public int CurrentDepth => reader.CurrentDepth;

    public bool ValueTextEquals(ReadOnlySpan<byte> text) =>
        reader.ValueTextEquals(text);

    public string? GetString() =>
        reader.GetString();

    public double GetDouble() =>
        reader.GetDouble();

    // Skips the value of the property just read, however many refills it spans
    public void Skip()
    {
        if (Read() is not (JsonTokenType.StartObject or JsonTokenType.StartArray))
        {
            return;
        }

        // everything inside is deeper, down to the matching end
        var depth = reader.CurrentDepth;
        do
        {
            Read();
        } while (reader.CurrentDepth > depth);
    }
}
