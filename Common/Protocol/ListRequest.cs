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
    }
}
