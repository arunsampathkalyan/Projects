using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public enum PeriodCategory
    {
        Daily = 1,
        Weekly,
        Monthly,
        Yearly
    }

    [DataContract]
    public class ViolationsHistoricalDTO
    {
        public ViolationsHistoricalDTO()
        {
            ViolationsByLocations = new List<ViolationsGroupedByLocationsDTO>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public PeriodCategory ScheduleType { get; set; }

        [DataMember]
        public List<ViolationsGroupedByLocationsDTO> ViolationsByLocations { get; set; }

    }

    [DataContract]
    public class ViolationsCountForMapDTO
    {
        [DataMember]
        public string DateElement { get; set; }

        [DataMember]
        public List<ViolationsGroupedByLocationsDTO> AssetDetails { get; set; }
    }

    [DataContract]
    public class ViolationsGroupedByLocationsDTO
    {
        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public double Altitude { get; set; }

        [DataMember]
        public string LocationCode { get; set; }

        [DataMember]
        public int? ViolationsCount { get; set; }
    }
}
