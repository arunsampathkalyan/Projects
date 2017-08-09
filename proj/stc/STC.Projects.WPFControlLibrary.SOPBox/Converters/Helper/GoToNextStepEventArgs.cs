using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STC.Projects.WPFControlLibrary.SOPBox.Helper
{
    public class GoToNextStepEventArgs : EventArgs
    {
        public bool Confirmation { get; set; }
    }
}
