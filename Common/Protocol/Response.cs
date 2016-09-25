using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public abstract class Response
    {
        public abstract string Name { get; }

        [DataMember]
        public string DateTime { get; set; }

        [DataMember]
        public bool Success { get; set; }


        public void SetDateTime()
        {
            this.DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
