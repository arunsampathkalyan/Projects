using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class OfficerTaskDTO
    {
        [DataMember]
        public long OfficerTaskId { get; set; }
        [DataMember]
        public string OfficerMilitaryId { get; set; }
        [DataMember]
        public string TaskMessage { get; set; }
        [DataMember]
        public DateTime TaskTime { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public TaskStatusDTO TaskLastStatus { get; set; }
    }

    [DataContract]
    public class TaskStatusDTO
    {
        [DataMember]
        public int TaskStatusId { get; set; }
        [DataMember]
        public string TaskStatusNameAr { get; set; }
        [DataMember]
        public string TaskStatusNameEn { get; set; }
    }

    [DataContract]
    public class OfficerTaskStatusDTO
    {
        [DataMember]
        public long OfficerTaskStatusId { get; set; }
        [DataMember]
        public long OfficerTaskId { get; set; }
        [DataMember]
        public int TaskStatusId { get; set; }
        [DataMember]
        public DateTime StatusUpdateDate { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}
