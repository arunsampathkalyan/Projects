using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class SmartOfficerDTO
    {
        [DataMember]
        public long OfficerId { get; set; }
        [DataMember]
        public string OfficerMilitaryId { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public string OfficerPatrolPlateNumber { get; set; }
        [DataMember]
        public byte[] OfficerImage { get; set; }
        [DataMember]
        public string OfficerName { get; set; }
    }
}
