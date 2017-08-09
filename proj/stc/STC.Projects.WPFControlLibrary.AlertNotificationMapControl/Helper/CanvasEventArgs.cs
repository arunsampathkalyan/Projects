using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper
{
    public class CanvasEventArgs : EventArgs
    {
        public int Width { get; set; }

        public UIElement ChildControl { get; set; }
    }
}
