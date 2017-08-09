using STC.Projects.WCF.ServiceLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Request
{
    [DataContract]
    public class NewTicketRequest
    {
        [DataMember]
        public System.Nullable<long> TicketNo
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime TicketDateTime
        {
            get;
            set;
        }
        [DataMember]
        public string TicketType
        {
            get;
            set;
        }

        [DataMember]
        public int LocationCode
        {
            get;
            set;
        }

        [DataMember]
        public PlateKey PlateInfo
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
        public System.Nullable<long> DriverTcfNo
        {
            get;
            set;
        }
        [DataMember]

        public string DriverName
        {
            get;
            set;
        }
        [DataMember]

        public System.Nullable<int> VehicleMakeCode
        {
            get;
            set;
        }
        [DataMember]

        public System.Nullable<int> VehicleTypeCode
        {
            get;
            set;
        }
        [DataMember]

        public System.Nullable<int> VehicleModelCode
        {
            get;
            set;
        }
        [DataMember]

        public System.Nullable<int> VehicleColorCode
        {
            get;
            set;
        }
        [DataMember]

        public System.Nullable<long> VehicleOwnerTcfNo
        {
            get;
            set;
        }
        [DataMember]

        public string VehicleOwnerName
        {
            get;
            set;
        }
        [DataMember]

        public string[] MaterialsCodes
        {
            get;
            set;
        }
        [DataMember]

        public GPSLocation Location
        {
            get;
            set;
        }
        [DataMember]

        public string Remarks
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<int> MeasuredSpeed
        {
            get;
            set;
        }
        [DataMember]


        public System.Nullable<int> CapturedSpeed
        {
            get;
            set;
        }
        [DataMember]
        public System.Nullable<int> SpeedLimit
        {
            get;
            set;
        }
        [DataMember]

        public int RadarKindCode
        {
            get;
            set;
        }
        [DataMember]


        public System.Nullable<int> RadarTypeCode
        {
            get;
            set;
        }
    }
}