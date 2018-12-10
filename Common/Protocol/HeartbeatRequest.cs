using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class HeartbeatRequest : Request
    {
        public override string Name
        {
            get
            {
                return "heartbeat";
            }
        }

        protected override string Description
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
