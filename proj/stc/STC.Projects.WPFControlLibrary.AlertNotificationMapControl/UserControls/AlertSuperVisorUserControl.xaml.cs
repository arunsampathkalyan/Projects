using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper;
using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel;
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
    /// Interaction logic for AlertSuperVisorUserControl.xaml
    /// </summary>
    public partial class AlertSuperVisorUserControl : UserControl
    {
        AlertUserControlViewModel _AlertUserControlVM;

        #region Constructor
        public AlertSuperVisorUserControl()
        {
            InitializeComponent();
        }

        public AlertSuperVisorUserControl(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            _AlertUserControlVM = new AlertUserControlViewModel(supervisorNotificationDTO);

            DataContext = _AlertUserControlVM;

            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("showAndHideAlertSupUC");
            sb.Begin();

        }


        #endregion

        #region EventHandlers




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

        public event GoToNextStepEventHandler GoToNextStep;
        protected virtual void OnGoToNextStep(Object sender, GoToNextStepEventArgs E)
        {
            try
            {
                var handler = GoToNextStep;
                if (handler != null)
                    handler(sender, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(Object sender, CanvasEventArgs E)
        {
            try
            {
                var handler = CloseCanvas;
                if (handler != null)
                    handler(sender, E);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        public event CanvasEventHandler AddChildContent;
        private void OnAddChildContent(object sender, CanvasEventArgs E)
        {
            try
            {
                var handler = AddChildContent;
                if (handler != null)
                    handler(sender, E);
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

        private async void ClosePopup()
        {
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyStoryboardCloseRight");
            sb.Begin();


        }

        private void btnClosePopup_Click(object Sender, RoutedEventArgs E)
        {
            try
            {
                ClosePopup();

                ClearControls();

                OnProcessNextAlert(new ProcessNextItemEventArgs() { CanProcessNextItem = true });
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btnViewDetailsReportedAlert_Click(object sender, RoutedEventArgs e)
        {
            ClosePopup();
            ShowNotifiedAlertDetails(_AlertUserControlVM.SupervisorNotificationDTO);
            OnProcessNextAlert(new ProcessNextItemEventArgs() { CanProcessNextItem = false });

        }

        private void ShowNotifiedAlertDetails(SupervisorNotificationDTO supervisorNotificationDTO)
        {
            var detailsCtrl = new NotifiedAlertViolatorDetailsUserControl(supervisorNotificationDTO);

            detailsCtrl.CloseCanvas += OnCloseCanvas;
            detailsCtrl.GoToNextStep += OnGoToNextStep;


            CanvasEventArgs canvasEventArgs = new CanvasEventArgs
            {
                ChildControl = detailsCtrl,
                Width = 400
            };

            OnAddChildContent(this, canvasEventArgs);
        }

        #endregion
    }
}
