using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.DTO;


using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using STC.Projects.WPFControlLibrary.MessageBoxControl;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.Model;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for DangerousViolatorDetailsUserControl.xaml
    /// </summary>
    public partial class DangerousViolatorDetailsUserControl : UserControl, IUserControl
    {

        DagerousViolatorDetailsUserControlViewModel vm = null;
        public DangerousViolatorDetailsUserControl()
        {
            InitializeComponent();
            this.Loaded += DangerousViolatorDetailsUserControl_Loaded;
            vm = new DagerousViolatorDetailsUserControlViewModel();
            //if (imgGalleryUc.DataContext != null && (imgGalleryUc.DataContext as ImagePopupViewModel != null))
            //{
            //    vm.ImagePoupVM = imgGalleryUc.DataContext as ImagePopupViewModel;
            //}
            DataContext = vm;
        }

        void DangerousViolatorDetailsUserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (imgMediaGalleryUc.DataContext != null && (imgMediaGalleryUc.DataContext as ImagePopupViewModel != null))
            {
                vm.ImagePoupVM = imgMediaGalleryUc.DataContext as ImagePopupViewModel;

                SetDimentionsForImageMediaUC();
            }

            if (violatorItemImgUc.DataContext != null && (violatorItemImgUc.DataContext as ViolationItemDetailsViewModel != null))
            {
                vm.ViolationImageDetailsVM = violatorItemImgUc.DataContext as ViolationItemDetailsViewModel;

                vm.ViolationImageDetailsVM.ViolatorSearched += ViolationImageDetailsVM_ViolatorSearched;

                SetDimentionsForVioltionImageUC();
            }
        }

        void ViolationImageDetailsVM_ViolatorSearched(object sender, EventArgs e)
        {
            vm.ViolationImageDetailsVM_ViolatorSearched(sender, e);
        }

        private void SetDimentionsForImageMediaUC()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            imgMediaGalleryUc.Width = desktopWorkingArea.Width - 300;
            imgMediaGalleryUc.Height = desktopWorkingArea.Height - 100;

            imgMediaGalleryUc.Width = 1320;
            imgMediaGalleryUc.Height = 920;
            imgMediaGalleryUc.Margin = new Thickness(-910, -350, -275, -320);
            //imgMediaGalleryUc.Opacity = 1;
        }

        private void SetDimentionsForVioltionImageUC()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            violatorItemImgUc.Width = desktopWorkingArea.Width - 300;
            violatorItemImgUc.Height = desktopWorkingArea.Height - 100;

            violatorItemImgUc.Width = 650;
            violatorItemImgUc.Height = 700;
            violatorItemImgUc.Margin = new Thickness(-910, -200, 50, -100);
            //imgMediaGalleryUc.Opacity = 1;
        }
        /*
        public event ProcessNextItemEventHandler ProcessNextAlert;

        protected virtual void OnProcessNextAlert(ProcessNextItemEventArgs E)
        {
            try
            {
                var handler = ProcessNextAlert;
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
         */

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

        private void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                imgMediaGalleryUc.Visibility = Visibility.Collapsed;
                ClosePopup();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private async void ClosePopup()
        {
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyStoryboard");
            sb.Begin();
            await Task.Delay(1000);

            CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            OnCloseCanvas(canvasEventArgs);
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

        public void ProcessMessage(WantedCarModel Location)
        {
            if (vm == null)
                return;



            vm.PlateNumber = Location.VehiclePlateNumber;

            vm.SetCurrentWantedVehicle(Location);
            vm.RetriveVehicleDetails();


        }

        private void Approve_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {

                var msgBox = new MessageBoxUserControl(Properties.Resources.strApprovalConfirmation, true);
                msgBox.Owner = Window.GetWindow(this);
                msgBox.ShowDialog();

                var res = msgBox.GetResult();

                if (res == false)
                    return;


                OnGoToNextStep(new GoToNextStepEventArgs
                {
                    Confirmation = true
                });

                //OnProcessNextAlert(new ProcessNextItemEventArgs() { CanProcessNextItem = true });


                ClosePopup();
                ClearControls();

                //    var msgBoxDone = new MessageBoxUserControl(Properties.Resources.strDispatchedPatrolConfirm, false);
                //    msgBoxDone.Owner = Window.GetWindow(this);
                //    msgBoxDone.ShowDialog();

                //    var resCon = msgBoxDone.GetResult();

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

        private void ClearControls()
        {
            //CommentText.Text = "";
        }

        private void btnClosePopup_Click(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();

                ClearControls();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnRejectReportedAlert_Click(object sender, RoutedEventArgs e)
        {

            var msgBox = new MessageBoxUserControl("Are you sure want to Reject?", true);
            msgBox.Owner = Window.GetWindow(this);
            msgBox.ShowDialog();

            var res = msgBox.GetResult();

            if (res == false)
                return;

            OnGoToNextStep(new GoToNextStepEventArgs
            {
                Confirmation = false
            });

            //OnProcessNextAlert(new ProcessNextItemEventArgs() { CanProcessNextItem = true});


            ClearControls();

            //    var msgBoxDone = new MessageBoxUserControl(Properties.Resources.strDispatchedPatrolConfirm, false);
            //    msgBoxDone.Owner = Window.GetWindow(this);
            //    msgBoxDone.ShowDialog();

            //    var resCon = msgBoxDone.GetResult();

            CanvasEventArgs canvasEventArgs = new CanvasEventArgs();
            ClosePopup();

            OnCloseCanvas(canvasEventArgs);

            //ClosePopup();
            //Storyboard OpenNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("OpenNotifiedViolatorDetailsinRight");
            //OpenNotifiedAlertDetailsPopup.Begin();

            //Storyboard CloseNotifiedAlertDetailsPopup = (Storyboard)TryFindResource("CloseNotifiedViolatorDetailsinRight");
            //CloseNotifiedAlertDetailsPopup.Begin();
        }
    }
}
