using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using STC.Projects.ClassLibrary.Common;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;
using STC.Projects.ClassLibrary.DAL.Utilities;

namespace STC.Projects.ClassLibrary.DAL
{
    [Serializable]
    public class PatrolOfficersDetails
    {
        public PatrolOfficersDetails()
        {
            Officers = new List<StaffPatrolModel>();
        }
        public string PatrolAllocation { get; set; }
        public string PatrolCode { get; set; }
        public string PatrolPlateNumber { get; set; }
        public bool IsAvailable { get; set; }

        public List<StaffPatrolModel> Officers { get; set; }
    }

    public class TFMDAL
    {
        TFMEntities dbContext;
        STCOperationalDataContext operationalContext;
        readonly string baseURL = ConfigurationManager.AppSettings["TFMTaskMediaPath"];

        public List<string> GetTaskImagesURLs(long taskId)
        {
            Utility.WriteLog("Invokation Started");

            List<string> urls = new List<string>();

            try
            {
                string Environment = ConfigurationManager.AppSettings["Environment"];

                Utility.WriteLog(Environment);

                if (Environment != "Production")
                {
                    string imagesURL = baseURL + @"12345/Images";

                    Utility.WriteLog(imagesURL);

                    if (Directory.Exists(imagesURL))
                    {
                        string[] dirImages = Directory.GetFiles(imagesURL);

                        if (dirImages != null && dirImages.Length > 0)
                        {
                            foreach (var imageURL in dirImages)
                            {
                                Image img = Image.FromFile(imageURL);

                                if (img != null)
                                {
                                    using (MemoryStream m = new MemoryStream())
                                    {
                                        img.Save(m, img.RawFormat);
                                        byte[] imageBytes = m.ToArray();

                                        if (imageBytes != null && imageBytes.Length > 0)
                                        {
                                            string b64String = Convert.ToBase64String(imageBytes);

                                            Utility.WriteLog(b64String);

                                            urls.Add(b64String);
                                        }
                                    }
                                }
                            }

                            return urls;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }

            try
            {
                Utility.WriteLog("Production Environment");
                dbContext = new TFMEntities();
                Utility.WriteLog(taskId.ToString());

                iPatrolDuty task = new iPatrolDuty();

                task = dbContext.iPatrolDuties.Where(x => x.Id == taskId).FirstOrDefault();

                if (task == null)
                {
                    Utility.WriteLog("Task Is Null");
                    return null;
                }

                var nexttask = dbContext.iPatrolDuties.Where(t => t.Id > task.Id).OrderBy(t => t.Id).FirstOrDefault();

                Utility.WriteLog("Task Is Not Null");

                var taskUser = dbContext.iPatrolDutiesUsers.Where(x => x.DutyId == taskId).FirstOrDefault();

                if (taskUser != null && dbContext.iPatrolFilesSubmittedByUsers.Any(x => x.StaffId == taskUser.StaffId))
                {
                    var files = dbContext.iPatrolFilesSubmittedByUsers.Where(x => x.StaffId == taskUser.StaffId).ToList();

                    foreach (var fileObj in files)
                    {
                        string imagesURL = baseURL + fileObj.FileId.ToString() + @"/Images";

                        Utility.WriteLog("Base URL Is: " + imagesURL);

                        if (Directory.Exists(imagesURL))
                        {
                            string[] dirImages = Directory.GetFiles(imagesURL);

                            if (dirImages != null && dirImages.Length > 0)
                            {
                                foreach (var imageURL in dirImages)
                                {
                                    Utility.WriteLog(imageURL);

                                    Image img = Image.FromFile(imageURL);

                                    if (img != null)
                                    {
                                        DateTime fileDate = default(DateTime);
                                        DateTime nextTaskDate = DateTime.Now;

                                        try
                                        {
                                            fileDate = File.GetCreationTime(imageURL);

                                            if (nexttask != null)
                                                nextTaskDate = nexttask.Date.HasValue ? nexttask.Date.Value : DateTime.Now;
                                        }
                                        catch { }

                                        if (fileDate >= task.Date && fileDate <= nextTaskDate)
                                        {
                                            using (MemoryStream m = new MemoryStream())
                                            {
                                                img.Save(m, img.RawFormat);
                                                byte[] imageBytes = m.ToArray();

                                                if (imageBytes != null && imageBytes.Length > 0)
                                                {
                                                    string b64String = Convert.ToBase64String(imageBytes);

                                                    Utility.WriteLog(b64String);

                                                    urls.Add(b64String);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Utility.WriteLog("Image Could not be created!");
                                    }
                                }
                            }
                            else
                            {
                                Utility.WriteLog("Directory Is Empty!");
                            }
                        }
                    }
                }
                else
                {
                    Utility.WriteLog("Task User Is NULL");
                }

                return urls;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public List<string> GetTaskVideosURLs(long taskId)
        {
            List<string> urls = new List<string>();

            try
            {
                string Environment = ConfigurationManager.AppSettings["Environment"];

                if (Environment != "Production")
                {
                    string imagesURL = baseURL + @"12345/Video";

                    if (Directory.Exists(imagesURL))
                    {
                        string[] dirImages = Directory.GetFiles(imagesURL);

                        if (dirImages != null && dirImages.Length > 0)
                        {
                            foreach (var imageURL in dirImages)
                            {
                                byte[] videoArray = File.ReadAllBytes(imageURL);

                                if (videoArray != null && videoArray.Length > 0)
                                {
                                    string b64String = Convert.ToBase64String(videoArray);

                                    Utility.WriteLog(b64String);

                                    urls.Add(b64String);
                                }
                            }

                            return urls;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }

            try
            {
                dbContext = new TFMEntities();

                var task = dbContext.iPatrolDuties.Where(x => x.Id == taskId).FirstOrDefault();

                if (task == null)
                    return null;

                var nexttask = dbContext.iPatrolDuties.Where(t => t.Id > task.Id).OrderBy(t => t.Id).FirstOrDefault();

                var taskUser = dbContext.iPatrolDutiesUsers.Where(x => x.DutyId == taskId).FirstOrDefault();

                if (taskUser != null && dbContext.iPatrolFilesSubmittedByUsers.Any(x => x.StaffId == taskUser.StaffId))
                {
                    var files = dbContext.iPatrolFilesSubmittedByUsers.Where(x => x.StaffId == taskUser.StaffId).ToList();

                    foreach (var fileObj in files)
                    {
                        string imagesURL = baseURL + fileObj.FileId.ToString() + @"/Video";

                        if (Directory.Exists(imagesURL))
                        {
                            string[] dirImages = Directory.GetFiles(imagesURL);

                            if (dirImages != null && dirImages.Length > 0)
                            {
                                foreach (var imageURL in dirImages)
                                {
                                    DateTime fileDate = default(DateTime);
                                    DateTime nextTaskDate = DateTime.Now;

                                    try
                                    {
                                        fileDate = File.GetCreationTime(imageURL);

                                        if (nexttask != null)
                                            nextTaskDate = nexttask.Date.HasValue ? nexttask.Date.Value : DateTime.Now;
                                    }
                                    catch { }

                                    if (fileDate >= task.Date && fileDate <= nextTaskDate)
                                    {
                                        byte[] videoArray = File.ReadAllBytes(imageURL);

                                        if (videoArray != null && videoArray.Length > 0)
                                        {
                                            string b64String = Convert.ToBase64String(videoArray);

                                            Utility.WriteLog(b64String);

                                            urls.Add(b64String);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return urls;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public TFMMediaDTO GetTaskMedia(long taskId)
        {
            try
            {
                dbContext = new TFMEntities();

                TFMMediaDTO dto = new TFMMediaDTO();

                var task = dbContext.iPatrolDuties.Where(x => x.Id == taskId).FirstOrDefault();

                if (task == null)
                    return null;

                var taskUser = dbContext.iPatrolDutiesUsers.Where(x => x.DutyId == taskId).FirstOrDefault();

                if (taskUser != null && dbContext.iPatrolFilesSubmittedByUsers.Any(x => x.StaffId == taskUser.StaffId))
                {
                    var files = dbContext.iPatrolFilesSubmittedByUsers.Where(x => x.StaffId == taskUser.StaffId).ToList();

                    foreach (var item in files)
                    {

                    }
                }

                return dto;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public bool UpdatePatrolCurrentTask(Guid patrolOriginalId, long taskId)
        {
            try
            {
                operationalContext = new STCOperationalDataContext();

                var patrol = operationalContext.Patrols.Where(p => p.PatrolOriginalId == patrolOriginalId).FirstOrDefault();

                if (patrol != null)
                {
                    patrol.CurrentTaskId = taskId;
                }

                return operationalContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
        }

        public List<TFMIncident> GetOpenIncidentByPatrolId(Guid patrolId)
        {
            try
            {
                return (from I in dbContext.TFMIncidents
                        where I.PatrolId == patrolId
                        && I.CurrentStatusId != (int)IncidentStatusEnum.Closed
                        orderby I.IncidentDateTime descending
                        select I).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OfficersAttendance> GetCheckedInPatrolsByPatrolId(Guid patrolId)
        {
            try
            {
                var activePatrols = (from patrol in dbContext.OfficersAttendances
                                     where patrol.LoginDateTime != null
                                           && patrol.LogoutDateTime == null
                                           && patrol.PatrolId == patrolId
                                     select patrol).ToList();

                activePatrols = activePatrols.OrderByDescending(M => M.MDTLogoutDateTime)
                                .ThenByDescending(S => !S.SeenAt.HasValue).ThenByDescending(L => L.LoginDateTime).ToList();

                return activePatrols;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Transporter GetPatrolById(Guid PatrolId)
        {
            try
            {
                dbContext = new TFMEntities();

                var patrol = dbContext.Transporters.Where(x => x.ID == PatrolId).FirstOrDefault();

                return patrol;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PatrolOfficersDetails GetPatrolOfficersDetails(Guid patrolId)
        {
            var patrolOfficers = new PatrolOfficersDetails();
            var patrol = GetPatrolById(patrolId);
            if (patrol != null)
            {
                //if (patrol.Branch1 != null)
                //    patrolOfficers.PatrolAllocation = patrol.Branch1.name;
                patrolOfficers.PatrolCode = patrol.code;
                patrolOfficers.PatrolPlateNumber = patrol.plateNo;
                var openIncidents = GetOpenIncidentByPatrolId(patrolId);
                patrolOfficers.IsAvailable = openIncidents == null || openIncidents.Count == 0;
                var officersAttendance = GetCheckedInPatrolsByPatrolId(patrolId);
                if (officersAttendance != null)
                {
                    foreach (var item in officersAttendance)
                    {
                        if (item.Staff != null)
                        {
                            var officer = new StaffPatrolModel();
                            officer.FirstName = item.Staff.FirstName;
                            officer.LastName = item.Staff.LastName;
                            officer.MilitaryNumber = item.Staff.code;
                            officer.ImagePath = string.Format(@"{0}\{1}", item.Staff.subID, item.Staff.image);
                            patrolOfficers.Officers.Add(officer);
                        }
                    }
                }
            }
            return patrolOfficers;
        }

        public PatrolOfficersDetailsDTO GetPatrolDetails(Guid patrolId)
        {
            var patrol = new PatrolOfficersDetailsDTO();
            var entity = GetPatrolOfficersDetails(patrolId);

            if (entity != null)
            {
                patrol.IsAvailable = entity.IsAvailable;
                patrol.PatrolAllocation = entity.PatrolAllocation;
                patrol.PatrolCode = entity.PatrolCode;
                patrol.PatrolPlateNumber = entity.PatrolPlateNumber;
                foreach (var item in entity.Officers)
                {
                    patrol.Officers.Add(new StaffPatrolModel
                    {
                        FirstName = item.FirstName,
                        ImagePath = item.ImagePath,
                        LastName = item.LastName,
                        MilitaryNumber = item.MilitaryNumber
                    });
                }
            }
            return patrol;
        }

        public bool ValidateBeforeAssignPatrol(long notificationId, long patrolId)
        {
            var patrols = new PatrolsDAL().GetAssignedPatrolsByNotificationId(notificationId);

            if (patrols == null)
            {
                return true;
            }

            var patrol = patrols.Where(x => x.PatrolId == patrolId).FirstOrDefault();

            if (patrol == null)
            {
                return true;
            }

            return false;
        }

        public bool IsPatrolAvailable(Guid patrolOriginalId)
        {
            try
            {
                if (dbContext == null)
                    dbContext = new TFMEntities();

                var incident = dbContext.TFMIncidents.Where(x => x.CurrentStatusId != 4 && x.PatrolId == patrolOriginalId).FirstOrDefault();

                if (incident != null)
                    return false;

                var duty = dbContext.iPatrolDuties.Where(x => x.PatrolId == patrolOriginalId && x.FinishTime == null).FirstOrDefault();

                if (duty != null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return true;
            }
        }
    }
}
