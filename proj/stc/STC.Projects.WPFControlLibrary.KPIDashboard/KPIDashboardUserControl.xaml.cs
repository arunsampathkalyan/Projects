using STC.Projects.ClassLibrary.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STC.Projects.WPFControlLibrary.KPIDashboard
{
    /// <summary>
    /// Interaction logic for KPIDashboardUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "KPIDashboardUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class KPIDashboardUserControl : UserControl, IUserControl
    {
        public KPIDashboardUserControl()
        {
            InitializeComponent();


        }
    }

    public class chartmodel
    {
        public string Text { set; get; }
        public int Value { set; get; }
    }
}
