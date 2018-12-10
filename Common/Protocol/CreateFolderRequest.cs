using System;
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

        protected override string Description
        {
            get
            {
                return string.Format("path = {0}", FullPath) + 
                    (string.IsNullOrWhiteSpace(FileSet) ? "" : string.Format(" set = {1}", FileSet));
            }
        }
    }
}
