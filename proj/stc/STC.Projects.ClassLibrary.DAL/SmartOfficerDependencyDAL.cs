using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DTO.Interfaces;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SmartOfficerDependencyDAL
    {
        private IDependencySignalR<OfficerTaskDTO> _SmartOfficerBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<OfficerTask> _OfficerTask;

        public SmartOfficerDependencyDAL(IDependencySignalR<OfficerTaskDTO> smartOfficerBL)
        {
            _SmartOfficerBL = smartOfficerBL;
        }


        public void RegisterDependency()
        {
            try
            {
                var queryReceiver = _operationDB.OfficerTask.Where(x => !x.IsNoticed.Value);
                _OfficerTask = new ImmediateNotificationRegister<OfficerTask>(_operationDB, queryReceiver);
                _OfficerTask.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {

            }
        }

        public void UnRegisterDependency()
        {
            _OfficerTask.OnChanged -= dependency_OnChange;
        }

        public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    if (_SmartOfficerBL != null)
                    {
                        var changed = GetUpdated();

                        _SmartOfficerBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateChanged(List<OfficerTaskDTO> changed)
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                foreach (var item in changed)
                {
                    var entity = _operationDB.OfficerTask.FirstOrDefault(x => x.OfficerTaskId == item.OfficerTaskId);

                    if (entity != null)
                        entity.IsNoticed = true;
                }

                _operationDB.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private List<OfficerTaskDTO> GetUpdated()
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                var lstChanged = _operationDB.OfficerTask.Where(x => x.IsNoticed == false).Select(x => new OfficerTaskDTO()
                {
                    OfficerMilitaryId = x.OfficerMilitaryId,
                    Latitude = x.Latitude.HasValue ? x.Latitude.Value : 0,
                    Longitude = x.Longitude.HasValue ? x.Longitude.Value : 0,
                    OfficerTaskId = x.OfficerTaskId,
                    TaskMessage = x.TaskMessage,
                    TaskTime = x.TaskTime.HasValue ? x.TaskTime.Value : DateTime.Now,
                    UserId = x.UserId.HasValue ? x.UserId.Value : 0
                }).ToList();

                return lstChanged;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
