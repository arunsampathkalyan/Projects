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
    
    public partial class Conf_UserControlMessage
    {
        public Conf_UserControlMessage()
        {
            this.Conf_ControlPageMessages = new HashSet<Conf_ControlPageMessages>();
        }
    
        public int UserControlId { get; set; }
        public int MessageTypeId { get; set; }
        public int UserControlMessageId { get; set; }
    
        public virtual ICollection<Conf_ControlPageMessages> Conf_ControlPageMessages { get; set; }
        public virtual Conf_UserControl Conf_UserControl { get; set; }
        public virtual Conf_MessageType Conf_MessageType { get; set; }
    }
}
