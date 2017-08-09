using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.Projects.ClassLibrary.DTO;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class DrawPatrolsMessage : IXMLMessageObject
    {
        public DrawPatrolsMessage()
        {

        }

        public ObservableCollection<PatrolLastLocationDTO> PatrolsList { get; set; }
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

        public void SetDate(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTime CreatedDate { get; set; }
        public double Longitude { set; get; }

        public double Latitude { set; get; }

        public long TowerId { set; get; }

       
        public string MessageId { get; set; }

    }

}
