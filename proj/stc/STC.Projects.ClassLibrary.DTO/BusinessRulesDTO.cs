using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class BusinessRulesDTO
    {
        public int BusinessRuleId { get; set; }
        public string BusinessName { get; set; }
        public string Priority { get; set; }
        public string VehicleType { get; set; }
        public string SpeedInsideCity { get { return IsOverSpeedInsideCity ? string.Format("Speed Threshold - {0} Km/H, No. of Violations - {1}", InsideCityOverSpeedValue, InsideCityOverSpeedQty) : "-"; } set { } }
        public string SpeedOutsideCity { get { return IsOverSpeedOutsideCity ? string.Format("Speed Threshold - {0} Km/H, No. of Violations - {1}", OutsideCityOverSpeedValue, OutsideCityOverSpeedQty) : "-"; } set { } }
        public string RedLightCrossing { get { return IsTrafficCross ? string.Format("{0} Seconds, No. of Violations - {1}", TrafficCrossTimesValue, TrafficCrossQty) : "-"; } set { } }

        public string BusinessRuleTime { get; set; }
        public bool IsOverSpeedInsideCity { get; set; }
        public int InsideCityOverSpeedQty { get; set; }
        public int? InsideCityOverSpeedId { get; set; }
        public int InsideCityOverSpeedValue { get; set; }
        public bool IsOverSpeedOutsideCity { get; set; }
        public int OutsideCityOverSpeedQty { get; set; }
        public int? OutsideCityOverSpeedId { get; set; }
        public int OutsideCityOverSpeedValue { get; set; }

        public bool IsTrafficCross { get; set; }
        public int? TrafficCrossTimesId { get; set; }
        public int TrafficCrossTimesValue { get; set; }
        public int TrafficCrossQty { get; set; }
        public int? VehicleTypeId { get; set; }
        public int RuleInterval { get; set; }
        public int? PriorityId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public int NumOfOccur { get; set; }
    }

    public class OverSpeedDTO
    {
        public int OverSpeedId { get; set; }
        public int OverSpeedValue { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TrafficCrossDTO
    {
        public int TrafficCrossId { get; set; }
        public int TrafficCrossValue { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class BusinessRulePriorityDTO
    {
        public int PriorityID { get; set; }
        public string PriorityNameEn { get; set; }
        public string PriorityNameAr { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class VehicleTypeDTO
    {
        public int VehicleTypeID { get; set; }
        public string VehicleTypeEn { get; set; }
        public string VehicleTypeAr { get; set; }
        public bool IsDeleted { get; set; }

    }
}
