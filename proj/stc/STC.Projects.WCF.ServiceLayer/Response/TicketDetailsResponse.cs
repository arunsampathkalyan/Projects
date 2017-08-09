using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Response
{
    [DataContract]
    public class TicketsDetailsResponse
    {
        [DataMember]
        public long TicketNumber { get; set; }
        [DataMember]
        public int TicketYear { get; set; }
        [DataMember]
        public int TicketSourceCode { get; set; }
        [DataMember]
        public DateTime TicketDate { get; set; }
        [DataMember]
        public bool IsExternalTicket { get; set; }
        [DataMember]
        public DateTime TicketTime { get; set; }
        [DataMember]
        public string TicketType { get; set; }
        [DataMember]
        public string LocationDescriptionAr { get; set; }
        [DataMember]
        public string LocationDescriptionEn { get; set; }
        [DataMember]
        public long DriverTcfNo { get; set; }
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
        public long VehicleOwnerTcfNo { get; set; }
        [DataMember]
        public bool IsWebPayable { get; set; }
        [DataMember]
        public int LateCharges { get; set; }
        [DataMember]
        public double TotalAmount { get; set; }
        [DataMember]
        public double DiscountRate { get; set; }
        [DataMember]
        public double TotalAmountAfterDiscount { get; set; }
        [DataMember]
        public int BlackPoints { get; set; }
    }
}