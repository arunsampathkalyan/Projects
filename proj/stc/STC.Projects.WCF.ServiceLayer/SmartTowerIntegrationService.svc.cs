using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.SmartTowerIntegration.DAL;
using STC.Projects.ClassLibrary.SmartTowerIntegration.DTO;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartTowerIntegrationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartTowerIntegrationService.svc or SmartTowerIntegrationService.svc.cs at the Solution Explorer and start debugging.
    public class SmartTowerIntegrationService : ISmartTowerIntegrationService
    {
        public List<TowerPredefinedMessageDTO> GetAllTowerStaticMessages(string TowerId)
        {
            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.GetAllTowerStaticMessages(TowerId);
        }

        public bool UpdateTowerCurrentMessage(TowerMessageDTO TowerMessage)
        {
            List<string> pUsers = new UsersDAL().GetActivePublicUsers().Select(x => x.NotificationToken).ToList();

            if (pUsers != null)
            {
                new ServiceLayer().SendNotification(pUsers, TowerMessage.ArabicMessage);
            }

            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.UpdateTowerCurrentMessage(TowerMessage);
        }

        public TowerMessageDTO GetTowerCurrentMessage(string TowerId)
        {
            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.GetTowerCurrentMessage(TowerId);
        }

        public SensorReadingDTO GetTowerSensorCurrentReading(string SensorId)
        {
            SmartTowerDAL smartTowerDAL = new SmartTowerDAL();
            return smartTowerDAL.GetTowerSensorCurrentReading(SensorId);
        }
    }
}
