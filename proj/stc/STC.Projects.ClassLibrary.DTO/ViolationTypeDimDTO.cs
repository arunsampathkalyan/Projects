using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
   public class ViolationTypeDimDTO
    {
        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationType { get; set; }
    }
}
