using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class UnifiedIDRequest
    {
        [DataMember]
        public long UnifiedID
        {
            get;
            set;
        }
    }
}