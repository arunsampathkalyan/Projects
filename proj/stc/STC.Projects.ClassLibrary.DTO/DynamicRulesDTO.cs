using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class DynamicRulesDTO
    {
        [DataMember]
        public long RuleId { get; set; }
        [DataMember]
        public string RuleName { get; set; }
        [DataMember]
        public BusinessRulePriorityDTO Priority { get; set; }
        [DataMember]
        public int TimeIntervalMins { get; set; }
        [DataMember]
        public BusinessRuleRegionDTO LocationsDetails { get; set; }
        [DataMember]
        public List<BusinessRuleViolationsDetailsDTO> ViolationDetails { get; set; }
        [DataMember]
        public BusinessRuleVehicleDetailsDTO VehicleDetails { get; set; }
        [DataMember]
        public BusinessDriverDetailsDTO DriverDetails { get; set; }
        [DataMember]
        public TimeDetailsDTO TimeDetails { get; set; }
        [DataMember]
        public int CreatedById { get; set; }
        [DataMember]
        public int? ModifiedBy { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int NumberOfOccerance { get; set; }
    }

    [DataContract]
    public class BusinessDriverDetailsDTO
    {
        [DataMember]
        public int? MinAge { get; set; }
        [DataMember]
        public int? MaxAge { get; set; }
    }

    [DataContract]
    public class BusinessRuleVehicleDetailsDTO
    {
        [DataMember]
        public VehicleTypeDTO VehicleType { get; set; }
        [DataMember]
        public int? VehicleYear { get; set; }
        [DataMember]
        public VehicleBrandDTO VehicleBrand { get; set;}
    }

    [DataContract]
    public class VehicleBrandDTO
    {
        [DataMember]
        public int VehicleBrandId { get; set; }
        [DataMember]
        public string VehicleBrandName { get; set; }
    }

    [DataContract]
    public class BusinessRuleViolationsDetailsDTO
    {
        [DataMember]
        public ViolationTypesEnum ViolationType { get; set; }
        [DataMember]
        public OverSpeedDTO SpeedOverKMsDetails { get; set; }
        [DataMember]
        public TrafficCrossDTO TrafficCrossDetails { get; set; }
        [DataMember]
        public int ViolationQty { get; set; }

    }
    [DataContract]
    public class TimeDetailsDTO
    {
        [DataMember]
        public int TimeDetailsId { get; set; }
        [DataMember]
        public int TimeType { get; set; }
        [DataMember]
        public DateTime? FromDate { get; set; }
        [DataMember]
        public DateTime? ToDate { get; set; }
        [DataMember]
        public TimeSpan? FromTime { get; set; }
        [DataMember]
        public TimeSpan? ToTime { get; set; }
        [DataMember]
        public List<int> WeekDays { get; set; }
    }
    [DataContract]
    public class BusinessRuleRegionDTO
    {
        public BusinessRuleRegionDTO()
        {
            RegionPoints = new List<MapPointDTO>();
            RegionTypes = (int)RegionType.NORMAL;
        }

        [DataMember]
        public int RegionId { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public int RegionTypes { get; set; }
        [DataMember]
        public List<MapPointDTO> RegionPoints { get; set; }
    }

    
    [DataContract]
    public enum RegionType
    {
        NORMAL=0,
        CUSTEM,
        STREET
    }

    [DataContract]
    public enum ScheduleType
    {
        ALWAYS =0,
        SPECIFIC,
        WEEKDAYS
    }

    [DataContract]
    public class MapPointDTO
    {
        [DataMember]
        public double Latitude {get;set;}
        [DataMember]
        public double Longitude {get;set;}
    }
}
