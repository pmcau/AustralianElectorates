using System.Diagnostics;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class MediaFeedTests :
    VerifyBase
{
    [Fact]
    public void Run()
    {
        foreach (var contest in MediaFeedService.HouseOfReps.Contests)
        {
            var electorate = contest.ContestIdentifier.ContestName;
            Debug.WriteLine(electorate);
            var candidatePreferred = contest.TwoCandidatePreferred;
            foreach (var candidate in candidatePreferred.Candidate)
            {
                if (candidate.AffiliationIdentifier == null)
                {
                    Debug.WriteLine($"    {candidate.CandidateIdentifier.CandidateName} {candidate.Votes.Swing}");
                }
                else
                {
                    Debug.WriteLine($"    {candidate.AffiliationIdentifier.ShortCode} {candidate.Votes.Swing}");
                }
            }
        }
    }

    public MediaFeedTests(ITestOutputHelper output) :
        base(output)
    {
    }
}