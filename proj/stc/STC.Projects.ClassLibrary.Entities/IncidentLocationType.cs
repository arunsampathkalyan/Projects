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
    
    public partial class IncidentLocationType
    {
        public IncidentLocationType()
        {
            this.Incident = new HashSet<Incident>();
        }
    
        public int Id { get; set; }
        public string LocationTypeNameAr { get; set; }
        public string LocationTypeNameEn { get; set; }
    
        public virtual ICollection<Incident> Incident { get; set; }
    }
}
