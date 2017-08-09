using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class IncidentHistoricalDTO
    {
        public IncidentHistoricalDTO()
        {
            Incidents = new List<IncidentsDTO>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public PeriodCategory ScheduleType { get; set; }

        [DataMember]
        public List<IncidentsDTO> Incidents { get; set; }

    }

    [DataContract]
    public class IncidentsDTO
    {
        public IncidentsDTO()
        {
            Notification = new NotificationDTO();
        }
        [DataMember]
        public long IncidentId { get; set; }

        [DataMember]
        public string IncidentNumber { get; set; }

        [DataMember]
        public int IncidentTypeId { get; set; }

        [DataMember]
        public string IncidentTypeName { get; set; }


        [DataMember]
        public double? Latitude { get; set; }

        [DataMember]
        public double? Longitude { get; set; }

        [DataMember]
        public string IncidentAddress { get; set; }

        [DataMember]
        public string CallerName { get; set; }

        [DataMember]
        public string CallerNumber { get; set; }

        [DataMember]
        public string CallerAddress { get; set; }

        [DataMember]
        public string CallerLanguage { get; set; }

        [DataMember]
        public int? CallTakerId { get; set; }

        [DataMember]
        public string CallTakerName { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public int? DispatcherId { get; set; }

        [DataMember]
        public string DispatcherName { get; set; }

        [DataMember]
        public DateTime? CreatedTime { get; set; }

        [DataMember]
        public DateTime? DispatcheTime { get; set; }

        [DataMember]
        public DateTime? ArrivedTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public bool IsNoticed { get; set; }

        [DataMember]
        public string MessageText { get; set; }

        [DataMember]
        public string BackgroundColor { get; set; }

        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember]
        public long NotificationId { get; set; }
        [DataMember]
        public NotificationDTO Notification { get; set; }
        [DataMember]
        public int AreaId { get; set; }
        [DataMember]
        public int CauseId { get; set; }
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public int CrashSeverityId { get; set; }
        [DataMember]
        public int CrashTypeId { get; set; }
        [DataMember]
        public int EmirateId { get; set; }
        [DataMember]
        public int IntersectionId { get; set; }
        [DataMember]
        public int LightingId { get; set; }
        [DataMember]
        public int LocationId { get; set; }
        [DataMember]
        public int LocationTypeId { get; set; }
        [DataMember]
        public int PConditionId { get; set; }
        [DataMember]
        public int RoadTypeId { get; set; }
        [DataMember]
        public int WeatherId { get; set; }
        [DataMember]
        public int Speed { get; set; }
        [DataMember]
        public int LanesCount { get; set; }
        [DataMember]
        public int SlightInjuries { get; set; }
        [DataMember]
        public int MediumInjuries { get; set; }
        [DataMember]
        public int SevereInjuries { get; set; }
        [DataMember]
        public int Fatalities { get; set; }
        [DataMember]
        public int TotalInjuriesFatalities { get; set; }
        [DataMember]
        public string IncidentDescription { get; set; }
        [DataMember]
        public string LocationDescription { get; set; }
        [DataMember]
        public string ZoneDescription { get; set; }
        [DataMember]
        public string PoliceStation { get; set; }

        [DataMember]
        public string AreaName { get; set; }
        [DataMember]
        public string CauseName { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public string CrashSeverityName { get; set; }
        [DataMember]
        public string CrashTypeName { get; set; }
        [DataMember]
        public string IntersectionName { get; set; }
        [DataMember]
        public string EmirateName { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string LightingName { get; set; }
        [DataMember]
        public string RoadTypeName { get; set; }
        [DataMember]
        public string LocationTypeName { get; set; }
        [DataMember]
        public string WeatherName { get; set; }
        [DataMember]
        public string PConditionName { get; set; }
    }
}
