using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsCountGroupedByTypeAndLocationPerDayOfWeekDTO
    {
        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public int LocationCode { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public int? Count { get; set; }
    }
}