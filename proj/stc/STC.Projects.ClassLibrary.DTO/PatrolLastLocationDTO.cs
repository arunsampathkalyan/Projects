using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class PatrolLastLocationDTO
    {
        [DataMember]
        public long PatrolLatLocationId { get; set; }

        [DataMember]
        public long PatrolId { get; set; }

        [DataMember]
        public string PatrolCode { get; set; }

        [DataMember]
        public int Speed { get; set; }

        [DataMember]
        public double? Latitude { get; set; }

        [DataMember]
        public double? Longitude { get; set; }

        [DataMember]
        public double? Altitude { get; set; }
        //public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }

        [DataMember]
        public DateTime LocationDate { get; set; }

        [DataMember]
        public bool IsNoticed { get; set; }

        [DataMember]
        public int? StatusId { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string PatrolPlateNo { get; set; }

        [DataMember]
        public string OfficerName { get; set; }

        [DataMember]
        public byte[] PatrolImage { get; set; }

        [DataMember]
        public System.Guid PatrolOriginalId { get; set; }

        [DataMember]
        public int NumberOfAssignedIncident { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public bool IsRecommended { get; set; }
        [DataMember]
        public double? ETATime { get; set; }
        [DataMember]
        public bool isDeleted { get; set; }
        [DataMember]
        public bool isPatrol { get; set; }
        [DataMember]
        public long CurrentTaskId { get; set; }
        [DataMember]
        public bool IsBusy { get; set; }
    }

    [DataContract]
    public class PatrolDTO
    {
        [DataMember]
        public long PatrolId { get; set; }
        [DataMember]
        public string PatrolCode { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public string PatrolPlateNo { get; set; }
        [DataMember]
        public System.Guid PatrolOriginalId { get; set; }
        [DataMember]
        public byte[] PatrolImage { get; set; }
        [DataMember]
        public string OfficerName { get; set; }
        [DataMember]
        public bool IsPatrol { get; set; }
        [DataMember]
        public long CurrentTaskId { get; set; }
    }
}
