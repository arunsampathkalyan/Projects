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
    
    public partial class Conf_LayoutsPlaceHolders
    {
        public Conf_LayoutsPlaceHolders()
        {
            this.Conf_PageDetails = new HashSet<Conf_PageDetails>();
        }
    
        public long LayoutPlaceHolderId { get; set; }
        public int PlaceHolderID { get; set; }
        public int LayoutId { get; set; }
    
        public virtual Conf_Layout Conf_Layout { get; set; }
        public virtual Conf_PlaceHolder Conf_PlaceHolder { get; set; }
        public virtual ICollection<Conf_PageDetails> Conf_PageDetails { get; set; }
    }
}