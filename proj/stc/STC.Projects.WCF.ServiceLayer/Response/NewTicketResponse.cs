using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class NewTicketResponse
    {
         [DataMember]
        public long TicketNo
        {
            get;
            set;
        }
        [DataMember]

        public int TicketSourceCode
        {
            get;
            set;
        }
        
    }
}