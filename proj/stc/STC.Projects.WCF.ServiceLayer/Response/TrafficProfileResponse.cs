using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class TrafficProfileResponse
    {
        [DataMember]
        public long TcfNo { get; set; }
        [DataMember]

        public int TcfKindCode { get; set; }
        [DataMember]

        public string ArabicName { get; set; }
        [DataMember]

        public string EnglishName { get; set; }
        [DataMember]

        public long? UnifiedID { get; set; }
        [DataMember]

        public string EstablishmentNo { get; set; }
        [DataMember]

        public long? EstablishmentSSourceCode { get; set; }
        [DataMember]

        public int? TcfTypeCode { get; set; }
        [DataMember]

        public string Gender { get; set; }
        [DataMember]

        public System.DateTime BirthDate { get; set; }
        [DataMember]

        public int? ReligionCode { get; set; }
        [DataMember]

        public int? EducationCode { get; set; }
        [DataMember]

        public int? MaritalCode { get; set; }
        [DataMember]

        public int? OccupationCode { get; set; }
        [DataMember]

        public int? NationalityCode { get; set; }
        [DataMember]

        public string PassportNo { get; set; }
        [DataMember]

        public System.DateTime PassportIssueDate { get; set; }
        [DataMember]

        public System.DateTime PassportExpiryDate { get; set; }
        [DataMember]

        public string ResidenceNo { get; set; }
        [DataMember]

        public string HomeAddress { get; set; }
        [DataMember]

        public int? HomeStreetCode { get; set; }
        [DataMember]

        public string HomeBuildingNo { get; set; }
        [DataMember]

        public string HomeFlatNo { get; set; }
        [DataMember]

        public string HomeTelephoneNo { get; set; }
        [DataMember]

        public string WorkAddress { get; set; }
        [DataMember]

        public int? WorkStreetCode { get; set; }
        [DataMember]

        public string WorkBuildingNo { get; set; }
        [DataMember]

        public string WorkTelephoneNo { get; set; }
        [DataMember]

        public string POBox { get; set; }
        [DataMember]

        public string Mobile { get; set; }
        [DataMember]

        public string Fax { get; set; }
        [DataMember]

        public string Email { get; set; }
        [DataMember]

        public string TradeLicenseNo { get; set; }
        [DataMember]

        public int? TradeLicenseSourceCode { get; set; }
        [DataMember]

        public System.DateTime? TradeLicenseIssueDate { get; set; }
        [DataMember]

        public System.DateTime? TradeLicenseExpiryDate { get; set; }
    }
}