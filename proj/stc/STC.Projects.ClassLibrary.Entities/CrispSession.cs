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
    
    public partial class CrispSession
    {
        public long SessionId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public string NodeXmlPath { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string SessionName { get; set; }
        public Nullable<int> MaxAllowedTimeMin { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
