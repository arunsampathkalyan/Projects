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
    
    public partial class ManualViolation
    {
        public long ViolationId { get; set; }
        public string ViolationNumber { get; set; }
        public string StreetName { get; set; }
        public Nullable<System.DateTime> ViolationDate { get; set; }
        public Nullable<System.DateTime> ViolationTime { get; set; }
        public Nullable<int> ReasonId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> RadarClassId { get; set; }
        public Nullable<int> RadarTypeId { get; set; }
        public Nullable<int> EmirateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lon { get; set; }
        public string TCFNumber { get; set; }
        public Nullable<int> LicenseSourceId { get; set; }
        public Nullable<System.DateTime> LicenseIssueDate { get; set; }
        public string OwnerTCFNumber { get; set; }
        public string OwnerName { get; set; }
        public Nullable<long> PlateSourceId { get; set; }
        public Nullable<long> PlateKindId { get; set; }
        public Nullable<int> VehicleTypeId { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> AgeClassId { get; set; }
        public Nullable<int> LevelOfEducationId { get; set; }
        public Nullable<int> MaritalStatusId { get; set; }
        public Nullable<int> RoadSpeed { get; set; }
    
        public virtual AgeClass AgeClass { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual IncidentCity IncidentCity { get; set; }
        public virtual IncidentEmirate IncidentEmirate { get; set; }
        public virtual LevelOfEducation LevelOfEducation { get; set; }
        public virtual ManualViolationLicenseSource ManualViolationLicenseSource { get; set; }
        public virtual ManualViolationRadarClass ManualViolationRadarClass { get; set; }
        public virtual ManualViolationRadarType ManualViolationRadarType { get; set; }
        public virtual ManualViolationReason ManualViolationReason { get; set; }
        public virtual ManualViolationType ManualViolationType { get; set; }
        public virtual ManualViolationVehicleType ManualViolationVehicleType { get; set; }
        public virtual MaritalStatu MaritalStatu { get; set; }
        public virtual VehiclePlateKind VehiclePlateKind { get; set; }
        public virtual VehiclePlateSource VehiclePlateSource { get; set; }
    }
}
