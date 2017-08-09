using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SupervisorNotificationDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public bool SaveSupervisorNotification(SupervisorNotificationDTO supervisorNotification)
        {
            var notification = operationalDataContext.SupervisorNotifications.FirstOrDefault(x => x.SupervisorNotificationId == supervisorNotification.SupervisorNoticationId);

            if (notification == null)
            {
                notification = new SupervisorNotification();

                notification.NotificationTime = supervisorNotification.NotificationTime;
                notification.SenderId = supervisorNotification.SenderId;
                notification.ReceiverId = supervisorNotification.ReceiverId;
                notification.IsNoticed = false;
                if (supervisorNotification.ResponseToId == 0)
                    notification.ResponseToId = null;
                else
                {
                    notification.ResponseToId = supervisorNotification.ResponseToId;

                    if (notification.ResponseToId != null)
                        SetSupervisorNotificationNoticed(notification.ResponseToId.Value);
                }
                notification.StatusId = (int)supervisorNotification.Status;

                operationalDataContext.SupervisorNotifications.Add(notification);

                notification.ReportDangerous.Add(new ReportDangerou
                {
                    NotificationText = supervisorNotification.DangerousViolatorDetails.NotificationText,
                    PlateNumber = supervisorNotification.DangerousViolatorDetails.PlateNumber,
                    PlateAuthority = supervisorNotification.DangerousViolatorDetails.PlateAuthority,
                    PlateColor = supervisorNotification.DangerousViolatorDetails.PlateColor,
                    PlateKind = supervisorNotification.DangerousViolatorDetails.PlateKind,
                    BusinessRuleId = supervisorNotification.DangerousViolatorDetails.BusinessRuleId,
                    Lat = supervisorNotification.DangerousViolatorDetails.Lat,
                    Lon = supervisorNotification.DangerousViolatorDetails.Lon,
                    MediaURL = supervisorNotification.DangerousViolatorDetails.MediaURL,
                    MediaType = (int)supervisorNotification.DangerousViolatorDetails.MediaType,
                    SupervisorNotificationId = notification.SupervisorNotificationId,

                });
            }
            else
            {
                //already exists
            }



            return operationalDataContext.SaveChanges() > 0;
        }

        public SupervisorNotificationDTO GetSupervisorNotificationById(int supervisorNotificationId)
        {
            SupervisorNotificationDTO dto = null;

            var y = operationalDataContext.SupervisorNotifications.Where(x => x.SupervisorNotificationId == supervisorNotificationId).FirstOrDefault();

            if (y == null)
                return null;

            dto = new SupervisorNotificationDTO();

            dto.SupervisorNoticationId = y.SupervisorNotificationId;
            dto.NotificationTime = y.NotificationTime.Value;
            dto.SenderId = y.SenderId.Value;
            dto.ReceiverId = y.ReceiverId.Value;
            dto.IsNoticed = y.IsNoticed.Value;
            dto.ResponseToId = y.ResponseToId == null ? 0 : y.ResponseToId.Value;
            dto.Status = (SupervisorNotificationStatus)y.StatusId;

            if (y.ReportDangerous != null && y.ReportDangerous.Count > 0)
            {
                dto.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO();

                dto.DangerousViolatorDetails.NotificationText = y.ReportDangerous.First().NotificationText;
                dto.DangerousViolatorDetails.ReportDangerousId = y.ReportDangerous.First().ReportDangerousId;
                dto.DangerousViolatorDetails.SupervisorNotificationId = y.SupervisorNotificationId;
                dto.DangerousViolatorDetails.PlateNumber = y.ReportDangerous.First().PlateNumber;
                dto.DangerousViolatorDetails.PlateAuthority = y.ReportDangerous.First().PlateAuthority;
                dto.DangerousViolatorDetails.PlateColor = y.ReportDangerous.First().PlateColor;
                dto.DangerousViolatorDetails.PlateKind = y.ReportDangerous.First().PlateKind;
                dto.DangerousViolatorDetails.BusinessRuleId = y.ReportDangerous.First().BusinessRuleId;
                dto.DangerousViolatorDetails.Lat = y.ReportDangerous.First().Lat;
                dto.DangerousViolatorDetails.Lon = y.ReportDangerous.First().Lon;
                dto.DangerousViolatorDetails.MediaURL = y.ReportDangerous.First().MediaURL;
                dto.DangerousViolatorDetails.MediaType = (MediaTypes)y.ReportDangerous.First().MediaType;
            }

            return dto;
        }

        public List<SupervisorNotificationDTO> GetSupervisorNotificationsByUserId(int userId)
        {
            List<SupervisorNotificationDTO> lst = new List<SupervisorNotificationDTO>();
            SupervisorNotificationDTO dto = null;

            var notifications = operationalDataContext.SupervisorNotifications.Where(x => (x.IsNoticed.HasValue && x.IsNoticed.Value == false) && x.ReceiverId == userId).ToList();

            foreach (var y in notifications)
            {
                dto = new SupervisorNotificationDTO();

                dto.SupervisorNoticationId = y.SupervisorNotificationId;
                dto.NotificationTime = y.NotificationTime.Value;
                dto.SenderId = y.SenderId.Value;
                dto.ReceiverId = y.ReceiverId.Value;
                dto.IsNoticed = y.IsNoticed.Value;
                dto.ResponseToId = y.ResponseToId == null ? 0 : y.ResponseToId.Value;
                dto.Status = (SupervisorNotificationStatus)y.StatusId;

                if (y.ReportDangerous != null && y.ReportDangerous.Count > 0)
                {
                    dto.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO();

                    dto.DangerousViolatorDetails.NotificationText = y.ReportDangerous.First().NotificationText;
                    dto.DangerousViolatorDetails.ReportDangerousId = y.ReportDangerous.First().ReportDangerousId;
                    dto.DangerousViolatorDetails.SupervisorNotificationId = y.SupervisorNotificationId;
                    dto.DangerousViolatorDetails.PlateNumber = y.ReportDangerous.First().PlateNumber;
                    dto.DangerousViolatorDetails.PlateAuthority = y.ReportDangerous.First().PlateAuthority;
                    dto.DangerousViolatorDetails.PlateColor = y.ReportDangerous.First().PlateColor;
                    dto.DangerousViolatorDetails.PlateKind = y.ReportDangerous.First().PlateKind;
                    dto.DangerousViolatorDetails.BusinessRuleId = y.ReportDangerous.First().BusinessRuleId;
                    dto.DangerousViolatorDetails.Lat = y.ReportDangerous.First().Lat;
                    dto.DangerousViolatorDetails.Lon = y.ReportDangerous.First().Lon;
                    dto.DangerousViolatorDetails.MediaURL = y.ReportDangerous.First().MediaURL;
                    dto.DangerousViolatorDetails.BusinessRuleName = y.ReportDangerous.First().NewCorrelationRule != null ? y.ReportDangerous.First().NewCorrelationRule.RuleName : "";
                    dto.DangerousViolatorDetails.MediaType = (MediaTypes)(y.ReportDangerous.First().MediaType ?? 0);
                }

                lst.Add(dto);
            }

            return lst;
        }

        public List<SupervisorNotificationDTO> GetChangedSupervisorNotifications()
        {

            List<SupervisorNotificationDTO> lst = new List<SupervisorNotificationDTO>();
            SupervisorNotificationDTO dto = null;

            var notifications = operationalDataContext.SupervisorNotifications.Where(x => x.IsNoticed.HasValue && x.IsNoticed.Value == false).ToList();

            foreach (var y in notifications)
            {
                dto = new SupervisorNotificationDTO();

                dto.SupervisorNoticationId = y.SupervisorNotificationId;
                dto.NotificationTime = y.NotificationTime.Value;
                dto.SenderId = y.SenderId.Value;
                dto.ReceiverId = y.ReceiverId.Value;
                dto.IsNoticed = y.IsNoticed.Value;
                dto.ResponseToId = y.ResponseToId == null ? 0 : y.ResponseToId.Value;
                dto.Status = (SupervisorNotificationStatus)y.StatusId;

                if (y.ReportDangerous != null && y.ReportDangerous.Count > 0)
                {
                    dto.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO();

                    dto.DangerousViolatorDetails.NotificationText = y.ReportDangerous.First().NotificationText;
                    dto.DangerousViolatorDetails.ReportDangerousId = y.ReportDangerous.First().ReportDangerousId;
                    dto.DangerousViolatorDetails.SupervisorNotificationId = y.SupervisorNotificationId;
                    dto.DangerousViolatorDetails.PlateNumber = y.ReportDangerous.First().PlateNumber;
                    dto.DangerousViolatorDetails.PlateAuthority = y.ReportDangerous.First().PlateAuthority;
                    dto.DangerousViolatorDetails.PlateColor = y.ReportDangerous.First().PlateColor;
                    dto.DangerousViolatorDetails.PlateKind = y.ReportDangerous.First().PlateKind;
                    dto.DangerousViolatorDetails.BusinessRuleId = y.ReportDangerous.First().BusinessRuleId;
                    dto.DangerousViolatorDetails.Lat = y.ReportDangerous.First().Lat;
                    dto.DangerousViolatorDetails.Lon = y.ReportDangerous.First().Lon;
                    dto.DangerousViolatorDetails.MediaURL = y.ReportDangerous.First().MediaURL;
                    dto.DangerousViolatorDetails.MediaType = (MediaTypes)(y.ReportDangerous.First().MediaType ?? 0);
                }

                lst.Add(dto);
            }

            return lst;
        }

        public bool SetSupervisorNotificationNoticed(long notificationId, bool isNoticed = true)
        {
            var y = operationalDataContext.SupervisorNotifications.Where(x => x.SupervisorNotificationId == notificationId).FirstOrDefault();

            if (y == null)
                return false;

            y.IsNoticed = isNoticed;

            return operationalDataContext.SaveChanges() > 0;
        }
    }
}
