using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.Main
{
    public class NotificationBL : IDependencySignalR<NotificationDTO>
    {
        IPublisher<NotificationDTO> _notificationHub = null;
        NotificationDependencyDAL _notificationsDAL = null;
        public NotificationBL(IPublisher<NotificationDTO> notificationHub)
        {
            _notificationHub = notificationHub;
        }
        public void RegisterDependency()
        {
            _notificationsDAL = new NotificationDependencyDAL(this);
            _notificationsDAL.RegisterDependency();
        }
        public void Notify(List<NotificationDTO> changedControls)
        {
            if (_notificationHub != null)
            {
                _notificationHub.Publish(changedControls);
            }
        }
    }
}
