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
    
    public partial class Conf_UserControlPublisherMessage
    {
        public Conf_UserControlPublisherMessage()
        {
            this.Conf_ControlPagePublisherMessages = new HashSet<Conf_ControlPagePublisherMessages>();
        }
    
        public int UserControlId { get; set; }
        public int MessageTypeId { get; set; }
        public int UserControlPublisherMessageId { get; set; }
    
        public virtual ICollection<Conf_ControlPagePublisherMessages> Conf_ControlPagePublisherMessages { get; set; }
        public virtual Conf_UserControl Conf_UserControl { get; set; }
        public virtual Conf_MessageType Conf_MessageType { get; set; }
    }
}