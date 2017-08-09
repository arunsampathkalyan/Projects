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
    public class BusinessRulesBL : IDependencySignalR<BusinessRulesDTO>
    {
        IPublisher<BusinessRulesDTO> _notificationHub = null;
        BusinessRuleDependencyDAL _notificationsDAL = null;
        public BusinessRulesBL(IPublisher<BusinessRulesDTO> notificationHub)
        {
            _notificationHub = notificationHub;
        }
        public void RegisterDependency()
        {
            _notificationsDAL = new BusinessRuleDependencyDAL(this);
            _notificationsDAL.RegisterDependency();
        }
        public void Notify(List<BusinessRulesDTO> changedControls)
        {
            if (_notificationHub != null)
            {
                _notificationHub.Publish(changedControls);
            }
        }
    }
}
