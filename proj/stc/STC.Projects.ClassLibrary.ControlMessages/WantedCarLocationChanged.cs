using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.ControlMessages
{
    public class WantedCarLocationChanged
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string PlateNumber { get; set; }
        public string PlateKind { get; set; }
        public string PlateType { get; set; }
        public string PlateSource { get; set; }
        public string PlateColor { get; set; }

        public bool IsNeedAlert { get; set; }
    }
}
