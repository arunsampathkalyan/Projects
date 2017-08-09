using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class WantedCarMessage : IXMLMessageObject, IEventNotificationPublish
    {
        private string _Discription;
        private string _EnglishDiscription;

        public long TowerId { get; set; }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public string MessageId { get; set; }


        public string VehiclePlateNumber { get; set; }

        public string VehiclePlateKind { get; set; }

        public string VehiclePlateType { get; set; }

        public string VehiclePlateSource { get; set; }

        public string VehiclePlateColor { get; set; }

        public string BusinessRuleName { get; set; }

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public void SetRuleName(string businessName)
        {
            BusinessRuleName = businessName;
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

        public string Discription
        {
            get
            {
                return _Discription;
            }

            set
            {
                _Discription = value;
                EnglishDiscription = "Dangerous Violator - " + VehiclePlateNumber;
            }
        }

        public string EnglishDiscription
        {
            get
            {
                return _EnglishDiscription;
            }

            set
            {
                _EnglishDiscription = value;
            }
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
    }

    public class WantedCarToSOPMessage : IXMLMessageObject
    {
        private string _Discription;
        private string _EnglishDiscription;

        public long TowerId { get; set; }

        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public string MessageId { get; set; }

        public string Discription
        {
            get
            {
                return _Discription;
            }

            set
            {
                _Discription = value;
                EnglishDiscription = "Dangerous Violator - " + VehiclePlateNumber;
            }
        }

        public string EnglishDiscription
        {
            get
            {
                return _EnglishDiscription;
            }

            set
            {
                _EnglishDiscription = value;
            }
        }

        public string VehiclePlateNumber { get; set; }

        public string VehiclePlateKind { get; set; }

        public string VehiclePlateType { get; set; }

        public string VehiclePlateSource { get; set; }

        public string VehiclePlateColor { get; set; }

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
