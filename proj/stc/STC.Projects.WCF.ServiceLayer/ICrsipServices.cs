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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICrsipServices" in both code and config file together.
    [ServiceContract]
    public interface ICrsipServices
    {
        [OperationContract]
        [WebGet]
        bool AddSession(string sessionName);
        [OperationContract]
        [WebGet]
        bool RemoveSession(string sessionName);
        [OperationContract]
        [WebGet]
        bool ParseNode(string sessionName, string nodesXml);
        [OperationContract]
        [WebGet]
        bool ParseCars(string sessionName, string carsXml);
        [OperationContract]
        [WebGet]
        PatrolLastLocationDTO RecommendCar(double lat, double lon);
        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> PositionsCars();
        [OperationContract]
        [WebGet]
        void HandleSessions();
        [OperationContract]
        [WebGet]
        void HandleCars(List<PatrolLastLocationDTO> availableCars);
        [OperationContract]
        [WebGet]
        void PrepareCars();
        [OperationContract]
        [WebGet]
        void SetMaxAllowedTime(int maxAllowedTimeInMins);
        [OperationContract]
        [WebGet]
        bool AddPatrol(string patrolCode, string patrolPlateNumber);
        [OperationContract]
        [WebGet]
        bool RemovePatrol(int patrolId);
        [OperationContract]
        [WebGet]
        bool TestCarParse(string sessionName);
        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetPatrolsETA(double latitude, double longitude);
        [OperationContract]
        [WebGet]
        int getMaxAllowedTime();
        [OperationContract]
        [WebGet]
        List<PatrolLastLocationDTO> GetPatrolsETAs(double latitude, double longitude, List<PatrolLastLocationDTO> patrols);
    }
}
