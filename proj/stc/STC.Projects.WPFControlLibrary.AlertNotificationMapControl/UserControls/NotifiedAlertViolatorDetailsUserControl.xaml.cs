using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel;
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

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls
{
    /// <summary>
    /// Interaction logic for NotifiedAlertViolatorDetailsUserControl.xaml
    /// </summary>
    public partial class NotifiedAlertViolatorDetailsUserControl : UserControl, IUserControl
    {
        NotifiedAlertViolatorDetailsUserControlViewModel _NotifiedAlertViolatorDetailsUcVM;

        #region Constructor

        public NotifiedAlertViolatorDetailsUserControl()
        {
            InitializeComponent();
        }

        public NotifiedAlertViolatorDetailsUserControl(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            _NotifiedAlertViolatorDetailsUcVM = new NotifiedAlertViolatorDetailsUserControlViewModel(supervisorNotificationDTO);
            _NotifiedAlertViolatorDetailsUcVM.NotificationHandled += _NotifiedAlertViolatorDetailsUcVM_NotificationHandled;

            DataContext = _NotifiedAlertViolatorDetailsUcVM;

            this.Loaded += NotifiedAlertViolatorDetailsUserControl_Loaded;
            //OnProcessNextAlert(new ProcessNextItemEventArgs() { CanProcessNextItem = false});

        }

        void NotifiedAlertViolatorDetailsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (imgMediaGalleryUc.DataContext != null && (imgMediaGalleryUc.DataContext as ImagePopupViewModel != null))
            {
                _NotifiedAlertViolatorDetailsUcVM.ImagePoupVM = imgMediaGalleryUc.DataContext as ImagePopupViewModel;

                SetDimentionsForImageMediaUC();
            }
        }

        void _NotifiedAlertViolatorDetailsUcVM_NotificationHandled(object sender, EventArgs e)
        {
            if (_NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO.Status != SupervisorNotificationStatus.Pending)
            {
                OnGoToNextStep(new GoToNextStepEventArgs
                {
                    Confirmation = true,
                    Notification = _NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO
                });



                Application.Current.Dispatcher.Invoke(() =>
                    {
                        ClearControls();


                        ClosePopup();
                    });


                CanvasEventArgs canvasEventArgs = new CanvasEventArgs();
                OnCloseCanvas(canvasEventArgs);
            }

            //throw new NotImplementedException();
        }

        #endregion

        #region Commented


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

        #endregion

        #region EventHandlers
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

        private void btnClosePopup_Click(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();

                ClearControls();

                if (_NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO != null && _NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO.Status != SupervisorNotificationStatus.Pending)
                {
                    _NotifiedAlertViolatorDetailsUcVM.SetSupervisorNotificationNoticed(_NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO, true);
                    OnGoToNextStep(new GoToNextStepEventArgs() { Confirmation = true, Notification = _NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO });
                }
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

            SendNotificationResultToReporter(false);
        }

        private void SendNotificationResultToReporter(bool isApproveReport)
        {

            string msg = isApproveReport ? Properties.Resources.strMsgApproveNotification : Properties.Resources.strMsgRejectNotification;
            var msgBox = new MessageBoxUserControl(msg, true);
            msgBox.Owner = Window.GetWindow(this);
            msgBox.ShowDialog();

            var res = msgBox.GetResult();

            if (res == false)
                return;
            _NotifiedAlertViolatorDetailsUcVM.currentUserid = Utility.GetCurrentUserId(this);
            _NotifiedAlertViolatorDetailsUcVM.SendNotificationToReporter(isApproveReport);



        }


        private void Approve_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {

                SendNotificationResultToReporter(true);

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

        #endregion

        private void Attachement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //_NotifiedAlertViolatorDetailsUcVM.SupervisorNotificationDTO.DangerousViolatorDetails.MediaType

            _NotifiedAlertViolatorDetailsUcVM.SetMediaSource();
        }

        private void SetDimentionsForImageMediaUC()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            imgMediaGalleryUc.Width = desktopWorkingArea.Width - 300;
            imgMediaGalleryUc.Height = desktopWorkingArea.Height - 100;

            imgMediaGalleryUc.Width = 1320;
            imgMediaGalleryUc.Height = 920;
            imgMediaGalleryUc.Margin = new Thickness(-600, -450, -500, -200);
            //imgMediaGalleryUc.Opacity = 1;
        }

    }
}
