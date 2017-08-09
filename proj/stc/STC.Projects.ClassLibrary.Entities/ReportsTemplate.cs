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
    
    public partial class ReportsTemplate
    {
        public ReportsTemplate()
        {
            this.AutomaticReports = new HashSet<AutomaticReports>();
            this.ReportTemplateParameter = new HashSet<ReportTemplateParameter>();
        }
    
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public bool IsEmail { get; set; }
        public bool IsSMS { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> LastModifiedUserId { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string EmailSubjectArabic { get; set; }
        public string EmailSubjectEnglish { get; set; }
    
        public virtual ICollection<AutomaticReports> AutomaticReports { get; set; }
        public virtual User Users { get; set; }
        public virtual User Users1 { get; set; }
        public virtual ICollection<ReportTemplateParameter> ReportTemplateParameter { get; set; }
    }
}