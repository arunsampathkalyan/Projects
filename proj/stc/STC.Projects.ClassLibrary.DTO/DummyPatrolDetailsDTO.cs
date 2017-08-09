using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PatrolFullDetailsDTO
    {
        public PatrolFullDetailsDTO()
        {
            PatrolDetails = new PatrolDetailsDTO();
            PatrolOfficers = new PatrolOfficersDTO();
            PatrolPerformance = new PatrolPerformanceDTO();
        }
        [DataMember]
        public PatrolDetailsDTO PatrolDetails { get; set; }
        [DataMember]
        public PatrolOfficersDTO PatrolOfficers { get; set; }
        [DataMember]
        public PatrolPerformanceDTO PatrolPerformance { get; set; }
    }

    [DataContract]
    public class PatrolDetailsDTO
    {
        [DataMember]
        public int PatrolId { get; set; }
        [DataMember]
        public string BasicAllocationHub { get; set; }
    }

    [DataContract]
    public class PatrolOfficersDTO
    {
        [DataMember]
        public string OfficersName { get; set; }
        [DataMember]
        public string OfficersMilitaryNumbers { get; set; }
        [DataMember]
        public string OfficersPhoneNumbers { get; set; }
    }

    [DataContract]
    public class PatrolPerformanceDTO
    {
        [DataMember]
        public string LastResponseTime { get; set; }
        [DataMember]
        public string LastIncidentTime { get; set; }
        [DataMember]
        public int NumActiveHours { get; set; }
        [DataMember]
        public string AverageResponseTime { get; set; }
        [DataMember]
        public int NumberOfIncidentHandled { get; set; }
    }
}
