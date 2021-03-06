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
    
    public partial class TFMIncident
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TFMIncident()
        {
            this.Staffs = new HashSet<Staff>();
        }
    
        public long Id { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<int> IncidentTypeId { get; set; }
        public Nullable<int> IncidentPriorityId { get; set; }
        public Nullable<System.Guid> PatrolId { get; set; }
        public string CallerName { get; set; }
        public string CallerNumber { get; set; }
        public string CallerAddress { get; set; }
        public Nullable<double> CallerLongitude { get; set; }
        public Nullable<double> CallerLatitude { get; set; }
        public Nullable<System.DateTime> IncidentDateTime { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public string Remarks { get; set; }
        public System.Guid SubId { get; set; }
        public Nullable<int> CallSource { get; set; }
        public string DispatcherName { get; set; }
        public Nullable<int> IncidentClosureReasonId { get; set; }
        public Nullable<int> CurrentStatusId { get; set; }
        public string IncidentReportGeneralComments { get; set; }
        public Nullable<int> IncidentSecondArrivalReasonId { get; set; }
        public Nullable<int> IncidentLateArrivalReasonId { get; set; }
        public string ClosureFromWebReason { get; set; }
        public Nullable<int> DispatcherId { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string Injury { get; set; }
        public string LanguageOfCaller { get; set; }
        public Nullable<int> InitialIncidentTypeId { get; set; }
        public Nullable<int> FinalIncidentTypeId { get; set; }
        public Nullable<int> ISSINumber { get; set; }
        public Nullable<int> DispatchedVehiclesNo { get; set; }
        public string CallTakerName { get; set; }
        public Nullable<int> CallTakerId { get; set; }
        public string OwnerWorkStationName { get; set; }
        public Nullable<System.DateTime> Ringing { get; set; }
        public Nullable<System.DateTime> CallReceived { get; set; }
        public Nullable<System.DateTime> IdentifiedLocation { get; set; }
        public Nullable<System.DateTime> SaveIncident { get; set; }
        public Nullable<System.DateTime> TransferIncident { get; set; }
        public Nullable<System.DateTime> IncidentOpenedByDipatcher { get; set; }
        public Nullable<System.DateTime> DispatchToVehicle { get; set; }
        public Nullable<System.DateTime> OnRoadTime { get; set; }
        public Nullable<System.DateTime> AtSeenTime { get; set; }
        public Nullable<System.DateTime> ClosedIncident { get; set; }
        public Nullable<System.DateTime> DisposeIncident { get; set; }
        public string IncidentSource { get; set; }
    
        public virtual Transporter Transporter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
