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
    
    public partial class Conf_SOPSourceSOPView
    {
        public int SOPId { get; set; }
        public string SOPContent { get; set; }
        public string SourceName { get; set; }
        public int Rank { get; set; }
        public int SourceId { get; set; }
        public string SOPViewModelType { get; set; }
        public string SOPViewDetailsControl { get; set; }
        public string SOPViewDetailsMessage { get; set; }
        public string MessageDetailsXSLT { get; set; }
        public string SOPViewListMessage { get; set; }
        public string MessageListXSLT { get; set; }
        public Nullable<int> PriorityId { get; set; }
        public Nullable<int> SOPViewDetailsMessageId { get; set; }
        public Nullable<int> SOPViewListMessageId { get; set; }
    }
}
