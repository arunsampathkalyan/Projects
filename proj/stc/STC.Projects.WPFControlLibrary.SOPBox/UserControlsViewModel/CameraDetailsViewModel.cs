using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel
{
    class CameraDetailsViewModel
    {
        public AssetsViewDTO Camera { get; set; }

        public CameraDetailsViewModel()
        {
            Camera = new AssetsViewDTO();
        }
    }
}
