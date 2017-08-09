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
    
    public partial class CorrelationMessagesLog
    {
        public long Id { get; set; }
        public long MessageId { get; set; }
        public long BusinessRuleId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime CorrelationDate { get; set; }
        public string PlateNumber { get; set; }
        public string PlateKind { get; set; }
        public string PlateColor { get; set; }
        public string PlateSource { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual NewCorrelationRule NewCorrelationRule { get; set; }
        public virtual UsersUserControl UsersUserControl { get; set; }
    }
}