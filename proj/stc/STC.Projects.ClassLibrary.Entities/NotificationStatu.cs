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
    
    public partial class NotificationStatu
    {
        public NotificationStatu()
        {
            this.Notifications = new HashSet<Notification>();
            this.NotificationLogs = new HashSet<NotificationLog>();
            this.NotificationLogs1 = new HashSet<NotificationLog>();
        }
    
        public int NotificationStatusId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs1 { get; set; }
    }
}