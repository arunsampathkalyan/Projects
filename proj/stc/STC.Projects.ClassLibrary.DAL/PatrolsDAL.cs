using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Data.Entity.Spatial;
using STC.Projects.ClassLibrary.Common;

namespace STC.Projects.ClassLibrary.DAL
{
    public class PatrolsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public PatrolLastLocationDTO GetPatrolDetails(int PatrolId)
        {
            try
            {
                var patrol = operationalDataContext.PatrolLastLocationViews.Where(x => x.PatrolId == PatrolId)
                    .Select(p => new PatrolLastLocationDTO
                    {
                        Latitude = p.Latitude,
                        LocationDate = p.LocationDate,
                        Longitude = p.Longitude,
                        PatrolCode = p.PatrolCode,
                        PatrolPlateNo = p.PatrolPlateNo,
                        Speed = p.Speed,
                        StatusId = p.StatusId,
                        StatusName = p.StatusName,
                        PatrolOriginalId = p.PatrolOriginalId,
                        isPatrol = p.IsPatrol.HasValue ? p.IsPatrol.Value : true,
                        OfficerName = p.OfficerName,
                        PatrolImage = p.PatrolImage,
                        CurrentTaskId = p.CurrentTaskId.HasValue ? p.CurrentTaskId.Value : 0

                    }).FirstOrDefault();
                return patrol;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public bool AddPatrol(string patrolCode, string patrolPlate, Guid? patrolOrgId)
        {
            try
            {
                operationalDataContext.Patrols.Add(new Entities.Patrol
                {
                    PatrolCode = patrolCode,
                    PatrolOriginalId = patrolOrgId.HasValue ? patrolOrgId.Value : new Guid(),
                    PatrolPlateNo = patrolPlate,
                    StatusId = 1,
                    StatusName = "Available",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    IsPatrol = true,
                    IsInTFM = patrolOrgId.HasValue
                });
                return operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool ChangePatrolActivation(int patrolId, bool isDeleted = true)
        {
            try
            {
                var patrol = operationalDataContext.Patrols.FirstOrDefault(x => x.PatrolId == patrolId);
                if (patrol != null)
                    patrol.IsDeleted = isDeleted;

                return operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public List<PatrolLastLocationDTO> GetAllPatrols()
        {
            var listPatrols = operationalDataContext.Patrols.Select(x => new PatrolLastLocationDTO
            {
                PatrolCode = x.PatrolCode,
                PatrolId = x.PatrolId,
                PatrolPlateNo = x.PatrolPlateNo,
                StatusId = x.StatusId,
                StatusName = x.StatusName,
                isDeleted = x.IsDeleted ?? false,
                PatrolOriginalId = x.PatrolOriginalId,
                CreationDate = x.DateCreated.HasValue ? x.DateCreated.Value : DateTime.Now,
                NumberOfAssignedIncident = x.IncidentDispatches.Count,
                isPatrol = x.IsPatrol.HasValue ? x.IsPatrol.Value : true,
                OfficerName = x.OfficerName,
                PatrolImage = x.PatrolImage,
                CurrentTaskId = x.CurrentTaskId.HasValue ? x.CurrentTaskId.Value : 0
            }).ToList();
            return listPatrols;
        }

        public List<PatrolLastLocationDTO> GetPatrolsList(int? PatrolStatusId)
        {
            try
            {
                var lstPatrols = operationalDataContext.PatrolLastLocationViews
                    .Where(Patrol => (PatrolStatusId == null || Patrol.StatusId == PatrolStatusId) && (!Patrol.IsInTFM.HasValue || Patrol.IsInTFM.Value))
                    .Select(Patrol => new PatrolLastLocationDTO
                    {
                        Altitude = Patrol.Altitude,
                        Latitude = Patrol.Latitude,
                        LocationDate = Patrol.LocationDate,
                        Longitude = Patrol.Longitude,
                        PatrolCode = Patrol.PatrolCode,
                        PatrolId = Patrol.PatrolId,
                        PatrolLatLocationId = Patrol.PatrolLatLocationId,
                        Speed = Patrol.Speed,
                        IsNoticed = Patrol.IsNoticed,
                        StatusId = Patrol.StatusId,
                        StatusName = Patrol.StatusName,
                        PatrolOriginalId = Patrol.PatrolOriginalId,
                        PatrolPlateNo = Patrol.PatrolPlateNo,
                        CreationDate = Patrol.DateCreated.HasValue ? Patrol.DateCreated.Value : DateTime.Now,
                        isDeleted = Patrol.IsDeleted ?? false,
                        isPatrol = Patrol.IsPatrol.HasValue ? Patrol.IsPatrol.Value : true,
                        OfficerName = Patrol.OfficerName,
                        PatrolImage = Patrol.PatrolImage,
                        CurrentTaskId = Patrol.CurrentTaskId.HasValue ? Patrol.CurrentTaskId.Value : 0
                        //NumberOfAssignedIncident = operationalDataContext.IncidentDispatches.Count(x=> x.PatrolId == Patrol.PatrolId)
                    }).ToList();

                if (lstPatrols.Count > 0)
                {
                    foreach (var patrol in lstPatrols)
                    {
                        if (patrol.isPatrol)
                            patrol.IsBusy = !new TFMDAL().IsPatrolAvailable(patrol.PatrolOriginalId);
                    }
                }

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public void SetPatrolFree(long patrolId)
        {
            var patrolIncidents = operationalDataContext.IncidentDispatches.Where(p => p.PatrolId == patrolId).ToList();

            if (patrolIncidents != null && patrolIncidents.Count > 0)
            {
                foreach (var incident in patrolIncidents)
                {
                    incident.IsDeleted = true;
                }

                operationalDataContext.SaveChanges();
            }
        }

        public bool RecordDispatchLog(int PatrolId, long NotificationId, DateTime DispatchDate)
        {
            SetPatrolFree(PatrolId);

            var incidentDispatchItem = operationalDataContext.IncidentDispatches.Where(i => i.PatrolId == PatrolId && i.IncidentId == NotificationId).FirstOrDefault();

            if (incidentDispatchItem != null)
            {
                incidentDispatchItem.IsDeleted = false;

                return operationalDataContext.SaveChanges() > 0;
            }

            operationalDataContext.IncidentDispatches.Add(new IncidentDispatch
            {
                DispatchDate = DispatchDate,
                IncidentId = NotificationId,
                IsByVehicle = true,
                IsDeleted = false,
                PatrolId = PatrolId
            });

            return operationalDataContext.SaveChanges() > 0;
        }
        public bool UpdatePatrolLocation(List<PatrolLastLocationDTO> patrolLocations)
        {
            var allPatrols = operationalDataContext.Patrols.ToList();

            var list = new List<PatrolLastLocation>();
            var index = 1;
            foreach (var item in patrolLocations)
            {
                var patrol = new PatrolLastLocation
                {
                    PatrolLatLocationId = index++,
                    Altitude = 1,
                    IsNoticed = false,
                    Latitude = item.Latitude,
                    LocationDate = item.LocationDate,
                    Longitude = item.Longitude,
                    PatrolCode = item.PatrolCode,
                    PatrolId = item.PatrolId,
                    Speed = item.Speed,
                    StatusId = item.StatusId.HasValue ? item.StatusId.Value : 1,
                    StatusName = item.StatusName != null ? item.StatusName : "Available",
                    
                };
                var pointString = string.Format(
               "POINT({0} {1})",
               item.Longitude.ToString(),
               item.Latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString);
                patrol.GeoLocation = dbGeography;
                var patrolItem = allPatrols.FirstOrDefault(x => x.PatrolPlateNo == item.PatrolPlateNo);
                if (patrolItem != null)
                {
                    if(patrolItem.IsInTFM.HasValue && !patrolItem.IsInTFM.Value)
                    {
                        patrolItem.IsInTFM = true;
                        patrolItem.PatrolOriginalId = item.PatrolOriginalId;
                    }
                    patrol.PatrolId = patrolItem.PatrolId;


                    //index++;
                    list.Add(patrol);
                }
            }
            // 25.04.2016 Commented to solve the production issue
            //operationalDataContext.PatrolLastLocations.RemoveRange(operationalDataContext.PatrolLastLocations.ToList().Where(x => patrolLocations.Any(y => y.PatrolId == x.PatrolId)));
            operationalDataContext.PatrolLastLocations.RemoveRange(operationalDataContext.PatrolLastLocations.ToList().Where(x => patrolLocations.Any(y => y.PatrolCode == x.PatrolCode)));


            operationalDataContext.PatrolLastLocations.AddRange(list);
            return operationalDataContext.SaveChanges() > 0;
        }

        public List<PatrolLastLocationDTO> GetUpdatedPatrolsList(bool? IsNoticed)
        {
            try
            {
                var lstPatrols = operationalDataContext.PatrolLastLocationViews
                    .Where(Patrol => Patrol.IsDeleted == false && (!IsNoticed.HasValue || Patrol.IsNoticed == IsNoticed.Value))
                    .Select(Patrol => new PatrolLastLocationDTO
                    {
                        Altitude = Patrol.Altitude,
                        Latitude = Patrol.Latitude,
                        LocationDate = Patrol.LocationDate,
                        Longitude = Patrol.Longitude,
                        PatrolCode = Patrol.PatrolCode,
                        PatrolId = Patrol.PatrolId,
                        PatrolLatLocationId = Patrol.PatrolLatLocationId,
                        Speed = Patrol.Speed,
                        IsNoticed = Patrol.IsNoticed,
                        StatusId = Patrol.StatusId,
                        StatusName = Patrol.StatusName,
                        PatrolOriginalId = Patrol.PatrolOriginalId,
                        PatrolPlateNo = Patrol.PatrolPlateNo,
                        isPatrol = Patrol.IsPatrol.HasValue ? Patrol.IsPatrol.Value : true,
                        OfficerName = Patrol.OfficerName,
                        PatrolImage = Patrol.PatrolImage,
                        CurrentTaskId = Patrol.CurrentTaskId.HasValue ? Patrol.CurrentTaskId.Value : 0
                    }).ToList();

                return lstPatrols;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public List<PatrolLastLocationDTO> GetNearByPatrolsByLatLon(double Longitude, double Latitude, double Radius, int patrolsCount)
        {
            try
            {
                var strPoint = string.Format("POINT({0} {1})", Longitude, Latitude);
                DbGeography pointGeography = DbGeography.FromText(strPoint);
                DbGeography dbGeography = DbGeography.FromText(strPoint).Buffer(Radius);

                var lstPatrols = (from patrol in operationalDataContext.PatrolLastLocationViews
                                  where ((!patrol.IsInTFM.HasValue) || patrol.IsInTFM.Value)
                                      // where patrol.GeoLocation.Intersects(dbGeography) 
                                  orderby patrol.GeoLocation.Distance(pointGeography)
                                  select new PatrolLastLocationDTO
                                  {
                                      Altitude = patrol.Altitude,
                                      Latitude = patrol.Latitude,
                                      LocationDate = patrol.LocationDate,
                                      Longitude = patrol.Longitude,
                                      PatrolCode = patrol.PatrolCode,
                                      PatrolId = patrol.PatrolId,
                                      PatrolLatLocationId = patrol.PatrolLatLocationId,
                                      Speed = patrol.Speed,
                                      IsNoticed = patrol.IsNoticed,
                                      StatusId = patrol.StatusId,
                                      StatusName = patrol.StatusName,
                                      PatrolOriginalId = patrol.PatrolOriginalId,
                                      PatrolPlateNo = patrol.PatrolPlateNo,
                                      isPatrol = patrol.IsPatrol.HasValue ? patrol.IsPatrol.Value : true,
                                      OfficerName = patrol.OfficerName,
                                      PatrolImage = patrol.PatrolImage,
                                      CurrentTaskId = patrol.CurrentTaskId.HasValue ? patrol.CurrentTaskId.Value : 0
                                  }).Take(patrolsCount).ToList();


                return lstPatrols;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex, @"C:\STC\Websites\Server\WCF\");
            }

            return null;
        }

        public List<PatrolDTO> GetAssignedPatrolsByNotificationId(long notificationId)
        {
            try
            {
                var list = operationalDataContext.IncidentDispatches.Where(x => !x.IsDeleted && x.IncidentId == notificationId).Select(y => new PatrolDTO()
                {
                    PatrolCode = y.Patrol.PatrolCode,
                    PatrolId = y.Patrol.PatrolId,
                    PatrolOriginalId = y.Patrol.PatrolOriginalId,
                    PatrolPlateNo = y.Patrol.PatrolPlateNo,
                    IsPatrol = y.Patrol.IsPatrol.HasValue ? y.Patrol.IsPatrol.Value : true,
                    OfficerName = y.Patrol.OfficerName,
                    PatrolImage = y.Patrol.PatrolImage,
                    CurrentTaskId = y.Patrol.CurrentTaskId.HasValue ? y.Patrol.CurrentTaskId.Value : 0
                }).ToList();
                return list;
            }
            catch (Exception ex)
            {

            }
            return new List<PatrolDTO>();
        }


    }
}
