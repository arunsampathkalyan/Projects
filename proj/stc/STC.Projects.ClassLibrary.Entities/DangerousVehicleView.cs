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
    
    public partial class DangerousVehicleView
    {
        public long ActivityId { get; set; }
        public string OriginalIdent { get; set; }
        public string SourceName { get; set; }
        public string ActivityCategoryCode { get; set; }
        public string ActivityCategoryDescription { get; set; }
        public string ActivityReasonCode { get; set; }
        public string ActivityReasonDescription { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ImportanceHighLowCode { get; set; }
        public string ImportanceHighLowDescription { get; set; }
        public string HighLowCode { get; set; }
        public string HighLowDescription { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public Nullable<System.DateTime> ActivityStartDate { get; set; }
        public Nullable<System.DateTime> ActivityEndDate { get; set; }
        public Nullable<System.DateTime> ActivityDueDate { get; set; }
        public Nullable<System.DateTime> ActivityExpirationDate { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<System.DateTime> ActivityDate { get; set; }
        public Nullable<System.DateTime> CreateDateTimeStamp { get; set; }
        public Nullable<System.DateTime> ModifiedDateTimeStamp { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string RowStatusCode { get; set; }
        public string RowStatusDescription { get; set; }
        public string PlateCode { get; set; }
        public string PlateKind { get; set; }
        public string PlateColor { get; set; }
        public string PlateNumber { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<int> CorrelationPeriod { get; set; }
    }
}
