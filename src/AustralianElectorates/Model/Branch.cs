using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Branch : IParty
    {
        internal Branch()
        {
        }

        [DataMember]
        public ushort Id { get; internal set; }
        [DataMember]
        public string Name { get; internal set; }
        [DataMember]
        public string Code { get; internal set; }
        [DataMember]
        public string Abbreviation { get; set; }
        [DataMember]
        public string RegisterDate { get; internal set; }
        [DataMember]
        public string AmendmentDate { get; internal set; }
        [DataMember]
        public string Address { get; internal set; }
        [DataMember]
        public Officer Officer { get; internal set; }

        [DataMember(Name = nameof(DeputyOfficers))]
        internal List<Officer> deputyOfficers;
        public IReadOnlyList<Officer> DeputyOfficers
        {
            get => deputyOfficers;
        }

        [DataMember]
        public Party Party { get; internal set; }
    }
}