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
    
    public partial class AutomaticReports
    {
        public AutomaticReports()
        {
            this.AutomaticReportDiminsion = new HashSet<AutomaticReportDiminsion>();
            this.AutomaticReportGroups = new HashSet<AutomaticReportGroups>();
        }
    
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public int TemplateId { get; set; }
        public bool IsEmail { get; set; }
        public bool IsSMS { get; set; }
        public int ScheduleId { get; set; }
        public bool IsArabic { get; set; }
        public bool IsEnglish { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<AutomaticReportDiminsion> AutomaticReportDiminsion { get; set; }
        public virtual ICollection<AutomaticReportGroups> AutomaticReportGroups { get; set; }
        public virtual ReportSchedule ReportSchedule { get; set; }
        public virtual ReportsTemplate ReportsTemplate { get; set; }
        public virtual User Users { get; set; }
        public virtual User Users1 { get; set; }
    }
}
