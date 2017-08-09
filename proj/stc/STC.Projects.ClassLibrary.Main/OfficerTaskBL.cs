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
    public class OfficerTaskBL : IDependencySignalR<OfficerTaskDTO>
    {
        IPublisher<OfficerTaskDTO> _SmartOfficerHub = null;
        SmartOfficerDependencyDAL _SmartOfficerDAL = null;
        public OfficerTaskBL(DTO.Interfaces.IPublisher<OfficerTaskDTO> smartOfficerHub)
        {
            _SmartOfficerHub = smartOfficerHub;
        }

        public void Notify(List<OfficerTaskDTO> changedObjects)
        {
            if (_SmartOfficerHub != null)
                _SmartOfficerHub.Publish(changedObjects);
        }

        public void RegisterDependency()
        {
            _SmartOfficerDAL = new SmartOfficerDependencyDAL(this);

            _SmartOfficerDAL.RegisterDependency();
        }

        public void UnRegisterDependency()
        {
            _SmartOfficerDAL = new SmartOfficerDependencyDAL(this);

            _SmartOfficerDAL.UnRegisterDependency();
        }
    }
}
