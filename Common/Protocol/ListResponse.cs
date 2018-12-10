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

        protected override string Description
        {
            get
            {
                if (Files != null)
                {
                    return base.Description + string.Format(" fileCount = {0}", Files.Count);
                }
                return base.Description;
            }
        }
    }
}
