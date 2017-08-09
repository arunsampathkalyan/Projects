using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class LookupRecordResponse
    {
        [DataMember]
        public string Code
        {
            get;
            set;
        }
        [DataMember]

        public string ArabicDescription
        {
            get;
            set;
        }
        [DataMember]

      
        public string EnglishDescription
        {
            get;
            set;
        }
        
    }
}