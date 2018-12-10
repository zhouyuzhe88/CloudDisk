using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class EchoRequest : Request
    {
        [DataMember]
        public string Content { get; set; }

        public override string Name
        {
            get
            {
                return "echo";
            }
        }
        protected override string Description
        {
            get
            {
                return string.Format("content = {0}", Content);
            }
        }
    }
}
