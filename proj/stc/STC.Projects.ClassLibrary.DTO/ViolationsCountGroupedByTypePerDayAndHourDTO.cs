using System;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountGroupedByTypePerDayAndHourDTO
    {
        [DataMember]
        public DateTime? ViolationDate { get; set; }

        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public int? ViolationHour { get; set; }

        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}