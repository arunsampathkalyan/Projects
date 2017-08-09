using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class AccidentFileDetailsDTO
    {
        [DataMember]
        public string OriginalFileName { get; set; }
        [DataMember]
        public int UploadedByUserId { get; set; }
        [DataMember]
        public List<AccidentStandardDTO> Accidents { get; set; }
    }
}
