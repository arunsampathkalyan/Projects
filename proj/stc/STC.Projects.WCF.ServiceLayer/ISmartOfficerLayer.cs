using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    [ServiceContract]
    public interface ISmartOfficerLayer
    {
        [OperationContract]
        [WebGet]
        bool UpdateOfficerLocation(string officerCode, double lat, double lon);
        [OperationContract]
        [WebGet]
        bool AddNewOfficer(string militaryId, string officerPatrolCode, string officerName, string image);
        [OperationContract]
        [WebGet]
        List<string> GetAvailablePatrolPlateNumbers();
        [OperationContract]
        [WebGet]
        SmartOfficerDTO GetOfficer(string militaryId);
        [OperationContract]
        [WebGet]
        bool AddTask(OfficerTaskDTO task);
        [OperationContract]
        [WebGet]
        bool ChangeTaskStatus(OfficerTaskStatusDTO taskStatus);
        [OperationContract]
        [WebGet]
        List<TaskStatusDTO> GetAvailableTaskStatuses();
        [OperationContract]
        [WebGet]
        List<OfficerTaskDTO> GetOfficerPendingTasks(string officerMilitaryId);
        [OperationContract]
        [WebGet]
        bool AddDangerousViolator(int senderUserId, int businessRuleId, double lat, double lon, string plateNumber, string plateColor, string plateSource, string plateKind, string ImageB64String, MediaTypes MediafileType, string MediaFileExtension);
        [OperationContract]
        [WebGet]
        DangerousVehicleDetailsDTO SearchforDangerousViolator(string plateNumber, string plateCategory, string plateSource, string plateColor);
        [OperationContract]
        [WebGet]
        bool AddSmartOfficerNews(SmartOfficerNewsDTO news);
        [OperationContract]
        [WebGet]
        List<SmartOfficerNewsDTO> GetSmartOfficerOldNews(DateTime startDate);
        [OperationContract]
        [WebGet]
        List<BusinessRulesDTO> GetAllBusinessRules();
        [OperationContract]
        [WebGet]
        List<OfficerTaskDTO> GetOfficerHistoricalTasksFromDate(string officerMilitaryId, DateTime fromDate);
    }
}
