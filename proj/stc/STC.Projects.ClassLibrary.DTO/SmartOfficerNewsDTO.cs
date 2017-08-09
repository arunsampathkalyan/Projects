using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class SmartOfficerNewsDTO
    {
        [DataMember]
        public long OfficerNewsId { get; set; }
        [DataMember]
        public string OfficerNewsText { get; set; }
        [DataMember]
        public string OfficerNewsImage { get; set; }
        [DataMember]
        public DateTime OfficerNewsDate { get; set; }
        [DataMember]
        public int OfficerNewsCreatedBy { get; set; }
        [DataMember]
        public bool IsNoticed { get; set; }
    }
}
