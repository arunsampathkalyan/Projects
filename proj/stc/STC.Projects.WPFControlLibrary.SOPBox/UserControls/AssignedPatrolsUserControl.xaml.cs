using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using System.Windows.Media.Animation;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for AssignedPatrolsUserControl.xaml
    /// </summary>
    public partial class AssignedPatrolsUserControl : UserControl
    {
        AssignedPatrolsViewModel _assignedPatrolsViewModel;
        //ImagePopupWindow popupWindow = null;

        //DagerousViolatorDetailsUserControlViewModel _assignedPatrolsViewModel = null;
        public AssignedPatrolsUserControl()
        {
            InitializeComponent();
            _assignedPatrolsViewModel = new AssignedPatrolsViewModel();
            DataContext = _assignedPatrolsViewModel;
            this.Loaded += AssignedPatrolsUserControl_Loaded;
            this.Unloaded += AssignedPatrolsUserControl_Unloaded;

            //_assignedPatrolsViewModel = new DagerousViolatorDetailsUserControlViewModel();

            // DataContext = _assignedPatrolsViewModel;
        }

        void AssignedPatrolsUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            imgMediaGalleryUc.Visibility = Visibility.Collapsed;
        }

        void AssignedPatrolsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //_assignedPatrolsViewModel = this.DataContext as AssignedPatrolsViewModel;

            if (_assignedPatrolsViewModel == null) return;


            if (imgMediaGalleryUc.DataContext != null && (imgMediaGalleryUc.DataContext as ImagePopupViewModel != null))
            {
                _assignedPatrolsViewModel.ImagePoupVM = imgMediaGalleryUc.DataContext as ImagePopupViewModel;

                SetDimentionsForImageMediaUC();
            }

           
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

        public void SetNotificationID(long notificationId)
        {
            if (_assignedPatrolsViewModel != null)
            {
                _assignedPatrolsViewModel.NotificationId = notificationId;
                _assignedPatrolsViewModel.GetAssignedPatrols();
            }
        }

        private void PatrolsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  throw new NotImplementedException();
        }


        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            var handler = CloseCanvas;
            if (handler != null)
                handler(this, E);
        }

        public event CanvasEventHandler AddChildContent;
        protected virtual void OnAddChildContent(CanvasEventArgs E)
        {
            try
            {
                var handler = AddChildContent;
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

        //private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    _assignedPatrolsViewModel = this.DataContext as AssignedPatrolsViewModel;

        //    if (_assignedPatrolsViewModel == null) return;

        //    if (_assignedPatrolsViewModel.ImagePoupVM != null)
        //    {
        //        popupWindow.Show();

        //        popupWindow.Activate();
        //        popupWindow.Focus();
        //        popupWindow.Topmost = true;

        //        _assignedPatrolsViewModel.ImagePoupVM.ImageURLBitmap = null;
        //    }

        //}

        //private void Video_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    _assignedPatrolsViewModel = this.DataContext as AssignedPatrolsViewModel;

        //    if (_assignedPatrolsViewModel == null) return;

        //    if (_assignedPatrolsViewModel.ImagePoupVM != null)
        //    {
        //        popupWindow.Show();

        //        popupWindow.Activate();
        //        popupWindow.Focus();
        //        popupWindow.Topmost = true;

        //        _assignedPatrolsViewModel.ImagePoupVM.VideoURL = string.Empty;
        //    }
        //}

    }
}
