using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class DetectedAccidentMessage : IXMLMessageObject,IEventNotificationPublish
    {
        public DetectedAccidentMessage()
        {
            TowersList = new List<AssetsViewDTO>();

            RadarsList = new List<AssetsViewDTO>();

            CamerasList = new List<AssetsViewDTO>();
        }

        public void SetMessageId(string MessageId)
        {
            this.MessageId = MessageId;
        }
        public void SetIsPublishSOP(bool IsPublishSOP)
        {
            this.IsPublishSOP = IsPublishSOP;
        }

        public void SetNotificationId(long NotificationId)
        {
            this.NotificationId = NotificationId;
        }

        public double GetLatitude()
        {
            return Latitude;
        }

        public double GetLongitude()
        {
            return Longitude;
        }

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public long TowerId { set; get; }

        public bool IsPublishSOP { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public List<AssetsViewDTO> TowersList { get; set; }

        public List<AssetsViewDTO> RadarsList { get; set; }

        public List<AssetsViewDTO> CamerasList { get; set; }

        public long NotificationId { get; set; }
        public string MessageId { get; set; }

        public string Discription { get; set; }
    }

    public class DetectedAccidentSOPMessage : IXMLMessageObject
    {
        public DetectedAccidentSOPMessage()
        {
            TowersList = new List<AssetsViewDTO>();

            RadarsList = new List<AssetsViewDTO>();

            CamerasList = new List<AssetsViewDTO>();
        }


        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public long TowerId { set; get; }

        public List<AssetsViewDTO> TowersList { get; set; }

        public List<AssetsViewDTO> RadarsList { get; set; }

        public List<AssetsViewDTO> CamerasList { get; set; }


        public string MessageId { get; set; }

        public string Discription { get; set; }

        public void SetMessageId(string MessageId)
        {
            this.MessageId = MessageId;
        }

        public void SetNotificationId(long NotificationId)
        {
            this.NotificationId = NotificationId;
        }

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }


        public long NotificationId { get; set; }
        public double GetLatitude()
        {
            return Latitude;
        }

        public double GetLongitude()
        {
            return Longitude;
        }
    }
}
