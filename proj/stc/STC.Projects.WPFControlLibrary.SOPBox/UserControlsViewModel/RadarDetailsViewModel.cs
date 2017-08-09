using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    internal class RadarDetailsViewModel
    {
        public AssetsViewDTO Radar { get; set; }
        public int NewSpeedValue { get; set; }
        public string OldSpeedValue { get; set; }
        public RadarDetailsViewModel()
        {
            Radar = new AssetsViewDTO();
            NewSpeedValue = 80;
        }
    }
}
