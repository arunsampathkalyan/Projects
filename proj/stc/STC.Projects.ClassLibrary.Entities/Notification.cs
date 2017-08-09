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
    
    public partial class Notification
    {
        public Notification()
        {
            this.NotificationSOPLogs = new HashSet<NotificationSOPLog>();
            this.UsersUserControls = new HashSet<UsersUserControl>();
            this.ViolationNotifications = new HashSet<ViolationNotification>();
            this.NotificationLogs = new HashSet<NotificationLog>();
            this.IncidentDispatches = new HashSet<IncidentDispatch>();
            this.Incident = new HashSet<Incident>();
        }
    
        public long NotificationId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int LastStatus { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> OwnerId { get; set; }
        public Nullable<bool> IsNoticed { get; set; }
    
        public virtual NotificationStatu NotificationStatu { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<NotificationSOPLog> NotificationSOPLogs { get; set; }
        public virtual ICollection<UsersUserControl> UsersUserControls { get; set; }
        public virtual ICollection<ViolationNotification> ViolationNotifications { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<IncidentDispatch> IncidentDispatches { get; set; }
        public virtual ICollection<Incident> Incident { get; set; }
    }
}