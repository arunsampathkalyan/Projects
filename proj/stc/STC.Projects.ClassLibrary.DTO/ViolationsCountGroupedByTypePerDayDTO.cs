using System;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountGroupedByTypePerDayDTO
    {
        [DataMember]
        public DateTime? ViolationDate { get; set; }

        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}