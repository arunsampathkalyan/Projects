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
using STC.Projects.WPFControlLibrary.MapControl.Helper;
using STC.Projects.ClassLibrary.Common.ExtensionClasses;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.MessageBoxControl;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for SmartTowerDetails.xaml
    /// </summary>
    public partial class SmartTowerDetailsUserControl : UserControl
    {
        public SmartTowerDetailsUserControl(AssetsViewDTO Tower)
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            TowerDetailsViewModel vm = new TowerDetailsViewModel
            {
                Tower = Tower
            };

            vm.GetTowerCurrentMsg();

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

        private void SaveVMS_OnClick(object Sender, RoutedEventArgs E)
        {
            //ChangeTowerVMS 

            try
            {

                var button = E.Source as Button;

                if (button == null)
                    return;

                var vm = DataContext as TowerDetailsViewModel;

                if (vm == null)
                    return;

                var curItem = vm.Tower;

                if (curItem == null)
                    return;

                // Iterate whole listbox tree and search for this items
                if (vm.SelectedAction == null)
                    return;

                var msgBox = new MessageBoxUserControl("سوف يتم تغيير الرسالة . هل أنت متأكد؟", true);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();

                var res = msgBox.GetResult();

                if (res == false)
                    return;

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

                bool msgUpdated = vm.UpdateTowerMessage();

                lblCurrentMsg.Text = vm.SelectedAction.MessageDescription;
                lblCurrentMsg2.Text = vm.SelectedAction.MessageDescription;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
            }
        }
    }
}
