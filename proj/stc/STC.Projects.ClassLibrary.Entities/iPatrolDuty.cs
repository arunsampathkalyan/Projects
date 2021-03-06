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
    
    public partial class iPatrolDuty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public iPatrolDuty()
        {
            this.iPatrolDutiesUsers = new HashSet<iPatrolDutiesUser>();
        }
    
        public long Id { get; set; }
        public string Duty { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> DutySource { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<System.DateTime> AcknowledgementTime { get; set; }
        public Nullable<System.Guid> PatrolId { get; set; }
        public Nullable<double> AcknowledgementLatitude { get; set; }
        public Nullable<double> AcknowledgementLongitude { get; set; }
        public Nullable<int> LocationType { get; set; }
        public Nullable<int> Radius { get; set; }
        public Nullable<double> ArrivalLongitude { get; set; }
        public Nullable<double> ArrivalLatitude { get; set; }
        public Nullable<System.DateTime> ArrivalTime { get; set; }
        public Nullable<double> FinishLongitude { get; set; }
        public Nullable<double> FinishLatitude { get; set; }
        public Nullable<System.DateTime> FinishTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iPatrolDutiesUser> iPatrolDutiesUsers { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}
