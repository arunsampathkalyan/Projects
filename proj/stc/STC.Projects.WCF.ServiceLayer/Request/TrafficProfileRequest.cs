using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class TrafficProfileRequest
    {
        [DataMember]
        public long TcfNo { get; set; }
    }
}