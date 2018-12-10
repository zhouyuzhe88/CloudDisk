using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public abstract class Protocol
    {
        [DataMember]
        public string DateTime { get; set; }

        [DataMember]
        public string RequestId { get; set; }

        public abstract string Name { get; }

        protected abstract string Description { get; }

        public void SetDateTime()
        {
            DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", GetType().Name, RequestId.Substring(0, 5), Description);
        }
    }
}
