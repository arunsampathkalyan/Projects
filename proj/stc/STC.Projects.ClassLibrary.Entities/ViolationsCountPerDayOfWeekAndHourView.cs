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
    
    public partial class ViolationsCountPerDayOfWeekAndHourView
    {
        public long id { get; set; }
        public string DayOfWeekName { get; set; }
        public Nullable<int> ViolationHour { get; set; }
        public Nullable<int> Count { get; set; }
    }
}
