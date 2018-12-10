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

        protected override string Description
        {
            get
            {
                return string.Format("path = {0}", RemoteFileFullPath) + 
                    (string.IsNullOrWhiteSpace(FileSet) ? "" : string.Format(" set = {1}", FileSet));
            }
        }
    }
}
