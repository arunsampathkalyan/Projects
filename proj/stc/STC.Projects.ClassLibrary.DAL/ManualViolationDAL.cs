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
    public class ManualViolationDAL
    {
        #region General Declrations

        private readonly Entities.Entities context = new Entities.Entities();
        private readonly STCOperationalDataContext operational = new STCOperationalDataContext();

        public int insertedRowsCount = 0;
        public int duplicatedRowsCount = 0;
        public int corruptedRowsCount = 0;

        #endregion

        #region Save Manual Violations To CDS.

        private string GetRandomNumber()
        {
            try
            {
                return "MVR-" + new Random().Next(1, 999).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private Activity GetManualViolationFromCDS(string originalIdent)
        {
            var ManualViolationSource = context.Sources.Where(x => x.SourceName == "ManualViolation").FirstOrDefault();

            if (ManualViolationSource == null)
            {
                return null;
            }

            var manualViolation = context.Activities.FirstOrDefault(ServiceCall => ServiceCall.OriginalIdent.ToLower().Trim() == originalIdent.ToLower().Trim() && ServiceCall.SourceId == ManualViolationSource.SourceId);

            return manualViolation;
        }

        public bool SaveManualViolationListToCDS(List<ManualViolationDTO> manualViolations)
        {
            if (manualViolations != null && manualViolations.Count > 0)
            {
                foreach (ManualViolationDTO violation in manualViolations)
                {
                    if (!AddNewManualViolationToCDS(violation))
                    {
                        Utility.WriteLog("Failed to write Manual Violation to CDS. Violation Number: " + violation.ViolationNumber + " at: " + DateTime.UtcNow.ToString());

                        continue;
                    }
                }

                return true;
            }

            return false;
        }

        private bool AddNewManualViolationToCDS(ManualViolationDTO manualViolation)
        {
            try
            {
                if (manualViolation == null)
                {
                    corruptedRowsCount++;
                    return true;
                }

                var oldManualViolation = GetManualViolationFromCDS(manualViolation.ViolationNumber);

                if (oldManualViolation != null)
                {
                    duplicatedRowsCount++;
                    return true;
                }

                var activityReason = context.ActivityReasonDIMs.Where(x => x.Description == manualViolation.Reason.Trim()).FirstOrDefault();
                var activityCategory = context.ActivityCategoryDIMs.Where(x => x.Description == "Manual Violation").FirstOrDefault();
                var source = context.Sources.Where(x => x.SourceName == "ManualViolation").FirstOrDefault();
                var itemRegistrationAuthority = context.ItemRegistrationAuthorityDIMs.Where(x => x.Description == ((manualViolation.PlateSource != null && manualViolation.PlateSource.Trim().Length > 0) ? manualViolation.PlateSource : "Undefined")).FirstOrDefault();
                var itemRegistrationKind = context.ItemRegistrationPlateKindDIMs.Where(x => x.Description == ((manualViolation.PlateKind != null && manualViolation.PlateKind.Trim().Length > 0) ? manualViolation.PlateKind : "Undefined")).FirstOrDefault();
                var itemRegistrationCategory = context.ItemRegistrationPlateCategoryDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var itemRegistrationCode = context.ItemRegistrationPlateCodeDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var itemCategory = context.ItemCategoryDIMs.Where(x => x.Description == (manualViolation.VehicleType.Trim().Length > 0 ? manualViolation.VehicleType : "Undefined")).FirstOrDefault();
                var itemInvolvement = context.ItemInvolvementDIMs.Where(x => x.Description == "Manual Violation Vehicle").FirstOrDefault();
                var personInvolvement = context.PersonInvolvementDIMs.Where(x => x.Description == "Manual Violation Vehicle").FirstOrDefault();
                var locationInvolvement = context.LocationInvolvementDIMs.Where(x => x.Description == "Manual Violation Vehicle").FirstOrDefault();
                var personCategory = context.PersonCategoryDIMs.Where(x => x.Description == (manualViolation.AgeClass.Trim().Length > 0 ? manualViolation.AgeClass : "Undefined")).FirstOrDefault();
                var personGender = context.PersonGenderDIMs.Where(x => x.Description == (manualViolation.Gender.Trim().Length > 0 ? manualViolation.Gender : "Undefined")).FirstOrDefault();
                var location = context.Locations.Where(x => x.LocationName == (manualViolation.StreetName.Trim().Length > 0 ? manualViolation.StreetName : "Undefined")).FirstOrDefault();
                var educationCategory = context.EducationCategoryDIMs.Where(x => x.Description == (manualViolation.LevelOfEducation.Trim().Length > 0 ? manualViolation.LevelOfEducation : "Undefined")).FirstOrDefault();
                var educationDegree = context.EducationDegreeDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var organization = context.Organizations.Where(x => x.Name == "Undefined").FirstOrDefault();
                var educationStatus = context.EducationStatusDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var locationDatum = context.LocationDatumDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var locationCategory = context.LocationCategoryDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();
                var status = context.StatusDIMs.Where(x => x.Description == "Undefined").FirstOrDefault();


                if (activityReason == null)
                {
                    activityReason = new ActivityReasonDIM
                    {
                        Code = "MVR-" + manualViolation.ReasonCode.ToString(),
                        Description = manualViolation.Reason
                    };

                    activityReason = context.ActivityReasonDIMs.Add(activityReason);
                    context.SaveChanges();
                }

                if (activityCategory == null)
                {
                    activityCategory = new ActivityCategoryDIM
                    {
                        Description = "Manual Violation",
                        Code = "MANUALVIOLATION"
                    };

                    activityCategory = context.ActivityCategoryDIMs.Add(activityCategory);
                    context.SaveChanges();
                }

                if (source == null)
                {
                    source = new Source()
                    {
                        Description = "ManualViolation",
                        SourceName = "ManualViolation",
                        SourcePolicyId = 1,
                        SourceCategoryId = 1
                    };

                    source = context.Sources.Add(source);
                    context.SaveChanges();
                }

                if (itemRegistrationAuthority == null)
                {
                    itemRegistrationAuthority = new ItemRegistrationAuthorityDIM
                    {
                        Description = (manualViolation.PlateSource != null && manualViolation.PlateSource.Trim().Length > 0) ? manualViolation.PlateSource : "Undefined",
                        Code = GetRandomNumber()
                    };

                    itemRegistrationAuthority = context.ItemRegistrationAuthorityDIMs.Add(itemRegistrationAuthority);
                    context.SaveChanges();
                }

                if (itemRegistrationKind == null)
                {
                    itemRegistrationKind = new ItemRegistrationPlateKindDIM
                    {
                        Description = (manualViolation.PlateKind != null && manualViolation.PlateKind.Trim().Length > 0) ? manualViolation.PlateKind : "Undefined",
                        Code = GetRandomNumber()
                    };

                    itemRegistrationKind = context.ItemRegistrationPlateKindDIMs.Add(itemRegistrationKind);
                    context.SaveChanges();
                }

                if (itemRegistrationCategory == null)
                {
                    itemRegistrationCategory = new ItemRegistrationPlateCategoryDIM
                    {
                        Description = "Undefined",
                        Code = GetRandomNumber()
                    };

                    itemRegistrationCategory = context.ItemRegistrationPlateCategoryDIMs.Add(itemRegistrationCategory);
                    context.SaveChanges();
                }

                if (itemRegistrationCode == null)
                {
                    itemRegistrationCode = new ItemRegistrationPlateCodeDIM
                    {
                        Description = "Undefined",
                        Code = GetRandomNumber()
                    };

                    itemRegistrationCode = context.ItemRegistrationPlateCodeDIMs.Add(itemRegistrationCode);
                    context.SaveChanges();
                }

                if (itemCategory == null)
                {
                    itemCategory = new ItemCategoryDIM
                    {
                        Description = manualViolation.VehicleType.Trim().Length > 0 ? manualViolation.VehicleType : "Undefined",
                        Code = GetRandomNumber()
                    };

                    itemCategory = context.ItemCategoryDIMs.Add(itemCategory);
                    context.SaveChanges();
                }

                if (itemInvolvement == null)
                {
                    itemInvolvement = new ItemInvolvementDIM
                    {
                        Description = "Manual Violation Vehicle",
                        Code = GetRandomNumber()
                    };

                    itemInvolvement = context.ItemInvolvementDIMs.Add(itemInvolvement);
                    context.SaveChanges();
                }

                if (personInvolvement == null)
                {
                    personInvolvement = new PersonInvolvementDIM
                    {
                        Description = "Manual Violation Vehicle",
                        Code = GetRandomNumber()
                    };

                    personInvolvement = context.PersonInvolvementDIMs.Add(personInvolvement);
                    context.SaveChanges();
                }

                if (locationInvolvement == null)
                {
                    locationInvolvement = new LocationInvolvementDIM
                    {
                        Description = "Manual Violation Vehicle",
                        Code = GetRandomNumber()
                    };

                    locationInvolvement = context.LocationInvolvementDIMs.Add(locationInvolvement);
                    context.SaveChanges();
                }

                var item = new Item
                {
                    ItemCategoryId = itemCategory.ItemCategoryId,
                    SourceId = source.SourceId,
                };

                item = context.Items.Add(item);
                context.SaveChanges();

                var itemReg = new ItemRegistration
                {
                    ItemRegistrationAuthorityId = itemRegistrationAuthority.ItemRegistrationAuthorityId,
                    ItemRegistrationPlateCategoryId = itemRegistrationCategory.ItemRegistrationPlateCategoryId,
                    ItemRegistrationPlateCodeId = itemRegistrationCode.ItemRegistrationPlateCodeId,
                    ItemRegistrationPlateKindId = itemRegistrationKind.ItemRegistrationPlateKindId,
                    SourceId = source.SourceId,
                    PlateIdentification = "Undefiend",
                    ItemId = item.ItemId
                };

                itemReg = context.ItemRegistrations.Add(itemReg);
                context.SaveChanges();

                if (personCategory == null)
                {
                    personCategory = new PersonCategoryDIM
                    {
                        Code = GetRandomNumber(),
                        Description = manualViolation.AgeClass.Trim().Length > 0 ? manualViolation.AgeClass : "Undefined"
                    };

                    personCategory = context.PersonCategoryDIMs.Add(personCategory);
                    context.SaveChanges();
                }

                if (personGender == null)
                {
                    personGender = new PersonGenderDIM
                    {
                        Code = GetRandomNumber(),
                        Description = (manualViolation.Gender.Trim().Length > 0 ? manualViolation.Gender : "Undefined")
                    };

                    personGender = context.PersonGenderDIMs.Add(personGender);
                    context.SaveChanges();
                }

                if (educationCategory == null)
                {
                    educationCategory = new EducationCategoryDIM
                    {
                        Code = GetRandomNumber(),
                        Description = manualViolation.LevelOfEducation.Trim().Length > 0 ? manualViolation.LevelOfEducation : "Undefined"
                    };

                    educationCategory = context.EducationCategoryDIMs.Add(educationCategory);
                    context.SaveChanges();
                }

                if (educationDegree == null)
                {
                    educationDegree = new EducationDegreeDIM
                    {
                        Code = GetRandomNumber(),
                        Description = "Undefined"
                    };

                    educationDegree = context.EducationDegreeDIMs.Add(educationDegree);
                    context.SaveChanges();
                }

                if (educationStatus == null)
                {
                    educationStatus = new EducationStatusDIM
                    {
                        Code = GetRandomNumber(),
                        Description = "Undefined"
                    };

                    educationStatus = context.EducationStatusDIMs.Add(educationStatus);
                    context.SaveChanges();
                }

                if (organization == null)
                {
                    organization = new Organization
                    {
                        Name = "Undefined",
                        Description = "Undefined",
                        SourceId = source.SourceId
                    };

                    organization = context.Organizations.Add(organization);
                    context.SaveChanges();
                }

                Person person = new Person
                {
                    SourceId = source.SourceId,
                    LastName = manualViolation.OwnerName,
                    OriginalIdent = manualViolation.OwnerTCFNumber,
                    PersonCategoryId = personCategory.PersonCategoryId,
                    PersonGenderId = personGender.PersonGenderId,
                    Age = manualViolation.Age
                };

                person = context.People.Add(person);
                context.SaveChanges();

                var personDriverLicense = new PersonDriverLicense
                {
                    EffectiveDate = manualViolation.LicenseIssueDate,
                    PersonDriverLicenseDescription = manualViolation.LicenseSource,
                    PersonId = person.PersonId,
                    SourceId = source.SourceId
                };

                personDriverLicense = context.PersonDriverLicenses.Add(personDriverLicense);
                context.SaveChanges();

                var levelOfEducation = new PersonEducation
                {
                    PersonEducationDescription = manualViolation.LevelOfEducation.Trim().Length > 0 ? manualViolation.LevelOfEducation : "Undefined",
                    PersonId = person.PersonId,
                    SourceId = source.SourceId,
                    EducationCategoryId = educationCategory.EducationCategoryId,
                    EducationDegreeId = educationDegree.EducationDegreeId,
                    EducationStatusId = educationStatus.EducationStatusId,
                    EducationOrganizationId = organization.OrganizationId
                };

                levelOfEducation = context.PersonEducations.Add(levelOfEducation);
                context.SaveChanges();

                if (locationDatum == null)
                {
                    locationDatum = new LocationDatumDIM
                    {
                        Code = GetRandomNumber(),
                        Description = "Undefined"
                    };

                    locationDatum = context.LocationDatumDIMs.Add(locationDatum);
                    context.SaveChanges();
                }

                if (locationCategory == null)
                {
                    locationCategory = new LocationCategoryDIM
                    {
                        Code = GetRandomNumber(),
                        Description = "Undefined"
                    };

                    locationCategory = context.LocationCategoryDIMs.Add(locationCategory);
                    context.SaveChanges();
                }

                //GeoPoint
                var geoPoint = DbGeometry.FromText(
                    "POINT(" + (manualViolation.Lon) + " " +
                    (manualViolation.Lat) + ")", 4326);

                if (location == null)
                {
                    location = new Location
                    {
                        Latitude = manualViolation.Lat,
                        Longitude = manualViolation.Lon,
                        OrgLat = manualViolation.Lat.ToString(),
                        OrgLong = manualViolation.Lon.ToString(),
                        GeoPoint = geoPoint,
                        Address1 = (manualViolation.StreetName.Trim().Length > 0 ? manualViolation.StreetName : "Undefined"),
                        LocationName = (manualViolation.StreetName.Trim().Length > 0 ? manualViolation.StreetName : "Undefined"),
                        Description = (manualViolation.StreetName.Trim().Length > 0 ? manualViolation.StreetName : "Undefined"),
                        SourceId = source.SourceId,
                        locationDatumId = locationDatum.LocationDatumId,
                        LocationCategoryId = locationCategory.LocationCategoryId
                    };

                    location = context.Locations.Add(location);
                    context.SaveChanges();
                }

                if (status == null)
                {
                    status = new StatusDIM
                    {
                        Code = GetRandomNumber(),
                        Description = "Undefined"
                    };

                    status = context.StatusDIMs.Add(status);
                    context.SaveChanges();
                }

                Activity activity = new Activity()
                {
                    ActivityDate = manualViolation.ViolationDate,
                    OriginalIdent = manualViolation.ViolationNumber,
                    SourceId = source.SourceId,
                    ActivityReasonId = activityReason.ActivityReasonId,
                    ActivityCategoryId = activityCategory.ActivityCategoryId,
                    StatusId = status.StatusId
                };

                activity = context.Activities.Add(activity);
                context.SaveChanges();

                var activityLocation = new ActivityLocation()
                {
                    LocationId = location.LocationId,
                    ActivityId = activity.ActivityId,
                    CreateDateTimeStamp = DateTime.Now,
                    ModifiedDateTimeStamp = DateTime.Now,
                    ActivityLocationDescription = "Manual Violation Location",
                    SourceId = source.SourceId,
                    LocationInvolvementId = locationInvolvement.LocationInvolvementId
                };

                context.ActivityLocations.Add(activityLocation);
                context.SaveChanges();

                var activityItem = new ActivityItem
                {
                    ItemId = item.ItemId,
                    ActivityId = activity.ActivityId,
                    CreateDateTimeStamp = DateTime.Now,
                    ModifiedDateTimeStamp = DateTime.Now,
                    ItemInvolvementId = itemInvolvement.ItemInvolvementId,
                    SourceId = source.SourceId
                };

                context.ActivityItems.Add(activityItem);
                context.SaveChanges();

                var activityPerson = new ActivityPerson
                {
                    PersonId = person.PersonId,
                    ActivityId = activity.ActivityId,
                    CreateDateTimeStamp = DateTime.Now,
                    ModifiedDateTimeStamp = DateTime.Now,
                    SourceId = source.SourceId,
                    PersonInvolvementId = personInvolvement.PersonInvolvementId
                };

                context.ActivityPersons.Add(activityPerson);
                context.SaveChanges();

                insertedRowsCount++;

                return true;
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

        #region Save Manual Violations to Operational.

        public bool SaveManualViolationListToOperational(List<ManualViolationDTO> manualViolations)
        {
            if (manualViolations != null && manualViolations.Count > 0)
            {
                foreach (ManualViolationDTO violation in manualViolations)
                {
                    if (violation.ViolationDate.Year != DateTime.Now.Year)
                        continue;

                    if (!SaveManualViolationToOperational(violation))
                    {
                        Utility.WriteLog("Failed to write manual violation to Operational. Violation Number: " + violation.ViolationNumber + " at: " + DateTime.UtcNow.ToString());

                        continue;
                    }
                }

                return true;
            }

            return false;
        }

        private bool SaveManualViolationToOperational(ManualViolationDTO manualViolation)
        {
            try
            {
                if (manualViolation == null)
                    return false;

                var violation = operational.ManualViolations.Where(i => i.ViolationNumber == manualViolation.ViolationNumber).FirstOrDefault();

                if (violation != null)
                    return true;

                int? ReasonId = null;
                int? TypeId = null;
                int? RadarClassId = null;
                int? RadarTypeId = null;
                int? EmirateId = null;
                int? CityId = null;
                int? LicenseSourceId = null;
                long? PlateSourceId = null;
                long? PlateKindId = null;
                int? VehicleTypeId = null;
                int? GenderId = null;
                int? AgeClassId = null;
                int? LevelOfEducationId = null;
                int? MaritalStatusId = null;

                if (manualViolation.Reason != null && manualViolation.Reason.Trim().Length > 0)
                {
                    var reason = operational.ManualViolationReasons.Where(x => x.Reason == manualViolation.Reason.Trim()).FirstOrDefault();

                    if (reason == null)
                        reason = operational.ManualViolationReasons.Add(new ManualViolationReason { ReasonCode = manualViolation.ReasonCode, Reason = manualViolation.Reason.Trim() });

                    operational.SaveChanges();

                    ReasonId = reason.ReasonId;
                }

                if (manualViolation.Type != null && manualViolation.Type.Trim().Length > 0)
                {
                    var Type = operational.ManualViolationTypes.Where(x => x.Type == manualViolation.Type.Trim()).FirstOrDefault();

                    if (Type == null)
                        Type = operational.ManualViolationTypes.Add(new ManualViolationType { Type = manualViolation.Type.Trim() });

                    operational.SaveChanges();

                    TypeId = Type.TypeId;
                }

                if (manualViolation.RadarClass != null && manualViolation.RadarClass.Trim().Length > 0)
                {
                    var RadarClass = operational.ManualViolationRadarClasses.Where(x => x.RadarClass == manualViolation.RadarClass.Trim()).FirstOrDefault();

                    if (RadarClass == null)
                        RadarClass = operational.ManualViolationRadarClasses.Add(new ManualViolationRadarClass { RadarClass = manualViolation.RadarClass.Trim() });

                    operational.SaveChanges();

                    RadarClassId = RadarClass.RadarClassId;
                }

                if (manualViolation.RadarType != null && manualViolation.RadarType.Trim().Length > 0)
                {
                    var RadarType = operational.ManualViolationRadarTypes.Where(x => x.RadarType == manualViolation.RadarType.Trim()).FirstOrDefault();

                    if (RadarType == null)
                        RadarType = operational.ManualViolationRadarTypes.Add(new ManualViolationRadarType { RadarType = manualViolation.RadarType.Trim() });

                    operational.SaveChanges();

                    RadarTypeId = RadarType.RadarTypeId;
                }

                if (manualViolation.Emirate != null && manualViolation.Emirate.Trim().Length > 0)
                {
                    var Emirate = operational.IncidentEmirates.Where(x => x.EmirateNameAr == manualViolation.Emirate.Trim()).FirstOrDefault();

                    if (Emirate == null)
                        Emirate = operational.IncidentEmirates.Add(new IncidentEmirate { EmirateNameAr = manualViolation.Emirate.Trim() });

                    operational.SaveChanges();

                    EmirateId = Emirate.Id;
                }

                if (manualViolation.City != null && manualViolation.City.Trim().Length > 0)
                {
                    var City = operational.IncidentCities.Where(x => x.CityNameAr == manualViolation.City.Trim()).FirstOrDefault();

                    if (City == null)
                        City = operational.IncidentCities.Add(new IncidentCity { CityNameAr = manualViolation.City.Trim() });

                    operational.SaveChanges();

                    CityId = City.Id;
                }

                if (manualViolation.LicenseSource != null && manualViolation.LicenseSource.Trim().Length > 0)
                {
                    var LicenseSource = operational.ManualViolationLicenseSources.Where(x => x.LicenseSource == manualViolation.LicenseSource.Trim()).FirstOrDefault();

                    if (LicenseSource == null)
                        LicenseSource = operational.ManualViolationLicenseSources.Add(new ManualViolationLicenseSource { LicenseSource = manualViolation.LicenseSource.Trim() });

                    operational.SaveChanges();

                    LicenseSourceId = LicenseSource.LicenseSourceId;
                }

                if (manualViolation.PlateSource != null && manualViolation.PlateSource.Trim().Length > 0)
                {
                    var VehiclePlateSource = operational.VehiclePlateSources.Where(x => x.Name == manualViolation.PlateSource.Trim()).FirstOrDefault();

                    if (VehiclePlateSource != null)
                        PlateSourceId = VehiclePlateSource.Id;
                }

                if (manualViolation.PlateKind != null && manualViolation.PlateKind.Trim().Length > 0)
                {
                    var VehiclePlateKind = operational.VehiclePlateKinds.Where(x => x.Name == manualViolation.PlateKind.Trim()).FirstOrDefault();

                    if (VehiclePlateKind != null)
                        PlateKindId = VehiclePlateKind.Id;
                }

                if (manualViolation.VehicleType != null && manualViolation.VehicleType.Trim().Length > 0)
                {
                    var VehicleType = operational.ManualViolationVehicleTypes.Where(x => x.VehicleType == manualViolation.VehicleType.Trim()).FirstOrDefault();

                    if (VehicleType == null)
                        VehicleType = operational.ManualViolationVehicleTypes.Add(new ManualViolationVehicleType { VehicleType = manualViolation.VehicleType.Trim() });

                    operational.SaveChanges();

                    VehicleTypeId = VehicleType.VehicleTypeId;
                }

                if (manualViolation.Gender != null && manualViolation.Gender.Trim().Length > 0)
                {
                    var Gender = operational.Genders.Where(x => x.GenderName == manualViolation.Gender.Trim()).FirstOrDefault();

                    if (Gender == null)
                        Gender = operational.Genders.Add(new Gender { GenderName = manualViolation.Gender.Trim() });

                    operational.SaveChanges();

                    GenderId = Gender.GenderId;
                }

                if (manualViolation.LevelOfEducation != null && manualViolation.LevelOfEducation.Trim().Length > 0)
                {
                    var LevelOfEducation = operational.LevelOfEducations.Where(x => x.LevelOfEducationName == manualViolation.LevelOfEducation.Trim()).FirstOrDefault();

                    if (LevelOfEducation == null)
                        LevelOfEducation = operational.LevelOfEducations.Add(new LevelOfEducation { LevelOfEducationName = manualViolation.LevelOfEducation.Trim() });

                    operational.SaveChanges();

                    LevelOfEducationId = LevelOfEducation.LevelOfEducationId;
                }

                if (manualViolation.MaritalStatus != null && manualViolation.MaritalStatus.Trim().Length > 0)
                {
                    var MaritalStatu = operational.MaritalStatus.Where(x => x.MaritalStatusName == manualViolation.MaritalStatus.Trim()).FirstOrDefault();

                    if (MaritalStatu == null)
                        MaritalStatu = operational.MaritalStatus.Add(new MaritalStatu { MaritalStatusName = manualViolation.MaritalStatus.Trim() });

                    operational.SaveChanges();

                    MaritalStatusId = MaritalStatu.MaritalStatusId;
                }

                if (manualViolation.AgeClass != null && manualViolation.AgeClass.Trim().Length > 0)
                {
                    var AgeClass = operational.AgeClasses.Where(x => x.AgeClassName == manualViolation.AgeClass.Trim()).FirstOrDefault();

                    if (AgeClass == null)
                        AgeClass = operational.AgeClasses.Add(new AgeClass { AgeClassName = manualViolation.AgeClass.Trim() });

                    operational.SaveChanges();

                    AgeClassId = AgeClass.AgeClassId;
                }

                var manualViolationToInsert = new ManualViolation
                {
                    Age = manualViolation.Age,
                    AgeClassId = AgeClassId,
                    CityId = CityId,
                    EmirateId = EmirateId,
                    GenderId = GenderId,
                    Lat = manualViolation.Lat,
                    LevelOfEducationId = LevelOfEducationId,
                    LicenseIssueDate = manualViolation.LicenseIssueDate,
                    LicenseSourceId = LicenseSourceId,
                    Lon = manualViolation.Lon,
                    MaritalStatusId = MaritalStatusId,
                    OwnerName = manualViolation.OwnerName,
                    OwnerTCFNumber = manualViolation.OwnerTCFNumber,
                    PlateKindId = PlateKindId,
                    PlateSourceId = PlateSourceId,
                    RadarClassId = RadarClassId,
                    RadarTypeId = RadarTypeId,
                    ReasonId = ReasonId,
                    RoadSpeed = manualViolation.RoadSpeed,
                    StreetName = manualViolation.StreetName,
                    TCFNumber = manualViolation.TCFNumber,
                    TypeId = TypeId,
                    VehicleTypeId = VehicleTypeId,
                    ViolationDate = manualViolation.ViolationDate,
                    ViolationTime = manualViolation.ViolationTime,
                    ViolationNumber = manualViolation.ViolationNumber
                };

                operational.ManualViolations.Add(manualViolationToInsert);

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

        #endregion
    }
}
