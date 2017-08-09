using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    [DataContract]
    public class SubLicense
    {
        [DataMember]
        public int LicenseTypeCode
        {
            get;
            set;
        }
        [DataMember]


        public string LicenseTypeArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseTypeEnglishDesc
        {
            get;
            set;
        }
        [DataMember]
        public int GearCode
        {
            get;
            set;
        }
        [DataMember]
        public string GearArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string GearEnglishDesc
        {
            get;
            set;
        }
        [DataMember]
        public System.DateTime LicenseTypeIssueDate
        {
            get;
            set;
        }
        [DataMember]
        public int LicenseTypeSourceCode
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseTypeSourceArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseTypeSourceEnglishDesc {
            get;
            set;
        }
        
    }
}