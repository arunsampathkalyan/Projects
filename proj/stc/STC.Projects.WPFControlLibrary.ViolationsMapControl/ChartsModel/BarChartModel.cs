using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.Model
{
    public class BarChartModel
    {
        public string Text { set; get; }
        public int Value { set; get; }
        public Brush BarColor { set; get; }
    }
}
