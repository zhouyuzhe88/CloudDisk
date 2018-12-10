using System;
using System.Runtime.Serialization;

namespace Common.Protocol
{
    public class ListRequest : Request
    {
        public override string Name
        {
            get
            {
                return "list";
            }
        }

        [DataMember]
        public string RemotePath { get; set; }

        [DataMember]
        public string FileSet { get; set; }

        protected override string Description
        {
            get
            {
                return string.Format("path = {0}", RemotePath) +
                    (string.IsNullOrWhiteSpace(FileSet) ? "" : string.Format(" set = {1}", FileSet));
            }
        }
    }
}
