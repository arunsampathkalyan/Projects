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
    
    public partial class ItemRegistrationPlateCodeDIM
    {
        public ItemRegistrationPlateCodeDIM()
        {
            this.ItemRegistrations = new HashSet<ItemRegistration>();
        }
    
        public int ItemRegistrationPlateCodeId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> VersionDateTime { get; set; }
        public Nullable<int> RowStatusId { get; set; }
    
        public virtual ICollection<ItemRegistration> ItemRegistrations { get; set; }
    }
}
