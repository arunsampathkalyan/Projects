using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.WPFControlLibrary.SOPBox.Model
{
    public class WantedCarModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long TowerId { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string PlateKind { get; set; }
        public string PlateType { get; set; }
        public string PlateSource { get; set; }
        public string PlateColor { get; set; }
        public long NotificationId { get; set; }
    }
}
