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
    
    public partial class AssetsOperationalView
    {
        public long ItemId { get; set; }
        public string OriginalIdent { get; set; }
        public string ItemName { get; set; }
        public long SourceId { get; set; }
        public string SourceName { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public Nullable<int> ModelYear { get; set; }
        public Nullable<int> ItemStatusId { get; set; }
        public string ItemStatusName { get; set; }
        public Nullable<int> ItemMakeModelId { get; set; }
        public string ItemMakeName { get; set; }
        public string ItemModelName { get; set; }
        public string LocationInvolvementName { get; set; }
        public string LocationCode { get; set; }
        public Nullable<double> Altitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string ModelCode { get; set; }
        public string MakeCode { get; set; }
        public string ItemUsageName { get; set; }
        public Nullable<int> ItemUsageId { get; set; }
    }
}
