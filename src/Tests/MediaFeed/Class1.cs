[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed", IsNullable = false)]
public class MediaFeed
{

    MediaFeedManagingAuthority managingAuthorityField;

    string messageLanguageField;

    MediaFeedMessageGenerator messageGeneratorField;

    MediaFeedCycle cycleField;

    MediaFeedResults resultsField;

    string idField;

    System.DateTime createdField;

    byte schemaVersionField;

    byte emlVersionField;

    /// <remarks/>
    public MediaFeedManagingAuthority ManagingAuthority
    {
        get
        {
            return managingAuthorityField;
        }
        set
        {
            managingAuthorityField = value;
        }
    }

    /// <remarks/>
    public string MessageLanguage
    {
        get
        {
            return messageLanguageField;
        }
        set
        {
            messageLanguageField = value;
        }
    }

    /// <remarks/>
    public MediaFeedMessageGenerator MessageGenerator
    {
        get
        {
            return messageGeneratorField;
        }
        set
        {
            messageGeneratorField = value;
        }
    }

    /// <remarks/>
    public MediaFeedCycle Cycle
    {
        get
        {
            return cycleField;
        }
        set
        {
            cycleField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResults Results
    {
        get
        {
            return resultsField;
        }
        set
        {
            resultsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Created
    {
        get
        {
            return createdField;
        }
        set
        {
            createdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte SchemaVersion
    {
        get
        {
            return schemaVersionField;
        }
        set
        {
            schemaVersionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte EmlVersion
    {
        get
        {
            return emlVersionField;
        }
        set
        {
            emlVersionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedManagingAuthority
{

    private AuthorityIdentifier authorityIdentifierField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AuthorityIdentifier AuthorityIdentifier
    {
        get
        {
            return authorityIdentifierField;
        }
        set
        {
            authorityIdentifierField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class AuthorityIdentifier
{

    private string idField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public string Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedMessageGenerator
{

    private string nameField;

    private string environmentField;

    private string siteField;

    private string serverField;

    private string platformField;

    private string versionField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    public string Environment
    {
        get
        {
            return environmentField;
        }
        set
        {
            environmentField = value;
        }
    }

    /// <remarks/>
    public string Site
    {
        get
        {
            return siteField;
        }
        set
        {
            siteField = value;
        }
    }

    /// <remarks/>
    public string Server
    {
        get
        {
            return serverField;
        }
        set
        {
            serverField = value;
        }
    }

    /// <remarks/>
    public string Platform
    {
        get
        {
            return platformField;
        }
        set
        {
            platformField = value;
        }
    }

    /// <remarks/>
    public string Version
    {
        get
        {
            return versionField;
        }
        set
        {
            versionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedCycle
{

    private System.DateTime createdField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Created
    {
        get
        {
            return createdField;
        }
        set
        {
            createdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public string Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResults
{

    private EventIdentifier eventIdentifierField;

    private MediaFeedResultsElection[] electionField;

    private System.DateTime updatedField;

    private string phaseField;

    private string verbosityField;

    private string granularityField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public EventIdentifier EventIdentifier
    {
        get
        {
            return eventIdentifierField;
        }
        set
        {
            eventIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Election")]
    public MediaFeedResultsElection[] Election
    {
        get
        {
            return electionField;
        }
        set
        {
            electionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Phase
    {
        get
        {
            return phaseField;
        }
        set
        {
            phaseField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Verbosity
    {
        get
        {
            return verbosityField;
        }
        set
        {
            verbosityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Granularity
    {
        get
        {
            return granularityField;
        }
        set
        {
            granularityField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class EventIdentifier
{

    private string eventNameField;

    private ushort idField;

    /// <remarks/>
    public string EventName
    {
        get
        {
            return eventNameField;
        }
        set
        {
            eventNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElection
{

    private ElectionIdentifier electionIdentifierField;

    private MediaFeedResultsElectionSenate senateField;

    private MediaFeedResultsElectionHouse houseField;

    private System.DateTime updatedField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ElectionIdentifier ElectionIdentifier
    {
        get
        {
            return electionIdentifierField;
        }
        set
        {
            electionIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenate Senate
    {
        get
        {
            return senateField;
        }
        set
        {
            senateField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouse House
    {
        get
        {
            return houseField;
        }
        set
        {
            houseField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class ElectionIdentifier
{

    private string electionNameField;

    private string electionCategoryField;

    private string idField;

    /// <remarks/>
    public string ElectionName
    {
        get
        {
            return electionNameField;
        }
        set
        {
            electionNameField = value;
        }
    }

    /// <remarks/>
    public string ElectionCategory
    {
        get
        {
            return electionCategoryField;
        }
        set
        {
            electionCategoryField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenate
{

    private MediaFeedResultsElectionSenateContest[] contestsField;

    private MediaFeedResultsElectionSenateAnalysis analysisField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Contest", IsNullable = false)]
    public MediaFeedResultsElectionSenateContest[] Contests
    {
        get
        {
            return contestsField;
        }
        set
        {
            contestsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysis Analysis
    {
        get
        {
            return analysisField;
        }
        set
        {
            analysisField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContest
{

    private ContestIdentifier contestIdentifierField;

    private MediaFeedResultsElectionSenateContestStateIdentifier stateIdentifierField;

    private MediaFeedResultsElectionSenateContestEnrolment enrolmentField;

    private byte numberOfPositionsField;

    private MediaFeedResultsElectionSenateContestQuota quotaField;

    private MediaFeedResultsElectionSenateContestFirstPreferences firstPreferencesField;

    private System.DateTime updatedField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ContestIdentifier ContestIdentifier
    {
        get
        {
            return contestIdentifierField;
        }
        set
        {
            contestIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestStateIdentifier StateIdentifier
    {
        get
        {
            return stateIdentifierField;
        }
        set
        {
            stateIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestEnrolment Enrolment
    {
        get
        {
            return enrolmentField;
        }
        set
        {
            enrolmentField = value;
        }
    }

    /// <remarks/>
    public byte NumberOfPositions
    {
        get
        {
            return numberOfPositionsField;
        }
        set
        {
            numberOfPositionsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestQuota Quota
    {
        get
        {
            return quotaField;
        }
        set
        {
            quotaField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferences FirstPreferences
    {
        get
        {
            return firstPreferencesField;
        }
        set
        {
            firstPreferencesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class ContestIdentifier
{

    private string contestNameField;

    private string idField;

    /// <remarks/>
    public string ContestName
    {
        get
        {
            return contestNameField;
        }
        set
        {
            contestNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestStateIdentifier
{

    private string idField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestEnrolment
{

    private uint closeOfRollsField;

    private uint historicField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint CloseOfRolls
    {
        get
        {
            return closeOfRollsField;
        }
        set
        {
            closeOfRollsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestQuota
{

    private bool provisionalField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Provisional
    {
        get
        {
            return provisionalField;
        }
        set
        {
            provisionalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferences
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroup[] groupField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidate[] ungroupedCandidateField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesFormal formalField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesInformal informalField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesTotal totalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Group")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroup[] Group
    {
        get
        {
            return groupField;
        }
        set
        {
            groupField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("UngroupedCandidate")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidate[] UngroupedCandidate
    {
        get
        {
            return ungroupedCandidateField;
        }
        set
        {
            ungroupedCandidateField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesFormal Formal
    {
        get
        {
            return formalField;
        }
        set
        {
            formalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesInformal Informal
    {
        get
        {
            return informalField;
        }
        set
        {
            informalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesTotal Total
    {
        get
        {
            return totalField;
        }
        set
        {
            totalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroup
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupIdentifier groupIdentifierField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidate[] candidateField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotes ticketVotesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportioned unapportionedField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotes groupVotesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupIdentifier GroupIdentifier
    {
        get
        {
            return groupIdentifierField;
        }
        set
        {
            groupIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Candidate")]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidate[] Candidate
    {
        get
        {
            return candidateField;
        }
        set
        {
            candidateField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotes TicketVotes
    {
        get
        {
            return ticketVotesField;
        }
        set
        {
            ticketVotesField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportioned Unapportioned
    {
        get
        {
            return unapportionedField;
        }
        set
        {
            unapportionedField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotes GroupVotes
    {
        get
        {
            return groupVotesField;
        }
        set
        {
            groupVotesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupIdentifier
{

    private string ticketField;

    private string groupNameField;

    private ushort idField;

    /// <remarks/>
    public string Ticket
    {
        get
        {
            return ticketField;
        }
        set
        {
            ticketField = value;
        }
    }

    /// <remarks/>
    public string GroupName
    {
        get
        {
            return groupNameField;
        }
        set
        {
            groupNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidate
{

    private CandidateIdentifier candidateIdentifierField;

    private AffiliationIdentifier affiliationIdentifierField;

    private byte ballotPositionField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateElected electedField;

    private bool incumbentField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes1[] votesByTypeField;

    private bool noAffiliationField;

    private bool noAffiliationFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier
    {
        get
        {
            return candidateIdentifierField;
        }
        set
        {
            candidateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier
    {
        get
        {
            return affiliationIdentifierField;
        }
        set
        {
            affiliationIdentifierField = value;
        }
    }

    /// <remarks/>
    public byte BallotPosition
    {
        get
        {
            return ballotPositionField;
        }
        set
        {
            ballotPositionField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateElected Elected
    {
        get
        {
            return electedField;
        }
        set
        {
            electedField = value;
        }
    }

    /// <remarks/>
    public bool Incumbent
    {
        get
        {
            return incumbentField;
        }
        set
        {
            incumbentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool NoAffiliation
    {
        get
        {
            return noAffiliationField;
        }
        set
        {
            noAffiliationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool NoAffiliationSpecified
    {
        get
        {
            return noAffiliationFieldSpecified;
        }
        set
        {
            noAffiliationFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class CandidateIdentifier
{

    private string candidateNameField;

    private ushort idField;

    /// <remarks/>
    public string CandidateName
    {
        get
        {
            return candidateNameField;
        }
        set
        {
            candidateNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:tc:evs:schema:eml")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml", IsNullable = false)]
public partial class AffiliationIdentifier
{

    private string registeredNameField;

    private ushort idField;

    private string shortCodeField;

    /// <remarks/>
    public string RegisteredName
    {
        get
        {
            return registeredNameField;
        }
        set
        {
            registeredNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateElected
{

    private bool historicField;

    private byte rankingField;

    private bool rankingFieldSpecified;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Ranking
    {
        get
        {
            return rankingField;
        }
        set
        {
            rankingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool RankingSpecified
    {
        get
        {
            return rankingFieldSpecified;
        }
        set
        {
            rankingFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes
{

    private decimal percentageField;

    private decimal quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupCandidateVotes1
{

    private string typeField;

    private decimal percentageField;

    private byte quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotes
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes
{

    private decimal percentageField;

    private decimal quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupTicketVotesVotes1
{

    private string typeField;

    private decimal percentageField;

    private byte quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportioned
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes
{

    private byte percentageField;

    private byte quotaProportionField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupUnapportionedVotes1
{

    private string typeField;

    private byte percentageField;

    private byte quotaProportionField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotes
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private decimal quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesGroupGroupVotesVotes1
{

    private string typeField;

    private decimal percentageField;

    private byte quotaProportionField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidate
{

    private CandidateIdentifier candidateIdentifierField;

    private AffiliationIdentifier affiliationIdentifierField;

    private byte ballotPositionField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateElected electedField;

    private bool incumbentField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes1[] votesByTypeField;

    private bool independentField;

    private bool independentFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier
    {
        get
        {
            return candidateIdentifierField;
        }
        set
        {
            candidateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier
    {
        get
        {
            return affiliationIdentifierField;
        }
        set
        {
            affiliationIdentifierField = value;
        }
    }

    /// <remarks/>
    public byte BallotPosition
    {
        get
        {
            return ballotPositionField;
        }
        set
        {
            ballotPositionField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateElected Elected
    {
        get
        {
            return electedField;
        }
        set
        {
            electedField = value;
        }
    }

    /// <remarks/>
    public bool Incumbent
    {
        get
        {
            return incumbentField;
        }
        set
        {
            incumbentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Independent
    {
        get
        {
            return independentField;
        }
        set
        {
            independentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IndependentSpecified
    {
        get
        {
            return independentFieldSpecified;
        }
        set
        {
            independentFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateElected
{

    private bool historicField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes
{

    private decimal percentageField;

    private decimal quotaProportionField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesUngroupedCandidateVotes1
{

    private string typeField;

    private decimal percentageField;

    private byte quotaProportionField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte QuotaProportion
    {
        get
        {
            return quotaProportionField;
        }
        set
        {
            quotaProportionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesFormal
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesFormalVotes1
{

    private string typeField;

    private decimal percentageField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesInformal
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesInformalVotes1
{

    private string typeField;

    private decimal percentageField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesTotal
{

    private MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes votesField;

    private MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateContestFirstPreferencesTotalVotes1
{

    private string typeField;

    private decimal percentageField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysis
{

    private MediaFeedResultsElectionSenateAnalysisNational nationalField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNational National
    {
        get
        {
            return nationalField;
        }
        set
        {
            nationalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNational
{

    private uint enrolmentField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferences firstPreferencesField;

    /// <remarks/>
    public uint Enrolment
    {
        get
        {
            return enrolmentField;
        }
        set
        {
            enrolmentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferences FirstPreferences
    {
        get
        {
            return firstPreferencesField;
        }
        set
        {
            firstPreferencesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferences
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroup[] partyGroupField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngrouped amalgamatedUngroupedField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroups amalgamatedGhostGroupsField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormal formalField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformal informalField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotal totalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PartyGroup")]
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroup[] PartyGroup
    {
        get
        {
            return partyGroupField;
        }
        set
        {
            partyGroupField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngrouped AmalgamatedUngrouped
    {
        get
        {
            return amalgamatedUngroupedField;
        }
        set
        {
            amalgamatedUngroupedField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups
    {
        get
        {
            return amalgamatedGhostGroupsField;
        }
        set
        {
            amalgamatedGhostGroupsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormal Formal
    {
        get
        {
            return formalField;
        }
        set
        {
            formalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformal Informal
    {
        get
        {
            return informalField;
        }
        set
        {
            informalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotal Total
    {
        get
        {
            return totalField;
        }
        set
        {
            totalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroup
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier partyGroupIdentifierField;

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier
    {
        get
        {
            return partyGroupIdentifierField;
        }
        set
        {
            partyGroupIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier
{

    private string partyGroupNameField;

    private uint idField;

    private string shortCodeField;

    /// <remarks/>
    public string PartyGroupName
    {
        get
        {
            return partyGroupNameField;
        }
        set
        {
            partyGroupNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesPartyGroupVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngrouped
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngroupedVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngroupedVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedUngroupedVotes
{

    private decimal percentageField;

    private decimal swingField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroups
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes
{

    private byte percentageField;

    private decimal swingField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormal
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesFormalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformal
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesInformalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotal
{

    private MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionSenateAnalysisNationalFirstPreferencesTotalVotes
{

    private decimal percentageField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouse
{

    private MediaFeedResultsElectionHouseContest[] contestsField;

    private MediaFeedResultsElectionHouseAnalysis analysisField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Contest", IsNullable = false)]
    public MediaFeedResultsElectionHouseContest[] Contests
    {
        get
        {
            return contestsField;
        }
        set
        {
            contestsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysis Analysis
    {
        get
        {
            return analysisField;
        }
        set
        {
            analysisField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContest
{

    private ContestIdentifier contestIdentifierField;

    private MediaFeedResultsElectionHouseContestPollingDistrictIdentifier pollingDistrictIdentifierField;

    private MediaFeedResultsElectionHouseContestEnrolment enrolmentField;

    private MediaFeedResultsElectionHouseContestFirstPreferences firstPreferencesField;

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferred twoCandidatePreferredField;

    private MediaFeedResultsElectionHouseContestCoalition[] twoPartyPreferredField;

    private System.DateTime updatedField;

    private System.DateTime declaredField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public ContestIdentifier ContestIdentifier
    {
        get
        {
            return contestIdentifierField;
        }
        set
        {
            contestIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestPollingDistrictIdentifier PollingDistrictIdentifier
    {
        get
        {
            return pollingDistrictIdentifierField;
        }
        set
        {
            pollingDistrictIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestEnrolment Enrolment
    {
        get
        {
            return enrolmentField;
        }
        set
        {
            enrolmentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferences FirstPreferences
    {
        get
        {
            return firstPreferencesField;
        }
        set
        {
            firstPreferencesField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferred TwoCandidatePreferred
    {
        get
        {
            return twoCandidatePreferredField;
        }
        set
        {
            twoCandidatePreferredField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestCoalition[] TwoPartyPreferred
    {
        get
        {
            return twoPartyPreferredField;
        }
        set
        {
            twoPartyPreferredField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Declared
    {
        get
        {
            return declaredField;
        }
        set
        {
            declaredField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestPollingDistrictIdentifier
{

    private string nameField;

    private MediaFeedResultsElectionHouseContestPollingDistrictIdentifierStateIdentifier stateIdentifierField;

    private ushort idField;

    private string shortCodeField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return nameField;
        }
        set
        {
            nameField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestPollingDistrictIdentifierStateIdentifier StateIdentifier
    {
        get
        {
            return stateIdentifierField;
        }
        set
        {
            stateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestPollingDistrictIdentifierStateIdentifier
{

    private string idField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestEnrolment
{

    private uint closeOfRollsField;

    private uint historicField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint CloseOfRolls
    {
        get
        {
            return closeOfRollsField;
        }
        set
        {
            closeOfRollsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferences
{

    private MediaFeedResultsElectionHouseContestFirstPreferencesCandidate[] candidateField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesGhost[] ghostField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesFormal formalField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesInformal informalField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesTotal totalField;

    private System.DateTime updatedField;

    private byte pollingPlacesReturnedField;

    private byte pollingPlacesExpectedField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Candidate")]
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidate[] Candidate
    {
        get
        {
            return candidateField;
        }
        set
        {
            candidateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Ghost")]
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhost[] Ghost
    {
        get
        {
            return ghostField;
        }
        set
        {
            ghostField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesFormal Formal
    {
        get
        {
            return formalField;
        }
        set
        {
            formalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesInformal Informal
    {
        get
        {
            return informalField;
        }
        set
        {
            informalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesTotal Total
    {
        get
        {
            return totalField;
        }
        set
        {
            totalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte PollingPlacesReturned
    {
        get
        {
            return pollingPlacesReturnedField;
        }
        set
        {
            pollingPlacesReturnedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte PollingPlacesExpected
    {
        get
        {
            return pollingPlacesExpectedField;
        }
        set
        {
            pollingPlacesExpectedField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesCandidate
{

    private CandidateIdentifier candidateIdentifierField;

    private AffiliationIdentifier affiliationIdentifierField;

    private byte ballotPositionField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesCandidateElected electedField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesCandidateIncumbent incumbentField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes votesField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes1[] votesByTypeField;

    private bool independentField;

    private bool independentFieldSpecified;

    private bool noAffiliationField;

    private bool noAffiliationFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier
    {
        get
        {
            return candidateIdentifierField;
        }
        set
        {
            candidateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier
    {
        get
        {
            return affiliationIdentifierField;
        }
        set
        {
            affiliationIdentifierField = value;
        }
    }

    /// <remarks/>
    public byte BallotPosition
    {
        get
        {
            return ballotPositionField;
        }
        set
        {
            ballotPositionField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateElected Elected
    {
        get
        {
            return electedField;
        }
        set
        {
            electedField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateIncumbent Incumbent
    {
        get
        {
            return incumbentField;
        }
        set
        {
            incumbentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Independent
    {
        get
        {
            return independentField;
        }
        set
        {
            independentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IndependentSpecified
    {
        get
        {
            return independentFieldSpecified;
        }
        set
        {
            independentFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool NoAffiliation
    {
        get
        {
            return noAffiliationField;
        }
        set
        {
            noAffiliationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool NoAffiliationSpecified
    {
        get
        {
            return noAffiliationFieldSpecified;
        }
        set
        {
            noAffiliationFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateElected
{

    private bool historicField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateIncumbent
{

    private bool notionalField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Notional
    {
        get
        {
            return notionalField;
        }
        set
        {
            notionalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes
{

    private ushort historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort matchedHistoricField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesCandidateVotes1
{

    private string typeField;

    private ushort historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesGhost
{

    private CandidateIdentifier candidateIdentifierField;

    private AffiliationIdentifier affiliationIdentifierField;

    private ushort ballotPositionField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesGhostElected electedField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesGhostIncumbent incumbentField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes votesField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes1[] votesByTypeField;

    private bool independentField;

    private bool independentFieldSpecified;

    private bool noAffiliationField;

    private bool noAffiliationFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier
    {
        get
        {
            return candidateIdentifierField;
        }
        set
        {
            candidateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier
    {
        get
        {
            return affiliationIdentifierField;
        }
        set
        {
            affiliationIdentifierField = value;
        }
    }

    /// <remarks/>
    public ushort BallotPosition
    {
        get
        {
            return ballotPositionField;
        }
        set
        {
            ballotPositionField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostElected Elected
    {
        get
        {
            return electedField;
        }
        set
        {
            electedField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostIncumbent Incumbent
    {
        get
        {
            return incumbentField;
        }
        set
        {
            incumbentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Independent
    {
        get
        {
            return independentField;
        }
        set
        {
            independentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IndependentSpecified
    {
        get
        {
            return independentFieldSpecified;
        }
        set
        {
            independentFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool NoAffiliation
    {
        get
        {
            return noAffiliationField;
        }
        set
        {
            noAffiliationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool NoAffiliationSpecified
    {
        get
        {
            return noAffiliationFieldSpecified;
        }
        set
        {
            noAffiliationFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesGhostElected
{

    private bool historicField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesGhostIncumbent
{

    private bool notionalField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Notional
    {
        get
        {
            return notionalField;
        }
        set
        {
            notionalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes
{

    private ushort historicField;

    private byte percentageField;

    private decimal swingField;

    private ushort matchedHistoricField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesGhostVotes1
{

    private string typeField;

    private ushort historicField;

    private byte percentageField;

    private decimal swingField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesFormal
{

    private MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes votesField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint matchedHistoricField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesFormalVotes1
{

    private string typeField;

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesInformal
{

    private MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes votesField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes
{

    private ushort historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort matchedHistoricField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesInformalVotes1
{

    private string typeField;

    private ushort historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesTotal
{

    private MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes votesField;

    private MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes1[] votesByTypeField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint matchedHistoricField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestFirstPreferencesTotalVotes1
{

    private string typeField;

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferred
{

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidate[] candidateField;

    private System.DateTime updatedField;

    private byte pollingPlacesReturnedField;

    private byte pollingPlacesExpectedField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Candidate")]
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidate[] Candidate
    {
        get
        {
            return candidateField;
        }
        set
        {
            candidateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public System.DateTime Updated
    {
        get
        {
            return updatedField;
        }
        set
        {
            updatedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte PollingPlacesReturned
    {
        get
        {
            return pollingPlacesReturnedField;
        }
        set
        {
            pollingPlacesReturnedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte PollingPlacesExpected
    {
        get
        {
            return pollingPlacesExpectedField;
        }
        set
        {
            pollingPlacesExpectedField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidate
{

    private CandidateIdentifier candidateIdentifierField;

    private AffiliationIdentifier affiliationIdentifierField;

    private byte ballotPositionField;

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateElected electedField;

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateIncumbent incumbentField;

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes votesField;

    private MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes1[] votesByTypeField;

    private bool independentField;

    private bool independentFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public CandidateIdentifier CandidateIdentifier
    {
        get
        {
            return candidateIdentifierField;
        }
        set
        {
            candidateIdentifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:tc:evs:schema:eml")]
    public AffiliationIdentifier AffiliationIdentifier
    {
        get
        {
            return affiliationIdentifierField;
        }
        set
        {
            affiliationIdentifierField = value;
        }
    }

    /// <remarks/>
    public byte BallotPosition
    {
        get
        {
            return ballotPositionField;
        }
        set
        {
            ballotPositionField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateElected Elected
    {
        get
        {
            return electedField;
        }
        set
        {
            electedField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateIncumbent Incumbent
    {
        get
        {
            return incumbentField;
        }
        set
        {
            incumbentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Votes", IsNullable = false)]
    public MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes1[] VotesByType
    {
        get
        {
            return votesByTypeField;
        }
        set
        {
            votesByTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Independent
    {
        get
        {
            return independentField;
        }
        set
        {
            independentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool IndependentSpecified
    {
        get
        {
            return independentFieldSpecified;
        }
        set
        {
            independentFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateElected
{

    private bool historicField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateIncumbent
{

    private bool notionalField;

    private bool valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public bool Notional
    {
        get
        {
            return notionalField;
        }
        set
        {
            notionalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public bool Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort matchedHistoricField;

    private ushort matchedHistoricFirstPrefsInField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort MatchedHistoricFirstPrefsIn
    {
        get
        {
            return matchedHistoricFirstPrefsInField;
        }
        set
        {
            matchedHistoricFirstPrefsInField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestTwoCandidatePreferredCandidateVotes1
{

    private string typeField;

    private ushort historicField;

    private decimal percentageField;

    private decimal swingField;

    private ushort valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public ushort Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public ushort Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestCoalition
{

    private MediaFeedResultsElectionHouseContestCoalitionCoalitionIdentifier coalitionIdentifierField;

    private MediaFeedResultsElectionHouseContestCoalitionVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestCoalitionCoalitionIdentifier CoalitionIdentifier
    {
        get
        {
            return coalitionIdentifierField;
        }
        set
        {
            coalitionIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseContestCoalitionVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestCoalitionCoalitionIdentifier
{

    private string coalitionNameField;

    private byte idField;

    private string shortCodeField;

    /// <remarks/>
    public string CoalitionName
    {
        get
        {
            return coalitionNameField;
        }
        set
        {
            coalitionNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseContestCoalitionVotes
{

    private uint historicField;

    private decimal percentageField;

    private decimal swingField;

    private uint matchedHistoricField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Historic
    {
        get
        {
            return historicField;
        }
        set
        {
            historicField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint MatchedHistoric
    {
        get
        {
            return matchedHistoricField;
        }
        set
        {
            matchedHistoricField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysis
{

    private MediaFeedResultsElectionHouseAnalysisNational nationalField;

    private MediaFeedResultsElectionHouseAnalysisState[] statesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNational National
    {
        get
        {
            return nationalField;
        }
        set
        {
            nationalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("State", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisState[] States
    {
        get
        {
            return statesField;
        }
        set
        {
            statesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNational
{

    private uint enrolmentField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferences firstPreferencesField;

    private MediaFeedResultsElectionHouseAnalysisNationalCoalition[] twoPartyPreferredField;

    /// <remarks/>
    public uint Enrolment
    {
        get
        {
            return enrolmentField;
        }
        set
        {
            enrolmentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferences FirstPreferences
    {
        get
        {
            return firstPreferencesField;
        }
        set
        {
            firstPreferencesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisNationalCoalition[] TwoPartyPreferred
    {
        get
        {
            return twoPartyPreferredField;
        }
        set
        {
            twoPartyPreferredField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferences
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroup[] partyGroupField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroups amalgamatedGhostGroupsField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormal formalField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformal informalField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotal totalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PartyGroup")]
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroup[] PartyGroup
    {
        get
        {
            return partyGroupField;
        }
        set
        {
            partyGroupField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups
    {
        get
        {
            return amalgamatedGhostGroupsField;
        }
        set
        {
            amalgamatedGhostGroupsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormal Formal
    {
        get
        {
            return formalField;
        }
        set
        {
            formalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformal Informal
    {
        get
        {
            return informalField;
        }
        set
        {
            informalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotal Total
    {
        get
        {
            return totalField;
        }
        set
        {
            totalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroup
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier partyGroupIdentifierField;

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier
    {
        get
        {
            return partyGroupIdentifierField;
        }
        set
        {
            partyGroupIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupPartyGroupIdentifier
{

    private string partyGroupNameField;

    private uint idField;

    private string shortCodeField;

    /// <remarks/>
    public string PartyGroupName
    {
        get
        {
            return partyGroupNameField;
        }
        set
        {
            partyGroupNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesPartyGroupVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroups
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesAmalgamatedGhostGroupsVotes
{

    private byte percentageField;

    private decimal swingField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormal
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesFormalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformal
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesInformalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotal
{

    private MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalFirstPreferencesTotalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalCoalition
{

    private MediaFeedResultsElectionHouseAnalysisNationalCoalitionCoalitionIdentifier coalitionIdentifierField;

    private MediaFeedResultsElectionHouseAnalysisNationalCoalitionVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalCoalitionCoalitionIdentifier CoalitionIdentifier
    {
        get
        {
            return coalitionIdentifierField;
        }
        set
        {
            coalitionIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisNationalCoalitionVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalCoalitionCoalitionIdentifier
{

    private string coalitionNameField;

    private byte idField;

    private string shortCodeField;

    /// <remarks/>
    public string CoalitionName
    {
        get
        {
            return coalitionNameField;
        }
        set
        {
            coalitionNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisNationalCoalitionVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisState
{

    private MediaFeedResultsElectionHouseAnalysisStateStateIdentifier stateIdentifierField;

    private uint enrolmentField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferences firstPreferencesField;

    private MediaFeedResultsElectionHouseAnalysisStateCoalition[] twoPartyPreferredField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateStateIdentifier StateIdentifier
    {
        get
        {
            return stateIdentifierField;
        }
        set
        {
            stateIdentifierField = value;
        }
    }

    /// <remarks/>
    public uint Enrolment
    {
        get
        {
            return enrolmentField;
        }
        set
        {
            enrolmentField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferences FirstPreferences
    {
        get
        {
            return firstPreferencesField;
        }
        set
        {
            firstPreferencesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Coalition", IsNullable = false)]
    public MediaFeedResultsElectionHouseAnalysisStateCoalition[] TwoPartyPreferred
    {
        get
        {
            return twoPartyPreferredField;
        }
        set
        {
            twoPartyPreferredField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateStateIdentifier
{

    private string idField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferences
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroup[] partyGroupField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroups amalgamatedGhostGroupsField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormal formalField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformal informalField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotal totalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PartyGroup")]
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroup[] PartyGroup
    {
        get
        {
            return partyGroupField;
        }
        set
        {
            partyGroupField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroups AmalgamatedGhostGroups
    {
        get
        {
            return amalgamatedGhostGroupsField;
        }
        set
        {
            amalgamatedGhostGroupsField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormal Formal
    {
        get
        {
            return formalField;
        }
        set
        {
            formalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformal Informal
    {
        get
        {
            return informalField;
        }
        set
        {
            informalField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotal Total
    {
        get
        {
            return totalField;
        }
        set
        {
            totalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroup
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupPartyGroupIdentifier partyGroupIdentifierField;

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupPartyGroupIdentifier PartyGroupIdentifier
    {
        get
        {
            return partyGroupIdentifierField;
        }
        set
        {
            partyGroupIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupPartyGroupIdentifier
{

    private string partyGroupNameField;

    private uint idField;

    private string shortCodeField;

    /// <remarks/>
    public string PartyGroupName
    {
        get
        {
            return partyGroupNameField;
        }
        set
        {
            partyGroupNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public uint Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesPartyGroupVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroups
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroupsVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroupsVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesAmalgamatedGhostGroupsVotes
{

    private byte percentageField;

    private decimal swingField;

    private byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public byte Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormal
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesFormalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformal
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesInformalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotal
{

    private MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotalVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotalVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateFirstPreferencesTotalVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateCoalition
{

    private MediaFeedResultsElectionHouseAnalysisStateCoalitionCoalitionIdentifier coalitionIdentifierField;

    private MediaFeedResultsElectionHouseAnalysisStateCoalitionVotes votesField;

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateCoalitionCoalitionIdentifier CoalitionIdentifier
    {
        get
        {
            return coalitionIdentifierField;
        }
        set
        {
            coalitionIdentifierField = value;
        }
    }

    /// <remarks/>
    public MediaFeedResultsElectionHouseAnalysisStateCoalitionVotes Votes
    {
        get
        {
            return votesField;
        }
        set
        {
            votesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateCoalitionCoalitionIdentifier
{

    private string coalitionNameField;

    private byte idField;

    private string shortCodeField;

    /// <remarks/>
    public string CoalitionName
    {
        get
        {
            return coalitionNameField;
        }
        set
        {
            coalitionNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public byte Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public string ShortCode
    {
        get
        {
            return shortCodeField;
        }
        set
        {
            shortCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
public partial class MediaFeedResultsElectionHouseAnalysisStateCoalitionVotes
{

    private decimal percentageField;

    private decimal swingField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Percentage
    {
        get
        {
            return percentageField;
        }
        set
        {
            percentageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute]
    public decimal Swing
    {
        get
        {
            return swingField;
        }
        set
        {
            swingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute]
    public uint Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}
