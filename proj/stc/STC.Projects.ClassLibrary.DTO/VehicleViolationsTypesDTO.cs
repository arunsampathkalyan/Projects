using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class VehicleViolationsTypesDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public int TrafficPoint { get; set; }
        [DataMember]
        public double FineValue { get; set; }
        [DataMember]
        public int ViolationDuration { get; set; }
        [DataMember]
        public int PresenseAbsenceStatus { get; set; }
        [DataMember]
        public int ViolationClassficationId { get; set; }
    }
}
