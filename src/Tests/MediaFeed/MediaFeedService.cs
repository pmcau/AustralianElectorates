using System.Xml.Serialization;

static class MediaFeedService
{
    static MediaFeedService()
    {
        using var reader = File.OpenText(@"MediaFeed\aec-mediafeed-results-standard-verbose-24310.xml");
        var serializer = new XmlSerializer(typeof(MediaFeed));
        Feed = (MediaFeed)serializer.Deserialize(reader)!;
        HouseOfReps = GetHouseOfReps(Feed);
    }

    public static MediaFeedResultsElectionHouse HouseOfReps;

    public static MediaFeed Feed;

    static MediaFeedResultsElectionHouse GetHouseOfReps(MediaFeed feed) =>
        feed.Results.Election
            .Single(x => x.ElectionIdentifier.Id == "H")
            .House;
}