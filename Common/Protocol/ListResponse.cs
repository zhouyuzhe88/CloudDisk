using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Protocol
{
    public class ListResponse : Response
    {
        public override string Name
        {
            get
            {
                return "list";
            }
        }

        [DataMember]
        public List<CloudFileInfo> Files { get; set; }
    }
}
