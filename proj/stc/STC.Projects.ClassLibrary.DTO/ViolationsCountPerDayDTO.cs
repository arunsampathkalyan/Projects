using System;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountPerDayDTO
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public DateTime? ViolationDate { get; set; }

        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}