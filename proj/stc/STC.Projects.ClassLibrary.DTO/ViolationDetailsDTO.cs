using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DTO
{
    [DataContract]
    public class ViolationDetailsDTO
    {
        [DataMember]
        public double Lat { get; set; }
        [DataMember]
        public double Lon { get; set; }
        
        [DataMember]
        public List<TotalViolationValuesByStatus> TotalsByStatus { get; set; }
        [DataMember]
        public List<TotalViolationValuesByTypes> TotalsByTypes { get; set; }
        [DataMember]
        public AssetDetailsForViolation AssetsDetails { get; set; }
        [DataMember]
        public string XML { get; set; }

        public string GetXML()
        {
            var notificationBox = new MapNotificationPopUpDTO();
            notificationBox.Lat = Lat;
            notificationBox.Lon = Lon;
            notificationBox.TypeName = "Violation";
            var tab1 = new TabDTO()
            {
                TabName = "TotalCountsByStatus"
            };
            foreach (var item in TotalsByStatus)
            {
                tab1.Attributes.Add(new TabItemDTO { KeyName = item.VioltionStatusName, ValueName = item.TotalCountOfViolations.ToString() });                
            }
            var tab2 = new TabDTO()
            {
                TabName = "TotalCountsByType"
            };
            foreach (var item in TotalsByTypes)
            {
                tab2.Attributes.Add(new TabItemDTO { KeyName = item.VioltionTypeName, ValueName = item.TotalCountOfViolations.ToString() });
            }
            var tab3 = new TabDTO()
            {
                TabName = "AssetDetails"
            };

            tab3.Attributes.Add(new TabItemDTO { KeyName = "AssetName", ValueName = AssetsDetails.AssetName });
            tab3.Attributes.Add(new TabItemDTO { KeyName = "AssetStatus", ValueName = AssetsDetails.AssetStatus });
            tab3.Attributes.Add(new TabItemDTO { KeyName = "LastMainteanceDate", ValueName =  AssetsDetails.LastMainteanceDate });
            tab3.Attributes.Add(new TabItemDTO { KeyName = "VendorName", ValueName = AssetsDetails.VendorName });
            notificationBox.Tabs.Add(tab1);
            notificationBox.Tabs.Add(tab2);
            notificationBox.Tabs.Add(tab3);
            return Helper.Serialize(notificationBox);
        }
    }

    [DataContract]
    public class TotalViolationValuesByStatus
    {
        [DataMember]
        public int TotalCountOfViolations { get; set; }
        [DataMember]
        public int VioltionStatusId { get; set; }
        [DataMember]
        public string VioltionStatusName { get; set; }
    }

    [DataContract]
    public class TotalViolationValuesByTypes
    {
        [DataMember]
        public int TotalCountOfViolations { get; set; }
        [DataMember]
        public int VioltionTypeId { get; set; }

        [DataMember]
        public string VioltionTypeName { get; set; }
    }

    [DataContract]
    public class AssetDetailsForViolation
    {
        [DataMember]
        public string AssetName { get; set; }
        [DataMember]
        public string AssetStatus { get; set; }
        [DataMember]
        public string LastMainteanceDate { get; set; }
        [DataMember]
        public string VendorName { get; set; }
    }


}
