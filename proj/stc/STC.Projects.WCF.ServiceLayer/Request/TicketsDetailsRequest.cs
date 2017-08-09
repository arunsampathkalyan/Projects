using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class TicketsDetailsRequest
    {
        [DataMember]
        public string PlateNo { get; set; }
        [DataMember]
        public long PlateOrgNo { get; set; }
        [DataMember]
        public int PlateColorCode { get; set; }
        [DataMember]
        public int PlateKindCode { get; set; }
        [DataMember]
        public int PlateTypeCode { get; set; }
        [DataMember]
        public int PlateSourceCode { get; set; }
        [DataMember]
        public long LicenseNumber { get; set; }
        [DataMember]
        public int LicenseSourceCode { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public long TcfNo { get; set; }
    }
}