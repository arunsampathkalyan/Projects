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
    
    public partial class ContactItem
    {
        public int ContactItemId { get; set; }
        public string Contact { get; set; }
        public string Data { get; set; }
        public Nullable<int> EmergencyContactId { get; set; }
    
        public virtual EmergencyContact EmergencyContact { get; set; }
    }
}
