using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class HeartbeatResponse : Response
    {
        [DataMember]
        public string Token { get; set; }

        public override string Name
        {
            get
            {
                return "heartbeat";
            }
        }
    }
}
