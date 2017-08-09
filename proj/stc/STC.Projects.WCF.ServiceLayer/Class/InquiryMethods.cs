using STC.Projects.WCF.ServiceLayer.ADPLicenseServiceReference;
using STC.Projects.WCF.ServiceLayer.Request;
using STC.Projects.WCF.ServiceLayer.Response;
using STC.Projects.WCF.ServiceLayer.ADPTicketsServiceReference;
using STC.Projects.WCF.ServiceLayer.ADPTrafficProfileReference;
using STC.Projects.WCF.ServiceLayer.ADPUtilitiesReference;
using STC.Projects.WCF.ServiceLayer.ADPVehiclesReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    public class InquiryMethods
    {
        string userId = Utility.GetUTSUserId();
        string username = Utility.GetUTSUserName();
        string password = Utility.GetUTSPassword();
        int systemCode = Utility.GetUTSSystemCode();

        public VehicleDetailsResponse GetVehicleDetails(VehicleDetailsRequest req)
        {
            try
            {
                ADPVehiclesReference.VehiclesServicesSoap obj =
                  new VehiclesServicesSoapClient();

                var header = new ADPVehiclesReference.ADPSoapHeaderIn()
                {

                    UserName = username,
                    Password = password
                };

                var inner = new GetVehicleDetailsRequest()
                {
                    UserID = userId,
                    SystemCode = systemCode
                };

                //plate info as null
                //if (req.ChassisNoExist)
                if (true)
                {
                    inner.PlateInfo = new ADPVehiclesReference.PlateKey()
                    {
                        PlateNo = req.PlateNo,
                        PlateOrgNo = req.PlateOrgNo,
                        PlateColorCode = req.PlateColorCode,
                        PlateKindCode = req.PlateKindCode,
                        PlateTypeCode = req.PlateTypeCode,
                        PlateSourceCode = req.PlateSourceCode
                    };
                }

                if (!req.ChassisNoExist)
                    inner.ChassisNo = req.ChassisNo;


                var objToSend = new getVehicleDetailsRequest1(header, inner);//header,
                var result = obj.getVehicleDetails(objToSend);
                Utility.WriteLog("After Execution");
                if (result == null)
                {
                    Utility.WriteLog("Object Is Null");
                    return null;
                }
                else
                {
                    Utility.WriteLog("Object Is not Null");
                    return result.getVehicleDetailsResult.ConvertToVehicleDetails();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return null;
            }
        }
        public bool GetVehicleIsWanted(VehicleDetailsRequest req)
        {
            try
            {
                ADPVehiclesReference.VehiclesServicesSoap obj =
                 new VehiclesServicesSoapClient();
                var header = new ADPVehiclesReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };

                var inner = new CheckVehicleWantedRequest()
                {
                    UserID = userId,
                    SystemCode = systemCode
                };

                //plate info as null
                if (!req.ChassisNoExist)
                {
                    inner.PlateInfo = new ADPVehiclesReference.PlateKey()
                    {
                        PlateNo = req.PlateNo,
                        PlateOrgNo = req.PlateOrgNo,
                        PlateColorCode = req.PlateColorCode,
                        PlateKindCode = req.PlateKindCode,
                        PlateTypeCode = req.PlateTypeCode,
                        PlateSourceCode = req.PlateSourceCode,

                    };
                }

                if (!req.ChassisNoExist)
                    inner.ChassisNo = req.ChassisNo;

                var objToSend = new checkVehicleWantedRequest1(header, inner);//header
                var result = obj.checkVehicleWanted(objToSend);


                if (result == null)
                {
                    return false;
                }
                else
                {
                    return result.checkVehicleWantedResult.IsWanted;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool GetPersonIsWanted(PersonDetailsRequest req)
        {
            try
            {
                TrafficProfileServicesSoap obj = new TrafficProfileServicesSoapClient();

                var header = new ADPTrafficProfileReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };

                var inner = new CheckWantedPersonRequest()
                {
                    TcfNo = req.TcfNo,
                    UnifiedID = req.UnifiedId,
                    UserID = userId,
                    SystemCode = systemCode
                };

                var objToSend = new CheckWantedPersonRequest1(header, inner);//header,
                var result = obj.CheckWantedPerson(objToSend);

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return result.CheckWantedPersonResult.IsWantedPerson;
                }
            }
            catch
            {
                return false;
            }
        }
        public TrafficProfileResponse GetTrafficProfile(TrafficProfileRequest req)
        {
            try
            {
                TrafficProfileServicesSoap obj = new TrafficProfileServicesSoapClient();

                var header = new ADPTrafficProfileReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };

                var inner = new GetTrafficProfileRequest()
                {
                    TcfNo = req.TcfNo,
                    UserID = userId,
                    SystemCode = systemCode
                };

                var objToSend = new GetTrafficProfileRequest1(header, inner);//header,
                var result = obj.GetTrafficProfile(objToSend);

                if (result.GetTrafficProfileResult == null)
                {
                    return null;
                }
                else
                {
                    return result.GetTrafficProfileResult.ConvertToTrafficProfile();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<TicketsDetailsResponse> GetTicketDetails(TicketsDetailsRequest req)
        {
            try
            {
                TicketsServicesSoap ticketSoap = new TicketsServicesSoapClient();

                var reqObj = new GetTicketsRequest()
                {
                    PlateInfo = new ADPTicketsServiceReference.PlateKey()
                    {
                        PlateNo = req.PlateNo,
                        PlateOrgNo = req.PlateOrgNo,
                        PlateColorCode = req.PlateColorCode,
                        PlateKindCode = req.PlateKindCode,
                        PlateTypeCode = req.PlateTypeCode,
                        PlateSourceCode = req.PlateSourceCode
                    },
                    LicenseInfo = new ADPTicketsServiceReference.LicenseKey()
                    {
                        LicenseNo = req.LicenseNumber,
                        LicenseSourceCode = req.LicenseSourceCode
                    },
                    TrafficProfile = new ADPTicketsServiceReference.TrafficNo()
                    {
                        TcfNo = req.TcfNo
                    },

                    DateFrom = req.DateFrom,
                    DateTo = req.DateTo,
                    SystemCode = systemCode,
                    UserID = userId
                };

                var header = new ADPTicketsServiceReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };

                var result = ticketSoap.GetTickets(new GetTicketsRequest1 { request = reqObj, ADPSoapHeaderIn = header });

                if (result != null)
                {
                    return result.GetTicketsResult.ConvertToTicketsDetails();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);

                return null;
            }
        }
        //============================================

        public TrafficNoResponse GetTrfNoByNID(NationalIDRequest req)
        {
            try
            {
                TrafficProfileServicesSoap obj = new TrafficProfileServicesSoapClient();

                var header = new ADPTrafficProfileReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };
                var objToSend = new GetTrfNoByNationalIDRequest()
                {
                    NationalNo = req.NationalNo,
                    UserID = userId,
                    SystemCode = systemCode
                };

                var result = obj.GetTrfNoByNationalID(new GetTrfNoByNationalIDRequest1 { ADPSoapHeaderIn = header, request = objToSend });

                if (result != null)
                {
                    return new TrafficNoResponse { TcfNo = result.GetTrfNoByNationalIDResult.TcfNo };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public TrafficNoResponse GetTrfNoByUID(UnifiedIDRequest req)
        {
            try
            {
                TrafficProfileServicesSoap obj = new TrafficProfileServicesSoapClient();
                var header = new ADPTrafficProfileReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };
                var objToSend = new GetTrfNoByUnifiedIDRequest
                {
                    UnifiedID = req.UnifiedID,
                    UserID = userId,
                    SystemCode = systemCode
                };

                var result = obj.GetTrfNoByUnifiedID(new GetTrfNoByUnifiedIDRequest1 { ADPSoapHeaderIn = header, request = objToSend });

                if (result != null)
                {
                    return new TrafficNoResponse { TcfNo = result.GetTrfNoByUnifiedIDResult.TcfNo };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public NewTicketResponse CreateNewTicket(NewTicketRequest req)
        {
            try
            {
                TicketsServicesSoap ticketSoap = new TicketsServicesSoapClient();

                var reqObj = new CreateNewTicketRequest()
                {
                    UserID = userId,
                    SystemCode = systemCode,
                    TicketDateTime = req.TicketDateTime,
                    TicketType = req.TicketType,
                    LocationCode = req.LocationCode,
                    PlateInfo = req.PlateInfo.ConvertToPlateKeysService(),
                    MaterialsCodes = req.MaterialsCodes,

                    //Location = req.TicketLocation
                };

                var header = new ADPTicketsServiceReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };
                var result = ticketSoap.CreateNewTicket(new CreateNewTicketRequest1 { request = reqObj, ADPSoapHeaderIn = header });

                if (result != null)
                {
                    return new NewTicketResponse { TicketNo = result.CreateNewTicketResult.TicketNo, TicketSourceCode = result.CreateNewTicketResult.TicketSourceCode };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public LookupRecordResponse[] GetLocationsLookup(string Username, string Password)
        {
            try
            {
                UtilitiesSoap utiltiy = new UtilitiesSoapClient();

                var header = new ADPUtilitiesReference.ADPSoapHeaderIn()
                {
                    UserName = Username,
                    Password = Password
                };

                GetLookupRequest InnerReq = new GetLookupRequest()
                {
                    LookupName = "ALL_LOCATIONS",
                    SystemCode = 0,
                    UserID = "nn"
                };
                var req = new getLookupRequest1 { request = InnerReq, ADPSoapHeaderIn = header };
                var result = utiltiy.getLookup(req);

                if (result != null)
                    return result.getLookupResult.LookUpRecords.Select(l => l.ConvertToLookupRecordResponse()).ToArray();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LicenseDetailsResponse GetLicenseDetails(LicenseDetailsRequest req)
        {
            try
            {
                LicenseServicesSoap LicenseSoap = new LicenseServicesSoapClient();
                var header = new ADPLicenseServiceReference.ADPSoapHeaderIn()
                {
                    UserName = username,
                    Password = password
                };
                var InnerReq = new GetLicenseDetailsRequest()
                {
                    UserID = userId,
                    SystemCode = systemCode,

                    LicenseInfo = req.LicenseInfo.ConvetToLicenseKeyService()
                };
                var ObjReq = new getLicenseDetailsRequest1
                {
                    ADPSoapHeaderIn = header,
                    request = InnerReq
                };

                var result = LicenseSoap.getLicenseDetails(ObjReq);

                if (result != null)
                {
                    return result.getLicenseDetailsResult.ConvertToLicenseDetailsResponse();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}