using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{

    [DataContract]
    public class GPSLocation
    {
        [DataMember]
        public string X
        {
            get;
            set;
        }
        [DataMember]
        public string Y
        {
            get;
            set;
        }
    }
}