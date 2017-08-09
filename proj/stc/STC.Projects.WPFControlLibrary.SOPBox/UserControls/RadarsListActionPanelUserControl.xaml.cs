using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.ExtensionClasses;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for RadarsListActionPanelUserControl.xaml
    /// </summary>
    public partial class RadarsListActionPanelUserControl : UserControl, IUserControl
    {
        ServiceLayerReference.ServiceLayerClient _client = new ServiceLayerReference.ServiceLayerClient();
        public RadarsListActionPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            var vm = new RadarsListActionPanelViewModel();

            DataContext = vm;
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            var vm = DataContext as RadarsListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            var vm = DataContext as RadarsListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
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
                var vm = DataContext as RadarsListActionPanelViewModel;

                if (vm == null)
                    return;

                if (vm.RadarsList == null)
                    return;

                foreach (var curItem in vm.RadarsList)
                {
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
                }

                lblCurrentSpeed.Text = Properties.Resources.strSpeedLimitSaved;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }
        }

        public event GoToNextStepEventHandler GoToNextStep;
        protected virtual void OnGoToNextStep(GoToNextStepEventArgs E)
        {
            try
            {
                var handler = GoToNextStep;
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

        private void ConfirmEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                OnGoToNextStep(new GoToNextStepEventArgs
                {
                    Confirmation = true
                });
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void SkipEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                OnGoToNextStep(new GoToNextStepEventArgs
                {
                    Confirmation = false
                });
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
