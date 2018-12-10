using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class UploadRequest : Request
    {
        [DataMember]
        public string RemoteFileFullPath { get; set; }
        
        [DataMember]
        public long FileLength { get; set; }

        [DataMember]
        public string FileSet { get; set; }

        public override string Name
        {
            get
            {
                return "upload";
            }
        }

        protected override string Description
        {
            get
            {
                return string.Format("path = {0}", RemoteFileFullPath) +
                    (string.IsNullOrWhiteSpace(FileSet) ? "" : string.Format(" set = {1}", FileSet)) +
                    string.Format(" length = {0}", FileLength);
            }
        }
    }
}
