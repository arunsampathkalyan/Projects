using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class TruckViolationMessage : IXMLMessageObject,IEventNotificationPublish
    {
        public long TowerId { get; set; }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public string MessageId { get; set; }

        public string Discription { get; set; }

        public string TruckPlateNumber { get; set; }

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }

        public double GetLatitude()
        {
            return Latitude;
        }

        public double GetLongitude()
        {
            return Longitude;
        }

        public bool IsPublishSOP { get; set; }
        public long NotificationId { get; set; }
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
    }

    public class TruckViolationToSOPMessage : IXMLMessageObject
    {
        public long TowerId { get; set; }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public string MessageId { get; set; }

        public string Discription { get; set; }

        public string TruckPlateNumber { get; set; }
        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }

        public double GetLatitude()
        {
            return Latitude;
        }

        public double GetLongitude()
        {
            return Longitude;
        }
        public long NotificationId { get; set; }
        public void SetMessageId(string MessageId)
        {
            this.MessageId = MessageId;
        }

        public void SetNotificationId(long NotificationId)
        {
            this.NotificationId = NotificationId;
        }
    }
}
