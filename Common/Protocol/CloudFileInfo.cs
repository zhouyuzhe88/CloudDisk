using System.Runtime.Serialization;

namespace Common.Protocol
{
    [DataContract]
    public class CloudFileInfo
    {
        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public long FileLength { get; set; }

        [DataMember]
        public bool IsDirectory { get; set; }

        public CloudFileInfo(string filePath, long fileLength, bool isDirectory)
        {
            FilePath = filePath;
            FileLength = fileLength;
            IsDirectory = isDirectory;
        }
    }
}
