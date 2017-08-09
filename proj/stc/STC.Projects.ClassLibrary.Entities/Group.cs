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
    
    public partial class Group
    {
        public Group()
        {
            this.AutomaticReportGroups = new HashSet<AutomaticReportGroups>();
            this.GroupUsers = new HashSet<GroupUser>();
        }
    
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<AutomaticReportGroups> AutomaticReportGroups { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
