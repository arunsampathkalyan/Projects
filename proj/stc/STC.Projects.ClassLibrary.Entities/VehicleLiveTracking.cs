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
    
    public partial class VehicleLiveTracking
    {
        public long Id { get; set; }
        public string PlateNumber { get; set; }
        public string LicenseNumber { get; set; }
        public Nullable<System.DateTime> LicenseExpiryDate { get; set; }
        public string Model { get; set; }
        public string OwnerName { get; set; }
        public string OwnerMobileNumber { get; set; }
        public string OwnerNationality { get; set; }
        public short OwnerAge { get; set; }
        public long TowerId { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public bool IsWanted { get; set; }
        public bool IsNoticed { get; set; }
        public Nullable<System.DateTime> CaptureTime { get; set; }
        public string PlateKind { get; set; }
        public string PlateType { get; set; }
        public string PlateSource { get; set; }
        public string PlateColor { get; set; }
    }
}