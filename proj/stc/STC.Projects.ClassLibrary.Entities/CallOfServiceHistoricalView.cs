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
    
    public partial class CallOfServiceHistoricalView
    {
        public long ActivityId { get; set; }
        public string OriginalIdent { get; set; }
        public string ServiceCallCategoryCode { get; set; }
        public string ServiceCallCategoryDescription { get; set; }
        public string ServiceCallPriorityCode { get; set; }
        public string ServiceCallPriorityDescription { get; set; }
        public string StatusDescription { get; set; }
        public string StatusCode { get; set; }
        public Nullable<double> IncidentLat { get; set; }
        public Nullable<double> IncidentLong { get; set; }
        public string IncidentAddress { get; set; }
        public string CallerName { get; set; }
        public string CallerAddress { get; set; }
        public string CallerNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public System.Data.Entity.Spatial.DbGeometry geopoint { get; set; }
    }
}
