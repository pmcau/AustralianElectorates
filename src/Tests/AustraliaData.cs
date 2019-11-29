using System.Collections.Generic;
using CountryData;

static class AustraliaData
{
    static AustraliaData()
    {
        Data = CountryLoader.LoadAustraliaLocationData();
        PostCodes = Data.PostCodes();
    }

    public static IReadOnlyDictionary<string, IReadOnlyList<IPlace>> PostCodes { get; }

    public static ICountry Data { get; }
}