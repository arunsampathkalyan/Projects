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
    
    public partial class OverSpeed
    {
        public OverSpeed()
        {
            this.CorrelationBusinessRules = new HashSet<CorrelationBusinessRule>();
            this.CorrelationBusinessRules1 = new HashSet<CorrelationBusinessRule>();
            this.CorrelationViolationDetails = new HashSet<CorrelationViolationDetail>();
        }
    
        public int OverSpeedId { get; set; }
        public int OverSpeedValue { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<CorrelationBusinessRule> CorrelationBusinessRules { get; set; }
        public virtual ICollection<CorrelationBusinessRule> CorrelationBusinessRules1 { get; set; }
        public virtual ICollection<CorrelationViolationDetail> CorrelationViolationDetails { get; set; }
    }
}
