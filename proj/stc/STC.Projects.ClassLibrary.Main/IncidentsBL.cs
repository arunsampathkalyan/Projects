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
    public class IncidentsBL : IDependencySignalR<IncidentsDTO>
    {
        IPublisher<IncidentsDTO> _incidentHub=null;
        IncidentsDependencyDAL _incidentDAL = null;
        public IncidentsBL(IPublisher<IncidentsDTO> incidentHub)
        {
            _incidentHub = incidentHub;
        }
        public void RegisterDependency()
        {
            _incidentDAL = new IncidentsDependencyDAL(this);
            _incidentDAL.RegisterDependency();
        }
        public void Notify(List<IncidentsDTO> changedIncidents)
        {
            if (_incidentHub != null)
            {
                _incidentHub.Publish(changedIncidents);
               
            }
        }
    }
}
