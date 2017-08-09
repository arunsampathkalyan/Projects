using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class UploadedFileReportDTO
    {
        [DataMember]
        public int InsertedRowsCount { get; set; }
        [DataMember]
        public int CorruptedRowsCount { get; set; }
        [DataMember]
        public int DuplicatedRowsCount { get; set; }
    }
}
