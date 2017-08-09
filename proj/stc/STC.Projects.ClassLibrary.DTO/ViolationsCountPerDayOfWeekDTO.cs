using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountPerDayOfWeekDTO
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}