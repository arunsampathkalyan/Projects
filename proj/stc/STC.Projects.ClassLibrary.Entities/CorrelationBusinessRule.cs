//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STC.Projects.ClassLibrary.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class CorrelationBusinessRule
    {
        public int BusinessRuleId { get; set; }
        public string BusinessName { get; set; }
        public bool IsOverSpeedInsideCity { get; set; }
        public int InsideCityOverSpeedQty { get; set; }
        public Nullable<int> InsideCityOverSpeedId { get; set; }
        public bool IsOverSpeedOutsideCity { get; set; }
        public int OutsideCityOverSpeedQty { get; set; }
        public Nullable<int> OutsideCityOverSpeedId { get; set; }
        public bool IsTrafficCross { get; set; }
        public Nullable<int> TrafficCrossTimesId { get; set; }
        public int TrafficCrossQty { get; set; }
        public Nullable<int> VehicleTypeId { get; set; }
        public int RuleInterval { get; set; }
        public Nullable<int> PriorityId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<int> NumberOfOccur { get; set; }
    
        public virtual BusinessRulePriority BusinessRulePriority { get; set; }
        public virtual OverSpeed OverSpeed { get; set; }
        public virtual OverSpeed OverSpeed1 { get; set; }
        public virtual TrafficCrossTime TrafficCrossTime { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}