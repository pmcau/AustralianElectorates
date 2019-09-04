using System.Runtime.Serialization;

namespace AustralianElectorates
{
    [DataContract]
    public class StateMap
    {
        internal StateMap()
        {
        }

        [DataMember]
        public State State { get; internal set; }
        [DataMember]
        public string GeoJson { get; internal set; }
    }
}