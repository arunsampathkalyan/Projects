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
    
    public partial class VehiclePlateKind
    {
        public VehiclePlateKind()
        {
            this.ManualViolations = new HashSet<ManualViolation>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Code { get; set; }
    
        public virtual ICollection<ManualViolation> ManualViolations { get; set; }
    }
}
