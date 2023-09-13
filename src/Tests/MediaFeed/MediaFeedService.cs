using System.Xml.Serialization;

static class MediaFeedService
{
    static MediaFeedService()
    {
        //from here ftp://mediafeedarchive.aec.gov.au/
        using var reader = File.OpenText(@"MediaFeed\aec-mediafeed-results-standard-verbose-27966.xml");
        var serializer = new XmlSerializer(typeof(MediaFeed));
        Feed = (MediaFeed)serializer.Deserialize(reader)!;
        HouseOfReps = GetHouseOfReps(Feed);
    }

    public static MediaFeedResultsElectionHouse HouseOfReps;

    public static MediaFeed Feed;

    static MediaFeedResultsElectionHouse GetHouseOfReps(MediaFeed feed) =>
        feed.Results.Election
            .Single(_ => _.ElectionIdentifier.Id == "H")
            .House;
}