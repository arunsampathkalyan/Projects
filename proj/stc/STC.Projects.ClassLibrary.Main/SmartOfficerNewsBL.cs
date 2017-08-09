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
    public class SmartOfficerNewsBL : IDependencySignalR<SmartOfficerNewsDTO>
    {
        IPublisher<SmartOfficerNewsDTO> _smartOfficerNewsHub = null;
        SmartOfficerNewsDependencyDAL _smartOfficerNewsDal = null;

        public SmartOfficerNewsBL(IPublisher<SmartOfficerNewsDTO> smartOfficerNewsHub)
        {
            _smartOfficerNewsHub = smartOfficerNewsHub;
        }

        public void RegisterDependency()
        {
            _smartOfficerNewsDal = new SmartOfficerNewsDependencyDAL(this);
            _smartOfficerNewsDal.RegisterDependency();
        }
        public void Notify(List<SmartOfficerNewsDTO> changedControls)
        {
            if (_smartOfficerNewsHub != null)
            {
                _smartOfficerNewsHub.Publish(changedControls);
            }
        }
    }
}
