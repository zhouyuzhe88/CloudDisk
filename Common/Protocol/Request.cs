using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public abstract class Request: Protocol
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Token { get; set; }
    }
}
