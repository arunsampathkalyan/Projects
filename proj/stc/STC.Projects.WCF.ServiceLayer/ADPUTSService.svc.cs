using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL;
using STC.Projects.WCF.ServiceLayer.Class;
using STC.Projects.WCF.ServiceLayer.Request;
using STC.Projects.WCF.ServiceLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    public class ADPUTSService : IADPUTSService
    {
        InquiryMethods inquiry;
        public ADPUTSService()
        {
            inquiry = new InquiryMethods();
        }

        public VehicleDetailsResponse GetVehicleDetails(VehicleDetailsRequest req)
        {
            try
            {
                return inquiry.GetVehicleDetails(req);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return null;
            }
        }
        public bool GetVehicleIsWanted(VehicleDetailsRequest req)
        {
            return inquiry.GetVehicleIsWanted(req);
        }
        public bool GetPersonIsWanted(PersonDetailsRequest req)
        {
            return inquiry.GetPersonIsWanted(req);
        }
        public TrafficProfileResponse GetTrafficProfile(TrafficProfileRequest req)
        {
            return inquiry.GetTrafficProfile(req);
        }
        public TrafficNoResponse GetTrfNoByNID(NationalIDRequest req)
        {
            return inquiry.GetTrfNoByNID(req);
        }
        public TrafficNoResponse GetTrfNoByUID(UnifiedIDRequest req)
        {
            return inquiry.GetTrfNoByUID(req);
        }
        public List<TicketsDetailsResponse> GetTicketDetails(TicketsDetailsRequest req)
        {
            return inquiry.GetTicketDetails(req);
        }
        public NewTicketResponse CreateNewTicket(NewTicketRequest req)
        {
            return inquiry.CreateNewTicket(req);
        }
        public LookupRecordResponse[] GetLocationsLookup(string Username, string Password)
        {
            return inquiry.GetLocationsLookup(Username, Password);
        }
        public LicenseDetailsResponse GetLicenseDetails(LicenseDetailsRequest req)
        {
            return inquiry.GetLicenseDetails(req);
        }


        public List<VehiclePlateClassificationsDTO> GetVehiclePlateClassifications()
        {
            return new UTSLookupsDAL().GetVehiclePlateClassifications();
        }

        public List<VehiclePlateColorDTO> GetVehiclePlateColor()
        {
            return new UTSLookupsDAL().GetVehiclePlateColor();
        }

        public List<VehiclePlateKindDTO> GetVehiclePlateKind()
        {
            return new UTSLookupsDAL().GetVehiclePlateKind();
        }

        public List<VehiclePlateSourceDTO> GetVehiclePlateSource()
        {
            return new UTSLookupsDAL().GetVehiclePlateSource();
        }

        public List<VehicleViolationClassificationsDTO> GetVehicleViolationClassifications()
        {
            return new UTSLookupsDAL().GetVehicleViolationClassifications();
        }

        public List<VehicleViolationInterceptsTypesDTO> GetVehicleViolationInterceptsTypes()
        {
            return new UTSLookupsDAL().GetVehicleViolationInterceptsTypes();
        }

        public List<VehicleViolationsTypesDTO> GetVehicleViolationsTypes()
        {
            return new UTSLookupsDAL().GetVehicleViolationsTypes();
        }

        public List<VehicleUTSTypeDTO> GetVehicleUTSTypes()
        {
            return new UTSLookupsDAL().GetVehicleUTSTypes();
        }

        public List<VehicleModelDTO> GetVehicleUTSModels()
        {
            return new UTSLookupsDAL().GetVehicleUTSModels();
        }
    }
}
