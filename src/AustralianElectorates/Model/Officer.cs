using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Officer
    {
        internal Officer()
        {
        }

        [DataMember]
        public string Title { get; internal set; } = null!;

        [DataMember]
        public string FamilyName { get; internal set; } = null!;

        [DataMember]
        public string GivenNames { get; internal set; } = null!;

        [DataMember]
        public string Capacity { get; internal set; } = null!;

        [DataMember]
        public Address Address { get; internal set; } = null!;

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}