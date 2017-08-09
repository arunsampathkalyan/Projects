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
using System.Windows.Threading;
using STC.Projects.WPFControlLibrary.VideoStreamingControl;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for TowerCamerasListActionPanelUserControl.xaml
    /// </summary>
    public partial class TowerCamerasListActionPanelUserControl : UserControl, IUserControl
    {
        public TowerCamerasListActionPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            var vm = new TowerCamerasListActionPanelViewModel();

            DataContext = vm;
        }

        public void ProcessMessage(TowerModel Tower)
        {
            var vm = DataContext as TowerCamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Tower);
        }

        public void ProcessMessage(DetectedAccidentTowerModel Tower)
        {
            var vm = DataContext as TowerCamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(Tower);
        }

        public event CanvasEventHandler CloseCanvas;
        protected virtual void OnCloseCanvas(CanvasEventArgs E)
        {
            var handler = CloseCanvas;
            if (handler != null)
                handler(this, E);
        }

        private async void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                //VlcContext.CloseAll();
                CloseVLC();
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

        public event CancelSOPEventHandler CancelSOP;
        protected virtual void OnCancelSOP(CancelSOPEventArgs E)
        {
            try
            {
                var handler = CancelSOP;
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

        private void FalseEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                //VlcContext.CloseAll();
                CloseVLC();
                OnCancelSOP(new CancelSOPEventArgs());
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

        //private void mediaElement1_MouseDown(object sender, MouseButtonEventArgs e)
        //{


        //    if (fullscreen == false)//e.ClickCount == 2 &&
        //    {
        //        flt_canvas_MediaPlayer.Visibility = Visibility.Hidden;

        //    }
        //    else if (fullscreen == true)//e.ClickCount == 2 &&
        //    {
        //        flt_canvas_MediaPlayer.Visibility = Visibility.Visible;

        //        var selectedVideo = e.Source as MediaElement;
        //        if (selectedVideo != null)
        //        {
        //            FullScreenVideo.Source = selectedVideo.Source;
        //        }
        //    }
        //    fullscreen = !fullscreen;
        //}

        private void ConfirmEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                //CamerasList.Items.

                CloseVLC();
                //  vlcControl.
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

        private void CloseVLC()
        {
            foreach (var item in CamerasList.Items)
            {
                ContentPresenter cp = CamerasList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                UserControl1 tb = Utility.FindVisualChild<UserControl1>(cp);
                if (tb != null)
                {
                    //  tb.CloseVLC();
                    tb.MouseDown -= UserControl1_MouseDown;
                    tb.TouchLeave -= UserControl1_TouchLeave;
                    tb.CloseVLC();
                }
            }

        }

        public void ExpandVideo(VideoStreamingControl.UserControl1 SelectedUC)
        {
            if (fullscreen == false)//e.ClickCount == 2 &&
            {
                GridMediaPlayer.Visibility = Visibility.Hidden;
                GridMediaPlayer.Children.RemoveAt(0);
                if (GridMediaPlayer.Children.Count > 1)
                    GridMediaPlayer.Children.RemoveAt(1);
            }
            else if (fullscreen == true)//e.ClickCount == 2 &&
            {
                GridMediaPlayer.Visibility = Visibility.Visible;
                GridMediaPlayer.Width = 360;
                GridMediaPlayer.Height = 180;
                var light = new VideoStreamingControl.UserControl1();
                light.Width = 380;
                light.Height = 180;
                light.MouseDown += UserControl1_MouseDown;
                light.TouchLeave += UserControl1_TouchLeave;
                // var SelectedUC = e.Source as VideoStreamingControl.UserControl1;
                light.TextExposedInXaml = SelectedUC.TextExposedInXaml;
                GridMediaPlayer.Children.Add(light);

                Button btn_min = new Button();
                btn_min.Content = "";
                btn_min.Click += btn_fullscreen_Click;
                GridMediaPlayer.Children.Add(btn_min);
            }
            fullscreen = !fullscreen;
        }
        private void UserControl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //ExpandVideo(e.Source as VideoStreamingControl.UserControl1);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void UserControl1_TouchLeave(object sender, TouchEventArgs e)
        {
            try
            {
                //ExpandVideo(e.Source as VideoStreamingControl.UserControl1);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }
        }

        private void btn_fullscreen_Click(object sender, RoutedEventArgs e)
        {
            #region Code
            try
            {
                if (fullscreen == false)
                {
                    GridMediaPlayer.Visibility = Visibility.Collapsed;

                    CamerasList.Visibility = Visibility.Visible;
                }
                else if (fullscreen == true)
                {
                    var button = e.Source as Button;
                    var param = button.CommandParameter;

                    CamerasList.Visibility = Visibility.Collapsed;

                    light.TextExposedInXaml = param.ToString();

                    GridMediaPlayer.Visibility = Visibility.Visible;
                    
                }

                fullscreen = !fullscreen;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                MessageBox.Show(Utility.GetErrorMessage());
                Utility.LogOut();
            }

            #endregion
        }
    }
}
