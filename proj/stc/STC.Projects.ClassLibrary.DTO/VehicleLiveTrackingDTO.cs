using System;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class VehicleLiveTrackingDTO
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string PlateNumber { get; set; }
        [DataMember]
        public string PlateKind { get; set; }
        [DataMember]
        public string PlateType { get; set; }
        [DataMember]
        public string PlateSource { get; set; }
        [DataMember]
        public string PlateColor { get; set; }
        [DataMember]
        public string LicenseNumber { get; set; }
        [DataMember]
        public DateTime? LicenseExpiryDate { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string OwnerName { get; set; }
        [DataMember]
        public string OwnerMobileNumber { get; set; }
        [DataMember]
        public string OwnerNationality { get; set; }
        [DataMember]
        public short OwnerAge { get; set; }
        [DataMember]
        public long TowerId { get; set; }
        [DataMember]
        public double? Latitude { get; set; }
        [DataMember]
        public double? Longitude { get; set; }
        [DataMember]
        public DateTime? CaptureTime { get; set; }
    }
}
