using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    public class AccidentSearchRequestDTO
    {
        public string AccidentNumber { get; set; } // Exact number search
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? EmirateId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; } // el takseem
        public int? AreaId { get; set; }
        public string Address { get; set; } // Like address not exact
        public int? LocationId { get; set; }
        public int? LocationTypeId { get; set; }
        public int? IntersectionTypeId { get; set; }
        public int? PoliceStationId { get; set; }
        public int? AccidentTypeId { get; set; }
        public int? CrashTypeId { get; set; }
        public int? ReasonTypeId { get; set; }
        public int? NumOfFatalInjuries { get; set; }
        public int? NumOfMedInjuries { get; set; }
        public int? NumOfEasyInjuries { get; set; }
        public int? NumOfDeaths { get; set; }
        public int? WeatherStatusId { get; set; }
        public int? LightStatusId { get; set; }
        public int? RoadWaterStatusId { get; set; }
        public int? RoadStatusId { get; set; }
        public int? SeverityId { get; set; }
        public int? Speed { get; set; }
    }

    public class IncidentAreaDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set;}
    }

    public class IncidentCityDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentEmirateDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentLocationDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentLocationTypeDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentRoadTypesDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentCrashTypeDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentWeatherDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentLightingDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentSevertiesDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentPConditionDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentPoliceStationDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }

    public class IncidentReportTypesDTO
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
