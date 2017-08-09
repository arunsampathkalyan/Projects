using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
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
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.LandingPage
{
    /// <summary>
    /// Interaction logic for LandingPageUserControl.xaml
    /// </summary>
    [Export(typeof(IUserControl))]
    [ExportMetadata("UserControlName", "LandingPageUserControl")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LandingPageUserControl : UserControl, IUserControl
    {
        public LandingPageUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());
            InitializeComponent();

            //EnableDisableManualKPIbutton();



        }

        private void hyperLinkManualKPI_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


        private void EnableDisableManualKPIbutton()
        {

            LandingPage.ViewModel.LandingPageUserControlViewModel vm = this.DataContext as LandingPage.ViewModel.LandingPageUserControlViewModel;

            var currentUserFeatures = Utility.GetCurrentUserFeatuers(this);
            if (currentUserFeatures != null && currentUserFeatures.Values.Contains(8))
            {
                vm.isUserHaveAccessToManualKPI = true;
                vm.EnableDisableManualKPIbutton();
            }
        }

        private void webBrowserManualKPI_Loaded(object sender, RoutedEventArgs e)
        {
            LandingPage.ViewModel.LandingPageUserControlViewModel vm = this.DataContext as LandingPage.ViewModel.LandingPageUserControlViewModel;

            if (!string.IsNullOrEmpty(vm.ManualKPIurl))
            {
                webBrowserManualKPI.Navigate(new Uri(vm.ManualKPIurl));
            }
            ((WebBrowser)sender).ObjectForScripting = new HtmlInteropInternalTestClass();

        }

        private void webBrowserInciHeatMapKPI_Loaded(object sender, RoutedEventArgs e)
        {
            LandingPage.ViewModel.LandingPageUserControlViewModel vm = this.DataContext as LandingPage.ViewModel.LandingPageUserControlViewModel;

            if (!string.IsNullOrEmpty(vm.HeatMapKPIurl))
            {
                webBrowserInciHeatMapKPI.Navigate(new Uri(vm.HeatMapKPIurl));
            }
            ((WebBrowser)sender).ObjectForScripting = new HtmlInteropInternalTestClass();
        }
    }

    // Object used for communication from JS -> WPF
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class HtmlInteropInternalTestClass
    {
        public void endDragMarkerCS(Decimal Lat, Decimal Lng)
        {
            //((MainWindow)Application.Current.MainWindow).tbLocation.Text = Math.Round(Lat, 5) + "," + Math.Round(Lng, 5);
        }
    }
}
