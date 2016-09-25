using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class UploadResponse : Response
    {
        [DataMember]
        public string UploadToken { get; set; }

        public override string Name
        {
            get
            {
                return "upload";
            }
        }
    }
}
