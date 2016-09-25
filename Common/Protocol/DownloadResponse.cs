using System.Runtime.Serialization;

namespace Common.Protocol
{
    public class DownloadResponse : Response
    {
        [DataMember]
        public string DownloadToken { get; set; }

        [DataMember]
        public long FileLength { get; set; }

        public override string Name
        {
            get
            {
                return "download";
            }
        }
    }
}
