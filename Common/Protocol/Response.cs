using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public abstract class Response : Protocol
    {
        [DataMember]
        public bool Success { get; set; }

        protected override string Description
        {
            get
            {
                return string.Format("success = {0}", Success);
            }
        }
    }
}
