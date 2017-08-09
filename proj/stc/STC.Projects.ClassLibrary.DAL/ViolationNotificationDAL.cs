using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Data.Entity.Spatial;
using System.Data.Objects.SqlClient;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DAL.Utilities;
using System.Configuration;
using System.Net;
using SimpleImpersonation;
using System.Drawing;
using System.IO;

namespace STC.Projects.ClassLibrary.DAL
{
    public class ViolationNotificationDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        private Entities.Entities stcDataContext = new Entities.Entities();

        public void AddWantedVehicle(string plateNumber, string plateCategory, string plateAuthority, string plateColor, DateTime violationTime, int corelationPeriod = 600, string reason = "")
        {
            WantedVehicle wantedVehicle = new WantedVehicle()
            {
                PlateNumber = plateNumber,
                PlateCategory = plateCategory,
                PlateAuthority = plateAuthority,
                PlateColor = plateColor,
                WantedDate = violationTime,
                IsActive = true
            };
            if (!operationalDataContext.WantedVehicles.Any(x => x.PlateNumber == plateNumber && x.PlateCategory == plateCategory && x.PlateAuthority == plateAuthority && x.PlateColor == plateColor && x.IsActive))
            {
                operationalDataContext.WantedVehicles.Add(wantedVehicle);
                operationalDataContext.SaveChanges();
            }
            try
            {

                //        List<ViolationNotificationDTO> violationList = GetDangerousVehicleViolationList(plateNumber, plateColor,
                //plateAuthority, plateCategory, DateTime.Now.AddMinutes(corelationPeriod * -1), DateTime.Now);

                DangerousVehicleView vehicle = new DangerousVehicleView
                {
                    PlateNumber = plateNumber,
                    PlateKind = plateCategory,
                    PlateCode = plateAuthority,
                    PlateColor = plateColor,
                    SourceName = "DangerousVehicle",
                    ActivityCategoryCode = "DangerousVehicle",
                    ActivityCategoryDescription = "New Dangerous Vehicle",
                    ActivityReasonCode = reason.Trim().Length == 0 ? "New Dangerous Vehicle " : reason,
                    ActivityReasonDescription = reason.Trim().Length == 0 ? "New Dangerous Vehicle " : reason,
                    StatusCode = "1",
                    StatusDescription = "New",
                    ActivityDate = DateTime.Now,
                    CorrelationPeriod = corelationPeriod
                };

                stcDataContext.DangerousVehicleViews.Add(vehicle);

                //if (violationList != null && violationList.Count > 0)
                //{
                //    foreach (ViolationNotificationDTO violation in violationList)
                //    {
                //        DangerousVehicleViolation vehicleViolation = new DangerousVehicleViolation
                //        {
                //           // ActivityId = vehicle.ActivityId,
                //            OldViolationActivityId = violation.ViolationNotificationId
                //        };
                //        //vehicle.
                //        stcDataContext.DangerousVehicleViolations.Add(vehicleViolation);
                //    }
                //}

                stcDataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                try
                {
                    Utility.WriteLog(ex, @"C:\STC\Websites\Server\WCF\");
                }
                catch (Exception exm)
                {

                }
            }
        }

        public List<ViolationNotificationDTO> GetUpdatedViolations()
        {
            try
            {
                var lstViolations = operationalDataContext.ViolationNotifications
                    .Where(Violation => Violation.IsNoticed == false && Violation.NotificationId.HasValue)
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.LocationCode,
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = Violation.Altitude,
                        AssetCode = Violation.AssetCode,
                        AssetId = Violation.AssetId,
                        CapturedSpeed = Violation.CapturedSpeed,
                        DateTaken = Violation.DateTaken,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.MesuredSpeed,
                        PlateColorName = Violation.PlateColorName,
                        PlateKindName = Violation.PlateKindName,
                        PlateNumber = Violation.PlateNumber,
                        PlateSourceName = Violation.PlateSourceName,
                        PlateTypeName = Violation.PlateTypeName,
                        SpeedLimit = Violation.SpeedLimit,
                        VehicleTypeId = Violation.VehicleTypeId,
                        VehicleTypeName = Violation.VehicleTypeName,
                        ViolationNotificationId = Violation.ViolationNotificationId,
                        ViolationTypeName = Violation.ViolationTypeName,
                        IsNoticed = Violation.IsNoticed,
                        BackgroundColor = "Orange",
                        MessageId = Violation.ViolationNotificationId.ToString(),
                        MessageText = "New Violation: " + Violation.ViolationNotificationId,
                        IsCritical = Violation.IsCritical,
                        Direction = Violation.Direction,
                        DirectionName = Violation.DirectionName,
                        LPRId = Violation.LPRId,
                        LaneNo = Violation.LaneNo,
                        SourceId = Violation.SourceId,
                        SourceName = Violation.SourceName,
                        ViolationStatusId = Violation.ViolationStatusId,
                        ViolationStatusName = Violation.ViolationStatusName,
                        NotificationId = Violation.NotificationId.HasValue ? Violation.NotificationId.Value : 0,
                        Notification = new NotificationDTO
                        {
                            DateCreated = Violation.Notification.DateCreated,
                            IsNoticed = false,
                            LastModified = Violation.Notification.LastModified,
                            LastModifiedBy = Violation.Notification.ModifiedBy,
                            LastStatus = Violation.Notification.LastStatus,
                            NotificationId = Violation.NotificationId.Value,
                            OwnerId = Violation.Notification.OwnerId
                        }
                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;

        }

        public List<ViolationNotificationDTO> GetUpdatedViolationsList(bool? IsNoticed)
        {
            try
            {
                var lstViolations = operationalDataContext.ViolationNotificationViews
                    .Where(Violation => Violation.IsNoticed == IsNoticed)
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.LocationCode,
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = Violation.Altitude,
                        AssetCode = Violation.AssetCode,
                        AssetId = Violation.AssetId,
                        CapturedSpeed = Violation.CapturedSpeed,
                        DateTaken = Violation.DateTaken,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.MesuredSpeed,
                        PlateColorName = Violation.PlateColorName,
                        PlateKindName = Violation.PlateKindName,
                        PlateNumber = Violation.PlateNumber,
                        PlateSourceName = Violation.PlateSourceName,
                        PlateTypeName = Violation.PlateTypeName,
                        SpeedLimit = Violation.SpeedLimit,
                        VehicleTypeId = Violation.VehicleTypeId,
                        VehicleTypeName = Violation.VehicleTypeName,
                        ViolationNotificationId = Violation.ViolationNotificationId,
                        ViolationTypeName = Violation.ViolationTypeName,
                        IsNoticed = Violation.IsNoticed,
                        BackgroundColor = "Orange",
                        MessageId = Violation.ViolationNotificationId.ToString(),
                        MessageText = "New Violation: " + Violation.ViolationNotificationId,
                        IsCritical = Violation.IsCritical,
                        Direction = Violation.Direction,
                        DirectionName = Violation.DirectionName,
                        LPRId = Violation.LPRId,
                        LaneNo = Violation.LaneNo,
                        SourceId = Violation.SourceId,
                        SourceName = Violation.SourceName,
                        ViolationStatusId = Violation.ViolationStatusId,
                        ViolationStatusName = Violation.ViolationStatusName,
                        NotificationId = Violation.NotificationId.HasValue ? Violation.NotificationId.Value : 0,

                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<ViolationNotificationDTO> GetViolationListByVehicle(string plateNumber)
        {
            try
            {
                var lstViolations = operationalDataContext.ViolationNotificationViews
                    .Where(Violation => Violation.PlateNumber == plateNumber)
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.LocationCode,
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = Violation.Altitude,
                        AssetCode = Violation.AssetCode,
                        AssetId = Violation.AssetId,
                        CapturedSpeed = Violation.CapturedSpeed,
                        DateTaken = Violation.DateTaken,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.MesuredSpeed,
                        PlateColorName = Violation.PlateColorName,
                        PlateKindName = Violation.PlateKindName,
                        PlateNumber = Violation.PlateNumber,
                        PlateSourceName = Violation.PlateSourceName,
                        PlateTypeName = Violation.PlateTypeName,
                        SpeedLimit = Violation.SpeedLimit,
                        VehicleTypeId = Violation.VehicleTypeId,
                        VehicleTypeName = Violation.VehicleTypeName,
                        ViolationNotificationId = Violation.ViolationNotificationId,
                        ViolationTypeName = Violation.ViolationTypeName,
                        IsNoticed = Violation.IsNoticed,
                        BackgroundColor = "Orange",
                        MessageId = Violation.ViolationNotificationId.ToString(),
                        MessageText = "New Violation: " + Violation.ViolationNotificationId,
                        IsCritical = Violation.IsCritical,
                        Direction = Violation.Direction,
                        DirectionName = Violation.DirectionName,
                        LPRId = Violation.LPRId,
                        LaneNo = Violation.LaneNo,
                        SourceId = Violation.SourceId,
                        SourceName = Violation.SourceName,
                        ViolationStatusId = Violation.ViolationStatusId,
                        ViolationStatusName = Violation.ViolationStatusName
                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<ViolationsGroupedByLocationsDTO> GetViolationsListGroupedByLocation(DateTime? StartDateTime, int? ViolationTypeId)
        {
            try
            {
                var lstViolations = operationalDataContext.fn_GroupViolationsByLocations(StartDateTime, ViolationTypeId)
                    .Where(Violation => Violation.Latitude != null && Violation.Longitude != null)
                    .Select(Violation => new ViolationsGroupedByLocationsDTO
                    {
                        Altitude = Violation.Altitude,
                        Latitude = Violation.Latitude.Value,
                        Longitude = Violation.Longitude.Value,
                        ViolationsCount = Violation.ViolationsCount,
                        LocationCode = Violation.LocationCode
                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<ViolationsGroupedByLocationsDTO> GetHistoricalViolationsListGroupedByLocation(DateTime? StartDateTime, DateTime? EndDateTime, int? ViolationTypeId)
        {
            try
            {
                List<AssetsViewDTO> AssetsList = new AssetsDAL().GetAssetsList(null, null);

                var lstViolations = stcDataContext.fn_GroupHistoricalViolationsByLocations(StartDateTime, EndDateTime, ViolationTypeId)
                    .Select(Violation => new ViolationsGroupedByLocationsDTO
                    {
                        Altitude = 1,
                        Latitude = Violation.Latitude.Value,
                        Longitude = Violation.Longitude.Value,
                        ViolationsCount = Violation.ViolationsCount,
                        LocationCode = Violation.SerialNumber
                    }).ToList();

                foreach (var item in lstViolations)
                {
                    string serialNumber = item.LocationCode;
                    var asset = AssetsList.Where(a => a.SerialNo == serialNumber).FirstOrDefault();

                    if (asset == null)
                    {
                        item.LocationCode = "";
                        continue;
                    }

                    if (item.Latitude == null || item.Latitude == 0)
                        item.Latitude = asset.Latitude.HasValue ? asset.Latitude.Value : item.Latitude;

                    if (item.Longitude == null || item.Longitude == 0)
                        item.Longitude = asset.Longitude.HasValue ? asset.Longitude.Value : item.Longitude;

                    item.LocationCode = asset.LocationCode;
                }

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public AssetViolationCountDTO GetViolationCountsByAssetCode(string assetCode)
        {
            string cubeCon = ConfigurationManager.AppSettings["AnalysisViolationConnectionString"];

            AssetViolationCountDTO dto = new AssetViolationCountDTO();

            var dayviolations = operationalDataContext.ViolationNotifications.Where(x => x.DateTaken.Day == DateTime.Now.Day && x.AssetCode == assetCode);

            if (dayviolations != null)
                dto.DayCount = dayviolations.ToList().Count;

            CubeDAL dal = new CubeDAL(cubeCon);

            dto.WeekCount = dal.GetViolationWeekCountForAsset(assetCode);
            dto.MonthCount = dal.GetViolationMonthCountForAsset(assetCode);
            dto.YearCount = dal.GetViolationYearCountForAsset(assetCode);

            return dto;
        }

        public List<ViolationsDetailsByLocationDTO> GetViolationsDetailsForLocation(string LocationCode, DateTime? StartDateTime, int? ViolationTypeId)
        {
            try
            {
                var lstViolationsDetails = operationalDataContext.fn_GetViolationsDetailsByLocation(StartDateTime, ViolationTypeId, LocationCode)
                    .Select(Violation => new ViolationsDetailsByLocationDTO
                    {
                        Altitude = Violation.Altitude,
                        Latitude = Violation.Latitude.Value,
                        Longitude = Violation.Longitude.Value,
                        AssetCode = Violation.AssetCode,
                        AssetId = Violation.AssetId,
                        ViolationTypeId = Violation.ViolationTypeId,
                        ViolationTypeName = Violation.ViolationTypeName,
                        ViolationsCount = Violation.ViolationsCount,
                        LocationCode = Violation.LocationCode
                    }).ToList();

                return lstViolationsDetails;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public ViolationNotificationDTO GetVehicleLastViolation(string plateNumber, string plateCategory, string plateAuthority, string plateColor)
        {
            try
            {
                var output = operationalDataContext.ViolationNotifications
            .Where(vehicle => vehicle.PlateNumber == plateNumber && vehicle.PlateKindName == plateCategory && vehicle.PlateSourceName == plateAuthority && vehicle.PlateColorName == plateColor)
            .Select(vehicle => new ViolationNotificationDTO
            {
                ViolationNotificationId = vehicle.ViolationNotificationId,
                LPRId = vehicle.LPRId,
                AssetId = vehicle.AssetId,
                AssetCode = vehicle.AssetCode,
                Latitude = vehicle.Latitude,
                Longitude = vehicle.Longitude,
                Altitude = vehicle.Altitude,
                PlateNumber = vehicle.PlateNumber,
                PlateKindName = vehicle.PlateKindName,
                PlateSourceName = vehicle.PlateSourceName,
                PlateColorName = vehicle.PlateColorName,
                PlateTypeName = vehicle.PlateTypeName,
                LocationCode = vehicle.LocationCode,
                ViolationTypeId = vehicle.ViolationTypeId,
                ViolationTypeName = vehicle.ViolationTypeName,
                DateTaken = vehicle.DateTaken,
                SpeedLimit = vehicle.SpeedLimit,
                MesuredSpeed = vehicle.MesuredSpeed,
                CapturedSpeed = vehicle.CapturedSpeed,
                VehicleTypeId = vehicle.VehicleTypeId,
                VehicleTypeName = vehicle.VehicleTypeName,
                Direction = vehicle.Direction,
                DirectionName = vehicle.DirectionName,
                LaneNo = vehicle.LaneNo,
                SourceId = vehicle.SourceId,
                SourceName = vehicle.SourceName,
                ViolationStatusId = vehicle.ViolationStatusId,
                ViolationStatusName = vehicle.ViolationStatusName,
                IsNoticed = vehicle.IsNoticed,
                IsCritical = vehicle.IsCritical
            }).OrderByDescending(v => v.ViolationNotificationId).FirstOrDefault();

                return output;
            }
            catch
            {
                return null;
            }
        }

        public List<ViolationNotificationDTO> GetViolationsListByDate(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            try
            {
                var lstViolations = operationalDataContext.ViolationNotificationViews
                    .Where(
                    Violation =>
                        (StartDateTime.HasValue == false || Violation.DateTaken >= StartDateTime.Value) &&
                        (EndDateTime.HasValue == false || Violation.DateTaken <= EndDateTime.Value)
                        )
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.LocationCode,
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = Violation.Altitude,
                        AssetCode = Violation.AssetCode,
                        AssetId = Violation.AssetId,
                        CapturedSpeed = Violation.CapturedSpeed,
                        DateTaken = Violation.DateTaken,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.MesuredSpeed,
                        PlateColorName = Violation.PlateColorName,
                        PlateKindName = Violation.PlateKindName,
                        PlateNumber = Violation.PlateNumber,
                        PlateSourceName = Violation.PlateSourceName,
                        PlateTypeName = Violation.PlateTypeName,
                        SpeedLimit = Violation.SpeedLimit,
                        VehicleTypeId = Violation.VehicleTypeId,
                        VehicleTypeName = Violation.VehicleTypeName,
                        ViolationNotificationId = Violation.ViolationNotificationId,
                        ViolationTypeName = Violation.ViolationTypeName,
                        IsNoticed = Violation.IsNoticed,
                        BackgroundColor = "Orange",
                        MessageId = Violation.ViolationNotificationId.ToString(),
                        MessageText = "New Violation: " + Violation.ViolationNotificationId,
                        IsCritical = Violation.IsCritical,
                        Direction = Violation.Direction,
                        DirectionName = Violation.DirectionName,
                        LPRId = Violation.LPRId,
                        LaneNo = Violation.LaneNo,
                        SourceId = Violation.SourceId,
                        SourceName = Violation.SourceName,
                        ViolationStatusId = Violation.ViolationStatusId,
                        ViolationStatusName = Violation.ViolationStatusName
                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<ViolationNotificationDTO> GetVehicleViolationsByDate(string plateNumber, string plateColor, string plateAuthority, string plateCategory, DateTime? StartDateTime,
            DateTime? EndDateTime)
        {
            return GetViolationsListByDate(StartDateTime, EndDateTime).Where(vehicle => vehicle.PlateNumber == plateNumber && vehicle.PlateKindName == plateCategory && vehicle.PlateSourceName == plateAuthority && vehicle.PlateColorName == plateColor).ToList();
        }

        public List<ViolationsHistoricalDTO> GetViolationHistoricalViewByDate(DateTime StartDateTime, DateTime? EndDateTime, PeriodCategory ScheduleType)
        {
            var res = new List<ViolationsHistoricalDTO>();

            var allViolations = GetViolationsHistoryListByDate(StartDateTime, EndDateTime, "");
            if (allViolations != null)
            {
                foreach (var item in allViolations)
                {
                    var segId = DTO.Helper.GetSegmentId(StartDateTime, item.DateTaken, ScheduleType);
                    var seg = res.FirstOrDefault(x => x.Id == segId);
                    if (seg == null)
                    {
                        seg = new ViolationsHistoricalDTO
                        {
                            Id = segId,
                            Name = ScheduleType.ToString() + segId,
                            ScheduleType = ScheduleType,
                        };
                        seg.ViolationsByLocations.Add(new ViolationsGroupedByLocationsDTO() { Latitude = item.Latitude.Value, LocationCode = item.LocationCode, Longitude = item.Longitude.Value, ViolationsCount = 1 });
                        res.Add(seg);
                    }
                    else
                    {
                        var violationByLoc = seg.ViolationsByLocations.FirstOrDefault(x => x.LocationCode == item.LocationCode);
                        if (violationByLoc != null)
                            violationByLoc.ViolationsCount++;
                        else
                        {
                            seg.ViolationsByLocations.Add(new ViolationsGroupedByLocationsDTO() { Latitude = item.Latitude.Value, LocationCode = item.LocationCode, Longitude = item.Longitude.Value, ViolationsCount = 1 });
                        }
                    }
                }
            }
            return res;
        }

        public List<ViolationNotificationDTO> GetViolationsHistoryListByDate(DateTime? StartDateTime, DateTime? EndDateTime, string PlateNumber)
        {
            try
            {
                var lstViolations = stcDataContext.ViolationViews
                    .Where(
                    Violation =>
                        Violation.ActivityDate.HasValue &&
                        (StartDateTime.HasValue == false || Violation.ActivityDate >= StartDateTime.Value) &&
                        (EndDateTime.HasValue == false || Violation.ActivityDate <= EndDateTime.Value) &&
                        (PlateNumber == "" || Violation.PlateIdentification == PlateNumber)
                        )
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        ViolationNotificationId = Violation.Activityid,
                        LocationCode = Violation.Locationid.ToString(),
                        ViolationTypeId = Violation.ViolationTypeId,
                        ViolationTypeName = Violation.ViolationType,
                        Altitude = 1,
                        AssetCode = Violation.SerialNumber,
                        AssetId = Violation.LicensePlateCameraId.ToString(),
                        CapturedSpeed = Violation.RecordedSpeedRate.HasValue ? Violation.RecordedSpeedRate.Value : 0,
                        DateTaken = Violation.ActivityDate.Value,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        PlateColorName = Violation.PlateCategory,
                        PlateKindName = Violation.PlateKind,
                        PlateNumber = Violation.PlateIdentification,
                        PlateSourceName = Violation.PlateAuthority,
                        PlateTypeName = Violation.PlateCategory,
                        SpeedLimit = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        MessageId = Violation.Activityid.ToString()

                    }).OrderByDescending(x => x.DateTaken).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public List<ViolationNotificationDTO> GetDangerousVehicleViolationList(string plateNumber, string plateColor, string plateAuthority, string plateCategory, DateTime? StartDateTime,
            DateTime? EndDateTime)
        {
            try
            {
                var lstViolations = stcDataContext.ViolationViews
                    .Where(
                    Violation =>
                        Violation.ActivityDate.HasValue &&
                        (Violation.ActivityDate >= StartDateTime.Value) &&
                        (Violation.ActivityDate <= EndDateTime.Value) &&
                        (Violation.PlateIdentification == plateNumber) &&
                        (Violation.PlateCategory == plateCategory) &&
                        (Violation.PlateAuthority == plateAuthority)
                        )
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        ViolationNotificationId = Violation.Activityid,
                        LocationCode = Violation.Locationid.ToString(),
                        ViolationTypeId = Violation.ViolationTypeId,
                        ViolationTypeName = Violation.ViolationType,
                        Altitude = 1,
                        AssetCode = Violation.SerialNumber,
                        AssetId = Violation.LicensePlateCameraId.ToString(),
                        CapturedSpeed = Violation.RecordedSpeedRate.HasValue ? Violation.RecordedSpeedRate.Value : 0,
                        DateTaken = Violation.ActivityDate.Value,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        PlateColorName = Violation.PlateCategory,
                        PlateKindName = Violation.PlateKind,
                        PlateNumber = Violation.PlateIdentification,
                        PlateSourceName = Violation.PlateAuthority,
                        PlateTypeName = Violation.PlateCategory,
                        SpeedLimit = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        MessageId = Violation.Activityid.ToString()

                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public List<string> GetViolationImageURLsById(long ViolationNotificationId)
        {
            try
            {
                var lstImages = stcDataContext.ViolationImageViews
                    .Where(image => image.ActivityId == ViolationNotificationId && image.ImageCategoryId == (int)STC.Projects.ClassLibrary.DAL.Utilities.ImageCategory.Photo)
                    .Select(url => url.BinaryLocationURI).ToList();

                return lstImages;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);

                return null;
            }
        }

        public List<Byte[]> GetViolationImagesById(long ViolationNotificationId)
        {
            try
            {
                using (Impersonation.LogonUser("AGHQSRV453", @"AUH-POLICE\stcadmin", "P@ssword1", LogonType.Network))
                {
                    List<Byte[]> lst = new List<byte[]>();
                    Byte[] img = null;

                    var lstImages = stcDataContext.ViolationImageViews
                        .Where(image => image.ActivityId == ViolationNotificationId && image.ImageCategoryId == (int)STC.Projects.ClassLibrary.DAL.Utilities.ImageCategory.Photo)
                        .Select(url => url.BinaryLocationURI).ToList();

                    if (lstImages != null)
                    {
                        foreach (var uriString in lstImages)
                        {
                            img = (new WebClient()).DownloadData(new Uri(uriString));

                            if (img != null)
                                lst.Add(img);
                        }

                        return lst;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return null;
        }

        public string GetViolationVideoURLById(long ViolationNotificationId)
        {
            try
            {
                var Video = stcDataContext.ViolationImageViews
                    .Where(image => image.ActivityId == ViolationNotificationId && image.ImageCategoryId == (int)STC.Projects.ClassLibrary.DAL.Utilities.ImageCategory.Video)
                    .Select(url => url.BinaryLocationURI).FirstOrDefault();

                return Video;
            }
            catch
            {
                return null;
            }
        }

        public int GetViolationsCountByAsset(DateTime? StartDateTime, DateTime? EndDateTime, string serialNumber)
        {
            try
            {
                var lstViolations = stcDataContext.ViolationViews
                    .Where(
                    Violation =>
                        Violation.ActivityDate.HasValue &&
                         (serialNumber == "" || Violation.SerialNumber.Contains(serialNumber))
                        )
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.Locationid.ToString(),
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = 1,
                        AssetCode = Violation.SerialNumber,
                        AssetId = Violation.LicensePlateCameraId.ToString(),
                        CapturedSpeed = Violation.RecordedSpeedRate.HasValue ? Violation.RecordedSpeedRate.Value : 0,
                        DateTaken = Violation.ActivityDate.Value,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        PlateColorName = Violation.PlateCategory,
                        PlateKindName = Violation.PlateKind,
                        PlateNumber = Violation.PlateIdentification,
                        PlateSourceName = Violation.PlateAuthority,
                        PlateTypeName = Violation.PlateCategory,
                        SpeedLimit = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0

                    }).ToList();

                return lstViolations.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<ViolationNotificationDTO> GetViolationsHistoryListByAsset(string serialNumber, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            try
            {
                var lstViolations = stcDataContext.ViolationViews
                    .Where(
                    Violation =>
                        Violation.ActivityDate.HasValue &&
                         (serialNumber == "" || Violation.SerialNumber.ToLower() == serialNumber.ToLower())
                         && ((!startDateTime.HasValue) || Violation.ActivityDate.Value >= startDateTime.Value)
                         && ((!endDateTime.HasValue) || Violation.ActivityDate.Value <= endDateTime.Value)
                        )
                    .Select(Violation => new ViolationNotificationDTO
                    {
                        LocationCode = Violation.Locationid.ToString(),
                        ViolationTypeId = Violation.ViolationTypeId,
                        Altitude = 1,
                        AssetCode = Violation.SerialNumber,
                        AssetId = Violation.LicensePlateCameraId.ToString(),
                        CapturedSpeed = Violation.RecordedSpeedRate.HasValue ? Violation.RecordedSpeedRate.Value : 0,
                        DateTaken = Violation.ActivityDate.Value,
                        Latitude = Violation.Latitude,
                        Longitude = Violation.Longitude,
                        MesuredSpeed = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        PlateColorName = Violation.PlateCategory,
                        PlateKindName = Violation.PlateKind,
                        PlateNumber = Violation.PlateIdentification,
                        PlateSourceName = Violation.PlateAuthority,
                        PlateTypeName = Violation.PlateCategory,
                        SpeedLimit = Violation.LegalSpeedLimitForCapture.HasValue ? Violation.LegalSpeedLimitForCapture.Value : 0,
                        ViolationTypeName = Violation.ViolationType
                    }).ToList();

                return lstViolations;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public ViolationDetailsDTO GetViolationDetailsByAsset(string LocationCode, DateTime? StartDateTime, int? ViolationTypeId)
        {
            var res = new ViolationDetailsDTO();

            try
            {
                res.AssetsDetails = new AssetDetailsForViolation();
                res.TotalsByTypes = operationalDataContext.fn_GroupViolationsByLocationAndType(StartDateTime, ViolationTypeId, LocationCode).Select(Violation => new TotalViolationValuesByTypes
                {
                    TotalCountOfViolations = Violation.ViolationCountByType.Value,
                    VioltionTypeId = Violation.ViolationTypeId,
                    VioltionTypeName = Violation.ViolationTypeName
                }).ToList();
                res.TotalsByStatus = operationalDataContext.fn_GroupViolationsByLocationAndStatus(StartDateTime, ViolationTypeId, LocationCode).Select(Violation => new TotalViolationValuesByStatus
                {
                    TotalCountOfViolations = Violation.ViolationsCountByStatus.Value,
                    VioltionStatusId = Violation.ViolationStatusId,
                    VioltionStatusName = Violation.ViolationStatusName
                }).ToList();
                res.AssetsDetails = operationalDataContext.AssetsViews.Where(asset => asset.LocationCode == LocationCode).Select(assetdata => new AssetDetailsForViolation
                {
                    AssetName = assetdata.Name,
                    AssetStatus = assetdata.AssetStatus,
                    LastMainteanceDate = "",
                    VendorName = assetdata.VendorName
                }).FirstOrDefault();
            }
            catch (Exception)
            {

            }
            return res;
        }

        //will be used later
        //public List<ViolationsGroupedByLocationsDTO> GetViolationsByLatLon(List<LonLatPointDTO> lonLatPoints)
        //{
        //    try
        //    {
        //        StringBuilder poly=new StringBuilder();
        //        poly.Append("POLYGON ((");
        //        for (int i = 0; i < lonLatPoints.Count(); i++)
        //        {
        //            poly.Append(lonLatPoints[i].Lon + " " + lonLatPoints[i].Lat);
        //            if (i != lonLatPoints.Count() - 1)
        //                poly.Append(",");
        //        }
        //        poly.Append("))");
        //         var dbGeography = DbGeography.FromText(poly.ToString(),4326);
        //         var lstViolations = operationalDataContext.ViolationNotificationViews.Where(x => x.GeoLocation.Intersects(dbGeography))
        //            .Select(Violation => new ViolationsGroupedByLocationsDTO
        //            {
        //                Altitude = Violation.Altitude,
        //                Latitude = Violation.GeoLocation.Latitude.Value,
        //                Longitude = Violation.GeoLocation.Longitude.Value,
        //                LocationCode = Violation.LocationCode
        //            }).ToList();

        //        return lstViolations;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return null;
        //}
    }
}
