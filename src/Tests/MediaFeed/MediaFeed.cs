#nullable disable
using System.Xml.Serialization;
// ReSharper disable RedundantAttributeSuffix

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
[XmlRoot(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed", IsNullable = false)]
public class MediaFeed
{
    public MediaFeedManagingAuthority ManagingAuthority { get; set; }

    public string MessageLanguage { get; set; }

    public MediaFeedMessageGenerator MessageGenerator { get; set; }

    public MediaFeedCycle Cycle { get; set; }

    public MediaFeedResults Results { get; set; }

    [XmlAttribute]
    public string Id { get; set; }

    [XmlAttribute]
    public DateTime Created { get; set; }

    [XmlAttribute]
    public byte SchemaVersion { get; set; }

    [XmlAttribute]
    public byte EmlVersion { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedManagingAuthority
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AuthorityIdentifier AuthorityIdentifier { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class AuthorityIdentifier
{
    [XmlAttribute]
    public string Id { get; set; }

    [XmlText]
    public string Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedMessageGenerator
{
    public string Name { get; set; }

    public string Environment { get; set; }

    public string Site { get; set; }

    public string Server { get; set; }

    public string Platform { get; set; }

    public string Version { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedCycle
{
    [XmlAttribute]
    public DateTime Created { get; set; }

    [XmlText]
    public string Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResults
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public EventIdentifier EventIdentifier { get; set; }

    [XmlElement("Election")]
    public MediaFeedResultsElection[] Election { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }

    [XmlAttribute]
    public string Phase { get; set; }

    [XmlAttribute]
    public string Verbosity { get; set; }

    [XmlAttribute]
    public string Granularity { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class EventIdentifier
{
    public string EventName { get; set; }

    [XmlAttribute]
    public ushort Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElection
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ElectionIdentifier ElectionIdentifier { get; set; }

    public MediaFeedResultsElectionSenate Senate { get; set; }

    public MediaFeedResultsElectionHouse House { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class ElectionIdentifier
{
    public string ElectionName { get; set; }

    public string ElectionCategory { get; set; }

    [XmlAttribute]
    public string Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenate
{
    [XmlArrayItem("Contest", IsNullable = false)]
    public MediaFeedResultsElectionSenateContest[] Contests { get; set; }

    public MediaFeedResultsElectionSenateAnalysis Analysis { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContest
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ContestIdentifier ContestIdentifier { get; set; }

    public MediaFeedResultsElectionSenateContestStateIdentifier StateIdentifier { get; set; }

    public MediaFeedResultsElectionSenateContestEnrolment Enrolment { get; set; }

    public byte NumberOfPositions { get; set; }

    public MediaFeedResultsElectionSenateContestQuota Quota { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferences FirstPreferences { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class ContestIdentifier
{
    public string ContestName { get; set; }

    [XmlAttribute]
    public string Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestStateIdentifier
{
    [XmlAttribute]
    public string Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestEnrolment
{
    [XmlAttribute]
    public uint CloseOfRolls { get; set; }

    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestQuota
{
    [XmlAttribute]
    public bool Provisional { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferences
{
    [XmlElement("Group")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroup[] Group { get; set; }

    [XmlElement("UngroupedCandidate")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidate[] UngroupedCandidate { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesFormal Formal { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesInformal Informal { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesTotal Total { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroup
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupIdentifier GroupIdentifier { get; set; }

    [XmlElement("Candidate")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidate[] Candidate { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotes TicketVotes { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportioned Unapportioned { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotes GroupVotes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupIdentifier
{
    public string Ticket { get; set; }

    public string GroupName { get; set; }

    [XmlAttribute]
    public ushort Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidate
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier { get; set; }

    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier { get; set; }

    public byte BallotPosition { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateElected Elected { get; set; }

    public bool Incumbent { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes1[] VotesByType { get; set; }

    [XmlAttribute]
    public bool NoAffiliation { get; set; }

    [XmlIgnoreAttribute]
    public bool NoAffiliationSpecified { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class CandidateIdentifier
{
    public string CandidateName { get; set; }

    [XmlAttribute]
    public ushort Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[XmlRoot(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public class AffiliationIdentifier
{
    public string RegisteredName { get; set; }

    [XmlAttribute]
    public ushort Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateElected
{
    [XmlAttribute]
    public bool Historic { get; set; }

    [XmlAttribute]
    public byte Ranking { get; set; }

    [XmlIgnoreAttribute]
    public bool RankingSpecified { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotes
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportioned
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes
{
    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotes
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public decimal QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidate
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier { get; set; }

    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier { get; set; }

    public byte BallotPosition { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateElected Elected { get; set; }

    public bool Incumbent { get; set; }

    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes1[] VotesByType { get; set; }

    [XmlAttribute]
    public bool Independent { get; set; }

    [XmlIgnoreAttribute]
    public bool IndependentSpecified { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateElected
{
    [XmlAttribute]
    public bool Historic { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal QuotaProportion { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public byte QuotaProportion { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesFormal
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesInformal
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesTotal
{
    public MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysis
{
    public MediaFeedResultsElectionSenateAnalysisNational National { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNational
{
    public uint Enrolment { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferences FirstPreferences { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferences
{
    [XmlElement("PartyGroup")]
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroup[] PartyGroup { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngrouped AmalgamatedUngrouped { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormal Formal { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformal Informal { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotal Total { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroup
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier { get; set; }

    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier
{
    public string PartyGroupName { get; set; }

    [XmlAttribute]
    public uint Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngrouped
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngroupedVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngroupedVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroups
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes
{
    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormal
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformal
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotal
{
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouse
{
    [XmlArrayItem("Contest", IsNullable = false)]
    public MediaFeedResultsElectionHouseContest[] Contests { get; set; }

    public MediaFeedResultsElectionHouseAnalysis Analysis { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContest
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ContestIdentifier ContestIdentifier { get; set; }

    public MediaFeedResultsElectionHouseContestPollingDistrictIdentifier PollingDistrictIdentifier { get; set; }

    public MediaFeedResultsElectionHouseContestEnrolment Enrolment { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferences FirstPreferences { get; set; }

    public MediaFeedResultsElectionHouseContestTwoCandidatePreferred TwoCandidatePreferred { get; set; }

    [XmlArrayItem("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestCoalition[] TwoPartyPreferred { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }

    [XmlAttribute]
    public DateTime Declared { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestPollingDistrictIdentifier
{
    public string Name { get; set; }

    public MediaFeedResultsElectionHouseContestPollingDistrictIdentifierStateIdentifier StateIdentifier { get; set; }

    [XmlAttribute]
    public ushort Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestPollingDistrictIdentifierStateIdentifier
{
    [XmlAttribute]
    public string Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestEnrolment
{
    [XmlAttribute]
    public uint CloseOfRolls { get; set; }

    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferences
{
    [XmlElement("Candidate")]
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidate[] Candidate { get; set; }

    [XmlElement("Ghost")]
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhost[] Ghost { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesFormal Formal { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesInformal Informal { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesTotal Total { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }

    [XmlAttribute]
    public byte PollingPlacesReturned { get; set; }

    [XmlAttribute]
    public byte PollingPlacesExpected { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesCandidate
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier { get; set; }

    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier { get; set; }

    public byte BallotPosition { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateElected Elected { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateIncumbent Incumbent { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes1[] VotesByType { get; set; }

    [XmlAttribute]
    public bool Independent { get; set; }

    [XmlIgnoreAttribute]
    public bool IndependentSpecified { get; set; }

    [XmlAttribute]
    public bool NoAffiliation { get; set; }

    [XmlIgnoreAttribute]
    public bool NoAffiliationSpecified { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateElected
{
    [XmlAttribute]
    public bool Historic { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateIncumbent
{
    [XmlAttribute]
    public bool Notional { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes
{
    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public ushort MatchedHistoric { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesGhost
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier { get; set; }

    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier { get; set; }

    public ushort BallotPosition { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostElected Elected { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostIncumbent Incumbent { get; set; }

    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes1[] VotesByType { get; set; }

    [XmlAttribute]
    public bool Independent { get; set; }

    [XmlIgnoreAttribute]
    public bool IndependentSpecified { get; set; }

    [XmlAttribute]
    public bool NoAffiliation { get; set; }

    [XmlIgnoreAttribute]
    public bool NoAffiliationSpecified { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesGhostElected
{
    [XmlAttribute]
    public bool Historic { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesGhostIncumbent
{
    [XmlAttribute]
    public bool Notional { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes
{
    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public ushort MatchedHistoric { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesFormal
{
    public MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public uint MatchedHistoric { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesInformal
{
    public MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes
{
    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public ushort MatchedHistoric { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesTotal
{
    public MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes1[] VotesByType { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public uint MatchedHistoric { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferred
{
    [XmlElement("Candidate")]
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidate[] Candidate { get; set; }

    [XmlAttribute]
    public DateTime Updated { get; set; }

    [XmlAttribute]
    public byte PollingPlacesReturned { get; set; }

    [XmlAttribute]
    public byte PollingPlacesExpected { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidate
{
    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier { get; set; }

    [XmlElement(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier { get; set; }

    public byte BallotPosition { get; set; }

    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateElected Elected { get; set; }

    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateIncumbent Incumbent { get; set; }

    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes Votes { get; set; }

    [XmlArrayItem("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes1[] VotesByType { get; set; }

    [XmlAttribute]
    public bool Independent { get; set; }

    [XmlIgnoreAttribute]
    public bool IndependentSpecified { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateElected
{
    [XmlAttribute]
    public bool Historic { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateIncumbent
{
    [XmlAttribute]
    public bool Notional { get; set; }

    [XmlText]
    public bool Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public ushort MatchedHistoric { get; set; }

    [XmlAttribute]
    public ushort MatchedHistoricFirstPrefsIn { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes1
{
    [XmlAttribute]
    public string Type { get; set; }

    [XmlAttribute]
    public ushort Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public ushort Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestCoalition
{
    public MediaFeedResultsElectionHouseContestCoalitionCoalitionIdentifier CoalitionIdentifier { get; set; }

    public MediaFeedResultsElectionHouseContestCoalitionVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestCoalitionCoalitionIdentifier
{
    public string CoalitionName { get; set; }

    [XmlAttribute]
    public byte Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseContestCoalitionVotes
{
    [XmlAttribute]
    public uint Historic { get; set; }

    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlAttribute]
    public uint MatchedHistoric { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysis
{
    public MediaFeedResultsElectionHouseAnalysisNational National { get; set; }

    [XmlArrayItem("State", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisState[] States { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNational
{
    public uint Enrolment { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferences FirstPreferences { get; set; }

    [XmlArrayItem("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisNationalCoalition[] TwoPartyPreferred { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferences
{
    [XmlElement("PartyGroup")]
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroup[] PartyGroup { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormal Formal { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformal Informal { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotal Total { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroup
{
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier
{
    public string PartyGroupName { get; set; }

    [XmlAttribute]
    public uint Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroups
{
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes
{
    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormal
{
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformal
{
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotal
{
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalCoalition
{
    public MediaFeedResultsElectionHouseAnalysisNationalCoalitionCoalitionIdentifier CoalitionIdentifier { get; set; }

    public MediaFeedResultsElectionHouseAnalysisNationalCoalitionVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalCoalitionCoalitionIdentifier
{
    public string CoalitionName { get; set; }

    [XmlAttribute]
    public byte Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisNationalCoalitionVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisState
{
    public MediaFeedResultsElectionHouseAnalysisStateStateIdentifier StateIdentifier { get; set; }

    public uint Enrolment { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferences FirstPreferences { get; set; }

    [XmlArrayItem("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisStateCoalition[] TwoPartyPreferred { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateStateIdentifier
{
    [XmlAttribute]
    public string Id { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferences
{
    [XmlElement("PartyGroup")]
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroup[] PartyGroup { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormal Formal { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformal Informal { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotal Total { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroup
{
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupPartyGroupIdentifier
{
    public string PartyGroupName { get; set; }

    [XmlAttribute]
    public uint Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroups
{
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroupsVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroupsVotes
{
    [XmlAttribute]
    public byte Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public byte Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormal
{
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformal
{
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotal
{
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotalVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotalVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateCoalition
{
    public MediaFeedResultsElectionHouseAnalysisStateCoalitionCoalitionIdentifier CoalitionIdentifier { get; set; }

    public MediaFeedResultsElectionHouseAnalysisStateCoalitionVotes Votes { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateCoalitionCoalitionIdentifier
{
    public string CoalitionName { get; set; }

    [XmlAttribute]
    public byte Id { get; set; }

    [XmlAttribute]
    public string ShortCode { get; set; }
}

[Serializable]
[XmlType(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public class MediaFeedResultsElectionHouseAnalysisStateCoalitionVotes
{
    [XmlAttribute]
    public decimal Percentage { get; set; }

    [XmlAttribute]
    public decimal Swing { get; set; }

    [XmlText]
    public uint Value { get; set; }
}
