using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public enum ServiceCallEnum
    {
        NA = 0,
        New = 1,
        Dispatched = 2,
        StatusUpdated = 3,
        NewComment = 4,
        Closed = 5

    };
    [DataContract]
    public class AccidentStandardDTO
    {
        [DataMember]
        public string IncidentNo { get; set; }

        [DataMember]
        public ServiceCallEnum ServiceCallStep { get; set; }

        [DataMember]
        public string WorkstationName { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string StatusId { get; set; }

        [DataMember]
        public DateTime? StatusUpdateDate { get; set; }

        [DataMember]
        public DateTime? CreatedTime { get; set; }

        [DataMember]
        public string ServiceCallPriorityCode { get; set; }

        [DataMember]
        public string IncidentTypeName { get; set; }

        [DataMember]
        public string ServiceCallCategoryCode { get; set; }

        [DataMember]
        public string CrashTypeName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public double? Latitude { get; set; }

        [DataMember]
        public double? Longitude { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string CallerName { get; set; }

        [DataMember]
        public string CallerNumber { get; set; }

        [DataMember]
        public string CallerLocation { get; set; }

        [DataMember]
        public string LanguageOfCaller { get; set; }

        [DataMember]
        public string CallTakerName { get; set; }

        [DataMember]
        public string CallTakerId { get; set; }

        [DataMember]
        public DateTime? ServiceCallDispatchedDate { get; set; }

        [DataMember]
        public string DispatcherId { get; set; }

        [DataMember]
        public string DispatcherName { get; set; }

        [DataMember]
        public string CommentText { get; set; }

        [DataMember]
        public string CommentSubmittedById { get; set; }

        [DataMember]
        public string CommentSubmittedByName { get; set; }

        [DataMember]
        public DateTime? CommentDate { get; set; }

        [DataMember]
        public DateTime? ActivityEndDate { get; set; }

        [DataMember]
        public string StateName { get; set; }

        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public string WeatherName { get; set; }

        [DataMember]
        public string PConditionName { get; set; }

        [DataMember]
        public double? RoadSpeed { get; set; }

        [DataMember]
        public string AddressComment { get; set; }

        [DataMember]
        public string VehicleMake { get; set; }

        [DataMember]
        public string VehicleModel { get; set; }

        [DataMember]
        public string VehicleKind { get; set; }

        [DataMember]
        public string VehicleType { get; set; }

        [DataMember]
        public string VehicleProductingYear { get; set; }

        [DataMember]
        public string VehicleCountry { get; set; }

        [DataMember]
        public string InsuranceCompany { get; set; }

        [DataMember]
        public string InsuranceType { get; set; }

        [DataMember]
        public string VehiclesLiability { get; set; }

        [DataMember]
        public string VehicleCollisionPoint { get; set; }

        [DataMember]
        public string VehicleStatus { get; set; }

        [DataMember]
        public string DriverNationality { get; set; }

        [DataMember]
        public string DriverLicenceSource { get; set; }

        [DataMember]
        public string DriverLicenceType { get; set; }

        [DataMember]
        public string DriverGender { get; set; }

        [DataMember]
        public int LanesCount { get; set; }

        [DataMember]
        public int SlightInjuriesCount { get; set; }

        [DataMember]
        public int MediumInjuriesCount { get; set; }

        [DataMember]
        public int SevereInjuriesCount { get; set; }

        [DataMember]
        public int FatalitiesCount { get; set; }

        [DataMember]
        public int TotalInjuriesFatalities { get; set; }

        [DataMember]
        public string CrashSeverityCode { get; set; }

        [DataMember]
        public string CrashSeverityName { get; set; }

        [DataMember]
        public string ServiceCallReasonCode { get; set; }

        [DataMember]
        public string CauseName { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string LocationDescription { get; set; }

        [DataMember]
        public string CrashDescription { get; set; }

        [DataMember]
        public string LocationTypeName { get; set; }

        [DataMember]
        public string IntersectionName { get; set; }

        [DataMember]
        public string PoliceStationName { get; set; }

        [DataMember]
        public string LightingName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string EmirateName { get; set; }

        [DataMember]
        public string RoadTypeName { get; set; }
    }
}
