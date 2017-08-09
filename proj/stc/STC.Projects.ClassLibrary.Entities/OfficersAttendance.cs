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
    
    public partial class OfficersAttendance
    {
        public long Id { get; set; }
        public Nullable<System.Guid> StaffId { get; set; }
        public Nullable<System.DateTime> LoginDateTime { get; set; }
        public Nullable<double> LoginLongitude { get; set; }
        public Nullable<double> LoginLatitude { get; set; }
        public Nullable<System.DateTime> LogoutDateTime { get; set; }
        public Nullable<double> LogoutLongitude { get; set; }
        public Nullable<double> LogoutLatitude { get; set; }
        public string MDTDeviceCode { get; set; }
        public Nullable<System.Guid> PatrolId { get; set; }
        public Nullable<bool> IsSeen { get; set; }
        public Nullable<System.DateTime> SeenAt { get; set; }
        public Nullable<int> AssignedDutiesNumber { get; set; }
        public Nullable<int> AssignedIncidentsNumber { get; set; }
        public Nullable<int> FastResponseNumber { get; set; }
        public Nullable<int> InquiriesNumber { get; set; }
        public Nullable<int> ManualIncidentsNumber { get; set; }
        public Nullable<System.DateTime> MDTLogoutDateTime { get; set; }
        public Nullable<int> SecurityNotesNumber { get; set; }
        public Nullable<int> ViolationsNumber { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}