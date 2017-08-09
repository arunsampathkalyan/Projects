using System.Windows.Media.Animation;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using System;
using System.Collections.Generic;
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
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using System.Messaging;
using STC.Projects.ClassLibrary.Common;
using Telerik.Windows.Controls;
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.ClassLibrary.Common.ExtensionClasses;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.MessageBoxControl;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for RadarDetails.xaml
    /// </summary>
    public partial class RadarDetailsUserControl : UserControl
    {
        ServiceLayerReference.ServiceLayerClient _client = new ServiceLayerReference.ServiceLayerClient();
       
        public RadarDetailsUserControl(AssetsViewDTO Radar)
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            RadarDetailsViewModel vm = new RadarDetailsViewModel
            {
                Radar = Radar,
                OldSpeedValue = Radar.CurrentValue != "" ? Radar.CurrentValue : "-"
            };

            DataContext = vm;
        }

        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            try
            {
                var handler = CloseCanvas;
                if (handler != null)
                    handler(this, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }


        private async void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                Storyboard sb = new Storyboard();
                sb = (Storyboard)TryFindResource("MyStoryboard");
                sb.Begin();
                await Task.Delay(1000);

                CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

                OnCloseCanvas(canvasEventArgs);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void SaveSpeedLimit_OnClick(object Sender, RoutedEventArgs E)
        {
            //ChangeRadarSpeed 

            try
            {

                var button = E.Source as Button;

                if (button == null)
                    return;

                var vm = DataContext as RadarDetailsViewModel;

                if (vm == null)
                    return;

                var curItem = vm.Radar as AssetsViewDTO;

                if (curItem == null)
                    return;

                var msgBox = new MessageBoxUserControl("سوف يتم تغيير السرعة . هل أنت متأكد؟", true);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();

                var res = msgBox.GetResult();

                if (res == false)
                    return;

                _client.UpdateAssetValueAsync(curItem.ItemId, vm.NewSpeedValue.ToString());
                 
                curItem.SelectedSpeed = vm.NewSpeedValue;

                MessageQueue msgQ = new MessageQueue(".\\private$\\ChangeRadarSpeed");

                Message msg = new Message
                {
                    Label = "Change Radar Speed for " + curItem.ItemName,
                    Body = curItem.SerializeObject(),
                    UseDeadLetterQueue = true
                };

                msgQ.Send(msg);
                vm.OldSpeedValue = vm.NewSpeedValue.ToString();
                lblCurrentSpeed.Text = vm.NewSpeedValue.ToString();
                //lblCurrentSpeed2.Text = vm.NewSpeedValue.ToString();
                lblCurrentSpeed2.Text = Properties.Resources.strSpeedLimitSaved;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }
    }
}
