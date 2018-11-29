using System.Runtime.Serialization;

namespace Common.Protocol
{
    public class CreateFolderRequest : Request
    {
        [DataMember]
        public string FullPath { get; set; }

        [DataMember]
        public string FileSet { get; set; }

        public override string Name
        {
            get
            {
                return "createFolder";
            }
        }
    }
}
