using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DAL;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DTO.Interfaces;

namespace STC.Projects.ClassLibrary.Main
{
    public class VehicleLiveTrackingBL : IDependencySignalR<VehicleLiveTrackingDTO>
    {
        IPublisher<VehicleLiveTrackingDTO> _vehicleLiveTrackingHub = null;
        VehicleLiveTrackingDAL vehicleLiveTrackingDAL = null;

        public VehicleLiveTrackingBL(IPublisher<VehicleLiveTrackingDTO> vehicleLiveTrackingHub)
        {
            _vehicleLiveTrackingHub = vehicleLiveTrackingHub;
        }

        public void RegisterDependency() { }

        public void RegisterDependency(string plateNumber)
        {
            if (vehicleLiveTrackingDAL == null)
                vehicleLiveTrackingDAL = new VehicleLiveTrackingDAL(this);

            vehicleLiveTrackingDAL.RegisterDependency(plateNumber);
        }

        public void UnRegisterDependency()
        {
            if (vehicleLiveTrackingDAL == null)
                vehicleLiveTrackingDAL = new VehicleLiveTrackingDAL(this);

            vehicleLiveTrackingDAL.UnRegisterDependency();
        }

        public void Notify(List<VehicleLiveTrackingDTO> changedLocations)
        {
            if (_vehicleLiveTrackingHub != null)
            {
                _vehicleLiveTrackingHub.Publish(changedLocations);
            }
        }
    }
}
