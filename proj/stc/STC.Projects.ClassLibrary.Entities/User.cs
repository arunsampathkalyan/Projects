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
    
    public partial class User
    {
        public User()
        {
            this.Conf_UsersPages = new HashSet<Conf_UsersPages>();
            this.UserAreas = new HashSet<UserArea>();
            this.UserUserControlUsers = new HashSet<UserUserControlUser>();
            this.Notifications = new HashSet<Notification>();
            this.NotificationSOPLogs = new HashSet<NotificationSOPLog>();
            this.NotificationLogs = new HashSet<NotificationLog>();
            this.Notifications1 = new HashSet<Notification>();
            this.CorrelationBusinessRules = new HashSet<CorrelationBusinessRule>();
            this.CorrelationBusinessRules1 = new HashSet<CorrelationBusinessRule>();
            this.UserTokens = new HashSet<UserToken>();
            this.FAQs = new HashSet<FAQ>();
            this.Feedbacks = new HashSet<Feedback>();
            this.CrispSessions = new HashSet<CrispSession>();
            this.CrispSessions1 = new HashSet<CrispSession>();
            this.OfficerTask = new HashSet<OfficerTask>();
            this.AutomaticReports = new HashSet<AutomaticReports>();
            this.AutomaticReports1 = new HashSet<AutomaticReports>();
            this.AutomaticReportsLog = new HashSet<AutomaticReportsLog>();
            this.ReportsTemplate = new HashSet<ReportsTemplate>();
            this.ReportsTemplate1 = new HashSet<ReportsTemplate>();
            this.GroupUsers = new HashSet<GroupUser>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<bool> IsOwner { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public byte[] EncPassword { get; set; }
        public byte[] Salt { get; set; }
    
        public virtual ICollection<Conf_UsersPages> Conf_UsersPages { get; set; }
        public virtual ICollection<UserArea> UserAreas { get; set; }
        public virtual ICollection<UserUserControlUser> UserUserControlUsers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<NotificationSOPLog> NotificationSOPLogs { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
        public virtual ICollection<Notification> Notifications1 { get; set; }
        public virtual ICollection<CorrelationBusinessRule> CorrelationBusinessRules { get; set; }
        public virtual ICollection<CorrelationBusinessRule> CorrelationBusinessRules1 { get; set; }
        public virtual Rank Rank { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<FAQ> FAQs { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<CrispSession> CrispSessions { get; set; }
        public virtual ICollection<CrispSession> CrispSessions1 { get; set; }
        public virtual ICollection<OfficerTask> OfficerTask { get; set; }
        public virtual ICollection<AutomaticReports> AutomaticReports { get; set; }
        public virtual ICollection<AutomaticReports> AutomaticReports1 { get; set; }
        public virtual ICollection<AutomaticReportsLog> AutomaticReportsLog { get; set; }
        public virtual ICollection<ReportsTemplate> ReportsTemplate { get; set; }
        public virtual ICollection<ReportsTemplate> ReportsTemplate1 { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
