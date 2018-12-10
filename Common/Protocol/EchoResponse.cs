using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class EchoResponse : Response
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
                return base.Description + string.Format(" content = {0}", Content);
            }
        }
    }
}
