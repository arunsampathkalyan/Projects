using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class PersonDetailsRequest
    {
        [DataMember]
        public long TcfNo { get; set; }
        [DataMember]
        public long UnifiedId { get; set; }
        public PersonDetailsRequest()
        {
            TcfNo = 0;
            UnifiedId = 0;
        }
    }
}