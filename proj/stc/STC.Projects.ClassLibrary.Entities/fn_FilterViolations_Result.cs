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
    
    public partial class fn_FilterViolations_Result
    {
        public long ViolationNotificationId { get; set; }
        public string AssetId { get; set; }
        public string AssetCode { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public double Altitude { get; set; }
        public string LocationCode { get; set; }
        public int ViolationTypeId { get; set; }
        public string ViolationTypeName { get; set; }
        public int ViolationStatusId { get; set; }
        public string ViolationStatusName { get; set; }
    }
}
