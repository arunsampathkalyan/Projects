using STC.Projects.WCF.ServiceLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class VehicleDetailsResponse
    {
        [DataMember]

        public bool? HasHandicapPermit{get;set;}[DataMember]

        public bool? HasColorGlassPermit { get; set; }[DataMember]


        public string OwnerTcfNo { get; set; }[DataMember]


        public string OwnerTcfArabicName { get; set; }[DataMember]


        public string OwnerTcfEnglishName { get; set; }[DataMember]


        public System.Nullable<bool> IsDrivingBanned { get; set; }[DataMember]


        public PlateKey PlateInfo { get; set; }[DataMember]


        public string InsuranceCompanyName { get; set; }[DataMember]


        public string InsuranceKindArabicDesc { get; set; }[DataMember]


        public string InsuranceKindEnglishDesc { get; set; }[DataMember]


        public string InsuranceExpiryDate { get; set; }[DataMember]


        public string InsurancePolicyNo { get; set; }[DataMember]


        public string MortgageDesc { get; set; }[DataMember]


        public string MortgageRef { get; set; }[DataMember]


        public string ColorCode { get; set; }[DataMember]


        public string ColorArabicDesc { get; set; }[DataMember]


        public string ColorEnglishDesc { get; set; }[DataMember]


        public string TypeCode { get; set; }[DataMember]


        public string TypeArabicDesc { get; set; }[DataMember]


        public string TypeEnglishDesc { get; set; }[DataMember]


        public string NationalityCode { get; set; }[DataMember]


        public string NationalityArabicDesc { get; set; }[DataMember]


        public string NationalityEnglishDesc { get; set; }[DataMember]


        public string Year { get; set; }[DataMember]


        public string Chairs { get; set; }[DataMember]


        public string MakeCode{get;set;}[DataMember]

        public string MakeArabicDesc{get;set;}[DataMember]

        public string MakeEnglishDesc{get;set;}[DataMember]

        public string ModelCode{get;set;}[DataMember]

        public string ModelArabicDesc{get;set;}[DataMember]

        public string ModelEnglishDesc{get;set;}[DataMember]

        public string KindCode{get;set;}[DataMember]

        public string KindArabicDesc{get;set;}[DataMember]

        public string KindEnglishDesc{get;set;}[DataMember]

        public string WeightEmpty{get;set;}[DataMember]

        public string WeightFull{get;set;}[DataMember]

        public string RegistrationDate{get;set;}[DataMember]

        public string RegistrationExpiryDate{get;set;}[DataMember]

        public string EngineNo{get;set;}[DataMember]

        public string ChassisNo{get;set;}[DataMember]

        public string GearCode{get;set;}[DataMember]

        public string GearArabicDesc{get;set;}[DataMember]

        public string GearEnglishDesc{get;set;}[DataMember]

        public string FuelCode{get;set;}[DataMember]

        public string FuelArabicDesc{get;set;}[DataMember]

        public string FuelEnglishDesc{get;set;}[DataMember]

        public string WeightCode{get;set;}[DataMember]

        public string WeightArabicDesc{get;set;}[DataMember]

        public string WeightEnglishDesc{get;set;}[DataMember]

        public string SteeringCode{get;set;}[DataMember]

        public string SteeringArabicDesc{get;set;}[DataMember]

        public string SteeringEnglishDesc{get;set;}[DataMember]

        public string Cylinder{get;set;}[DataMember]

        public string AxisCount{get;set;}[DataMember]

        public string DoorCount{get;set;}[DataMember]

        public string HorsePower{get;set;}[DataMember]

        public string WheelsCount{get;set;}[DataMember]

        public string RegistrationRemarks{get;set;}
        
    }
}