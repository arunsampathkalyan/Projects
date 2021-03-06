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
    
    public partial class OrganizationView
    {
        public OrganizationView()
        {
            this.ActivityOrganizations = new HashSet<ActivityOrganization>();
        }
    
        public long OrganizationId { get; set; }
        public string OriginalIdent { get; set; }
        public string SourceName { get; set; }
        public string OrganizationCategoryCode { get; set; }
        public string OrganizationCategoryDescription { get; set; }
        public string OrganizationStatusCode { get; set; }
        public string OrganizationStatusDescription { get; set; }
        public string Name { get; set; }
        public string DoingBusinessAs { get; set; }
        public string Description { get; set; }
        public string AbbreviationText { get; set; }
        public string IMONumber { get; set; }
        public Nullable<System.DateTime> EstablishedDateTime { get; set; }
        public Nullable<System.DateTime> TerminationDateTime { get; set; }
        public Nullable<bool> IncorporatedIndicator { get; set; }
        public Nullable<System.DateTime> CreateDateTimeStamp { get; set; }
        public Nullable<System.DateTime> ModifiedDateTimeStamp { get; set; }
        public string RowStatusCode { get; set; }
        public string RowStatusDescription { get; set; }
    
        public virtual ICollection<ActivityOrganization> ActivityOrganizations { get; set; }
    }
}
