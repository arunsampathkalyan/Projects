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
    
    public partial class IncidentEmirate
    {
        public IncidentEmirate()
        {
            this.Incident = new HashSet<Incident>();
            this.ManualViolations = new HashSet<ManualViolation>();
        }
    
        public int Id { get; set; }
        public string EmirateNameAr { get; set; }
        public string EmirateNameEn { get; set; }
    
        public virtual ICollection<Incident> Incident { get; set; }
        public virtual ICollection<ManualViolation> ManualViolations { get; set; }
    }
}
