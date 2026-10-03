using System.Xml.Serialization;

static class MediaFeedService
{
    static MediaFeedService()
    {
        //from here ftp://mediafeedarchive.aec.gov.au/
        using var reader = ProjectFiles.MediaFeed.aec_mediafeed_results_standard_verbose_27966_xml.OpenText();
        var serializer = new XmlSerializer(typeof(MediaFeed));
        Feed = (MediaFeed) serializer.Deserialize(reader)!;
        HouseOfReps = GetHouseOfReps(Feed);
    }

    public static MediaFeedResultsElectionHouse HouseOfReps;

    public static MediaFeed Feed;

    static MediaFeedResultsElectionHouse GetHouseOfReps(MediaFeed feed) =>
        feed.Results.Election
            .Single(_ => _.ElectionIdentifier.Id == "H")
            .House;
}