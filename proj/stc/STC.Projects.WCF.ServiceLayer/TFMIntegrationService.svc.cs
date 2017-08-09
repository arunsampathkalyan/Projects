using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using STC.Projects.ClassLibrary.TFMIntegration;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL;
using System.Drawing;
using System.IO;
using System.ServiceModel.Activation;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TFMIntegrationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TFMIntegrationService.svc or TFMIntegrationService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
     public class TFMIntegrationService : ITFMIntegrationService
    {
        public long AddDuty(Guid PatrolOrgId, string MessageContent, DateTime DutyDateTime, double Latitude, double Longitude, long NotificationId, int PatrolId)
        {
            var tfmService = new TFMIntegration();

            var res = tfmService.AddDuty(PatrolOrgId, MessageContent, DutyDateTime, Latitude, Longitude);
            if (res > 0 && NotificationId > 0 && PatrolId > 0)
            {
                try
                {
                    new PatrolsDAL().RecordDispatchLog(PatrolId, NotificationId, DutyDateTime);
                }
                catch (Exception ex)
                {

                }
                //new CrsipServices().PositionsCars();
            }
            return res;
        }


        public bool AddIncident(Guid PatrolOrgId, DateTime IncidentDateTime, double Latitude, double Longitude, string MessageContent)
        {
            var tfmService = new TFMIntegration();

            var res = tfmService.AddIncident(PatrolOrgId, IncidentDateTime, Latitude, Longitude, MessageContent);

            return res;
        }

        public List<PatrolLastLocationDTO> GetPatrolsLocations()
        {
            var result = new List<PatrolLastLocationDTO>();
            var tfmService = new TFMIntegration();
            var allLocations = tfmService.GetAllPatrolLocation();
            if (allLocations != null)
            {
                var allStatus = new PatrolStatusDAL().GetPatrolStatusList();
                for (int i = 0; i < allLocations.Count(); i++)
                {
                    var patrolCode = allLocations[i].Name;
                    var patrolPlate = allLocations[i].Name;
                    var patrolArr = allLocations[i].Name.Split('/');
                    if (patrolArr.Count() == 2)
                    {
                        patrolCode = patrolArr[0];
                        patrolPlate = patrolArr[1];
                    }

                    var patrolLastLocation = new PatrolLastLocationDTO
                    {
                        Latitude = allLocations[i].Latitude,
                        Longitude = allLocations[i].Longitude,
                        PatrolCode = patrolCode,
                        Speed = allLocations[i].Speed,
                        PatrolPlateNo = patrolPlate,
                        LocationDate = allLocations[i].LocationDate.AddHours(4),
                        PatrolOriginalId = allLocations[i].PatrolId,
                        isPatrol = true,
                        StatusId = allLocations[i].StatusId,
                    };
                    if (allStatus != null && allStatus.Any(x => x.PatrolStatusId == patrolLastLocation.StatusId))
                        patrolLastLocation.StatusName = allStatus.FirstOrDefault(x => x.PatrolStatusId == patrolLastLocation.StatusId).PatrolStatus;
                    result.Add(patrolLastLocation);
                }
            }
            return result;
        }

        public bool UpdatePatrolLocations()
        {
            var allPatrolsLocations = GetPatrolsLocations();
            if (allPatrolsLocations != null && allPatrolsLocations.Any())
            {
                var res = new PatrolsDAL().UpdatePatrolLocation(allPatrolsLocations.ToList());
                new CrsipServices().PrepareCars();
                return res;
            }
            return false;
        }

        public PatrolLastLocationDTO GetPatrolDetails(int PatrolId)
        {
            return new PatrolsDAL().GetPatrolDetails(PatrolId);
        }

        public bool UpdatePatrolCurrentTask(Guid patrolOriginalId, long taskId)
        {
            return new TFMDAL().UpdatePatrolCurrentTask(patrolOriginalId, taskId);
        }

        public List<string> GetTaskImagesURLs(long taskId)
        {
            STCTFMService.TFMServiceClient client = new STCTFMService.TFMServiceClient();
            var output = client.GetTaskImagesURLs(taskId);

            if (output != null)
            {
                return output.ToList();
            }

            return null;
        }

        public List<string> GetTaskVideosURLs(long taskId)
        {
            STCTFMService.TFMServiceClient client = new STCTFMService.TFMServiceClient();

            var output = client.GetTaskVideosURLs(taskId);

            if (output != null)
                return output.ToList();

            return null;
        }

        public List<string> GetTaskVideosURLsTest(long taskId)
        {
            var res = new List<string>();
            STCTFMService.TFMServiceClient client = new STCTFMService.TFMServiceClient();
            var output = client.GetTaskVideosURLs(taskId);

            if (output != null)
            {
                var index = 0;
                var folderPath = string.Format(@"{0}\Videos\{1}", Utility.GetExecutionPath(), taskId);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                foreach (var str in output)
                {
                    index++;
                    //var videoStream = new MemoryStream(Convert.FromBase64String(str));
                    var videoBytes = Convert.FromBase64String(str);

                    var path = string.Format(@"{0}\{1}.wmv", folderPath, index);
                    
                    if(!File.Exists(path))
                        File.WriteAllBytes(path, videoBytes);
                  
                    //var vPath = OperationContext.Current.IncomingMessageHeaders.To.ToString().Replace("TFMIntegrationService.svc","")  + string.Format("Videos/{0}/{1}.wmv", taskId, index);
                    var vPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace("TFMIntegrationService.svc", "") + string.Format("Videos/{0}/{1}.wmv", taskId, index);
                    
                    res.Add(vPath);
                }
                return res;
            }

            return null;
        }

        public PatrolOfficersDetailsDTO GetPatrolDetailsFromTFM(Guid patrolId)
        {
            STCTFMService.TFMServiceClient client = new STCTFMService.TFMServiceClient();

            return client.GetPatrolDetails(patrolId);
        }

        public bool ValidateBeforeAssignPatrol(long notificationId, long patrolId)
        {
            return new TFMDAL().ValidateBeforeAssignPatrol(notificationId, patrolId);
        }
    }
}
