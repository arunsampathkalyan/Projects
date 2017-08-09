using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class VehicleDetailsRequest
    {
        public VehicleDetailsRequest()
        {
            ChassisNoExist = true;
            PlateOrgNo = 0;
        }

        [DataMember]
        public bool ChassisNoExist { get; set; }
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
        public string ChassisNo { get; set; }
    }
}