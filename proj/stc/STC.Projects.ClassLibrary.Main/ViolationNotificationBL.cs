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
    public class ViolationNotificationBL : IDependencySignalR<ViolationNotificationDTO>
    {
        IPublisher<ViolationNotificationDTO> _violationHub = null;
        ViolationDependencyDAL _violationDAL = null;
        public ViolationNotificationBL(IPublisher<ViolationNotificationDTO> violationHub)
        {
            _violationHub = violationHub;
        }
        public void RegisterDependency()
        {
            _violationDAL = new ViolationDependencyDAL(this);
            _violationDAL.RegisterDependency();
        }
        public void Notify(List<ViolationNotificationDTO> changedViolations)
        {
            if (_violationHub != null)
            {
                _violationHub.Publish(changedViolations);
                //if(_violationDAL != null)
                //    _violationDAL.UpdateNoticed(changedViolations);
            }
        }
    }
}
