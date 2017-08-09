using System;
using System.Runtime.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationNotificationDTO
    {
        public ViolationNotificationDTO()
        {
            Notification = new NotificationDTO();
        }
        [DataMember]
        public long ViolationNotificationId { get; set; }

        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public double? Latitude { get; set; }

        [DataMember]
        public double? Longitude { get; set; }

        //public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }

        [DataMember]
        public double Altitude { get; set; }

        [DataMember]
        public string LocationCode { get; set; }

        [DataMember]
        public int ViolationTypeId { get; set; }

        [DataMember]
        public string ViolationTypeName { get; set; }

        [DataMember]
        public DateTime DateTaken { get; set; }

        [DataMember]
        public int SpeedLimit { get; set; }

        [DataMember]
        public int MesuredSpeed { get; set; }

        [DataMember]
        public int CapturedSpeed { get; set; }

        [DataMember]
        public string PlateNumber { get; set; }

        [DataMember]
        public int VehicleTypeId { get; set; }

        [DataMember]
        public string VehicleTypeName { get; set; }

        [DataMember]
        public string PlateColorName { get; set; }

        [DataMember]
        public string PlateSourceName { get; set; }

        [DataMember]
        public string PlateTypeName { get; set; }

        [DataMember]
        public string PlateKindName { get; set; }

        [DataMember]
        public bool IsNoticed { get; set; }

        [DataMember]
        public int? Count { get; set; }

        [DataMember]
        public string MessageText { get; set; }

        [DataMember]
        public string BackgroundColor { get; set; }

        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        public bool IsCritical { get; set; }

        [DataMember]
        public int Direction { get; set; }

        [DataMember]
        public string DirectionName { get; set; }

        [DataMember]
        public int LaneNo { get; set; }

        [DataMember]
        public int SourceId { get; set; }

        [DataMember]
        public string SourceName { get; set; }

        [DataMember]
        public int ViolationStatusId { get; set; }

        [DataMember]
        public long LPRId { get; set; }

        [DataMember]
        public string ViolationStatusName { get; set; }

        [DataMember]
        public long NotificationId { get; set; }
        [DataMember]
        public NotificationDTO Notification { get; set; }
      
    }
}