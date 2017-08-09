using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class UserUserControlDTO
    {
        [DataMember]
        public long UserUserControlsID { get; set; }
        [DataMember]
        public string XML { get; set; }
        [DataMember]
        public MapNotificationPopUpDTO PopupContent { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public bool IsNoticed { get; set; }
        [DataMember]
        public long NotificationId { get; set; }
        [DataMember]
        public int? PriorityId { get; set; }
        [DataMember]
        public NotificationDTO Notification {get;set;}
        public UserUserControlDTO()
        {
            PopupContent = new MapNotificationPopUpDTO();
            Notification = new NotificationDTO();
        }
     
    }
}
