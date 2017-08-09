using STC.Projects.WCF.ServiceLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class LicenseDetailsRequest
    {
        [DataMember]

        public System.Nullable<long> TcfNo
        {
            get;
            set;
        }
        [DataMember]
        public int SystemCode
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<System.DateTime> TicketDate
        {
            get;
            set;
        }
        [DataMember]
        public LicenseKey LicenseInfo
        {
            get;
            set;
        }
        [DataMember]
        public string UserID
        {
            get;
            set;
        }
    }
}