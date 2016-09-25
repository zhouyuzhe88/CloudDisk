﻿using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class UploadRequest : Request
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FilePath { get; set; }

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
    }
}
