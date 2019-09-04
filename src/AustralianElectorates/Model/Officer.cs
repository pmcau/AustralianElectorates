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
        public string Title { get; internal set; }

        [DataMember]
        public string FamilyName { get; internal set; }

        [DataMember]
        public string GivenNames { get; internal set; }

        [DataMember]
        public string Capacity { get; internal set; }

        [DataMember]
        public Address Address { get; internal set; }

        public string FullName()
        {
            return $"{FamilyName}, {GivenNames}";
        }
    }
}