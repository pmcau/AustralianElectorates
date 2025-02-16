﻿public class MediaFeedTests
{
    [Fact]
    public void Run()
    {
        foreach (var contest in MediaFeedService.HouseOfReps.Contests)
        {
            var electorate = contest.ContestIdentifier.ContestName;
            Trace.WriteLine(electorate);
            var candidatePreferred = contest.TwoCandidatePreferred;
            foreach (var candidate in candidatePreferred.Candidate)
            {
                if (candidate.AffiliationIdentifier == null)
                {
                    Trace.WriteLine($"    {candidate.CandidateIdentifier.CandidateName} {candidate.Votes.Swing}");
                }
                else
                {
                    Trace.WriteLine($"    {candidate.AffiliationIdentifier.ShortCode} {candidate.Votes.Swing}");
                }
            }
        }
    }
}