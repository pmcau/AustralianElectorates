using System.IO;
using System.Text.Json;
using AustralianElectorates;

static class Serializer
{
    public static T Deserialize<T>(Stream stream)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new StateConverter());
        options.Converters.Add(new InterfaceConverter<TwoCandidatePreferred, ITwoCandidatePreferred>());
        options.Converters.Add(new InterfaceConverter<Address, IAddress>());
        options.Converters.Add(new InterfaceConverter<Candidate, ICandidate>());
        options.Converters.Add(new InterfaceConverter<Election, IElection>());
        options.Converters.Add(new InterfaceConverter<ElectorateMap, IElectorateMap>());
        options.Converters.Add(new InterfaceConverter<Location, ILocation>());
        options.Converters.Add(new InterfaceConverter<Member, IMember>());
        options.Converters.Add(new InterfaceConverter<Officer, IOfficer>());
        options.Converters.Add(new InterfaceConverter<Party, IParty>());
        options.Converters.Add(new InterfaceConverter<Branch, IBranch>());
        options.Converters.Add(new InterfaceConverter<StateMap, IStateMap>());
        return JsonSerializer.Deserialize<T>(ReadToEnd(stream), options);
    }

    static string ReadToEnd(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}