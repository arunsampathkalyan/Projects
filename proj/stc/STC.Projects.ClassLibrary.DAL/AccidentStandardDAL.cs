using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace STC.Projects.ClassLibrary.DAL
{
    public class AccidentStandardDAL
    {
        #region General Declrations

        private readonly Entities.Entities context = new Entities.Entities();
        private readonly STCOperationalDataContext operational = new STCOperationalDataContext();

        public int insertedRowsCount = 0;
        public int duplicatedRowsCount = 0;
        public int corruptedRowsCount = 0;

        #endregion

        #region Save Accidents To CDS

        public bool ValidateAccident(AccidentStandardDTO accident)
        {
            //do we need to have a validation method??
            return true;
        }

        private ServiceCall GetAccidentFromCDS(string originalIdent)
        {
            var serviceCall = context.ServiceCalls.FirstOrDefault(ServiceCall => ServiceCall.OriginalIdent.ToLower().Trim() == originalIdent.ToLower().Trim());

            return serviceCall;
        }

        public bool SaveAccidentsListToCDS(List<AccidentStandardDTO> accidentList)
        {
            if (accidentList == null || accidentList.Count == 0)
                return false;

            foreach (AccidentStandardDTO item in accidentList)
            {
                if (ValidateAccident(item))
                {
                    try
                    {
                        item.ServiceCallStep = ServiceCallEnum.New;
                        if (!SaveAccidentToCDS(item))
                        {
                            Utility.WriteLog("Failed to write accident to CDS. Accident Number: " + item.IncidentNo + " at: " + DateTime.UtcNow.ToString());
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            return true;
        }

        public bool SaveAccidentToCDS(AccidentStandardDTO accident)
        {
            switch (accident.ServiceCallStep)
            {
                case ServiceCallEnum.New:
                    return AddNewAccidentToCDS(accident);
                default:
                    return false;
            }
        }

        private string Truncate(string value, int length)
        {
            try
            {
                //if (value.Trim().Length > length)
                //    return value.Substring(0, length);
                //else
                //    return value.Trim();

                return new Random().Next(1, 999).ToString();
            }
            catch
            {
                return string.Empty;

            }
        }

        private bool AddNewAccidentToCDS(AccidentStandardDTO accident)
        {
            try
            {
                context.Database.CommandTimeout = 5000;
                ServiceCall oldServiceCall = GetAccidentFromCDS(accident.IncidentNo);

                if (oldServiceCall != null)
                {
                    try
                    {
                        duplicatedRowsCount++;
                        oldServiceCall.ServiceClassDescription = accident.ClassName;

                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Utility.WriteLog(ex);
                    }

                    return true;
                }

                //Service Call
                #region Service Call /Activity

                var newServiceCall = new ServiceCall
                {
                    OriginalIdent = accident.IncidentNo,
                    ActivityStartDate = accident.CreatedTime,
                    ServiceCallCategoryCode = Truncate(accident.CrashTypeName, 20),
                    ServiceCallCategoryDescription = accident.CrashTypeName,
                    ServiceCallPriorityCode = Truncate(accident.IncidentTypeName, 20),
                    ServiceCallPriorityDescription = accident.IncidentTypeName,
                    SourceName = "CAD",
                    ActivityCategoryCode = "CAD",
                    ActivityCategoryDescription = "Calls For Service",
                    ActivityReasonCode = Truncate(accident.CauseName, 20),
                    ActivityReasonDescription = accident.CauseName,
                    ServiceCallReceivedDate = accident.CreatedTime,
                    ServiceCallSourceCode = "CAD",
                    ServiceCallSourceDescription = "CAD",
                    StatusDescription = accident.StatusName,
                    StatusCode = accident.StatusId, //Pending
                    LanesCount = accident.LanesCount,
                    SlightInjuriesCount = accident.SlightInjuriesCount,
                    MediumInjuriesCount = accident.MediumInjuriesCount,
                    SevereInjuriesCount = accident.SevereInjuriesCount,
                    FatalitiesCount = accident.FatalitiesCount,
                    TotalInjuriesFatalities = accident.TotalInjuriesFatalities,
                    CrashSeverityCode = accident.CrashSeverityCode,
                    CrashSeverityDescription = accident.CrashSeverityName,
                    ActivityDescription = accident.CrashDescription,
                    LightingConditionDescription = accident.LightingName,
                    //ServiceClassCode = Truncate(ServiceCall.ClassName, 20),
                    ServiceClassDescription = accident.ClassName
                };

                context.ServiceCalls.Add(newServiceCall);
                context.SaveChanges();

                var serviceCallDriver = new ServiceCallDriver
                {
                    SourceName = "CAD",
                    DriverNationalityCode = Truncate(accident.DriverNationality, 20),
                    DriverNationalityDescription = accident.DriverNationality,
                    DriverLicenceSourceCode = Truncate(accident.DriverLicenceSource, 20),
                    DriverLicenceSourceDescription = accident.DriverLicenceSource,
                    DriverLicenceTypeCode = Truncate(accident.DriverLicenceType, 20),
                    DriverLicenceTypeDescription = accident.DriverLicenceType
                };

                context.ServiceCallDrivers.Add(serviceCallDriver);
                context.SaveChanges();

                var activityPeople = new ActivityPersonView
                {
                    ActivityId = newServiceCall.ActivityId,
                    PersonId = serviceCallDriver.PersonId,
                    SourceName = "CAD",
                    PersonInvolvementCode = "Service Call Driver",
                    PersonInvolvementDescription = "Service Call Driver",
                };

                context.ActivityPersonViews.Add(activityPeople);
                context.SaveChanges();

                var serviceCallVehicle = new ServiceCallVehicle
                {
                    SourceName = "CAD",
                    VehicleLiabilityCode = Truncate(accident.VehiclesLiability, 20),
                    VehicleLiabilityDescription = accident.VehiclesLiability,
                    VehicleCollisionPointCode = Truncate(accident.VehicleCollisionPoint, 20),
                    VehicleCollisionPointDescription = accident.VehicleCollisionPoint,
                    VehicleCountryCode = Truncate(accident.VehicleCountry, 20),
                    VehicleCountryDescription = accident.VehicleCountry,
                };

                context.ServiceCallVehicles.Add(serviceCallVehicle);
                context.SaveChanges();

                var activityItem = new ActivityItemView
                {
                    ItemId = serviceCallVehicle.ItemId,
                    ActivityId = newServiceCall.ActivityId,
                    SourceName = "CAD",
                    ItemInvolvementCode = "Service Call Vehicle",
                    ItemInvolvementDescription = "Service Call Vehicle"
                };

                context.ActivityItemViews.Add(activityItem);
                context.SaveChanges();
                #endregion

                var geoPoint = DbGeometry.FromText(
                    "POINT(" + (accident.Longitude.HasValue ? accident.Longitude.Value : 0) + " " +
                    (accident.Latitude.HasValue ? accident.Latitude.Value : 0) + ")", 4326);

                var _Location = new LocationView
                {
                    LocationCategoryCode = new Random().Next().ToString(),
                    LocationCategoryDescription = accident.LocationTypeName,
                    Latitude = accident.Latitude.HasValue ? accident.Latitude.Value : 0,
                    Longitude = accident.Longitude.HasValue ? accident.Longitude.Value : 0,
                    OrgLat = accident.Latitude.HasValue ? accident.Latitude.Value.ToString() : "0",
                    OrgLong = accident.Longitude.HasValue ? accident.Longitude.Value.ToString() : "0",
                    locationIntersectionTypeDescription = accident.IntersectionName,
                    GeoStateCode = Truncate(accident.StateName, 20),
                    GeoStateStateName = accident.StateName,
                    GeoCityCode = Truncate(accident.CityName, 20),
                    GeoCityCityName = accident.CityName,
                    GeoCountyCode = Truncate(accident.AreaName, 20),
                    GeoCountyCountyName = accident.AreaName,
                    GeoPoint = geoPoint,
                    Address1 = accident.Address,
                    LocationName = accident.LocationName,
                    Description = accident.LocationDescription,
                    locationDatumCode = "Service Call",
                    locationDatumDescription = "Service Call Location Datum",
                    SourceName = "CAD",
                };

                context.LocationViews.Add(_Location);
                context.SaveChanges();

                var activityLocation = new ActivityLocationView
                {
                    LocationId = _Location.LocationId,
                    CreateDateTimeStamp = DateTime.Now,
                    ModifiedDateTimeStamp = DateTime.Now,
                    ActivityId = newServiceCall.ActivityId,
                    ActivityLocationDescription = "Service Call Location",
                    LocationInvolvementCode = "Incident Location",
                    LocationInvolvementDescription = "Incident Location",
                    SourceName = "CAD"
                };

                context.ActivityLocationViews.Add(activityLocation);
                context.SaveChanges();

                var serviceCallInsurance = new ServiceCallInsurance
                {
                    SourceName = "CAD",
                    Name = accident.InsuranceCompany,
                    InsuranceTypeDescription = accident.InsuranceType
                };

                context.ServiceCallInsurances.Add(serviceCallInsurance);
                context.SaveChanges();

                var activityOrganization = new ActivityOrganization
                {
                    Organizationid = serviceCallInsurance.OrganizationId,
                    ActivityId = newServiceCall.ActivityId,
                    OrganizationInvolvementDescription = "Service Call Insurance Company",
                    SourceName = "CAD"
                };

                context.ActivityOrganizations.Add(activityOrganization);
                context.SaveChanges();

                if (accident.PoliceStationName != null && accident.PoliceStationName.Trim().Length > 0)
                {
                    var policeStation = new OrganizationView
                    {
                        SourceName = "CAD",
                        Name = accident.PoliceStationName,
                    };

                    context.OrganizationViews.Add(policeStation);

                    var policeActivityOrganization = new ActivityOrganization
                    {
                        Organizationid = policeStation.OrganizationId,
                        ActivityId = newServiceCall.ActivityId,
                        OrganizationInvolvementDescription = "Service Call Police Station",
                        SourceName = "CAD"
                    };

                    context.ActivityOrganizations.Add(policeActivityOrganization);
                }

                if (context.SaveChanges() > 0)
                {
                    insertedRowsCount++;
                    return true;
                }

                return false;
            }
            catch (DbEntityValidationException ex)
            {
                Utility.WriteLog(ex);
                corruptedRowsCount++;
                return false;
            }
            catch (DbUpdateException ex)
            {
                Utility.WriteLog(ex);
                corruptedRowsCount++;
                return false;
            }
            catch (EntityCommandCompilationException ex)
            {
                Utility.WriteLog(ex);
                corruptedRowsCount++;
                return false;
            }
            catch (EntityCommandExecutionException ex)
            {
                Utility.WriteLog(ex);
                corruptedRowsCount++;
                return false;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                corruptedRowsCount++;
                return false;
            }
        }

        #endregion

        #region Save Accidents to Operational

        private bool SaveAccidentToOperational(AccidentStandardDTO accident)
        {
            try
            {

                if (accident == null)
                    return false;

                var incident = operational.Incident.Where(i => i.IncidentNumber == accident.IncidentNo).FirstOrDefault();

                if (incident != null)
                    return true;

                int? incidentAreaId = null;
                int? incidentCauseId = null;
                int? incidentCityId = null;
                int? incidentCrashSeverityId = null;
                int? incidentCrashTypeId = null;
                int? incidentEmirateId = null;
                int? incidentIntersectionId = null;
                int? incidentLightingId = null;
                int? incidentLocationId = null;
                int? incidentLocationTypeId = null;
                int? incidentPConditionId = null;
                int? incidentPoliceStationId = null;
                int? incidentRoadTypeId = null;
                int? incidentTypeId = null;
                int? incidentWeatherId = null;

                if (accident.AreaName != null && accident.AreaName.Trim().Length > 0)
                {
                    var incidentArea = operational.IncidentAreas.Where(a => a.AreaNameAr == accident.AreaName).FirstOrDefault();

                    if (incidentArea == null)
                        incidentArea = operational.IncidentAreas.Add(new IncidentArea { AreaNameAr = accident.AreaName });

                    operational.SaveChanges();

                    incidentAreaId = incidentArea.Id;
                }

                if (accident.CauseName != null && accident.CauseName.Trim().Length > 0)
                {
                    var incidentCause = operational.IncidentCauses.Where(a => a.CauseNameAr == accident.CauseName).FirstOrDefault();

                    if (incidentCause == null)
                        incidentCause = operational.IncidentCauses.Add(new IncidentCause { CauseNameAr = accident.CauseName });

                    operational.SaveChanges();

                    incidentCauseId = incidentCause.Id;
                }

                if (accident.CityName != null && accident.CityName.Trim().Length > 0)
                {
                    var incidentCity = operational.IncidentCities.Where(a => a.CityNameAr == accident.CityName).FirstOrDefault();

                    if (incidentCity == null)
                        incidentCity = operational.IncidentCities.Add(new IncidentCity { CityNameAr = accident.CityName });

                    operational.SaveChanges();

                    incidentCityId = incidentCity.Id;
                }

                if (accident.CrashSeverityName != null && accident.CrashSeverityName.Trim().Length > 0)
                {
                    var incidentCrashSeverity = operational.IncidentCrashSeverities.Where(a => a.CrashSeverityNameAr == accident.CrashSeverityName).FirstOrDefault();

                    if (incidentCrashSeverity == null)
                        incidentCrashSeverity = operational.IncidentCrashSeverities.Add(new IncidentCrashSeverity { CrashSeverityNameAr = accident.CrashSeverityName });

                    operational.SaveChanges();

                    incidentCrashSeverityId = incidentCrashSeverity.Id;
                }

                if (accident.CrashTypeName != null && accident.CrashTypeName.Trim().Length > 0)
                {
                    var incidentCrashType = operational.IncidentCrashTypes.Where(a => a.IncidentCrashTypeNameAr == accident.CrashTypeName).FirstOrDefault();

                    if (incidentCrashType == null)
                        incidentCrashType = operational.IncidentCrashTypes.Add(new IncidentCrashType { IncidentCrashTypeNameAr = accident.CrashTypeName });

                    operational.SaveChanges();

                    incidentCrashTypeId = incidentCrashType.Id;
                }

                if (accident.EmirateName != null && accident.EmirateName.Trim().Length > 0)
                {
                    var incidentEmirate = operational.IncidentEmirates.Where(a => a.EmirateNameAr == accident.EmirateName).FirstOrDefault();

                    if (incidentEmirate == null)
                        incidentEmirate = operational.IncidentEmirates.Add(new IncidentEmirate { EmirateNameAr = accident.EmirateName });

                    operational.SaveChanges();

                    incidentEmirateId = incidentEmirate.Id;
                }

                if (accident.IntersectionName != null && accident.IntersectionName.Trim().Length > 0)
                {
                    var incidentIntersection = operational.IncidentIntersections.Where(a => a.IntersectionNameAr == accident.IntersectionName).FirstOrDefault();

                    if (incidentIntersection == null)
                        incidentIntersection = operational.IncidentIntersections.Add(new IncidentIntersection { IntersectionNameAr = accident.IntersectionName });

                    operational.SaveChanges();

                    incidentIntersectionId = incidentIntersection.Id;
                }

                if (accident.LightingName != null && accident.LightingName.Trim().Length > 0)
                {
                    var incidentLighting = operational.IncidentLightings.Where(a => a.LightingNameAr == accident.LightingName).FirstOrDefault();

                    if (incidentLighting == null)
                        incidentLighting = operational.IncidentLightings.Add(new IncidentLighting { LightingNameAr = accident.LightingName });

                    operational.SaveChanges();

                    incidentLightingId = incidentLighting.Id;
                }

                if (accident.LocationName != null && accident.LocationName.Trim().Length > 0)
                {
                    var incidentLocation = operational.IncidentLocations.Where(a => a.LocationNameAr == accident.LocationName).FirstOrDefault();

                    if (incidentLocation == null)
                        incidentLocation = operational.IncidentLocations.Add(new IncidentLocation { LocationNameAr = accident.LocationName });

                    operational.SaveChanges();

                    incidentLocationId = incidentLocation.Id;
                }

                if (accident.LocationTypeName != null && accident.LocationTypeName.Trim().Length > 0)
                {
                    var incidentLocationType = operational.IncidentLocationTypes.Where(a => a.LocationTypeNameAr == accident.LocationTypeName).FirstOrDefault();

                    if (incidentLocationType == null)
                        incidentLocationType = operational.IncidentLocationTypes.Add(new IncidentLocationType { LocationTypeNameAr = accident.LocationTypeName });

                    operational.SaveChanges();

                    incidentLocationTypeId = incidentLocationType.Id;
                }

                if (accident.PConditionName != null && accident.PConditionName.Trim().Length > 0)
                {
                    var incidentPCondition = operational.IncidentPConditions.Where(a => a.PConditionNameAr == accident.PConditionName).FirstOrDefault();

                    if (incidentPCondition == null)
                        incidentPCondition = operational.IncidentPConditions.Add(new IncidentPCondition { PConditionNameAr = accident.PConditionName });

                    operational.SaveChanges();

                    incidentPConditionId = incidentPCondition.Id;
                }

                if (accident.PoliceStationName != null && accident.PoliceStationName.Trim().Length > 0)
                {
                    var incidentPoliceStation = operational.IncidentPoliceStation.Where(a => a.PoliceStationNameAr == accident.PoliceStationName).FirstOrDefault();

                    if (incidentPoliceStation == null)
                        incidentPoliceStation = operational.IncidentPoliceStation.Add(new IncidentPoliceStation { PoliceStationNameAr = accident.PoliceStationName });

                    operational.SaveChanges();

                    incidentPoliceStationId = incidentPoliceStation.PoliceStationId;
                }

                if (accident.RoadTypeName != null && accident.RoadTypeName.Trim().Length > 0)
                {
                    var incidentRoadType = operational.IncidentRoadTypes.Where(a => a.RoadTypeNameAr == accident.RoadTypeName).FirstOrDefault();

                    if (incidentRoadType == null)
                        incidentRoadType = operational.IncidentRoadTypes.Add(new IncidentRoadType { RoadTypeNameAr = accident.RoadTypeName });

                    operational.SaveChanges();

                    incidentRoadTypeId = incidentRoadType.Id;
                }

                if (accident.IncidentTypeName != null && accident.IncidentTypeName.Trim().Length > 0)
                {
                    var incidentIncidentType = operational.IncidentTypes.Where(a => a.IncidentTypeNameAr == accident.IncidentTypeName).FirstOrDefault();

                    if (incidentIncidentType == null)
                        incidentIncidentType = operational.IncidentTypes.Add(new IncidentType { IncidentTypeNameAr = accident.IncidentTypeName });

                    operational.SaveChanges();

                    incidentTypeId = incidentIncidentType.IncidentTypeId;
                }

                if (accident.WeatherName != null && accident.WeatherName.Trim().Length > 0)
                {
                    var incidentWeather = operational.IncidentWeathers.Where(a => a.WeatherNameAr == accident.WeatherName).FirstOrDefault();

                    if (incidentWeather == null)
                        incidentWeather = operational.IncidentWeathers.Add(new IncidentWeather { WeatherNameAr = accident.WeatherName });

                    operational.SaveChanges();

                    incidentWeatherId = incidentWeather.Id;
                }

                var pointString = string.Format("POINT({0} {1})", accident.Longitude.ToString(), accident.Latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString);

                var newAccident = new Incident
                {
                    AreaId = incidentAreaId,
                    CauseId = incidentCauseId,
                    CityId = incidentCityId,
                    IncidentTypeId = incidentTypeId,
                    IntersectionId = incidentIntersectionId,
                    LocationId = incidentLocationId,
                    LightingId = incidentLightingId,
                    LocationTypeId = incidentLocationTypeId,
                    CrashSeverityId = incidentCrashSeverityId,
                    CrashTypeId = incidentCrashTypeId,
                    EmirateId = incidentEmirateId,
                    PConditionId = incidentPConditionId,
                    PoliceStationId = incidentPoliceStationId,
                    RoadTypeId = incidentRoadTypeId,
                    WeatherId = incidentWeatherId,
                    IncidentAddress = accident.AddressComment,
                    LanesCount = accident.LanesCount,
                    TotalInjuriesFatalities = accident.TotalInjuriesFatalities,
                    SevereInjuries = accident.SevereInjuriesCount,
                    SlightInjuries = accident.SlightInjuriesCount,
                    Latitude = accident.Latitude,
                    Longitude = accident.Longitude,
                    MediumInjuries = accident.MediumInjuriesCount,
                    Speed = (int)accident.RoadSpeed,
                    Fatalities = accident.FatalitiesCount,
                    GeoLocation = dbGeography,
                    IncidentNumber = accident.IncidentNo,
                    IsNoticed = false,
                    CreatedTime = accident.CreatedTime,
                    LocationDescription = accident.LocationDescription,
                    IncidentDescription = accident.CrashDescription
                };

                operational.Incident.Add(newAccident);

                return operational.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
            catch (DbUpdateException ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
            catch (EntityCommandCompilationException ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
            catch (EntityCommandExecutionException ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
        }

        public bool SaveAccidentListToOperational(List<AccidentStandardDTO> accidents)
        {
            if (accidents != null && accidents.Count > 0)
            {
                foreach (AccidentStandardDTO accident in accidents)
                {
                    if (accident.CreatedTime.HasValue && accident.CreatedTime.Value.Year != DateTime.Now.Year)
                        continue;

                    if (!SaveAccidentToOperational(accident))
                    {
                        Utility.WriteLog("Failed to write accident to Operational. Accident Number: " + accident.IncidentNo + " at: " + DateTime.UtcNow.ToString());

                        continue;
                    }
                }

                return true;
            }

            return false;
        }

        #endregion

        #region Get Excel files & Excel mapping details

        public List<ExcelMapDTO> GetAccidentExcelMappingDetails()
        {
            var map = operational.AccidentExcelMaps.Select(x => new ExcelMapDTO
            {
                ExcelMapId = x.AccidentExcelMapId,
                ExcelMapFieldName = x.AccidentExcelMapFieldName,
                ExcelMapFieldIndex = x.AccidentExcelMapFieldIndex ?? 0
            }).ToList();

            return map;
        }

        public bool AddUploadedFile(UploadedFileDTO uploadedFile)
        {
            operational.UploadedFiles.Add(new UploadedFile
            {
                FileOriginalName = uploadedFile.FileOriginalName,
                FileInsertedRowsCount = uploadedFile.FileInsertedRowsCount,
                FileCorruptedRowsCount = uploadedFile.FileCorruptedRowsCount,
                FileDuplicatedRowsCount = uploadedFile.FileDuplicatedRowsCount,
                FileUploadedBy = uploadedFile.FileUploadedById,
                FileSourceType = (int)uploadedFile.FileSourceType,
                FileUploadTime = uploadedFile.FileUploadTime
            });

            return operational.SaveChanges() > 0;
        }

        public List<UploadedFileDTO> GetAccidentUploadedFiles()
        {
            var accFiles = operational.UploadedFiles
                .Select(x => new UploadedFileDTO
                {
                    FileId = x.FileId,
                    FileOriginalName = x.FileOriginalName,
                    FileUploadTime = x.FileUploadTime ?? DateTime.Now,
                    FileUploadedById = x.FileUploadedBy ?? 0,
                    FileSourceType = (FileSourceTypes)x.FileSourceType,
                    FileInsertedRowsCount = x.FileInsertedRowsCount ?? 0,
                    FileDuplicatedRowsCount = x.FileDuplicatedRowsCount ?? 0,
                    FileCorruptedRowsCount = x.FileCorruptedRowsCount ?? 0
                }).ToList();

            if (accFiles != null && accFiles.Count > 0)
            {
                foreach (var file in accFiles)
                {
                    file.FileUploadedBy = new UsersDAL().GetUserById(file.FileUploadedById);
                }
            }

            return accFiles;
        }

        public bool InsertAccidentFileDetails(UploadedFileDTO fileDetails)
        {
            if (fileDetails == null)
                return false;

            var uploadedFile = new UploadedFile
            {
                FileOriginalName = fileDetails.FileOriginalName,
                FileUploadedBy = fileDetails.FileUploadedById,
                FileUploadTime = fileDetails.FileUploadTime,
                FileSourceType = (int)fileDetails.FileSourceType,
                FileInsertedRowsCount = fileDetails.FileInsertedRowsCount,
                FileDuplicatedRowsCount = fileDetails.FileDuplicatedRowsCount,
                FileCorruptedRowsCount = fileDetails.FileCorruptedRowsCount
            };

            operational.UploadedFiles.Add(uploadedFile);

            return operational.SaveChanges() > 0;
        }

        #endregion
    }
}
