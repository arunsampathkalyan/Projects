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
    
    public partial class PublicUser
    {
        public PublicUser()
        {
            this.UserTokens = new HashSet<UserToken>();
            this.FAQs = new HashSet<FAQ>();
            this.Feedbacks = new HashSet<Feedback>();
        }
    
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Mail { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public string ImageURL { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string NotificationToken { get; set; }
    
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<FAQ> FAQs { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
