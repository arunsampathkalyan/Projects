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
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for SmartTowersListActionPanelUserControl.xaml
    /// </summary>
    public partial class SmartTowersListActionPanelUserControl : UserControl, IUserControl
    {
        public SmartTowersListActionPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            var vm = new SmartTowersListActionPanelViewModel();

            DataContext = vm;
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            var vm = DataContext as SmartTowersListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            var vm = DataContext as SmartTowersListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            var vm = DataContext as SmartTowersListActionPanelViewModel;

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

        private void SaveVMS_OnClick(object Sender, RoutedEventArgs E)
        {
            //ChangeTowerVMS 

            try
            {

                var vm = DataContext as SmartTowersListActionPanelViewModel;

                if (vm == null)
                    return;

                if (vm.SelectedAction == null || vm.TowersList == null)
                    return;

                foreach (var curItem in vm.TowersList)
                {

                    curItem.SelectedAction = new TowerActionsDTO
                    {
                        Description = vm.SelectedAction.MessageDescription,
                        TowerActionId = vm.SelectedAction.MessageId
                    };

                    MessageQueue msgQ = new MessageQueue(".\\private$\\ChangeTowerVMS");

                    Message msg = new Message
                    {
                        Label = "Change VMS Message for " + curItem.ItemName,
                        Body = curItem.SerializeObject(),
                        UseDeadLetterQueue = true
                    };

                    msgQ.Send(msg);

                    bool msgUpdated = vm.UpdateTowerMessage(curItem);
                }

                lblCurrentMsg.Text = vm.SelectedAction.MessageDescription;

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
