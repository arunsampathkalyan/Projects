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
    
    public partial class SOPCommand
    {
        public SOPCommand()
        {
            this.NotificationSOPLogs = new HashSet<NotificationSOPLog>();
        }
    
        public int SOPCommandId { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
    
        public virtual ICollection<NotificationSOPLog> NotificationSOPLogs { get; set; }
    }
}
