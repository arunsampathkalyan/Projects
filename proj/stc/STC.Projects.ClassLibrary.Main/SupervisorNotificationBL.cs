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
    public class SupervisorNotificationBL : IDependencySignalR<SupervisorNotificationDTO>
    {
        IPublisher<SupervisorNotificationDTO> _notificationHub = null;
        SupervisorNotificationDependencyDAL _notificationsDAL = null;
        public SupervisorNotificationBL(IPublisher<SupervisorNotificationDTO> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public void RegisterDependency()
        {
            _notificationsDAL = new SupervisorNotificationDependencyDAL(this);
            _notificationsDAL.RegisterDependency();
        }

        public void UnRegisterDependency()
        {
            _notificationsDAL = new SupervisorNotificationDependencyDAL(this);
            _notificationsDAL.UnRegisterDependency();
        }

        public void Notify(List<SupervisorNotificationDTO> changedControls)
        {
            if (_notificationHub != null)
            {
                _notificationHub.Publish(changedControls);
            }
        }
    }
}
