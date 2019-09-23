using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Party : IPartyOrBranch
    {
        internal Party()
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

        [DataMember(Name = nameof(DeputyOfficers), Order = 100)]
        internal List<Officer> deputyOfficers = new List<Officer>();

        public IReadOnlyList<Officer> DeputyOfficers
        {
            get => deputyOfficers;
        }

        [DataMember(Name = nameof(Branches), Order = 100)]
        internal List<Branch> branches = new List<Branch>();

        public IReadOnlyList<Branch> Branches
        {
            get => branches;
        }
    }
}