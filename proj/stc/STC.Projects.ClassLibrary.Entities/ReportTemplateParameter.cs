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
    
    public partial class ReportTemplateParameter
    {
        public int ReportTemplateParameterId { get; set; }
        public int TemplateId { get; set; }
        public int ParameterId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ReportsTemplate ReportsTemplate { get; set; }
        public virtual TemplateParameters TemplateParameters { get; set; }
    }
}
