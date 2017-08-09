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
    public class PatrolLocationBL : IDependencySignalR<PatrolLastLocationDTO>
    {
        IPublisher<PatrolLastLocationDTO> _patrolHub = null;
        PatrolTrackDependencyDAL _patrolDAL = null;
        public PatrolLocationBL(IPublisher<PatrolLastLocationDTO> patrolHub)
        {
            _patrolHub = patrolHub;
        }
        public void RegisterDependency()
        {
            _patrolDAL = new PatrolTrackDependencyDAL(this);
            _patrolDAL.RegisterDependency();
        }
        public void Notify(List<PatrolLastLocationDTO> changedLocations)
        {
            if (_patrolHub != null)
            {
                _patrolHub.Publish(changedLocations);
               
            }
        }
    }
}
