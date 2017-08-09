using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class IncidentDetailsDTO
    {
        [DataMember]
        public double Lat { get; set; }
        [DataMember]
        public double Lon { get; set; }
        [DataMember]
        public string IncidentAddress { get; set; }
        [DataMember]
        public string IncidentType { get; set; }
        [DataMember]
        public string AssignedPatrol { get; set; }
        [DataMember]
        public string IncidentStatus { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string IncidentTime { get; set; }
        [DataMember]
        public string DispatchingTime { get; set; }
        [DataMember]
        public string PatrolArrived { get; set; }
        [DataMember]
        public string IncidentFinishTime { get; set; }
        [DataMember]
        public string XML { get; set; }

        public string GetXML()
        {
            var notificationBox = new MapNotificationPopUpDTO();
            notificationBox.Lat = Lat;
            notificationBox.Lon = Lon;
            notificationBox.TypeName = IncidentType;
            notificationBox.GeneralType = (int)NotificationMessageType.Incident;
            notificationBox.Discription = string.Format("Incident of type {0} at {1}", IncidentType, IncidentAddress);
            var tab = new TabDTO
            {
                TabName = "General"
            };
            tab.Attributes.Add( new TabItemDTO {KeyName= "Incident Address",ValueName= IncidentAddress});
            tab.Attributes.Add(new TabItemDTO { KeyName = "Incident Type", ValueName = IncidentType });
            tab.Attributes.Add(new TabItemDTO { KeyName = "Assigned Patrol", ValueName = AssignedPatrol });

            tab.Attributes.Add(new TabItemDTO { KeyName = "Incident Status", ValueName = IncidentStatus });
            tab.Attributes.Add(new TabItemDTO { KeyName = "Comments", ValueName = Comments });
            tab.Attributes.Add(new TabItemDTO { KeyName = "Incident Time", ValueName = IncidentTime});


            tab.Attributes.Add(new TabItemDTO {KeyName = "Dispatching Time", ValueName = DispatchingTime});
            tab.Attributes.Add(new TabItemDTO { KeyName = "Patrol Arrived", ValueName = PatrolArrived });
            tab.Attributes.Add(new TabItemDTO { KeyName = "Incident Finish Time", ValueName = IncidentFinishTime });

            notificationBox.Tabs.Add(tab);
            return Helper.Serialize(notificationBox);
        }
        
    }
}
