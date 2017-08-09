using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WCF.ServiceLayer.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace STC.Projects.WCF.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CrsipServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CrsipServices.svc or CrsipServices.svc.cs at the Solution Explorer and start debugging.
    public class CrsipServices : ICrsipServices
    {
        PCAServices.PCAServiceClient _client = new PCAServices.PCAServiceClient();
        public bool AddSession(string sessionName)
        {
            bool log = false;
            bool.TryParse(ConfigurationManager.AppSettings["CrispLog"], out log);
            var count = _client.newSession(sessionName, log);
            return count >= 0;
        }

        public bool RemoveSession(string sessionName)
        {
            return _client.dropSession(sessionName);
        }

        public bool ParseNode(string sessionName, string nodesXml)
        {
            return _client.parseNodes(sessionName, nodesXml);
        }

        public bool ParseCars(string sessionName, string carsXml)
        {
            return _client.parseCars(sessionName, carsXml);
        }

        public bool TestCarParse(string sessionName)
        {
            var xml = File.ReadAllText(@"D:\Crisp\cars.xml");
            var res = _client.parseCars(sessionName, xml);
           
            return res;
        }

        public PatrolLastLocationDTO RecommendCar(double lat, double lon)
        {
            int maxAllowedTimeInMin = 10;
            int.TryParse(ConfigurationManager.AppSettings["MaxAllowedTimeMins"], out maxAllowedTimeInMin);
            var currentSession = GetActiveSession();
            if (currentSession.MaxAllowedTimeMin.HasValue)
                maxAllowedTimeInMin = currentSession.MaxAllowedTimeMin.Value;
            DoIterate(currentSession.SessionName);
            var carRec = _client.recommendCar(currentSession.SessionName, lon, lat, maxAllowedTimeInMin * 60 * 1000);
            var crispCars = _client.getCars(currentSession.SessionName);
            var patrols = new PatrolsDAL().GetPatrolsList(null);
            if (crispCars != null && patrols != null && crispCars.Any() && patrols.Any())
            {
                var crispCar = crispCars.FirstOrDefault(x => x.id == carRec);
                var p = patrols.FirstOrDefault(x => x.PatrolId.ToString() == crispCar.id);
                if (p != null)
                    p.IsRecommended = true;
                return p;
            }
            return null;
        }

        public bool AddPatrol(string patrolCode, string patrolPlateNumber)
        {
            Guid? patrolOrg = null;
            var allTFMPatrol = new TFMIntegrationService().GetPatrolsLocations();
            if (allTFMPatrol != null && allTFMPatrol.Any(x => x.PatrolPlateNo == patrolPlateNumber))
            {
                var tfmPatr = allTFMPatrol.FirstOrDefault(x => x.PatrolPlateNo == patrolPlateNumber);
                patrolOrg = tfmPatr.PatrolOriginalId;
            }
            
            return new PatrolsDAL().AddPatrol(patrolCode, patrolPlateNumber, patrolOrg);
        }

        public bool RemovePatrol(int patrolId)
        {
            return new PatrolsDAL().ChangePatrolActivation(patrolId);
        }

        public void DoIterate(string sessionName)
        {
            int montCarloM = 1000;
            int montCarloL = 1000;
            int.TryParse(ConfigurationManager.AppSettings["MontCarloM"], out montCarloM);
            int.TryParse(ConfigurationManager.AppSettings["MontCarloL"], out montCarloL);
            
            _client.doIterate(sessionName, montCarloM, montCarloL);
        }
        public List<PatrolLastLocationDTO> GetPatrolsETA(double latitude,double longitude)
        {
            var res = new List<PatrolLastLocationDTO>();
            try
            {
                var sessionName = GetActiveSession().SessionName;
                DoIterate(sessionName);
                res = new PatrolsDAL().GetPatrolsList(null);
                var allCars = _client.getCars(sessionName);
                if(allCars != null && allCars.Any())
                {
                    foreach (var item in allCars)
                    {
                        var patrol = res.FirstOrDefault(x => x.PatrolId.ToString() == item.id);
                        if(patrol != null)
                        {
                            var time = _client.getETA(sessionName, longitude, latitude, item.id);
                            patrol.ETATime = time;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return res;
        }

        public List<PatrolLastLocationDTO> GetPatrolsETAs(double latitude, double longitude,List<PatrolLastLocationDTO> patrols)
        {
            if(patrols == null)
                return null;
            
            Utility.WriteLog("Start Get Patrols ETA:" + patrols.Count);
            var res = patrols;
            try
            {
                var sessionName = GetActiveSession().SessionName;
                DoIterate(sessionName);
               var allCars = _client.getCars(sessionName);
                if (allCars != null && allCars.Any())
                {
                    foreach (var item in allCars)
                    {
                        var patrol = res.FirstOrDefault(x => x.PatrolId.ToString() == item.id);
                        if (patrol != null)
                        {
                            var time = _client.getETA(sessionName, longitude, latitude, item.id);
                            patrol.ETATime = time;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 Utility.WriteErrorLog(ex);
            }
            return res;
        }
        
        public List<PatrolLastLocationDTO> PositionsCars()
        {
            var res = new List<PatrolLastLocationDTO>();
            var sessionName = GetActiveSession().SessionName;
            bool bMax = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsCrispBMax"], out bMax);
            DoIterate(sessionName);
            var locations = _client.getLocations(sessionName, bMax);
            var allLocations = _client.getNodes(sessionName);
            var allPatrols = new PatrolsDAL().GetPatrolsList(null);
            var allCars = _client.getCars(sessionName);
            if (locations != null && locations.Any() && allLocations != null && allLocations.Any() && allPatrols != null && allPatrols.Any() && allCars != null && allCars.Any())
            {
                int index = 0;
                foreach (var item in locations)
                {
                    var location = allLocations.FirstOrDefault(x => x.id == item);
                    var patrol = allPatrols.FirstOrDefault(x => x.PatrolId.ToString() == allCars[index].id);
                    if (patrol != null)
                    {
                        patrol.Longitude = location.X;
                        patrol.Latitude = location.Y;
                        patrol.IsRecommended = true;
                        res.Add(patrol);
                    }
                    index++;
                }
            }
            return res;
        }

        public void HandleSessions()
        {
            var sessions = GetAllSessions();
            var crispSessions = _client.listSessions().ToList();
            var droppedSessions = crispSessions.Where(x => !sessions.Any(y => y.SessionName.ToLower().Trim() == x.id.ToLower().Trim())).ToList();
            var toBeAddedSessions = sessions.Where(x => !crispSessions.Any(y => y.id.ToLower().Trim() == x.SessionName.ToLower().Trim())).ToList();
            foreach (var item in droppedSessions)
            {
                _client.dropSession(item.id);
            }
            if (toBeAddedSessions.Any())
            {
                foreach (var item in toBeAddedSessions)
                {
                    AddSession(item.SessionName);
                    try
                    {
                        if (item.NodeXmlPath != "" && File.Exists(item.NodeXmlPath))
                        {
                            var xml = File.ReadAllText(item.NodeXmlPath);
                            ParseNode(item.SessionName, xml);
                            //TestCarParse(item.SessionName);
                        }
                        PrepareCars();
                        
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void HandleCars(List<PatrolLastLocationDTO> availableCars)
        {
            var crispCars = new cars();
            crispCars.C = availableCars.Count().ToString();
            crispCars.car = new carsCar[availableCars.Count];
            var index = 0;
            foreach (var item in availableCars)
            {
                crispCars.car[index] = new carsCar
                {
                    c = (index + 1).ToString(),
                    id = item.PatrolId.ToString(),
                    S = "0",
                    X = item.Longitude ?? 0,
                    Y = item.Latitude ?? 0
                };
                index++;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(cars));
            var stringwriter = new System.IO.StringWriter();
            //var xmlWriter = new XmlWriter();
            serializer.Serialize(stringwriter, crispCars);
           // XmlWriterSettings settings = new XmlWriterSettings();
            //settings.OmitXmlDeclaration = true;

            //MemoryStream ms = new MemoryStream();
            //XmlWriter writer = XmlWriter.Create(ms, settings);

            //XmlSerializerNamespaces names = new XmlSerializerNamespaces();
            //names.Add("", "");

            //XmlSerializer cs = new XmlSerializer(typeof(cars));

            //cs.Serialize(writer, crispCars, names);

            //ms.Flush();
            //ms.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(ms);
            //var xml = sr.ReadToEnd();
            //var sessionName = GetActiveSession();
            var allSessions = _client.listSessions();
            if (allSessions != null)
            {
                foreach (var item in allSessions)
                {
                    ParseCars(item.id, stringwriter.ToString());
                }
            }

        }

        public void PrepareCars()
        {
            var patrols = new PatrolsDAL().GetPatrolsList(1);
            HandleCars(patrols);
        }

        public CrispSessionDTO GetActiveSession()
        {
            // return the current session
            HandleSessions();
            var currentTime = DateTime.Now;
            var currentDate = currentTime.Date;
            var currentHour = currentTime.Hour;
            var sessions = GetAllSessions();
            var currentSession = new CrispSessionDTO();
            if (sessions.Any(x => x.StartDate.HasValue && x.StartDate.Value >= currentDate && x.EndDate.HasValue && x.EndDate <= currentDate && x.StartHour <= currentHour && x.EndHour >= currentHour))
            {
                currentSession = sessions.FirstOrDefault(x => x.StartDate.HasValue && x.StartDate.Value >= currentDate && x.EndDate.HasValue && x.EndDate <= currentDate && x.StartHour <= currentHour && x.EndHour >= currentHour);
            }
            else if (sessions.Any(x => !x.StartDate.HasValue && !x.EndDate.HasValue && x.StartHour <= currentHour && x.EndHour >= currentHour))
            {
                currentSession = sessions.FirstOrDefault(x => !x.StartDate.HasValue && !x.EndDate.HasValue && x.StartHour <= currentHour && x.EndHour >= currentHour);
            }
            return currentSession;
        }

        public void SetMaxAllowedTime(int maxAllowedTimeInMins)
        {
            var allSession = GetAllSessions();
            if (allSession != null && allSession.Any())
            {
                foreach (var item in allSession)
                {
                    new CrispSessionDAL().UpdateMaxAllowedTimePerSession(item.SessionId, maxAllowedTimeInMins);
                }
            }
        }

        public int getMaxAllowedTime()
        {
            var allSession = GetAllSessions();
            if (allSession != null && allSession.Any())
            {
                return allSession.FirstOrDefault().MaxAllowedTimeMin.HasValue ? allSession.FirstOrDefault().MaxAllowedTimeMin.Value : 0;
            }
            return 0;
        }

        [AspNetCacheProfile("CacheForSessions")]
        [WebGet()]
        public List<CrispSessionDTO> GetAllSessions()
        {
            return new CrispSessionDAL().GetAvailableSessions();
        }
    }
}
