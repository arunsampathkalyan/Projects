using STC.Projects.ClassLibrary.DAL.Utilities;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class VehicleLiveTrackingDAL
    {
        private DTO.Interfaces.IDependencySignalR<VehicleLiveTrackingDTO> _vehicleLiveTrackingBL;
        private STCOperationalDataContext _operationDB = new STCOperationalDataContext();
        private ImmediateNotificationRegister<VehicleLiveTracking> _notification;
        private string _plateNumber;
        public VehicleLiveTrackingDAL()
        {

        }

        public VehicleLiveTrackingDAL(DTO.Interfaces.IDependencySignalR<VehicleLiveTrackingDTO> vehicleLiveTrackingBL)
        {
            _vehicleLiveTrackingBL = vehicleLiveTrackingBL;
        }

        public void RegisterDependency(string plateNumber)
        {
            try
            {
                _plateNumber = plateNumber;
                var query = _operationDB.VehicleLiveTrackings.Where(x => x.PlateNumber == plateNumber && !x.IsNoticed);

                _notification = new ImmediateNotificationRegister<VehicleLiveTracking>(_operationDB, query);
                _notification.OnChanged += Dependency_OnChange;
            }
            catch
            {

            }
        }

        public void UnRegisterDependency()
        {
            _notification.OnChanged -= Dependency_OnChange;

            WantedVehicle vehicle = _operationDB.WantedVehicles.Where(x => x.PlateNumber == _plateNumber).FirstOrDefault();

            if (vehicle != null)
            {
                vehicle.IsActive = false;

                _operationDB.SaveChanges();
            }
        }

        public void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    var changed = GetUpdated(_plateNumber);
                    if (_vehicleLiveTrackingBL != null && changed != null && changed.Any())
                    {
                        _vehicleLiveTrackingBL.Notify(changed);
                        UpdateChanged(changed);
                    }
                }
            }
            catch
            {

            }
        }

        public void UpdateChanged(List<VehicleLiveTrackingDTO> changed)
        {
            _operationDB = new STCOperationalDataContext();

            foreach (var item in changed)
            {
                var entity = _operationDB.VehicleLiveTrackings.FirstOrDefault(x => x.Id == item.Id);
                entity.IsNoticed = true;
            }
            _operationDB.SaveChanges();
        }

        private List<VehicleLiveTrackingDTO> GetUpdated(string plateNumber)
        {
            try
            {
                return GetUpdatedVehicles(plateNumber, false);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public VehicleLiveTrackingDTO GetVehicleDetailsByPlateNumber(string plateNumber)
        {
            var output = _operationDB.VehicleLiveTrackings
                        .Where(vehicle => vehicle.PlateNumber == plateNumber)
                        .Select(vehicle => new VehicleLiveTrackingDTO
                        {
                            Id = vehicle.Id,
                            PlateNumber = vehicle.PlateNumber,
                            PlateKind = vehicle.PlateKind,
                            PlateType = vehicle.PlateType,
                            PlateSource = vehicle.PlateSource,
                            PlateColor = vehicle.PlateColor,
                            LicenseNumber = vehicle.LicenseNumber,
                            LicenseExpiryDate = vehicle.LicenseExpiryDate,
                            Model = vehicle.Model,
                            OwnerName = vehicle.OwnerName,
                            OwnerMobileNumber = vehicle.OwnerMobileNumber,
                            OwnerNationality = vehicle.OwnerNationality,
                            OwnerAge = vehicle.OwnerAge,
                            TowerId = vehicle.TowerId,
                            Latitude = vehicle.Latitude,
                            Longitude = vehicle.Longitude,
                            CaptureTime = vehicle.CaptureTime
                        }).FirstOrDefault();

            return output;
        }

        public VehicleLiveTrackingDTO GetVehicleDetailsByPlateNumber(string plateNumber, string plateKind, string plateType, string plateSource, string plateColor)
        {
            var output = _operationDB.VehicleLiveTrackings
                        .Where(vehicle => vehicle.PlateNumber == plateNumber && vehicle.PlateKind == plateKind && vehicle.PlateType == plateType && vehicle.PlateSource == plateSource && vehicle.PlateColor == plateColor)
                        .Select(vehicle => new VehicleLiveTrackingDTO
                        {
                            Id = vehicle.Id,
                            PlateNumber = vehicle.PlateNumber,
                            LicenseNumber = vehicle.LicenseNumber,
                            LicenseExpiryDate = vehicle.LicenseExpiryDate,
                            Model = vehicle.Model,
                            OwnerName = vehicle.OwnerName,
                            OwnerMobileNumber = vehicle.OwnerMobileNumber,
                            OwnerNationality = vehicle.OwnerNationality,
                            OwnerAge = vehicle.OwnerAge,
                            TowerId = vehicle.TowerId,
                            Latitude = vehicle.Latitude,
                            Longitude = vehicle.Longitude,
                            CaptureTime = vehicle.CaptureTime
                        }).FirstOrDefault();

            return output;
        }

        public List<VehicleLiveTrackingDTO> GetAllWantedCarPathInPeriod(string plateNumber, int maxHour)
        {
            var output = _operationDB.VehicleLiveTrackings
                        .Where(vehicle => vehicle.PlateNumber == plateNumber && vehicle.CaptureTime.HasValue)
                        .Select(vehicle => new VehicleLiveTrackingDTO
                        {
                            Id = vehicle.Id,
                            PlateNumber = vehicle.PlateNumber,
                            PlateKind = vehicle.PlateKind,
                            PlateType = vehicle.PlateType,
                            PlateSource = vehicle.PlateSource,
                            PlateColor = vehicle.PlateColor,
                            LicenseNumber = vehicle.LicenseNumber,
                            LicenseExpiryDate = vehicle.LicenseExpiryDate,
                            Model = vehicle.Model,
                            OwnerName = vehicle.OwnerName,
                            OwnerMobileNumber = vehicle.OwnerMobileNumber,
                            OwnerNationality = vehicle.OwnerNationality,
                            OwnerAge = vehicle.OwnerAge,
                            TowerId = vehicle.TowerId,
                            Latitude = vehicle.Latitude,
                            Longitude = vehicle.Longitude,
                            CaptureTime = vehicle.CaptureTime
                        }).ToList();

            return output.Where(x => DateTime.Now.Subtract(x.CaptureTime.Value).TotalHours <= maxHour).ToList();
        }

        public List<VehicleLiveTrackingDTO> GetUpdatedVehicles(string plateNumber, bool IsNoticed)
        {
            try
            {
                var output = _operationDB.VehicleLiveTrackings
                                       .Where(vehicle => vehicle.PlateNumber == plateNumber && vehicle.IsNoticed == IsNoticed)
                                       .Select(vehicle => new VehicleLiveTrackingDTO
                                       {
                                           Id = vehicle.Id,
                                           PlateNumber = vehicle.PlateNumber,
                                           LicenseNumber = vehicle.LicenseNumber,
                                           LicenseExpiryDate = vehicle.LicenseExpiryDate,
                                           Model = vehicle.Model,
                                           OwnerName = vehicle.OwnerName,
                                           OwnerMobileNumber = vehicle.OwnerMobileNumber,
                                           OwnerNationality = vehicle.OwnerNationality,
                                           OwnerAge = vehicle.OwnerAge,
                                           TowerId = vehicle.TowerId,
                                           Latitude = vehicle.Latitude,
                                           Longitude = vehicle.Longitude,
                                           CaptureTime = vehicle.CaptureTime
                                       }).ToList();

                return output;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
