using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class FogEventMessage : IXMLMessageObject,IEventNotificationPublish
    {
        public FogEventMessage()
        {
            TowersList = new List<AssetsViewDTO>();

            RadarsList = new List<AssetsViewDTO>();

            CamerasList = new List<AssetsViewDTO>();
        }

        public void SetMessageId(string MessageId)
        {
            this.MessageId = MessageId;
        }

        public void SetNotificationId(long NotificationId)
        {
            this.NotificationId = NotificationId;
        }

        public void SetIsPublishSOP(bool IsPublishSOP)
        {
            this.IsPublishSOP = IsPublishSOP;
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

        public DateTime CreatedDate { get; set; }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public long TowerId { set; get; }

        public double VisibilityRadius { get; set; }

        public List<AssetsViewDTO> TowersList { get; set; }

        public List<AssetsViewDTO> RadarsList { get; set; }

        public List<AssetsViewDTO> CamerasList { get; set; }


        public string MessageId { get; set; }

        public long NotificationId { get; set; }
        public string Discription { get; set; }

        public bool IsPublishSOP { get; set; }
    }

    public class FogEventToSOPMessage : IXMLMessageObject
    {
        public FogEventToSOPMessage()
        {
            TowersList = new List<AssetsViewDTO>();

            RadarsList = new List<AssetsViewDTO>();

            CamerasList = new List<AssetsViewDTO>();
        }


        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public long TowerId { set; get; }

        public double VisibilityRadius { get; set; }

        public List<AssetsViewDTO> TowersList { get; set; }

        public List<AssetsViewDTO> RadarsList { get; set; }

        public List<AssetsViewDTO> CamerasList { get; set; }


        public string MessageId { get; set; }

        public string Discription { get; set; }

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }

        public void SetMessageId(string MessageId)
        {
            this.MessageId = MessageId;
        }

        public void SetNotificationId(long NotificationId)
        {
            this.NotificationId = NotificationId;
        }

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

    public interface IXMLMessageObject
    {
        void SetMessageId(string MessageId);
        void SetNotificationId(long NotificationId);
        void SetDate(DateTime createdDate);
        double GetLatitude();
        double GetLongitude();
    }

    public interface IEventNotificationPublish
    {
        void SetIsPublishSOP(bool IsPublishSOP);
    }
}
