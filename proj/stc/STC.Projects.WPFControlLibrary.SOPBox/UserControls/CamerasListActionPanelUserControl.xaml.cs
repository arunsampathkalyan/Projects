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
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.WPFControlLibrary.SOPBox.Helper;
using STC.Projects.WPFControlLibrary.SOPBox.Model;
using STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;
using STC.Projects.ClassLibrary.ControlMessages;
using STC.Projects.ClassLibrary.Common.Enums;
using Telerik.Windows.Controls;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for CamerasListActionPanelUserControl.xaml
    /// </summary>
    public partial class CamerasListActionPanelUserControl : UserControl, IUserControl
    {
        private UserControl _parentUserControl;
        public CamerasListActionPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();

            _parentUserControl = null;
            var vm = new CamerasListActionPanelViewModel();
            vm.CamerasLoaded += vm_CamerasLoaded;
            DataContext = vm;
        }

        void vm_CamerasLoaded(object sender, CanvasEventArgs e)
        {
            try
            {
                var vm = DataContext as CamerasListActionPanelViewModel;

                if (vm == null || vm.CamerasList == null || vm.CamerasList.Count == 0 || _parentUserControl == null)
                    return;

                foreach (var selectedCamera in vm.CamerasList)
                {
                    var drawMessage = new SOPMapDraw() { Lat = selectedCamera.Latitude.Value, Lon = selectedCamera.Longitude.Value, ObjectTypeToDraw = (int)MarkerType.Assets, ObjectToDraw = selectedCamera };
                    var zoomMessage = new SOPMapZoom() { Lat = selectedCamera.Latitude.Value, Lon = selectedCamera.Longitude.Value };

                    _parentUserControl.Publish(drawMessage);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        public void SetParentUserControl(UserControl parent)
        {
            _parentUserControl = parent;
        }

        public void ProcessMessage(FogLocationModel Location)
        {
            var vm = DataContext as CamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void ProcessMessage(DetectedAccidentLocationModel Location)
        {
            var vm = DataContext as CamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void ProcessMessage(WantedCarModel Location)
        {
            var vm = DataContext as CamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Location);
        }

        public void UpdateCameras(WantedCarLocationChanged message)
        {
            var vm = DataContext as CamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(new WantedCarModel { Latitude = message.Lat.Value, Longitude = message.Lon.Value });
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
        private bool fullscreen = true;
        private void mediaElement1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (fullscreen == false)//e.ClickCount == 2 &&
                {
                    flt_canvas_MediaPlayer.Visibility = Visibility.Hidden;

                }
                else if (fullscreen == true)//e.ClickCount == 2 &&
                {
                    flt_canvas_MediaPlayer.Visibility = Visibility.Visible;

                    var selectedVideo = e.Source as MediaElement;
                    if (selectedVideo != null)
                    {
                        FullScreenVideo.Source = selectedVideo.Source;
                    }
                }
                fullscreen = !fullscreen;
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
    }
}
