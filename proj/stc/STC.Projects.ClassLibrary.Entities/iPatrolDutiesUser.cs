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
    
    public partial class iPatrolDutiesUser
    {
        public System.Guid StaffId { get; set; }
        public long DutyId { get; set; }
        public Nullable<int> DutyCommunicationType { get; set; }
    
        public virtual iPatrolDuty iPatrolDuty { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
