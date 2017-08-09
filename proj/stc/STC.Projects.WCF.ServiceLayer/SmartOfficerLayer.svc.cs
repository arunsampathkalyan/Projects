using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WCF.ServiceLayer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;

namespace STC.Projects.WCF.ServiceLayer
{
    public class SmartOfficerLayer : ISmartOfficerLayer
    {
        public bool UpdateOfficerLocation(string officerCode, double lat, double lon)
        {
            return new SmartOfficerDAL().UpdateOfficerLocation(officerCode, lat, lon);
        }

        public bool AddNewOfficer(string militaryId, string officerPatrolCode, string officerName, string image)
        {
            return new SmartOfficerDAL().AddNewOfficer(militaryId, officerPatrolCode, officerName, image);
        }

        public List<string> GetAvailablePatrolPlateNumbers()
        {
            return new SmartOfficerDAL().GetAvailablePatrolPlateNumbers();
        }

        public SmartOfficerDTO GetOfficer(string militaryId)
        {
            return new SmartOfficerDAL().GetOfficer(militaryId);
        }

        public bool AddTask(OfficerTaskDTO task)
        {
            return new SmartOfficerDAL().AddTask(task);
        }

        public bool ChangeTaskStatus(OfficerTaskStatusDTO taskStatus)
        {
            return new SmartOfficerDAL().ChangeTaskStatus(taskStatus);
        }

        public List<TaskStatusDTO> GetAvailableTaskStatuses()
        {
            return new SmartOfficerDAL().GetAvailableTaskStatuses();
        }

        public List<OfficerTaskDTO> GetOfficerPendingTasks(string officerMilitaryId)
        {
            return new SmartOfficerDAL().GetOfficerPendingTasks(officerMilitaryId);
        }

        public bool AddDangerousViolator(int senderUserId, int businessRuleId, double lat, double lon, string plateNumber, string plateColor, string plateSource, string plateKind, string ImageB64String, MediaTypes MediafileType, string MediaFileExtension)
        {
            SupervisorNotificationDTO violatorDetails = new SupervisorNotificationDTO();

            violatorDetails.IsNoticed = false;
            violatorDetails.NotificationTime = DateTime.Now;
            violatorDetails.ReceiverId = new ServiceLayer().GetSupervisorId();
            violatorDetails.SenderId = senderUserId;
            violatorDetails.Status = SupervisorNotificationStatus.Pending;
            violatorDetails.DangerousViolatorDetails = new SupervisorNotificationReportDangerousDTO
            {
                PlateNumber = plateNumber,
                PlateColor = plateColor,
                PlateAuthority = plateSource,
                PlateKind = plateKind,
                BusinessRuleId = businessRuleId,
                Lat = lat,
                Lon = lon,
                NotificationText = "Dangerous violator report from the smart officer",
                MediaURL = ImageB64String,
                MediaType = MediafileType,
                MediaFileFormat = MediaFileExtension
            };

            return new SupervisorNotificationDAL().SaveSupervisorNotification(violatorDetails);
        }

        public DangerousVehicleDetailsDTO SearchforDangerousViolator(string plateNumber, string plateCategory, string plateSource, string plateColor)
        {
            try
            {
                CorrelationMessagesLogDAL correlationDal = new CorrelationMessagesLogDAL();

                List<CorrelationMessagesLogDTO> vehicleCorrelations = correlationDal.GetCorrelationLogByVehicleDetails(plateNumber, plateColor, plateSource, plateCategory);

                if (vehicleCorrelations == null || vehicleCorrelations.Count == 0)
                    return null;

                vehicleCorrelations = vehicleCorrelations.OrderByDescending(v => v.CorrelationDate).ToList();

                DangerousVehicleDetailsDTO dto = new DangerousVehicleDetailsDTO();

                dto.PlateNumber = plateNumber;
                dto.PlateColor = plateColor;
                dto.PlateKind = plateCategory;
                dto.PlateSource = plateSource;

                dto.VehicleViolations = new ViolationNotificationDAL().GetViolationsHistoryListByDate(DateTime.Now.AddYears(-1), DateTime.Now, plateNumber);

                return dto;
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return null;
            }
        }

        public bool AddSmartOfficerNews(SmartOfficerNewsDTO news)
        {
            try
            {
                return new SmartOfficerNewsDAL().AddSmartOfficerNews(news);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return false;
            }
        }

        public List<SmartOfficerNewsDTO> GetSmartOfficerOldNews(DateTime startDate)
        {
            return new SmartOfficerNewsDAL().GetSmartOfficerOldNews(startDate);
        }

        public List<BusinessRulesDTO> GetAllBusinessRules()
        {
            return new ServiceLayer().GetAllBusinessRules(true);
        }

        public List<OfficerTaskDTO> GetOfficerHistoricalTasksFromDate(string officerMilitaryId, DateTime fromDate)
        {
            return new SmartOfficerDAL().GetOfficerHistoricalTasksFromDate(officerMilitaryId, fromDate);
        }
    }
}
