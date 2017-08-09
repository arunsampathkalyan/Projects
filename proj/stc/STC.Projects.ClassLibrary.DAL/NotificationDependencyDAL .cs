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
    public class NotificationDependencyDAL 
    {
        private DTO.Interfaces.IDependencySignalR<NotificationDTO> _notificationBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<Notification> _notification;
        public NotificationDependencyDAL(DTO.Interfaces.IDependencySignalR<NotificationDTO> notificationBL)
        {
            _notificationBL = notificationBL;
        }
       
        public void RegisterDependency()
        {
            try
            {
                var query = _operationDB.Notifications.Where(x => x.IsNoticed.HasValue && x.IsNoticed.Value == false);
                
                _notification = new ImmediateNotificationRegister<Notification>(_operationDB, query);
                _notification.OnChanged += dependency_OnChange;
            }
            catch (Exception ex)
            {
                // Use Elmah To Record Exception Here
            }
        }
       
        public void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    var changed = GetUpdated();
                    changed = changed.Where(x => x.LastStatus > 1).ToList();
                    if (_notificationBL != null && changed != null && changed.Any())
                    {
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

        public void UpdateChanged(List<NotificationDTO> changed)
        {
            try
            {
                _operationDB = new STCOperationalDataContext();

                foreach (var item in changed)
                {
                    var entity = _operationDB.Notifications.FirstOrDefault(x => x.NotificationId == item.NotificationId);
                    entity.IsNoticed = true;
                }
                _operationDB.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }

        private List<NotificationDTO> GetUpdated()
        {
            try {
                var notificationsDAL = new NotificationDAL();

                return notificationsDAL.GetChangedNotifications();
            }
            catch (Exception ex) {
                // Use Elmah To Record Exception Here
            }
            return null;
        }
    }
}
