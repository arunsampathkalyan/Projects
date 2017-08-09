using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STC.Projects.ClassLibrary.DTO
{
   [XmlRoot("MapNotificationPopUpDTO")]
    [DataContract]
    public class MapNotificationPopUpDTO
    {
        public MapNotificationPopUpDTO()
        {
            Tabs = new List<TabDTO>();
        }
       [XmlElement("Lat")]
        [DataMember]
        public double Lat { get; set; }
       [XmlElement("Lon")]
        [DataMember]
        public double Lon { get; set; }
       [XmlElement("GeneralType")]
       [DataMember]
       public int GeneralType { get; set; }
       [XmlElement("Description")]
       [DataMember]
       public string Discription { get; set; }
       [XmlElement("TypeName")]
        [DataMember]
        public string TypeName { get; set; }
       [XmlArray("Tabs")]
       [XmlArrayItem("Tab")]
        [DataMember]
        public List<TabDTO> Tabs { get; set; }
    }

    [DataContract]
    public class TabDTO
    {
        public TabDTO()
        {
            Attributes = new List<TabItemDTO>();
        }
          [XmlElement("TabName")]
        [DataMember]
        public string TabName { get; set; }
          [XmlArray("TabItems")]
          [XmlArrayItem("Item")]
        [DataMember]
        public List<TabItemDTO> Attributes { get; set; }
    }
    [DataContract]
    public class TabItemDTO
    {
        [DataMember]
        public string KeyName { get; set; }
        [DataMember]
        public string ValueName { get; set; }
    }
}
