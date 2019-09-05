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
        public string Name { get; internal set; } = null!;
        [DataMember]
        public string Code { get; internal set; } = null!;
        [DataMember]
        public string Abbreviation { get; internal set; } = null!;
        [DataMember]
        public string RegisterDate { get; internal set; } = null!;
        [DataMember]
        public string AmendmentDate { get; internal set; } = null!;
        [DataMember]
        public string Address { get; internal set; } = null!;
        [DataMember]
        public Officer Officer { get; internal set; } = null!;

        [DataMember(Name = nameof(DeputyOfficers))]
        internal List<Officer> deputyOfficers  = null!;
        public IReadOnlyList<Officer> DeputyOfficers
        {
            get => deputyOfficers;
        }

        [DataMember]
        public Party Party { get; internal set; } = null!;
    }
}