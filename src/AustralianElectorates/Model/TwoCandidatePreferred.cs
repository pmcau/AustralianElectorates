using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class TwoCandidatePreferred
    {
        internal TwoCandidatePreferred()
        {
        }

        [DataMember]
        public Candidate Elected { get; internal set; }

        [DataMember]
        public Candidate Other { get; internal set; }
    }
}