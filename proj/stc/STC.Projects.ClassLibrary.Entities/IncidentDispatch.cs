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
    
    public partial class IncidentDispatch
    {
        public long IncidentDispatchId { get; set; }
        public long IncidentId { get; set; }
        public long PatrolId { get; set; }
        public Nullable<bool> IsByVehicle { get; set; }
        public System.DateTime DispatchDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Patrol Patrol { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
