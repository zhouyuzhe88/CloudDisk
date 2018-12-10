using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class SigninRequest : Request
    {
        [DataMember]
        public string Password { get; set; }
        
        public override string Name
        {
            get
            {
                return "signin";
            }
        }
        
        protected override string Description
        {
            get
            {
                return string.Format("user = {0}", UserName);
            }
        }
    }
}
