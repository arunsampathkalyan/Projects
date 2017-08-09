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
    
    public partial class VehicleViolationsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> TrafficPoint { get; set; }
        public Nullable<double> FineValue { get; set; }
        public Nullable<int> ViolationDuration { get; set; }
        public Nullable<int> PresenceAbsenceStatus { get; set; }
        public Nullable<int> ViolationClassificationId { get; set; }
    
        public virtual VehicleViolationClassification VehicleViolationClassification { get; set; }
    }
}