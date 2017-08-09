using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class DangerousVehicleDetailsDTO
    {
        [DataMember]
        public string PlateNumber { get; set; }
        [DataMember]
        public string PlateKind { get; set; }
        [DataMember]
        public string PlateSource { get; set; }
        [DataMember]
        public string PlateColor { get; set; }
        [DataMember]
        public int BusinessRuleId { get; set; }
        [DataMember]
        public string BusinessRuleName { get; set; }
        [DataMember]
        public List<ViolationNotificationDTO> VehicleViolations { get; set; }
        [DataMember]
        public CubeDTO VehiclePieKPIs { get; set; }
        [DataMember]
        public List<CubeDTO> VehicleLinerKPIs { get; set; }
    }
}
