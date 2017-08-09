using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using STC.Projects.ClassLibrary.SmartTowerIntegration.DTO;
using System.ServiceModel.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmartTowerIntegrationService" in both code and config file together.
    [ServiceContract]
    public interface ISmartTowerIntegrationService
    {
        [OperationContract]
        [WebGet]
        List<TowerPredefinedMessageDTO> GetAllTowerStaticMessages(string TowerId);

        [OperationContract]
        [WebGet]
        bool UpdateTowerCurrentMessage(TowerMessageDTO TowerMessage);

        [OperationContract]
        [WebGet]
        TowerMessageDTO GetTowerCurrentMessage(string TowerId);

        [OperationContract]
        [WebGet]
        SensorReadingDTO GetTowerSensorCurrentReading(string SensorId);
    }
}
