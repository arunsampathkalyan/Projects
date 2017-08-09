using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class NotificationDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public List<NotificationDTO> GetChangedNotifications()
        {
            return operationalDataContext.Notifications.Where(x => x.IsNoticed.HasValue && x.IsNoticed.Value == false).Select(x => new NotificationDTO
                {
                    DateCreated = x.DateCreated,
                    IsNoticed = false,
                    LastModified = x.LastModified,
                    LastModifiedBy = x.ModifiedBy,
                    LastStatus = x.LastStatus,
                    NotificationId = x.NotificationId,
                    OwnerId = x.OwnerId
                }).ToList();
        }

       public bool ChangeNotificationStatus(long NotificationId, int NewStatus, int UserId)
        {
            var notification = operationalDataContext.Notifications.FirstOrDefault(x => x.NotificationId == NotificationId);
            if (notification == null)
                return false;
           var oldStatus = notification.LastStatus;
            notification.LastStatus = NewStatus;
            notification.ModifiedBy = UserId;
            notification.LastModified = DateTime.Now;
            notification.OwnerId = UserId;
            notification.IsNoticed = false;
            operationalDataContext.NotificationLogs.Add(new NotificationLog
                {
                    ChangeDate = DateTime.Now,
                    NewStatus = NewStatus,
                     OldStatus = oldStatus,
                     UserId = UserId,
                     NotificationId = notification.NotificationId
                });
            return operationalDataContext.SaveChanges() > 0;
        }
        public List<ViolationNotificationDTO> GetLatestViolationNotification(int OldPeriodInHours)
       {
           var res = new List<ViolationNotificationDTO>();
           res = operationalDataContext.fn_GetRecentViolationNotification(OldPeriodInHours).Where(x=> x.NotificationId.HasValue)
               .Select(x=> new ViolationNotificationDTO
               {
                   BackgroundColor = "Orange",
                   MessageId = x.ViolationNotificationId.ToString(),
                   MessageText = "New Violation: " + x.ViolationNotificationId,
                   Altitude = x.Altitude,
                   AssetCode = x.AssetCode,
                   AssetId = x.AssetId,
                   CapturedSpeed = x.CapturedSpeed,
                   DateTaken = x.DateTaken,
                   Direction = x.Direction,
                   DirectionName = x.DirectionName,
                   IsCritical = x.IsCritical,
                   IsNoticed = x.IsNoticed,
                   LaneNo = x.LaneNo,
                   Latitude = x.Latitude,
                   LocationCode = x.LocationCode,
                   Longitude = x.Longitude,
                   LPRId = x.LPRId,
                   MesuredSpeed = x.MesuredSpeed,
                   PlateColorName = x.PlateColorName,
                   NotificationId = x.NotificationId.Value,
                   PlateKindName = x.PlateKindName,
                   PlateNumber = x.PlateNumber,
                   PlateSourceName = x.PlateSourceName,
                   PlateTypeName = x.PlateTypeName,
                   SourceId = x.SourceId,
                   SourceName = x.SourceName,
                   SpeedLimit = x.SpeedLimit,
                   VehicleTypeId = x.VehicleTypeId,
                   VehicleTypeName = x.VehicleTypeName,
                   ViolationNotificationId = x.ViolationNotificationId,
                   ViolationStatusId = x.ViolationStatusId,
                   ViolationStatusName = x.ViolationStatusName,
                   ViolationTypeId = x.ViolationTypeId,
                   ViolationTypeName = x.ViolationTypeName,
                   Notification = new NotificationDTO
                   {
                       DateCreated = x.DateCreated,
                       IsNoticed = x.NotificationIsNoticed.HasValue ? x.NotificationIsNoticed.Value : false,
                       LastModified = x.LastModified,
                       LastModifiedBy = x.ModifiedBy,
                       LastStatus = x.LastStatus,
                       NotificationId = x.NotificationId.Value,
                       OwnerId = x.OwnerId
                   }
               }).ToList();
           return res;
       }

        public List<IncidentsDTO> GetLatestIncidents(int OldPeriodInHours)
        {
            var res = new List<IncidentsDTO>();
            var now = DateTime.Now;
            res = operationalDataContext.fn_GetRecentIncidentsNotification(OldPeriodInHours).Where(x => x.NotificationId.HasValue)
                .Select(x => new IncidentsDTO
                {
                    ArrivedTime = x.ArrivedTime,
                    CallerAddress = x.CallerAddress,
                    CallerLanguage = x.CallerLanguage,
                    CallerName = x.CallerName,
                    CallerNumber = x.CallerNumber,
                    CallTakerId = x.CallTakerId,
                    CallTakerName = x.CallTakerName,
                    CreatedTime = x.CreatedTime,
                    DispatcherId = x.DispatcherId,
                    DispatcherName = x.DispatcherName,
                    DispatcheTime = x.DispatcheTime,
                    EndTime = x.EndTime,
                    IncidentAddress = x.IncidentAddress,
                    IncidentId = x.IncidentId,
                    IncidentNumber = x.IncidentNumber,
                    IncidentTypeId = x.IncidentTypeId,
                    IsCritical = true,
                    IsNoticed = x.IsNoticed,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    NotificationId = x.NotificationId.Value,
                    StatusId = x.StatusId,
                    MessageId = x.IncidentId.ToString(),
                    MessageText = "New Incident: " + x.IncidentId.ToString(),
                    BackgroundColor = "Red",
                    Notification = new NotificationDTO
                    {
                        DateCreated = x.DateCreated,
                        IsNoticed = x.NotificationIsNoticed.HasValue ? x.NotificationIsNoticed.Value : false,
                        LastModified = x.LastModified,
                        LastModifiedBy = x.ModifiedBy,
                        LastStatus = x.LastStatus,
                        NotificationId = x.NotificationId.Value,
                        OwnerId = x.OwnerId
                    }
                }).ToList();
            return res;
        }

        public List<UserUserControlDTO> GetLatestEvents(int OldPeriod, int UserId)
        {
            var res = new List<UserUserControlDTO>();
            res = operationalDataContext.fn_GetRecentEventsNotification(OldPeriod,UserId).Where(x=> x.NotificationId.HasValue)
                .Select(x => new UserUserControlDTO
                {
                    IsNoticed = x.IsNoticed,
                    NotificationId = x.NotificationId.Value,
                    UserUserControlsID = x.UserUserControlsID,
                    XML = x.XML,
                   
                    Notification = new NotificationDTO
                    {
                        DateCreated = x.DateCreated,
                        IsNoticed = x.NotificationIsNoticed.HasValue ? x.NotificationIsNoticed.Value : false,
                        LastModified = x.LastModified,
                        LastModifiedBy = x.ModifiedBy,
                        LastStatus = x.LastStatus,
                        NotificationId = x.NotificationId.Value,
                        OwnerId = x.OwnerId
                    }
                }).ToList();
            if (res != null && res.Any())
            {
                res.ForEach(x => x.PopupContent = STC.Projects.ClassLibrary.DTO.Helper.DesrializeXml(x.XML));
            }
            return res;
        }

        public bool SaveSOPNotificationLog(int sopStepId,long notificationId,int? sopCommandId,int userId,string previousValue,string currentValue)
        {
            var commandAr = "";
            var commandEn = "";
            if (sopCommandId.HasValue)
            {
                var command = operationalDataContext.SOPCommands.FirstOrDefault(x => x.SOPCommandId == sopCommandId);
                if (command == null)
                    return false;
                commandAr = command.ArabicDescription;
                commandEn = command.EnglishDescription;
                if (previousValue != "" && currentValue != "")
                {
                    commandAr += string.Format(" {0} {1} {2} {3}", "من", previousValue, "إلى", currentValue);
                    commandEn += string.Format(" {0} {1} {2} {3}", "From", previousValue, "To", currentValue);
                }
            }
            var log = new NotificationSOPLog
            {
                ArabicLogText = commandAr,
                CurrentValue = currentValue,
                DateChanged = DateTime.Now,
                EnglishLogText = commandEn,
                IsNoticed = false,
                ModifiedBy = userId,
                NotificationId = notificationId,
                PreviousValue = previousValue,
                SOPCommandId = sopCommandId,
                SOPStepId = sopStepId
            };
            operationalDataContext.NotificationSOPLogs.Add(log);
            return operationalDataContext.SaveChanges() > 0;
        }
    }
}
