using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class Party : IParty
    {
        internal Party()
        {
        }

        [DataMember]
        public ushort Id { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }

        [DataMember]
        public string Code { get; internal set; }

        [DataMember]
        public string Abbreviation { get; internal set; }

        [DataMember]
        public string RegisterDate { get; internal set; }

        [DataMember]
        public string AmendmentDate { get; internal set; }

        [DataMember]
        public string Address { get; internal set; }

        [DataMember]
        public Officer Officer { get; internal set; }

        [DataMember(Name = nameof(DeputyOfficers))]
        internal List<Officer> deputyOfficers = new List<Officer>();

        public IReadOnlyList<Officer> DeputyOfficers
        {
            get => deputyOfficers;
        }

        [DataMember(Name = nameof(Branches))]
        internal List<Branch> branches = new List<Branch>();

        public IReadOnlyList<Branch> Branches
        {
            get => branches;
        }
    }
}