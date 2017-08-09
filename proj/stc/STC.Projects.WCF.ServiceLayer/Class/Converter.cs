using STC.Projects.WCF.ServiceLayer.Response;
using STC.Projects.WCF.ServiceLayer.ADPVehiclesReference;
using STC.Projects.WCF.ServiceLayer.ADPTrafficProfileReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    public static class Converter
    {
        public static PlateKey ConvertToPlateKey(this ADPVehiclesReference.PlateKey input)
        {
            return new PlateKey
            {
                PlateColorArabicDesc = input.PlateColorArabicDesc,
                PlateColorCode = input.PlateColorCode,
                PlateColorEnglishDesc = input.PlateColorEnglishDesc,
                PlateKindArabicDesc = input.PlateKindArabicDesc,
                PlateKindCode = input.PlateKindCode,
                PlateKindEnglishDesc = input.PlateKindEnglishDesc,
                PlateNo = input.PlateNo,
                PlateOrgNo = input.PlateOrgNo,
                PlateSourceArabicDesc = input.PlateSourceArabicDesc,
                PlateSourceCode = input.PlateSourceCode,

                PlateSourceEnglishDesc = input.PlateSourceEnglishDesc,
                PlateTypeArabicDesc = input.PlateTypeArabicDesc,
                PlateTypeCode = input.PlateTypeCode,
                PlateTypeEnglishDesc = input.PlateTypeEnglishDesc
            };
        }
        public static ADPTicketsServiceReference.PlateKey ConvertToPlateKeysService(this PlateKey input)
        {
            return new ADPTicketsServiceReference.PlateKey
            {
                PlateColorArabicDesc = input.PlateColorArabicDesc,
                PlateColorCode = input.PlateColorCode,
                PlateColorEnglishDesc = input.PlateColorEnglishDesc,
                PlateKindArabicDesc = input.PlateKindArabicDesc,
                PlateKindCode = input.PlateKindCode,
                PlateKindEnglishDesc = input.PlateKindEnglishDesc,
                PlateNo = input.PlateNo,
                PlateOrgNo = input.PlateOrgNo,
                PlateSourceArabicDesc = input.PlateSourceArabicDesc,
                PlateSourceCode = input.PlateSourceCode,

                PlateSourceEnglishDesc = input.PlateSourceEnglishDesc,
                PlateTypeArabicDesc = input.PlateTypeArabicDesc,
                PlateTypeCode = input.PlateTypeCode,
                PlateTypeEnglishDesc = input.PlateTypeEnglishDesc
            };
        }
        public static VehicleDetailsResponse ConvertToVehicleDetails(this VehicleDetails input)
        {
            return new VehicleDetailsResponse
            {
                AxisCount = input.AxisCount,
                Chairs = input.Chairs,
                ChassisNo = input.ChassisNo,
                ColorArabicDesc = input.ColorArabicDesc,
                ColorCode = input.ColorCode,
                ColorEnglishDesc = input.ColorEnglishDesc,
                Cylinder = input.Cylinder,
                DoorCount = input.DoorCount,
                EngineNo = input.EngineNo,
                FuelArabicDesc = input.FuelArabicDesc,
                FuelCode = input.FuelCode,
                FuelEnglishDesc = input.FuelEnglishDesc,
                GearArabicDesc = input.GearArabicDesc,
                GearCode = input.GearCode,
                GearEnglishDesc = input.GearEnglishDesc,
                HasColorGlassPermit = input.HasColorGlassPermit,
                HasHandicapPermit = input.HasHandicapPermit,
                HorsePower = input.HorsePower,

                InsuranceCompanyName = input.InsuranceCompanyName,
                InsuranceExpiryDate = input.InsuranceExpiryDate,
                InsuranceKindArabicDesc = input.InsuranceKindArabicDesc,
                InsuranceKindEnglishDesc = input.InsuranceKindEnglishDesc,
                InsurancePolicyNo = input.InsurancePolicyNo,
                IsDrivingBanned = input.IsDrivingBanned,
                KindArabicDesc = input.KindArabicDesc,
                KindCode = input.KindCode,
                KindEnglishDesc = input.KindEnglishDesc,
                MakeArabicDesc = input.MakeArabicDesc,
                MakeCode = input.MakeCode,
                MakeEnglishDesc = input.MakeEnglishDesc,
                ModelArabicDesc = input.ModelArabicDesc,
                ModelCode = input.ModelCode,
                ModelEnglishDesc = input.ModelEnglishDesc,
                MortgageDesc = input.MortgageDesc,
                MortgageRef = input.MortgageRef,
                NationalityArabicDesc = input.NationalityArabicDesc,

                NationalityCode = input.NationalityCode,
                NationalityEnglishDesc = input.NationalityEnglishDesc,
                OwnerTcfArabicName = input.OwnerTcfArabicName,
                OwnerTcfEnglishName = input.OwnerTcfEnglishName,
                OwnerTcfNo = input.OwnerTcfNo,
                PlateInfo = input.PlateInfo.ConvertToPlateKey(),
                RegistrationDate = input.RegistrationDate,
                RegistrationExpiryDate = input.RegistrationExpiryDate,
                RegistrationRemarks = input.RegistrationRemarks,
                SteeringArabicDesc = input.SteeringArabicDesc,
                SteeringCode = input.SteeringCode,
                SteeringEnglishDesc = input.SteeringEnglishDesc,
                TypeArabicDesc = input.TypeArabicDesc,
                TypeCode = input.TypeCode,
                TypeEnglishDesc = input.TypeEnglishDesc,
                WeightArabicDesc = input.WeightArabicDesc,
                WeightCode = input.WeightCode,
                WeightEmpty = input.WeightEmpty,
                WeightEnglishDesc = input.WeightEnglishDesc,
                WeightFull = input.WeightFull,
                WheelsCount = input.WheelsCount,
                Year = input.Year

            };
        }

        public static List<TicketsDetailsResponse> ConvertToTicketsDetails(this ADPTicketsServiceReference.GetTicketsResponse input)
        {
            if (input != null && input.Tickets != null)
            {
                List<TicketsDetailsResponse> output = new List<TicketsDetailsResponse>();

                TicketsDetailsResponse response = null;

                foreach (var ticket in input.Tickets)
                {
                    response = new TicketsDetailsResponse();

                    if (ticket.PlateInfo != null)
                    {
                        response.PlateNo = ticket.PlateInfo.PlateNo;
                        response.PlateOrgNo = ticket.PlateInfo.PlateOrgNo.HasValue ? ticket.PlateInfo.PlateOrgNo.Value : 0;
                        response.PlateSourceCode = ticket.PlateInfo.PlateSourceCode.HasValue ? ticket.PlateInfo.PlateSourceCode.Value : 0;
                        response.PlateTypeCode = ticket.PlateInfo.PlateTypeCode.HasValue ? ticket.PlateInfo.PlateTypeCode.Value : 0;
                        response.PlateColorCode = ticket.PlateInfo.PlateColorCode.HasValue ? ticket.PlateInfo.PlateColorCode.Value : 0;
                        response.PlateKindCode = ticket.PlateInfo.PlateKindCode.HasValue ? ticket.PlateInfo.PlateKindCode.Value : 0;
                    }

                    if (ticket.LicenseInfo != null)
                    {
                        response.LicenseNumber = ticket.LicenseInfo.LicenseNo.HasValue ? ticket.LicenseInfo.LicenseNo.Value : 0;
                        response.LicenseSourceCode = ticket.LicenseInfo.LicenseSourceCode;
                    }

                    if (ticket.TicketID != null)
                    {
                        response.TicketNumber = ticket.TicketID.TicketNo;
                        response.TicketDate = ticket.TicketID.TicketDate;
                        response.TicketYear = ticket.TicketID.TicketYear;
                        response.TicketSourceCode = ticket.TicketID.TicketSourceCode;
                        response.IsExternalTicket = ticket.TicketID.IsExternalTicket;
                    }

                    response.TicketType = ticket.TicketType;
                    response.TicketTime = ticket.TicketTime;
                    response.TotalAmount = ticket.TotalAmount;
                    response.DiscountRate = ticket.DiscountRate;
                    response.TotalAmountAfterDiscount = ticket.TotalAmountAfterDiscount;
                    response.IsWebPayable = ticket.IsWebPayable;
                    response.LateCharges = ticket.LateCharges;
                    response.LocationDescriptionAr = ticket.LocationDescAr;
                    response.LocationDescriptionEn = ticket.LocationDescEn;
                    response.DriverTcfNo = ticket.DriverTcfNo.HasValue ? ticket.DriverTcfNo.Value : 0;
                    response.VehicleOwnerTcfNo = ticket.VehicleOwnerTcfNo.HasValue ? ticket.VehicleOwnerTcfNo.Value : 0;
                    response.BlackPoints = ticket.BlackPoints.HasValue ? ticket.BlackPoints.Value : 0;

                    output.Add(response);
                }

                return output;
            }

            return null;
        }

        public static TrafficProfileResponse ConvertToTrafficProfile(this TrafficProfile input)
        {

            return new TrafficProfileResponse
            {
                ArabicName = input.ArabicName,
                BirthDate = input.BirthDate,
                EducationCode = input.EducationCode,
                Email = input.Email,
                EnglishName = input.EnglishName,
                EstablishmentNo = input.EstablishmentNo,
                EstablishmentSSourceCode = input.EstablishmentSSourceCode,
                Fax = input.Fax,
                Gender = input.Gender,
                HomeAddress = input.HomeAddress,
                HomeBuildingNo = input.HomeBuildingNo,
                HomeFlatNo = input.HomeFlatNo,
                HomeStreetCode = input.HomeStreetCode,
                HomeTelephoneNo = input.HomeTelephoneNo,
                MaritalCode = input.MaritalCode,
                Mobile = input.Mobile,
                NationalityCode = input.NationalityCode,
                OccupationCode = input.OccupationCode,
                PassportExpiryDate = input.PassportExpiryDate,
                PassportIssueDate = input.PassportIssueDate,
                PassportNo = input.PassportNo,
                POBox = input.POBox,
                ReligionCode = input.ReligionCode,
                ResidenceNo = input.ResidenceNo,
                TcfKindCode = input.TcfKindCode,
                TcfNo = input.TcfNo,
                TcfTypeCode = input.TcfTypeCode,
                TradeLicenseExpiryDate = input.TradeLicenseExpiryDate,
                TradeLicenseIssueDate = input.TradeLicenseIssueDate,
                TradeLicenseNo = input.TradeLicenseNo,
                TradeLicenseSourceCode = input.TradeLicenseSourceCode,
                UnifiedID = input.UnifiedID,
                WorkAddress = input.WorkAddress,
                WorkBuildingNo = input.WorkBuildingNo,
                WorkStreetCode = input.WorkStreetCode,
                WorkTelephoneNo = input.WorkTelephoneNo
            };
        }
        public static LicenseKey ConvetToLicenseKey(this ADPLicenseServiceReference.LicenseKey input)
        {
            return new LicenseKey
            {
                LicenseNo = input.LicenseNo,
                LicenseSourceArabicDesc = input.LicenseSourceArabicDesc,
                LicenseSourceCode = input.LicenseSourceCode,
                LicenseSourceEnglishDesc = input.LicenseSourceEnglishDesc
            };
        }
        public static ADPLicenseServiceReference.LicenseKey ConvetToLicenseKeyService(this LicenseKey input)
        {
            return new ADPLicenseServiceReference.LicenseKey
            {
                LicenseNo = input.LicenseNo,
                LicenseSourceArabicDesc = input.LicenseSourceArabicDesc,
                LicenseSourceCode = input.LicenseSourceCode,
                LicenseSourceEnglishDesc = input.LicenseSourceEnglishDesc
            };
        }
        public static SubLicense ConvertToSubLicense(this ADPLicenseServiceReference.SubLicense input)
        {
            return new SubLicense
            {
                GearArabicDesc = input.GearArabicDesc,
                GearCode = input.GearCode,
                GearEnglishDesc = input.GearEnglishDesc,
                LicenseTypeArabicDesc = input.LicenseTypeArabicDesc,
                LicenseTypeCode = input.LicenseTypeCode,
                LicenseTypeEnglishDesc = input.LicenseTypeEnglishDesc,
                LicenseTypeIssueDate = input.LicenseTypeIssueDate,
                LicenseTypeSourceArabicDesc = input.LicenseTypeSourceArabicDesc,
                LicenseTypeSourceCode = input.LicenseTypeSourceCode,
                LicenseTypeSourceEnglishDesc = input.LicenseTypeSourceEnglishDesc

            };
        }
        public static LicenseDetailsResponse ConvertToLicenseDetailsResponse(this ADPLicenseServiceReference.LicenseDetails input)
        {
            return new LicenseDetailsResponse
            {
                BlackPoints = input.BlackPoints,
                DriverArabicName = input.DriverArabicName,
                DriverEnglishName = input.DriverEnglishName,
                FLSCode = input.FLSCode,
                FLSDesc = input.FLSDesc,
                HasBlackPointsFile = input.HasBlackPointsFile,
                IsBanned = input.IsBanned,
                LicenseIssueDate = input.LicenseIssueDate,
                LicenseKey = input.LicenseKey.ConvetToLicenseKey(),
                LicenseKindArabicDesc = input.LicenseKindArabicDesc,
                LicenseKindCode = input.LicenseKindCode,
                LicenseKindEnglishDesc = input.LicenseKindEnglishDesc,
                LicenseRenewingDate = input.LicenseRenewingDate,
                LicesenExpiryDate = input.LicesenExpiryDate,
                PhysicalStatusArabicDesc = input.PhysicalStatusArabicDesc,
                PhysicalStatusCode = input.PhysicalStatusCode,
                PhysicalStatusEnglishDesc = input.PhysicalStatusEnglishDesc,
                SubLicenses = input.SubLicenses.Select(i => i.ConvertToSubLicense()).ToArray(),
                TcfNo = input.TcfNo
            };
        }
        public static LookupRecordResponse ConvertToLookupRecordResponse(this ADPUtilitiesReference.LookupRecord input)
        {
            return new LookupRecordResponse
            {
                ArabicDescription = input.ArabicDescription,
                Code = input.Code,
                EnglishDescription = input.EnglishDescription
            };
        }
    }
}