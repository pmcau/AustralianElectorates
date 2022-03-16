using Bogus;
using Bogus.Premium;

namespace AustralianElectorates.Bogus;

public static class LocationDataExtensions
{
    public static ElectorateDataSet AustralianElectorates(this Faker faker) =>
        ContextHelper.GetOrSet(faker, () => new ElectorateDataSet());

    static Random random = new();

    static T Random<T>(this IReadOnlyList<T> source)
    {
        var r = random.Next(source.Count);
        return source[r];
    }

    static IEnumerable<T> Random<T>(this IReadOnlyList<T> source, uint count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return source.Random();
        }
    }
}