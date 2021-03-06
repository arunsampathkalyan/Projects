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
    
    public partial class ActivityOrganization
    {
        public long ActivityOrganizationId { get; set; }
        public string ActivityOrganizationDescription { get; set; }
        public long ActivityId { get; set; }
        public long Organizationid { get; set; }
        public string SourceName { get; set; }
        public string OrganizationInvolvementCode { get; set; }
        public string OrganizationInvolvementDescription { get; set; }
        public Nullable<System.DateTime> CreateDateTimeStamp { get; set; }
        public Nullable<System.DateTime> ModifiedDateTimeStamp { get; set; }
        public string RowStatusCode { get; set; }
        public string RowStatusDescription { get; set; }
    
        public virtual OrganizationView Organization { get; set; }
        public virtual ServiceCall ServiceCall { get; set; }
        public virtual ServiceCallInsurance ServiceCallInsurance { get; set; }
    }
}
