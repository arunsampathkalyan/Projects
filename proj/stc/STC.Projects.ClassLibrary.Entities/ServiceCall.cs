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
    
    public partial class ServiceCall
    {
        public ServiceCall()
        {
            this.ActivityLocations = new HashSet<ActivityLocationView>();
            this.ActivityPersons = new HashSet<ActivityPersonView>();
            this.ActivityOrganizations = new HashSet<ActivityOrganization>();
            this.ActivityItems = new HashSet<ActivityItemView>();
        }
    
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
        public string ServiceCallCategoryCode { get; set; }
        public string ServiceCallCategoryDescription { get; set; }
        public string ServiceCallSourceCode { get; set; }
        public string ServiceCallSourceDescription { get; set; }
        public string ServiceCallDispositionCode { get; set; }
        public string ServiceCallDispositionDescription { get; set; }
        public string ServiceCallClearanceCode { get; set; }
        public string ServiceCallClearanceDescription { get; set; }
        public string ServiceCallPriorityCode { get; set; }
        public string ServiceCallPriorityDescription { get; set; }
        public string ServiceClassCode { get; set; }
        public string ServiceClassDescription { get; set; }
        public string ServiceCallStartMechanismCode { get; set; }
        public string ServiceCallStartMechanismDescription { get; set; }
        public string ServiceCallEndMechanismCode { get; set; }
        public string ServiceCallEndMechanismDescription { get; set; }
        public Nullable<System.DateTime> ServiceCallArrivedDate { get; set; }
        public Nullable<System.DateTime> ServiceCallDispatchedDate { get; set; }
        public Nullable<System.DateTime> ServiceCallClearedDate { get; set; }
        public Nullable<System.DateTime> ServiceCallReceivedDate { get; set; }
        public Nullable<System.DateTime> ServiceCallStagedDate { get; set; }
        public Nullable<System.DateTime> ServiceCallInControlDate { get; set; }
        public Nullable<System.DateTime> ServiceCallEnrouteDate { get; set; }
        public Nullable<bool> HazardCallIndicator { get; set; }
        public Nullable<bool> IsSelfInitiated { get; set; }
        public Nullable<bool> HasPriorsIndicator { get; set; }
        public Nullable<bool> OutofServiceIndicator { get; set; }
        public Nullable<int> LanesCount { get; set; }
        public Nullable<int> SlightInjuriesCount { get; set; }
        public Nullable<int> MediumInjuriesCount { get; set; }
        public Nullable<int> SevereInjuriesCount { get; set; }
        public Nullable<int> FatalitiesCount { get; set; }
        public Nullable<int> TotalInjuriesFatalities { get; set; }
        public string CrashSeverityCode { get; set; }
        public string CrashSeverityDescription { get; set; }
        public string LightingConditionCode { get; set; }
        public string LightingConditionDescription { get; set; }
    
        public virtual ICollection<ActivityLocationView> ActivityLocations { get; set; }
        public virtual ICollection<ActivityPersonView> ActivityPersons { get; set; }
        public virtual ICollection<ActivityOrganization> ActivityOrganizations { get; set; }
        public virtual ICollection<ActivityItemView> ActivityItems { get; set; }
    }
}