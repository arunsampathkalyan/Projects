using STC.Projects.WCF.ServiceLayer.Request;
using STC.Projects.WCF.ServiceLayer.Response;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    [ServiceContract]
    public interface IADPUTSService
    {
        [OperationContract]
        [WebGet]
        TrafficProfileResponse GetTrafficProfile(TrafficProfileRequest req);
        [OperationContract]
        [WebGet]
        bool GetPersonIsWanted(PersonDetailsRequest req);
        [OperationContract]
        [WebGet]
        bool GetVehicleIsWanted(VehicleDetailsRequest req);
        [OperationContract]
        [WebGet]
        VehicleDetailsResponse GetVehicleDetails(VehicleDetailsRequest req);
        [OperationContract]
        [WebGet]
        TrafficNoResponse GetTrfNoByNID(NationalIDRequest req);
        [OperationContract]
        [WebGet]
        TrafficNoResponse GetTrfNoByUID(UnifiedIDRequest req);
        [OperationContract]
        [WebGet]
        List<TicketsDetailsResponse> GetTicketDetails(TicketsDetailsRequest req);
        [OperationContract]
        [WebGet]
        NewTicketResponse CreateNewTicket(NewTicketRequest req);
        [OperationContract]
        [WebGet]
        LookupRecordResponse[] GetLocationsLookup(string Username, string Password);
        [OperationContract]
        [WebGet]
        LicenseDetailsResponse GetLicenseDetails(LicenseDetailsRequest req);
        [OperationContract]
        [WebGet]
        List<VehiclePlateClassificationsDTO> GetVehiclePlateClassifications();
        [OperationContract]
        [WebGet]
        List<VehiclePlateColorDTO> GetVehiclePlateColor();
        [OperationContract]
        [WebGet]
        List<VehiclePlateKindDTO> GetVehiclePlateKind();
        [OperationContract]
        [WebGet]
        List<VehiclePlateSourceDTO> GetVehiclePlateSource();
        [OperationContract]
        [WebGet]
        List<VehicleViolationClassificationsDTO> GetVehicleViolationClassifications();
        [OperationContract]
        [WebGet]
        List<VehicleViolationInterceptsTypesDTO> GetVehicleViolationInterceptsTypes();
        [OperationContract]
        [WebGet]
        List<VehicleViolationsTypesDTO> GetVehicleViolationsTypes();
        [OperationContract]
        [WebGet]
        List<VehicleUTSTypeDTO> GetVehicleUTSTypes();
        [OperationContract]
        [WebGet]
        List<VehicleModelDTO> GetVehicleUTSModels();
    }
}
