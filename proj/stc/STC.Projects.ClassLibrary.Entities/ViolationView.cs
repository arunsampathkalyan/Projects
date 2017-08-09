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
    
    public partial class ViolationView
    {
        public Nullable<System.DateTime> SimpleDate2 { get; set; }
        public string ViolationHour { get; set; }
        public int LicensePlateCameraId { get; set; }
        public string SerialNumber { get; set; }
        public string originalident { get; set; }
        public long Activityid { get; set; }
        public string PlateIdentification { get; set; }
        public Nullable<System.DateTime> ActivityDate { get; set; }
        public int ViolationTypeId { get; set; }
        public string ViolationType { get; set; }
        public Nullable<int> RecordedSpeedRate { get; set; }
        public Nullable<int> LegalSpeedLimitForCapture { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public System.Data.Entity.Spatial.DbGeometry GeoPoint { get; set; }
        public long Locationid { get; set; }
        public int ItemRegistrationAuthorityId { get; set; }
        public string PlateAuthority { get; set; }
        public string PlateCategory { get; set; }
        public int ItemRegistrationPlateCategoryId { get; set; }
        public string PlateKind { get; set; }
        public int ItemRegistrationPlateKindid { get; set; }
        public Nullable<int> TimePeriodId { get; set; }
        public Nullable<int> SpeedId { get; set; }
        public int ViolationInsideOutsideCityId { get; set; }
        public Nullable<int> GeoStateId { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
    }
}
