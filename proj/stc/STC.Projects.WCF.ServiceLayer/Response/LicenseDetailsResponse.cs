using STC.Projects.WCF.ServiceLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class LicenseDetailsResponse
    {
        [DataMember]
        public LicenseKey LicenseKey
        {
            get;
            set;
        }
        [DataMember]

        /// <remarks/>

        public int LicenseKindCode
        {
            get;
            set;
        }
        [DataMember]


        public string LicenseKindArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseKindEnglishDesc
        {
            get;
            set;
        }
        [DataMember]
        public string DriverArabicName
        {
            get;
            set;
        }
        [DataMember]
        public string DriverEnglishName
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<System.DateTime> LicenseIssueDate
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<System.DateTime> LicesenExpiryDate
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<System.DateTime> LicenseRenewingDate
        {
            get;
            set;
        }
        [DataMember]
        public SubLicense[] SubLicenses
        {
            get;
            set;
        }
        [DataMember]
        public int PhysicalStatusCode
        {
            get;
            set;
        }
        [DataMember]
        public string PhysicalStatusArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string PhysicalStatusEnglishDesc
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<long> TcfNo
        {
            get;
            set;
        }
        [DataMember]
        public int FLSCode
        {
            get;
            set;
        }
        [DataMember]
        public string FLSDesc
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<int> BlackPoints
        {
            get;
            set;
        }
        [DataMember]
        public bool IsBanned
        {
            get;
            set;
        }
        [DataMember]
        public bool HasBlackPointsFile{
            get;
            set;
        }
        
        
    }
}