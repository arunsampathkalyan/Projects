using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.Entities;
using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.DAL
{
    public class SupervisorNotificationDependencyDAL
    {
        private DTO.Interfaces.IDependencySignalR<SupervisorNotificationDTO> _notificationBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<SupervisorNotification> _notification;

        public SupervisorNotificationDependencyDAL(DTO.Interfaces.IDependencySignalR<SupervisorNotificationDTO> notificationBL)
        {
            _notificationBL = notificationBL;
        }

        public void RegisterDependency()
        {
            try
            {
                var queryReceiver = _operationDB.SupervisorNotifications.Where(x => !x.IsNoticed.Value);
                _notification = new ImmediateNotificationRegister<SupervisorNotification>(_operationDB, queryReceiver);
                _notification.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }

        public void UnRegisterDependency()
        {
            _notification.OnChanged -= dependency_OnChange;
        }

        public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    if (_notificationBL != null)
                    {
                        var changed = GetUpdated();

                        _notificationBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }

        public void UpdateChanged(List<SupervisorNotificationDTO> changed)
        {
            try
            {
                //_operationDB = new STCOperationalDataContext();

                //foreach (var item in changed)
                //{
                //    var entity = _operationDB.SupervisorNotifications.FirstOrDefault(x => x.SupervisorNotificationId == item.SupervisorNoticationId);
                //    entity.IsNoticed = true;
                //}
                //_operationDB.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private List<SupervisorNotificationDTO> GetUpdated()
        {
            try
            {
                var notificationsDAL = new SupervisorNotificationDAL();

                return notificationsDAL.GetChangedSupervisorNotifications();
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
