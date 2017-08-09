using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    [DataContract]
    public class LicenseKey
    {
        [DataMember]
        public System.Nullable<long> LicenseNo
        {
            get;
            set;
        }
        [DataMember]

        public int LicenseSourceCode
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseSourceArabicDesc
        {
            get;
            set;
        }
        [DataMember]
        public string LicenseSourceEnglishDesc
        {
            get;
            set;
        }
        
    }
}