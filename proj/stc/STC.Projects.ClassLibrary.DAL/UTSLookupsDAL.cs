using STC.Projects.ClassLibrary.DTO;
using System.Collections.Generic;
using System.Linq;
using STC.Projects.ClassLibrary.Entities;

namespace STC.Projects.ClassLibrary.DAL
{
    public class UTSLookupsDAL
    {
        STCOperationalDataContext _operation = new STCOperationalDataContext();

        public List<VehiclePlateClassificationsDTO> GetVehiclePlateClassifications()
        {
            var output = new List<VehiclePlateClassificationsDTO>();

            output = _operation.VehiclePlateClassifications.Select(x => new VehiclePlateClassificationsDTO()
            {
                Id = x.Id,
                Code = x.Code.Value,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehiclePlateColorDTO> GetVehiclePlateColor()
        {
            var output = new List<VehiclePlateColorDTO>();

            output = _operation.VehiclePlateColors.Select(x => new VehiclePlateColorDTO()
            {
                Id = x.Id,
                Code = x.Code.Value,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehicleUTSTypeDTO> GetVehicleUTSTypes()
        {
            var output = new List<VehicleUTSTypeDTO>();

            output = _operation.VehicleUTSTypes.Select(x => new VehicleUTSTypeDTO()
            {
                Id = x.VET_ID,
                Code = x.VET_CODE.Value,
                Name = x.VET_DESC_AR
            }).ToList();

            return output;
        }

        public List<VehicleModelDTO> GetVehicleUTSModels()
        {
            var output = new List<VehicleModelDTO>();

            output = _operation.VehicleMakes.Select(x => new VehicleModelDTO()
            {
                Id = x.VEM_ID,
                Code = x.VEM_CODE.Value,
                Name = x.VEM_DESC_AR
            }).ToList();

            return output;
        }

        public List<VehiclePlateKindDTO> GetVehiclePlateKind()
        {
            var output = new List<VehiclePlateKindDTO>();

            output = _operation.VehiclePlateKinds.Select(x => new VehiclePlateKindDTO()
            {
                Id = x.Id,
                Code = x.Code.Value,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehiclePlateSourceDTO> GetVehiclePlateSource()
        {
            var output = new List<VehiclePlateSourceDTO>();

            output = _operation.VehiclePlateSources.Select(x => new VehiclePlateSourceDTO()
            {
                Id = x.Id,
                Code = x.Code.Value,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehicleViolationClassificationsDTO> GetVehicleViolationClassifications()
        {
            var output = new List<VehicleViolationClassificationsDTO>();

            output = _operation.VehicleViolationClassifications.Select(x => new VehicleViolationClassificationsDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehicleViolationInterceptsTypesDTO> GetVehicleViolationInterceptsTypes()
        {
            var output = new List<VehicleViolationInterceptsTypesDTO>();

            output = _operation.VehicleViolationInterceptsTypes.Select(x => new VehicleViolationInterceptsTypesDTO()
            {
                Id = x.Id,
                Duration = x.Duration.Value,
                Name = x.Name
            }).ToList();

            return output;
        }

        public List<VehicleViolationsTypesDTO> GetVehicleViolationsTypes()
        {
            var output = new List<VehicleViolationsTypesDTO>();

            output = _operation.VehicleViolationsTypes.Select(x => new VehicleViolationsTypesDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code == null ? 0 : x.Code.Value,
                TrafficPoint = x.TrafficPoint == null ? 0 : x.TrafficPoint.Value,
                FineValue = x.FineValue == null ? 0 : x.FineValue.Value,
                ViolationDuration = x.ViolationDuration == null ? 0 : x.ViolationDuration.Value,
                PresenseAbsenceStatus = x.PresenceAbsenceStatus == null ? 0 : x.PresenceAbsenceStatus.Value,
                ViolationClassficationId = x.ViolationClassificationId == null ? 0 : x.ViolationClassificationId.Value
            }).ToList();

            return output;
        }
    }
}
