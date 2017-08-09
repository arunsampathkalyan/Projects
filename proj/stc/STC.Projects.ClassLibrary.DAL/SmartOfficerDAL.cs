using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System.Data.Entity.Spatial;
using STC.Projects.ClassLibrary.Common;
using System.Xml.Serialization;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SmartOfficerDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();

        public bool UpdateOfficerLocation(string officerCode, double lat, double lon)
        {
            var pointString = string.Format("POINT({0} {1})", lon.ToString(), lat.ToString());
            DbGeography dbGeography = DbGeography.FromText(pointString);
            var officer = operationalDataContext.Patrols.Where(x => x.PatrolCode == officerCode).FirstOrDefault();
            long maxIndex = 0;

            if (operationalDataContext.PatrolLastLocations != null)
                maxIndex = operationalDataContext.PatrolLastLocations.Select(y => y.PatrolLatLocationId).Max();

            if (officer == null)
            {
                officer = new Patrol
                {
                    PatrolCode = officerCode,
                    PatrolPlateNo = "officer",
                    DateCreated = DateTime.Now,
                    PatrolOriginalId = Guid.NewGuid(),
                    StatusId = 1,
                    StatusName = "Available",
                    IsDeleted = false,
                    IsPatrol = false
                };

                operationalDataContext.Patrols.Add(officer);

                if (!(operationalDataContext.SaveChanges() > 0))
                    return false;
            }

            var officerLocation = operationalDataContext.PatrolLastLocations.Where(p => p.PatrolCode == officerCode).FirstOrDefault();

            if (officerLocation == null)
            {
                officerLocation = new PatrolLastLocation
                {
                    PatrolLatLocationId = ++maxIndex,
                    PatrolId = officer.PatrolId,
                    PatrolCode = officer.PatrolCode,
                    Altitude = 1,
                    Latitude = lat,
                    Longitude = lon,
                    IsNoticed = false,
                    GeoLocation = dbGeography,
                    LocationDate = DateTime.Now,
                    Speed = 0,
                    StatusId = 1,
                    StatusName = "Available",
                    IsPatrol = false
                };

                operationalDataContext.PatrolLastLocations.Add(officerLocation);
            }
            else
            {
                officerLocation.Latitude = lat;
                officerLocation.Longitude = lon;
                officerLocation.GeoLocation = dbGeography;
                officerLocation.LocationDate = DateTime.Now;
                officerLocation.IsNoticed = false;
            }

            if (!(operationalDataContext.SaveChanges() > 0))
                return false;

            return true;
        }

        public List<string> GetAvailablePatrolPlateNumbers()
        {
            try
            {
                var list = operationalDataContext.Patrols.Where(x => x.IsPatrol.Value && !x.IsDeleted.Value).Select(y => y.PatrolPlateNo).ToList();
                return list;
            }
            catch (Exception ex)
            {

            }
            return new List<string>();
        }

        public bool AddNewOfficer(string militaryId, string officerPatrolPlateNumber, string officerName, string image)
        {
            byte[] imageArr = null;

            if (image != null)
                imageArr = Convert.FromBase64String(image);

            var officer = operationalDataContext.Patrols.Where(x => x.PatrolCode == militaryId).FirstOrDefault();

            if (officer == null)
            {
                officer = new Patrol()
                {
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    IsPatrol = false,
                    OfficerName = officerName,
                    PatrolCode = militaryId,
                    PatrolImage = imageArr,
                    PatrolPlateNo = officerPatrolPlateNumber,
                    StatusId = 1,
                    StatusName = "Available"
                };

                operationalDataContext.Patrols.Add(officer);
            }
            else
            {
                officer.IsPatrol = false;
                officer.OfficerName = officerName;
                officer.PatrolImage = imageArr;
                officer.PatrolPlateNo = officerPatrolPlateNumber;
                officer.StatusId = 1;
                officer.StatusName = "Available";
            }

            return operationalDataContext.SaveChanges() > 0;
        }

        public SmartOfficerDTO GetOfficer(string militaryId)
        {
            var officer = operationalDataContext.Patrols.Where(x => !x.IsPatrol.Value && x.PatrolCode == militaryId).FirstOrDefault();

            if (officer != null)
            {
                SmartOfficerDTO dto = new SmartOfficerDTO();
                dto.OfficerMilitaryId = officer.PatrolCode;
                dto.OfficerId = officer.PatrolId;
                dto.OfficerName = officer.OfficerName;
                dto.OfficerImage = officer.PatrolImage;
                dto.OfficerPatrolPlateNumber = officer.PatrolPlateNo;
                dto.StatusId = officer.StatusId;
                dto.StatusName = officer.StatusName;

                return dto;
            }

            return null;
        }

        public bool AddTask(OfficerTaskDTO task)
        {
            try
            {
                if (task == null)
                    return false;

                var officer = operationalDataContext.Patrols.Where(p => !p.IsPatrol.Value && p.PatrolCode == task.OfficerMilitaryId).FirstOrDefault();

                if (officer == null)
                    return false;

                var pointString = string.Format("POINT({0} {1})", task.Longitude.ToString(), task.Latitude.ToString());
                DbGeography dbGeography = DbGeography.FromText(pointString);

                var officerTask = new OfficerTask
                {
                    CreateDate = DateTime.Now,
                    GeoLocation = dbGeography,
                    Latitude = task.Latitude,
                    Longitude = task.Longitude,
                    OfficerMilitaryId = task.OfficerMilitaryId,
                    TaskMessage = task.TaskMessage,
                    TaskTime = task.TaskTime,
                    UserId = task.UserId,
                    IsNoticed = false
                };

                officerTask = operationalDataContext.OfficerTask.Add(officerTask);

                var officertaskstatus = new OfficerTaskStatus
                {
                    OfficerTaskId = officerTask.OfficerTaskId,
                    StatusUpdateDate = DateTime.Now,
                    TaskStatusId = 1,
                    Latitude = task.Latitude,
                    Longitude = task.Longitude,
                    GeoLocation = dbGeography,
                    IsNoticed = false
                };

                operationalDataContext.OfficerTaskStatus.Add(officertaskstatus);

                return operationalDataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeTaskStatus(OfficerTaskStatusDTO taskStatus)
        {
            try
            {
                if (taskStatus == null)
                    return false;

                var task = operationalDataContext.OfficerTask.Where(t => t.OfficerTaskId == taskStatus.OfficerTaskId).FirstOrDefault();

                if (task != null)
                {
                    var pointString = string.Format("POINT({0} {1})", taskStatus.Longitude.ToString(), taskStatus.Latitude.ToString());
                    DbGeography dbGeography = DbGeography.FromText(pointString);

                    var officertaskstatus = new OfficerTaskStatus
                    {
                        OfficerTaskId = taskStatus.OfficerTaskId,
                        StatusUpdateDate = DateTime.Now,
                        TaskStatusId = taskStatus.TaskStatusId,
                        Notes = taskStatus.Notes,
                        Latitude = taskStatus.Latitude,
                        Longitude = taskStatus.Longitude,
                        GeoLocation = dbGeography,
                        IsNoticed = false
                    };

                    operationalDataContext.OfficerTaskStatus.Add(officertaskstatus);

                    return operationalDataContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }

            return false;
        }

        public List<TaskStatusDTO> GetAvailableTaskStatuses()
        {
            var output = operationalDataContext.TaskStatus.Where(x => x.AvailableToOfficer.Value)
                       .Select(s => new TaskStatusDTO
                       {
                           TaskStatusId = s.TaskStatusId,
                           TaskStatusNameAr = s.TaskStatusNameAr,
                           TaskStatusNameEn = s.TaskStatusNameEn
                       }).ToList();

            return output;
        }

        public List<OfficerTaskDTO> GetOfficerHistoricalTasksFromDate(string officerMilitaryId, DateTime fromDate)
        {
            try
            {
                operationalDataContext = new STCOperationalDataContext();

                var lstCompleted = operationalDataContext.OfficerTaskStatus.Where(x => x.TaskStatusId != 1 && x.TaskStatusId != 2 && x.TaskStatusId != 3).ToList();
                List<long> completedTaskIDs = new List<long>();

                if (lstCompleted != null && lstCompleted.Count > 0)
                {
                    completedTaskIDs = lstCompleted.Select(x => x.OfficerTaskId ?? 0).ToList();
                }

                var lstChanged = operationalDataContext.OfficerTask.Where(x => x.OfficerMilitaryId == officerMilitaryId && x.CreateDate >= fromDate && !completedTaskIDs.Contains(x.OfficerTaskId)).Select(x => new OfficerTaskDTO()
                {
                    OfficerMilitaryId = x.OfficerMilitaryId,
                    Latitude = x.Latitude ?? 0,
                    Longitude = x.Longitude ?? 0,
                    OfficerTaskId = x.OfficerTaskId,
                    TaskMessage = x.TaskMessage,
                    TaskTime = x.TaskTime ?? DateTime.Now,
                    UserId = x.UserId ?? 0
                }).ToList();

                return lstChanged;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OfficerTaskDTO> GetOfficerPendingTasks(string officerMilitaryId)
        {
            try
            {
                operationalDataContext = new STCOperationalDataContext();

                var lstChanged = operationalDataContext.OfficerTask.Where(x => x.OfficerMilitaryId == officerMilitaryId && x.IsNoticed == false).Select(x => new OfficerTaskDTO()
                {
                    OfficerMilitaryId = x.OfficerMilitaryId,
                    Latitude = x.Latitude ?? 0,
                    Longitude = x.Longitude ?? 0,
                    OfficerTaskId = x.OfficerTaskId,
                    TaskMessage = x.TaskMessage,
                    TaskTime = x.TaskTime ?? DateTime.Now,
                    UserId = x.UserId ?? 0
                }).ToList();

                return lstChanged;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
