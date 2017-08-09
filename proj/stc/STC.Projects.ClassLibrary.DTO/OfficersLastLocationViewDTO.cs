using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class OfficersLastLocationViewDTO
    {
        [DataMember]
        public int OfficerLastLocationId { get; set; }
        [DataMember]
        public int OfficerId { get; set; }
        [DataMember]
        public int Speed { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Altitude { get; set; }
        [DataMember]
        public System.DateTime LocationDate { get; set; }
        [DataMember]
        public double OfficerHeartPulse { get; set; }
        [DataMember]
        public bool IsNoticed { get; set; }
        [DataMember]
        public string OfficerName { get; set; }
        [DataMember]
        public string OfficerCode { get; set; }
        [DataMember]
        public string CameraURL { get; set; }
    }
}
