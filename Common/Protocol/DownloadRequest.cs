using System.Runtime.Serialization;

namespace Common.Protocol
{
    public class DownloadRequest : Request
    {
        [DataMember]
        public string RemoteFileFullPath { get; set; }

        [DataMember]
        public string FileSet { get; set; }

        public override string Name
        {
            get
            {
                return "download";
            }
        }
    }
}
