using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public abstract class Request
    {
        [DataMember]
        public string DateTime { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Token { get; set; }

        public abstract string Name { get; }

        public void SetDateTime()
        {
            DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
