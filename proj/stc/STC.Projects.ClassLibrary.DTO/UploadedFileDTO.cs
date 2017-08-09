using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public enum FileSourceTypes
    {
        None = 0,
        Accident = 1,
        PresentViolation = 2,
        AbsenseViolation = 3,
        ArabicFileAccident = 4,
        PDOAccident = 5
    }

    [DataContract]
    public class UploadedFileDTO
    {
        [DataMember]
        public int FileId { get; set; }
        [DataMember]
        public string FileOriginalName { get; set; }
        [DataMember]
        public DateTime FileUploadTime { get; set; }
        [DataMember]
        public int FileUploadedById { get; set; }
        [DataMember]
        public UsersDTO FileUploadedBy { get; set; }
        [DataMember]
        public FileSourceTypes FileSourceType { get; set; }
        [DataMember]
        public int FileInsertedRowsCount { get; set; }
        [DataMember]
        public int FileDuplicatedRowsCount { get; set; }
        [DataMember]
        public int FileCorruptedRowsCount { get; set; }
    }
}
