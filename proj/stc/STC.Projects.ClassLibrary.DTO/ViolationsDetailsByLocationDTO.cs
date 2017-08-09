using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationsDetailsByLocationDTO
    {
        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public double Altitude { get; set; }

        [DataMember]
        public string LocationCode { get; set; }

        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public int? ViolationsCount { get; set; }
    }
}
