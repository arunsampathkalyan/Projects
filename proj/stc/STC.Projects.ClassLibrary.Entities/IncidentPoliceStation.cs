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
    
    public partial class IncidentPoliceStation
    {
        public IncidentPoliceStation()
        {
            this.Incident = new HashSet<Incident>();
        }
    
        public int PoliceStationId { get; set; }
        public string PoliceStationNameAr { get; set; }
        public string PoliceStationNameEn { get; set; }
    
        public virtual ICollection<Incident> Incident { get; set; }
    }
}
