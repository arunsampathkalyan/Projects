using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ExcelMapDTO
    {
        [DataMember]
        public int ExcelMapId { set; get; }
        [DataMember]
        public string ExcelMapFieldName { get; set; }
        [DataMember]
        public int ExcelMapFieldIndex { get; set; }
    }
}
