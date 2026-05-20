namespace AustralianElectorates;

public static class MapCollectionExtensions
{
    static ConcurrentDictionary<MapCollection, Lazy<ElectorateLocator>> locators = new();

    public static IElectorate? LocateElectorate(this MapCollection maps, double latitude, double longitude, int? postcode = null)
    {
        var locator = locators.GetOrAdd(maps, _ => new(() => new(_.GetAustralia())));
        return locator.Value.Find(latitude, longitude, postcode);
    }
}
