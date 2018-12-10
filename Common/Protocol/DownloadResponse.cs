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
        protected override string Description
        {
            get
            {
                return base.Description + string.Format(" length = {0}", FileLength);
            }
        }
    }
}
