using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ManualViolationDTO
    {
        [DataMember]
        public long ViolationId { get; set; }
        [DataMember]
        public string ViolationNumber { get; set; }
        [DataMember]
        public string StreetName { get; set; }
        [DataMember]
        public DateTime ViolationDate { get; set; }
        [DataMember]
        public DateTime ViolationTime { get; set; }
        [DataMember]
        public int ReasonCode { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string RadarClass { get; set; }
        [DataMember]
        public string RadarType { get; set; }
        [DataMember]
        public string Emirate { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public double Lat { get; set; }
        [DataMember]
        public double Lon { get; set; }
        [DataMember]
        public string TCFNumber { get; set; }
        [DataMember]
        public string LicenseSource { get; set; }
        [DataMember]
        public DateTime LicenseIssueDate { get; set; }
        [DataMember]
        public string OwnerTCFNumber { get; set; }
        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string PlateSource { get; set; }
        [DataMember]
        public string PlateKind { get; set; }
        [DataMember]
        public string VehicleType { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string AgeClass { get; set; }
        [DataMember]
        public string LevelOfEducation { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public int RoadSpeed { get; set; }
    }

    [DataContract]
    public class ManualViolationReasonDTO
    {
        [DataMember]
        public int ReasonId { get; set; }
        [DataMember]
        public int ReasonCode { get; set; }
        [DataMember]
        public string Reason { get; set; }
    }

    [DataContract]
    public class ManualViolationTypeDTO
    {
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public string Type { get; set; }
    }

    [DataContract]
    public class ManualViolationRadarClassDTO
    {
        [DataMember]
        public int RadarClassId { get; set; }
        [DataMember]
        public string RadarClass { get; set; }
    }

    [DataContract]
    public class ManualViolationRadarTypeDTO
    {
        [DataMember]
        public int RadarTypeId { get; set; }
        [DataMember]
        public string RadarType { get; set; }
    }

    [DataContract]
    public class ManualViolationLicenseSourceDTO
    {
        [DataMember]
        public int LicenseSourceId { get; set; }
        [DataMember]
        public string LicenseSource { get; set; }
    }

    [DataContract]
    public class ManualViolationVehicleTypeDTO
    {
        [DataMember]
        public int VehicleTypeId { get; set; }
        [DataMember]
        public string VehicleType { get; set; }
    }

    [DataContract]
    public class LevelOfEducationDTO
    {
        [DataMember]
        public int LevelOfEducationId { get; set; }
        [DataMember]
        public string LevelOfEducation { get; set; }

    }

    [DataContract]
    public class AgeClassDTO
    {
        [DataMember]
        public int AgeClassId { get; set; }
        [DataMember]
        public string AgeClass { get; set; }
    }

    [DataContract]
    public class GenderDTO
    {
        [DataMember]
        public int GenderId { get; set; }
        [DataMember]
        public string Gender { get; set; }
    }

    [DataContract]
    public class MaritalStatusDTO
    {
        [DataMember]
        public int MaritalStatusId { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
    }
}
