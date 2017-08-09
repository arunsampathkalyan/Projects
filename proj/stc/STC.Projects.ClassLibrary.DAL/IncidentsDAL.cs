using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Data.Entity.Spatial;

namespace STC.Projects.ClassLibrary.DAL
{
    public class IncidentsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        private Entities.Entities stcDataContext = new Entities.Entities();

        public List<IncidentAreaDTO> GetAllAreas()
        {
            try
            {
                return operationalDataContext.IncidentAreas.Select(x => new IncidentAreaDTO
                    {
                        ArabicName = x.AreaNameAr,
                        EnglishName = x.AreaNameEn,
                        Id = x.Id
                    }).ToList();
            }
            catch(Exception ex)
            {

            }
            return new List<IncidentAreaDTO>();
        }

        public List<IncidentCityDTO> GetAllCities()
        {
            try
            {
                return operationalDataContext.IncidentCities.Select(x => new IncidentCityDTO
                {
                    ArabicName = x.CityNameAr,
                    EnglishName = x.CityNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentCityDTO>();
        }

        public List<IncidentEmirateDTO> GetAllEmirates()
        {
            try
            {
                return operationalDataContext.IncidentEmirates.Select(x => new IncidentEmirateDTO
                {
                    ArabicName = x.EmirateNameAr,
                    EnglishName = x.EmirateNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentEmirateDTO>();
        }

        public List<IncidentLocationDTO> GetAllLocations()
        {
            try
            {
                return operationalDataContext.IncidentLocations.Select(x => new IncidentLocationDTO
                {
                    ArabicName = x.LocationNameAr,
                    EnglishName = x.LocationNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentLocationDTO>();
        }

        public List<IncidentLocationTypeDTO> GetAllLocationsTypes()
        {
            try
            {
                return operationalDataContext.IncidentLocationTypes.Select(x => new IncidentLocationTypeDTO
                {
                    ArabicName = x.LocationTypeNameAr,
                    EnglishName = x.LocationTypeNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentLocationTypeDTO>();
        }

        public List<IncidentRoadTypesDTO> GetAllRoadTypes()
        {
            try
            {
                return operationalDataContext.IncidentRoadTypes.Select(x => new IncidentRoadTypesDTO
                {
                    ArabicName = x.RoadTypeNameAr,
                    EnglishName = x.RoadTypeNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentRoadTypesDTO>();
        }

        public List<IncidentCrashTypeDTO> GetAllCrashTypes()
        {
            try
            {
                return operationalDataContext.IncidentCrashTypes.Select(x => new IncidentCrashTypeDTO
                {
                    ArabicName = x.IncidentCrashTypeNameAr,
                    EnglishName = x.IncidentCrashTypeNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentCrashTypeDTO>();
        }

        public List<IncidentWeatherDTO> GetAllWeather()
        {
            try
            {
                return operationalDataContext.IncidentWeathers.Select(x => new IncidentWeatherDTO
                {
                    ArabicName = x.WeatherNameAr,
                    EnglishName = x.WeatherNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentWeatherDTO>();
        }

        public List<IncidentLightingDTO> GetAllLighting()
        {
            try
            {
                return operationalDataContext.IncidentLightings.Select(x => new IncidentLightingDTO
                {
                    ArabicName = x.LightingNameAr,
                    EnglishName = x.LightingNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentLightingDTO>();
        }

        public List<IncidentSevertiesDTO> GetAllSeverties()
        {
            try
            {
                return operationalDataContext.IncidentCrashSeverities.Select(x => new IncidentSevertiesDTO
                {
                    ArabicName = x.CrashSeverityNameAr,
                    EnglishName = x.CrashSeverityNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentSevertiesDTO>();
        }

        public List<IncidentPConditionDTO> GetAllPCondition()
        {
            try
            {
                return operationalDataContext.IncidentPConditions.Select(x => new IncidentPConditionDTO
                {
                    ArabicName = x.PConditionNameAr,
                    EnglishName = x.PConditionNameEn,
                    Id = x.Id
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentPConditionDTO>();
        }

        public List<IncidentPoliceStationDTO> GetAllPoliceStations()
        {
            try
            {
                return operationalDataContext.IncidentPoliceStation.Select(x => new IncidentPoliceStationDTO
                {
                    ArabicName = x.PoliceStationNameAr,
                    EnglishName = x.PoliceStationNameEn,
                    Id = x.PoliceStationId
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentPoliceStationDTO>();
        }

        public List<IncidentReportTypesDTO> GetAllReportTypes()
        {
            try
            {
                return operationalDataContext.IncidentTypes.Select(x => new IncidentReportTypesDTO
                {
                    ArabicName = x.IncidentTypeNameAr,
                    EnglishName = x.IncidentTypeNameEn,
                    Id = x.IncidentTypeId
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<IncidentReportTypesDTO>();
        }


        public List<IncidentsDTO> GetUpdatedIncidents()
        {
            try
            {
                var lstIncidents = operationalDataContext.Incident
                                                         .Where(Incident => Incident.IsNoticed == false && Incident.EndTime == null && Incident.NotificationId.HasValue)
                                                         .Select(Incident => new IncidentsDTO
                                                         {
                                                             ArrivedTime = Incident.ArrivedTime,
                                                             CallerAddress = Incident.CallerAddress,
                                                             CallerLanguage = Incident.CallerLanguage,
                                                             CallerName = Incident.CallerName,
                                                             CallerNumber = Incident.CallerNumber,
                                                             CallTakerId = Incident.CallTakerId,
                                                             CallTakerName = Incident.CallTakerName,
                                                             CreatedTime = Incident.CreatedTime,
                                                             DispatcherId = Incident.DispatcherId,
                                                             DispatcherName = Incident.DispatcherName,
                                                             DispatcheTime = Incident.DispatcheTime,
                                                             EndTime = Incident.EndTime,
                                                             IncidentAddress = Incident.IncidentAddress,
                                                             IncidentId = Incident.IncidentId,
                                                             IncidentNumber = Incident.IncidentNumber,
                                                             IncidentTypeId = Incident.IncidentTypeId.Value,
                                                             IncidentTypeName = Incident.IncidentType.IncidentTypeNameAr,
                                                             IsNoticed = Incident.IsNoticed.Value,
                                                             Latitude = Incident.Latitude,
                                                             Longitude = Incident.Longitude,
                                                             StatusId = Incident.StatusId.HasValue ? Incident.StatusId.Value : 0,
                                                             BackgroundColor = "Red",
                                                             IsCritical = Incident.IsCritical.HasValue ? Incident.IsCritical.Value : false,
                                                             MessageId = Incident.IncidentId.ToString(),
                                                             MessageText = "New Incident: " + Incident.IncidentId.ToString(),
                                                             NotificationId = Incident.NotificationId.HasValue ? Incident.NotificationId.Value : 0,
                                                             Speed = Incident.Speed.HasValue ? Incident.Speed.Value : 0,
                                                             LanesCount = Incident.LanesCount.HasValue ? Incident.LanesCount.Value : 0,
                                                             SlightInjuries = Incident.SlightInjuries.HasValue ? Incident.SlightInjuries.Value : 0,
                                                             MediumInjuries = Incident.MediumInjuries.HasValue ? Incident.MediumInjuries.Value : 0,
                                                             SevereInjuries = Incident.SevereInjuries.HasValue ? Incident.SevereInjuries.Value : 0,
                                                             Fatalities = Incident.Fatalities.HasValue ? Incident.Fatalities.Value : 0,
                                                             TotalInjuriesFatalities = Incident.TotalInjuriesFatalities.HasValue ? Incident.TotalInjuriesFatalities.Value : 0,
                                                             IncidentDescription = Incident.IncidentDescription,
                                                             LocationDescription = Incident.LocationDescription,
                                                             ZoneDescription = Incident.ZoneDescription,
                                                             PoliceStation = Incident.IncidentPoliceStation.PoliceStationNameAr,
                                                             StatusName = Incident.IncidentStatus != null ? Incident.IncidentStatus.StatusNameAr : "",
                                                             Notification = new NotificationDTO
                                                             {
                                                                 DateCreated = Incident.Notifications.DateCreated,
                                                                 IsNoticed = false,
                                                                 LastModified = Incident.Notifications.LastModified,
                                                                 LastModifiedBy = Incident.Notifications.ModifiedBy,
                                                                 LastStatus = Incident.Notifications.LastStatus,
                                                                 NotificationId = Incident.NotificationId.Value,
                                                                 OwnerId = Incident.Notifications.OwnerId
                                                             }

                                                         }).ToList();
                return lstIncidents;
            }
            catch (Exception ex)
            {


            }

            return null;

        }

        public List<IncidentsDTO> SearchActiveIncidents(AccidentSearchRequestDTO searchReq)
        {
            try
            {
                var list = operationalDataContext.IncidentViews
                    .Where(x=> (searchReq.AccidentNumber.Trim() == "" || x.IncidentNumber == searchReq.AccidentNumber.Trim())
                    && (!searchReq.AccidentTypeId.HasValue || (searchReq.AccidentTypeId.HasValue && searchReq.AccidentTypeId.Value == x.IncidentTypeId))
                    && (!searchReq.AreaId.HasValue || (searchReq.AreaId.HasValue && x.AreaId.HasValue && searchReq.AreaId.Value == x.AreaId.Value))
                    && (!searchReq.CityId.HasValue || (searchReq.CityId.HasValue && x.CityId.HasValue && searchReq.CityId.Value == x.CityId.Value))
                    && (!searchReq.CrashTypeId.HasValue || (searchReq.CrashTypeId.HasValue && x.CrashTypeId.HasValue && searchReq.CrashTypeId.Value == x.CrashTypeId.Value))
                    && (!searchReq.EmirateId.HasValue || (searchReq.EmirateId.HasValue && x.EmirateId.HasValue && searchReq.EmirateId.Value == x.EmirateId.Value))
                    && (!searchReq.EndTime.HasValue || (searchReq.EndTime.HasValue && x.CreatedTime.HasValue && searchReq.EndTime >= x.CreatedTime.Value))
                    && (!searchReq.IntersectionTypeId.HasValue || (searchReq.IntersectionTypeId.HasValue && x.IntersectionId.HasValue && searchReq.IntersectionTypeId.Value == x.IntersectionId.Value))
                    && (!searchReq.LightStatusId.HasValue || (searchReq.LightStatusId.HasValue && x.LightingId.HasValue && searchReq.LightStatusId.Value == x.LightingId.Value))
                    && (!searchReq.LocationId.HasValue || (searchReq.LocationId.HasValue && x.LocationId.HasValue && searchReq.LocationId.Value == x.LocationId.Value))
                    && (!searchReq.LocationTypeId.HasValue || (searchReq.LocationTypeId.HasValue && x.LocationTypeId.HasValue && searchReq.LocationTypeId.Value == x.LocationTypeId.Value))
                    && (!searchReq.NumOfDeaths.HasValue || (searchReq.NumOfDeaths.HasValue && x.Fatalities.HasValue && searchReq.NumOfDeaths.Value <= x.Fatalities.Value))
                    && (!searchReq.NumOfEasyInjuries.HasValue || (searchReq.NumOfEasyInjuries.HasValue && x.SlightInjuries.HasValue && searchReq.NumOfEasyInjuries <= x.SlightInjuries.Value))
                    && (!searchReq.NumOfFatalInjuries.HasValue || (searchReq.NumOfFatalInjuries.HasValue && x.SevereInjuries.HasValue && searchReq.NumOfFatalInjuries.Value <= x.SevereInjuries.Value))
                    && (!searchReq.NumOfMedInjuries.HasValue || (searchReq.NumOfMedInjuries.HasValue && x.MediumInjuries.HasValue && searchReq.NumOfMedInjuries.Value <= x.MediumInjuries.Value))
                    && (!searchReq.PoliceStationId.HasValue || (searchReq.PoliceStationId.HasValue && x.PoliceStationId.HasValue && searchReq.PoliceStationId.Value == x.PoliceStationId.Value))
                    && (!searchReq.ReasonTypeId.HasValue || (searchReq.ReasonTypeId.HasValue && x.CauseId.HasValue && searchReq.ReasonTypeId.Value == x.CauseId.Value))
                    && (!searchReq.RoadStatusId.HasValue || (searchReq.RoadStatusId.HasValue && x.RoadTypeId.HasValue && searchReq.RoadStatusId.Value == x.RoadTypeId.Value))
                    && (!searchReq.RoadWaterStatusId.HasValue || (searchReq.RoadWaterStatusId.HasValue && x.PConditionId.HasValue && searchReq.RoadWaterStatusId.Value == x.PConditionId.Value))
                    && (!searchReq.SeverityId.HasValue || (searchReq.SeverityId.HasValue && x.CrashSeverityId.HasValue && searchReq.SeverityId.Value == x.CrashSeverityId.Value))
                    && (!searchReq.Speed.HasValue || (searchReq.Speed.HasValue && x.Speed.HasValue && searchReq.Speed.Value <= x.Speed.Value))
                    && (!searchReq.StartTime.HasValue || (searchReq.StartTime.HasValue && x.CreatedTime.HasValue && searchReq.StartTime <= x.CreatedTime.Value))
                    && (!searchReq.WeatherStatusId.HasValue || (searchReq.WeatherStatusId.HasValue && x.WeatherId.HasValue && searchReq.WeatherStatusId.Value == x.WeatherId.Value))
                    ).Select(Incident => new IncidentsDTO
                                                         {
                                                             ArrivedTime = Incident.ArrivedTime,
                                                             CallerAddress = Incident.CallerAddress,
                                                             CallerLanguage = Incident.CallerLanguage,
                                                             CallerName = Incident.CallerName,
                                                             CallerNumber = Incident.CallerNumber,
                                                             CallTakerId = Incident.CallTakerId,
                                                             CallTakerName = Incident.CallTakerName,
                                                             CreatedTime = Incident.CreatedTime,
                                                             DispatcherId = Incident.DispatcherId,
                                                             DispatcherName = Incident.DispatcherName,
                                                             DispatcheTime = Incident.DispatcheTime,
                                                             EndTime = Incident.EndTime,
                                                             IncidentAddress = Incident.IncidentAddress,
                                                             IncidentId = Incident.IncidentId,
                                                             IncidentNumber = Incident.IncidentNumber,
                                                             IncidentTypeId = Incident.IncidentTypeId,
                                                             IncidentTypeName = Incident.IncidentTypeName,
                                                             IsNoticed = Incident.IsNoticed,
                                                             Latitude = Incident.Latitude,
                                                             Longitude = Incident.Longitude,
                                                             StatusId = Incident.StatusId,
                                                             StatusName = Incident.StatusName,
                                                             BackgroundColor = "Red",
                                                             IsCritical = Incident.IsCritical.HasValue ? Incident.IsCritical.Value : false,
                                                             MessageId = Incident.IncidentId.ToString(),
                                                             MessageText = "New Incident: " + Incident.IncidentId.ToString(),
                                                             NotificationId = Incident.NotificationId.HasValue ? Incident.NotificationId.Value : 0,
                                                             Speed = Incident.Speed.HasValue ? Incident.Speed.Value : 0,
                                                             LanesCount = Incident.LanesCount.HasValue ? Incident.LanesCount.Value : 0,
                                                             SlightInjuries = Incident.SlightInjuries.HasValue ? Incident.SlightInjuries.Value : 0,
                                                             MediumInjuries = Incident.MediumInjuries.HasValue ? Incident.MediumInjuries.Value : 0,
                                                             SevereInjuries = Incident.SevereInjuries.HasValue ? Incident.SevereInjuries.Value : 0,
                                                             Fatalities = Incident.Fatalities.HasValue ? Incident.Fatalities.Value : 0,
                                                             TotalInjuriesFatalities = Incident.TotalInjuriesFatalities.HasValue ? Incident.TotalInjuriesFatalities.Value : 0,
                                                             IncidentDescription = Incident.IncidentDescription,
                                                             LocationDescription = Incident.LocationDescription,
                                                             ZoneDescription = Incident.ZoneDescription,
                                                             PoliceStation = Incident.PoliceStation,
                                                             CauseId = Incident.CauseId.HasValue ? Incident.CauseId.Value : 0,
                                                             AreaId = Incident.AreaId.HasValue ? Incident.AreaId.Value : 0,
                                                             CityId = Incident.CityId.HasValue ? Incident.CityId.Value : 0,
                                                             CrashSeverityId = Incident.CrashSeverityId.HasValue ? Incident.CrashSeverityId.Value : 0,
                                                             CrashTypeId = Incident.CrashTypeId.HasValue ? Incident.CrashTypeId.Value : 0,
                                                             IntersectionId = Incident.IntersectionId.HasValue ? Incident.IntersectionId.Value : 0,
                                                             EmirateId = Incident.EmirateId.HasValue ? Incident.EmirateId.Value : 0,
                                                             LocationId = Incident.LocationId.HasValue ? Incident.LocationId.Value : 0,
                                                             LightingId = Incident.LightingId.HasValue ? Incident.LightingId.Value : 0,
                                                             RoadTypeId = Incident.RoadTypeId.HasValue ? Incident.RoadTypeId.Value : 0,
                                                             LocationTypeId = Incident.LocationTypeId.HasValue ? Incident.LocationTypeId.Value : 0,
                                                             WeatherId = Incident.WeatherId.HasValue ? Incident.WeatherId.Value : 0,
                                                             PConditionId = Incident.PConditionId.HasValue ? Incident.PConditionId.Value : 0,
                                                             CauseName = Incident.CauseName,
                                                             AreaName = Incident.AreaName,
                                                             CityName = Incident.CityName,
                                                             CrashSeverityName = Incident.CrashSeverityName,
                                                             CrashTypeName = Incident.CrashTypeName,
                                                             IntersectionName = Incident.IntersectionName,
                                                             EmirateName = Incident.EmirateName,
                                                             LocationName = Incident.LocationName,
                                                             LightingName = Incident.LightingName,
                                                             RoadTypeName = Incident.RoadTypeName,
                                                             LocationTypeName = Incident.LocationTypeName,
                                                             WeatherName = Incident.WeatherName,
                                                             PConditionName = Incident.PConditionName

                                                         }).ToList();
                
                if(searchReq.Address.Trim() != "" && list != null)
                {
                    list = list.Where(x => x.IncidentAddress.Contains(searchReq.Address.Trim())).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {


            }

            return null;
        }
        public List<IncidentsDTO> GetActiveIncidentsList(bool? IsNoticed)
        {
            try
            {
                var lstIncidents = operationalDataContext.IncidentViews
                                                         .Where(Incident => (IsNoticed == null || Incident.IsNoticed == IsNoticed) && Incident.EndTime == null)
                                                         .Select(Incident => new IncidentsDTO
                                                         {
                                                             ArrivedTime = Incident.ArrivedTime,
                                                             CallerAddress = Incident.CallerAddress,
                                                             CallerLanguage = Incident.CallerLanguage,
                                                             CallerName = Incident.CallerName,
                                                             CallerNumber = Incident.CallerNumber,
                                                             CallTakerId = Incident.CallTakerId,
                                                             CallTakerName = Incident.CallTakerName,
                                                             CreatedTime = Incident.CreatedTime,
                                                             DispatcherId = Incident.DispatcherId,
                                                             DispatcherName = Incident.DispatcherName,
                                                             DispatcheTime = Incident.DispatcheTime,
                                                             EndTime = Incident.EndTime,
                                                             IncidentAddress = Incident.IncidentAddress,
                                                             IncidentId = Incident.IncidentId,
                                                             IncidentNumber = Incident.IncidentNumber,
                                                             IncidentTypeId = Incident.IncidentTypeId,
                                                             IncidentTypeName = Incident.IncidentTypeName,
                                                             IsNoticed = Incident.IsNoticed,
                                                             Latitude = Incident.Latitude,
                                                             Longitude = Incident.Longitude,
                                                             StatusId = Incident.StatusId,
                                                             StatusName = Incident.StatusName,
                                                             BackgroundColor = "Red",
                                                             IsCritical = Incident.IsCritical.HasValue ? Incident.IsCritical.Value : false,
                                                             MessageId = Incident.IncidentId.ToString(),
                                                             MessageText = "New Incident: " + Incident.IncidentId.ToString(),
                                                             NotificationId = Incident.NotificationId.HasValue ? Incident.NotificationId.Value : 0,
                                                             Speed = Incident.Speed.HasValue ? Incident.Speed.Value : 0,
                                                             LanesCount = Incident.LanesCount.HasValue ? Incident.LanesCount.Value : 0,
                                                             SlightInjuries = Incident.SlightInjuries.HasValue ? Incident.SlightInjuries.Value : 0,
                                                             MediumInjuries = Incident.MediumInjuries.HasValue ? Incident.MediumInjuries.Value : 0,
                                                             SevereInjuries = Incident.SevereInjuries.HasValue ? Incident.SevereInjuries.Value : 0,
                                                             Fatalities = Incident.Fatalities.HasValue ? Incident.Fatalities.Value : 0,
                                                             TotalInjuriesFatalities = Incident.TotalInjuriesFatalities.HasValue ? Incident.TotalInjuriesFatalities.Value : 0,
                                                             IncidentDescription = Incident.IncidentDescription,
                                                             LocationDescription = Incident.LocationDescription,
                                                             ZoneDescription = Incident.ZoneDescription,
                                                             PoliceStation = Incident.PoliceStation,
                                                             CauseId = Incident.CauseId.HasValue ? Incident.CauseId.Value : 0,
                                                             AreaId = Incident.AreaId.HasValue ? Incident.AreaId.Value : 0,
                                                             CityId = Incident.CityId.HasValue ? Incident.CityId.Value : 0,
                                                             CrashSeverityId = Incident.CrashSeverityId.HasValue ? Incident.CrashSeverityId.Value : 0,
                                                             CrashTypeId = Incident.CrashTypeId.HasValue ? Incident.CrashTypeId.Value : 0,
                                                             IntersectionId = Incident.IntersectionId.HasValue ? Incident.IntersectionId.Value : 0,
                                                             EmirateId = Incident.EmirateId.HasValue ? Incident.EmirateId.Value : 0,
                                                             LocationId = Incident.LocationId.HasValue ? Incident.LocationId.Value : 0,
                                                             LightingId = Incident.LightingId.HasValue ? Incident.LightingId.Value : 0,
                                                             RoadTypeId = Incident.RoadTypeId.HasValue ? Incident.RoadTypeId.Value : 0,
                                                             LocationTypeId = Incident.LocationTypeId.HasValue ? Incident.LocationTypeId.Value : 0,
                                                             WeatherId = Incident.WeatherId.HasValue ? Incident.WeatherId.Value : 0,
                                                             PConditionId = Incident.PConditionId.HasValue ? Incident.PConditionId.Value : 0,
                                                             CauseName = Incident.CauseName,
                                                             AreaName = Incident.AreaName,
                                                             CityName = Incident.CityName,
                                                             CrashSeverityName = Incident.CrashSeverityName,
                                                             CrashTypeName = Incident.CrashTypeName,
                                                             IntersectionName = Incident.IntersectionName,
                                                             EmirateName = Incident.EmirateName,
                                                             LocationName = Incident.LocationName,
                                                             LightingName = Incident.LightingName,
                                                             RoadTypeName = Incident.RoadTypeName,
                                                             LocationTypeName = Incident.LocationTypeName,
                                                             WeatherName = Incident.WeatherName,
                                                             PConditionName = Incident.PConditionName

                                                         }).ToList();
                return lstIncidents;
            }
            catch (Exception ex)
            {


            }

            return null;
        }

        public IncidentsDTO GetIncidentDetails(int IncidentId)
        {
            try
            {
                var incidentDetails = operationalDataContext.IncidentViews
                                                            .Where(Incident => Incident.IncidentId == IncidentId && Incident.Latitude.HasValue && Incident.Longitude.HasValue)
                                                            .Select(Incident => new IncidentsDTO
                                                            {
                                                                ArrivedTime = Incident.ArrivedTime,
                                                                CallerAddress = Incident.CallerAddress,
                                                                CallerLanguage = Incident.CallerLanguage,
                                                                CallerName = Incident.CallerName,
                                                                CallerNumber = Incident.CallerNumber,
                                                                CallTakerId = Incident.CallTakerId,
                                                                CallTakerName = Incident.CallTakerName,
                                                                CreatedTime = Incident.CreatedTime,
                                                                DispatcherId = Incident.DispatcherId,
                                                                DispatcherName = Incident.DispatcherName,
                                                                DispatcheTime = Incident.DispatcheTime,
                                                                EndTime = Incident.EndTime,
                                                                IncidentAddress = Incident.IncidentAddress,
                                                                IncidentId = Incident.IncidentId,
                                                                IncidentNumber = Incident.IncidentNumber,
                                                                IncidentTypeId = Incident.IncidentTypeId,
                                                                IncidentTypeName = Incident.IncidentTypeName,
                                                                IsNoticed = Incident.IsNoticed,
                                                                Latitude = Incident.Latitude,
                                                                Longitude = Incident.Longitude,
                                                                StatusId = Incident.StatusId,
                                                                StatusName = Incident.StatusName,
                                                                BackgroundColor = "Red",
                                                                IsCritical = Incident.IsCritical.HasValue ? Incident.IsCritical.Value : false,
                                                                MessageId = Incident.IncidentId.ToString(),
                                                                MessageText = "New Incident: " + Incident.IncidentId.ToString(),
                                                                NotificationId = Incident.NotificationId.HasValue ? Incident.NotificationId.Value : 0,
                                                                Speed = Incident.Speed.HasValue ? Incident.Speed.Value : 0,
                                                                LanesCount = Incident.LanesCount.HasValue ? Incident.LanesCount.Value : 0,
                                                                SlightInjuries = Incident.SlightInjuries.HasValue ? Incident.SlightInjuries.Value : 0,
                                                                MediumInjuries = Incident.MediumInjuries.HasValue ? Incident.MediumInjuries.Value : 0,
                                                                SevereInjuries = Incident.SevereInjuries.HasValue ? Incident.SevereInjuries.Value : 0,
                                                                Fatalities = Incident.Fatalities.HasValue ? Incident.Fatalities.Value : 0,
                                                                TotalInjuriesFatalities = Incident.TotalInjuriesFatalities.HasValue ? Incident.TotalInjuriesFatalities.Value : 0,
                                                                IncidentDescription = Incident.IncidentDescription,
                                                                LocationDescription = Incident.LocationDescription,
                                                                ZoneDescription = Incident.ZoneDescription,
                                                                PoliceStation = Incident.PoliceStation,
                                                                CauseId = Incident.CauseId.HasValue ? Incident.CauseId.Value : 0,
                                                                AreaId = Incident.AreaId.HasValue ? Incident.AreaId.Value : 0,
                                                                CityId = Incident.CityId.HasValue ? Incident.CityId.Value : 0,
                                                                CrashSeverityId = Incident.CrashSeverityId.HasValue ? Incident.CrashSeverityId.Value : 0,
                                                                CrashTypeId = Incident.CrashTypeId.HasValue ? Incident.CrashTypeId.Value : 0,
                                                                IntersectionId = Incident.IntersectionId.HasValue ? Incident.IntersectionId.Value : 0,
                                                                EmirateId = Incident.EmirateId.HasValue ? Incident.EmirateId.Value : 0,
                                                                LocationId = Incident.LocationId.HasValue ? Incident.LocationId.Value : 0,
                                                                LightingId = Incident.LightingId.HasValue ? Incident.LightingId.Value : 0,
                                                                RoadTypeId = Incident.RoadTypeId.HasValue ? Incident.RoadTypeId.Value : 0,
                                                                LocationTypeId = Incident.LocationTypeId.HasValue ? Incident.LocationTypeId.Value : 0,
                                                                WeatherId = Incident.WeatherId.HasValue ? Incident.WeatherId.Value : 0,
                                                                PConditionId = Incident.PConditionId.HasValue ? Incident.PConditionId.Value : 0,
                                                                CauseName = Incident.CauseName,
                                                                AreaName = Incident.AreaName,
                                                                CityName = Incident.CityName,
                                                                CrashSeverityName = Incident.CrashSeverityName,
                                                                CrashTypeName = Incident.CrashTypeName,
                                                                IntersectionName = Incident.IntersectionName,
                                                                EmirateName = Incident.EmirateName,
                                                                LocationName = Incident.LocationName,
                                                                LightingName = Incident.LightingName,
                                                                RoadTypeName = Incident.RoadTypeName,
                                                                LocationTypeName = Incident.LocationTypeName,
                                                                WeatherName = Incident.WeatherName,
                                                                PConditionName = Incident.PConditionName
                                                            }).First();
                return incidentDetails;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<IncidentsDTO> GetIncidentsListByDate(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            try
            {
                var lstIncidents = operationalDataContext.IncidentViews
                    .Where(
                    Incident =>
                        (StartDateTime.HasValue == false || Incident.CreatedTime.HasValue == false || Incident.CreatedTime.Value >= StartDateTime.Value) &&
                        (EndDateTime.HasValue == false || Incident.CreatedTime.HasValue == false || Incident.CreatedTime.Value <= EndDateTime.Value)
                        )
                    .Select(Incident => new IncidentsDTO
                    {
                        ArrivedTime = Incident.ArrivedTime,
                        CallerAddress = Incident.CallerAddress,
                        CallerLanguage = Incident.CallerLanguage,
                        CallerName = Incident.CallerName,
                        CallerNumber = Incident.CallerNumber,
                        CallTakerId = Incident.CallTakerId,
                        CallTakerName = Incident.CallTakerName,
                        CreatedTime = Incident.CreatedTime,
                        DispatcherId = Incident.DispatcherId,
                        DispatcherName = Incident.DispatcherName,
                        DispatcheTime = Incident.DispatcheTime,
                        EndTime = Incident.EndTime,
                        IncidentAddress = Incident.IncidentAddress,
                        IncidentId = Incident.IncidentId,
                        IncidentNumber = Incident.IncidentNumber,
                        IncidentTypeId = Incident.IncidentTypeId,
                        IncidentTypeName = Incident.IncidentTypeName,
                        IsNoticed = Incident.IsNoticed,
                        Latitude = Incident.Latitude,
                        Longitude = Incident.Longitude,
                        StatusId = Incident.StatusId,
                        StatusName = Incident.StatusName,
                        BackgroundColor = "Red",
                        IsCritical = Incident.IsCritical.HasValue ? Incident.IsCritical.Value : false,
                        MessageId = Incident.IncidentId.ToString(),
                        MessageText = "New Incident: " + Incident.IncidentId.ToString(),
                        NotificationId = Incident.NotificationId.HasValue ? Incident.NotificationId.Value : 0,
                        Speed = Incident.Speed.HasValue ? Incident.Speed.Value : 0,
                        LanesCount = Incident.LanesCount.HasValue ? Incident.LanesCount.Value : 0,
                        SlightInjuries = Incident.SlightInjuries.HasValue ? Incident.SlightInjuries.Value : 0,
                        MediumInjuries = Incident.MediumInjuries.HasValue ? Incident.MediumInjuries.Value : 0,
                        SevereInjuries = Incident.SevereInjuries.HasValue ? Incident.SevereInjuries.Value : 0,
                        Fatalities = Incident.Fatalities.HasValue ? Incident.Fatalities.Value : 0,
                        TotalInjuriesFatalities = Incident.TotalInjuriesFatalities.HasValue ? Incident.TotalInjuriesFatalities.Value : 0,
                        IncidentDescription = Incident.IncidentDescription,
                        LocationDescription = Incident.LocationDescription,
                        ZoneDescription = Incident.ZoneDescription,
                        PoliceStation = Incident.PoliceStation,
                        CauseId = Incident.CauseId.HasValue ? Incident.CauseId.Value : 0,
                        AreaId = Incident.AreaId.HasValue ? Incident.AreaId.Value : 0,
                        CityId = Incident.CityId.HasValue ? Incident.CityId.Value : 0,
                        CrashSeverityId = Incident.CrashSeverityId.HasValue ? Incident.CrashSeverityId.Value : 0,
                        CrashTypeId = Incident.CrashTypeId.HasValue ? Incident.CrashTypeId.Value : 0,
                        IntersectionId = Incident.IntersectionId.HasValue ? Incident.IntersectionId.Value : 0,
                        EmirateId = Incident.EmirateId.HasValue ? Incident.EmirateId.Value : 0,
                        LocationId = Incident.LocationId.HasValue ? Incident.LocationId.Value : 0,
                        LightingId = Incident.LightingId.HasValue ? Incident.LightingId.Value : 0,
                        RoadTypeId = Incident.RoadTypeId.HasValue ? Incident.RoadTypeId.Value : 0,
                        LocationTypeId = Incident.LocationTypeId.HasValue ? Incident.LocationTypeId.Value : 0,
                        WeatherId = Incident.WeatherId.HasValue ? Incident.WeatherId.Value : 0,
                        PConditionId = Incident.PConditionId.HasValue ? Incident.PConditionId.Value : 0,
                        CauseName = Incident.CauseName,
                        AreaName = Incident.AreaName,
                        CityName = Incident.CityName,
                        CrashSeverityName = Incident.CrashSeverityName,
                        CrashTypeName = Incident.CrashTypeName,
                        IntersectionName = Incident.IntersectionName,
                        EmirateName = Incident.EmirateName,
                        LocationName = Incident.LocationName,
                        LightingName = Incident.LightingName,
                        RoadTypeName = Incident.RoadTypeName,
                        LocationTypeName = Incident.LocationTypeName,
                        WeatherName = Incident.WeatherName,
                        PConditionName = Incident.PConditionName
                    }).ToList();

                return lstIncidents;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<IncidentHistoricalDTO> GetIncidentHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType)
        {
            var res = new List<IncidentHistoricalDTO>();

            var allIncidents = GetIncidentssHistoryListByDate(StartDateTime, EndDateTime);
            if (allIncidents != null)
            {
                foreach (var item in allIncidents)
                {
                    var segId = Helper.GetSegmentId(StartDateTime, item.CreatedTime.Value, ScheduleType);
                    var seg = res.FirstOrDefault(x => x.Id == segId);
                    if (seg == null)
                    {
                        seg = new IncidentHistoricalDTO
                        {
                            Id = segId,
                            Name = ScheduleType.ToString() + segId,
                            ScheduleType = ScheduleType,
                        };
                        seg.Incidents.Add(item);
                        res.Add(seg);
                    }
                    else
                    {
                        seg.Incidents.Add(item);
                    }
                }
            }
            return res;
        }

        public DateTime GetIncidentFirstDate()
        {
            if (stcDataContext.CallOfServiceHistoricalViews != null && stcDataContext.CallOfServiceHistoricalViews.ToList().Count > 0)
                return stcDataContext.CallOfServiceHistoricalViews.OrderBy(x => x.CreatedDate).Select(x => x.CreatedDate).First().Value;
            else
                return DateTime.Now;
        }

        public IncidentsDTO GetIncidentHistoricalDetail(int IncidentId)
        {
            try
            {
                var lstIncidents = stcDataContext.CallOfServiceHistoricalViews
                   .Where(
                   Incident =>
                       Incident.ActivityId == IncidentId
                       )
                   .Select(Violation => new IncidentsDTO
                   {
                       IncidentId = Violation.ActivityId,
                       IncidentNumber = Violation.OriginalIdent,
                       //   IncidentTypeId = Violation.ServiceCallCategoryCode,
                       IncidentTypeName = Violation.ServiceCallCategoryDescription,
                       //PriorityId = Violation.ServiceCallPriorityCode.ToString(),
                       CreatedTime = Violation.CreatedDate.Value,
                       Latitude = Violation.IncidentLat,
                       Longitude = Violation.IncidentLong,
                       IncidentAddress = Violation.IncidentAddress,
                       CallerName = Violation.CallerName,
                       CallerNumber = Violation.CallerNumber,
                       CallerAddress = Violation.CallerAddress,
                       //StatusId = Violation.StatusCode,
                       StatusName = Violation.StatusDescription,

                   }).FirstOrDefault();
                return lstIncidents;

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<IncidentsDTO> GetIncidentssHistoryListByDate(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            try
            {
                var lstIncidents = stcDataContext.CallOfServiceHistoricalViews
                   .Where(
                   Incident =>
                       Incident.CreatedDate.HasValue && (StartDateTime.HasValue == false || Incident.CreatedDate >= StartDateTime.Value) &&
                       (EndDateTime.HasValue == false || Incident.CreatedDate <= EndDateTime.Value)
                       )
                   .Select(Violation => new IncidentsDTO
                   {
                       IncidentId = Violation.ActivityId,
                       IncidentNumber = Violation.OriginalIdent,
                       IncidentTypeId = 1,
                       IncidentTypeName = Violation.ServiceCallCategoryDescription,
                       //PriorityId = Violation.ServiceCallPriorityCode.ToString(),
                       CreatedTime = Violation.CreatedDate.Value,
                       Latitude = Violation.IncidentLat,
                       Longitude = Violation.IncidentLong,
                       IncidentAddress = Violation.IncidentAddress,
                       CallerName = Violation.CallerName,
                       CallerNumber = Violation.CallerNumber,
                       CallerAddress = Violation.CallerAddress,
                       //StatusId = Violation.StatusCode,
                       StatusName = Violation.StatusDescription,

                   }).ToList();
                return lstIncidents;

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
