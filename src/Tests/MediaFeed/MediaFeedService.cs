using System.Xml.Serialization;

static class MediaFeedService
{
    static MediaFeedService()
    {
        using var reader = File.OpenText(@"MediaFeed\aec-mediafeed-results-standard-verbose-24310.xml");
        XmlSerializer serializer = new(typeof(MediaFeed));
        Feed = (MediaFeed)serializer.Deserialize(reader)!;
        HouseOfReps = GetHouseOfReps(Feed);
    }

    public static MediaFeedResultsElectionHouse HouseOfReps;

    public static MediaFeed Feed;

    static MediaFeedResultsElectionHouse GetHouseOfReps(MediaFeed feed)
    {
        return feed.Results.Election
            .Where(x => x.ElectionIdentifier.Id == "H")
            .Single()
            .House;
    }

}