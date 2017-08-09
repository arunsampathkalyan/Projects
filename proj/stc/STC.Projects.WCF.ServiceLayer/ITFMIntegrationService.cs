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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITFMIntegrationService" in both code and config file together.
    [ServiceContract]
    public interface ITFMIntegrationService
    {
        [OperationContract]
        [WebGet]
        long AddDuty(Guid PatrolOrgId, string MessageContent, DateTime DutyDateTime, double Latitude, double Longitude, long NotificationId, int PatrolId);

        [OperationContract]
        [WebGet]
        bool AddIncident(Guid PatrolOrgId, DateTime IncidentDateTime, double Latitude, double Longitude, string MessageContent);

        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetPatrolsLocations();

        [OperationContract]
        [WebGet]
        bool UpdatePatrolLocations();

        [OperationContract]
        [WebGet]
        PatrolLastLocationDTO GetPatrolDetails(int PatrolId);
        [OperationContract]
        [WebGet]
        bool UpdatePatrolCurrentTask(Guid patrolOriginalId, long taskId);
        [OperationContract]
        [WebGet]
        List<string> GetTaskImagesURLs(long taskId);
        [OperationContract]
        [WebGet]
        List<string> GetTaskVideosURLs(long taskId);
        [OperationContract]
        [WebGet]
        List<string> GetTaskVideosURLsTest(long taskId);
        [OperationContract]
        [WebGet]
        PatrolOfficersDetailsDTO GetPatrolDetailsFromTFM(Guid patrolId);
        [OperationContract]
        [WebGet]
        bool ValidateBeforeAssignPatrol(long notificationId, long patrolId);
    }
}
