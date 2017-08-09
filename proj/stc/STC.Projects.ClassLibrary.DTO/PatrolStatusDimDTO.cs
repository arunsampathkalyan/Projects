using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PatrolStatusDimDTO
    {
        [DataMember]
        public int PatrolStatusId { get; set; }

        [DataMember]
        public string PatrolStatus { get; set; }
    }
}
