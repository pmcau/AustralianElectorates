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
        public Candidate Elected { get; internal set; } = null!;

        [DataMember]
        public Candidate Other { get; internal set; } = null!;
    }
}