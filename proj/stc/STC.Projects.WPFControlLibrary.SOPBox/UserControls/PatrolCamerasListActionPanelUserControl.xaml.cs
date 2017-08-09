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
    public partial class PatrolCamerasListActionPanelUserControl : UserControl, IUserControl
    {
        public PatrolCamerasListActionPanelUserControl()
        {
            Properties.Resources.Culture = new CultureInfo(Utility.GetLang());

            InitializeComponent();


            var vm = new PatrolCamerasListActionPanelViewModel();

            DataContext = vm;
        }

        public void ProcessMessage(FogLocationModel LocationModel)
        {
            var vm = DataContext as PatrolCamerasListActionPanelViewModel;

            if (vm == null)
                return;

            vm.SetViewModelData(LocationModel);
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

        private void CloseVLC()
        {
            foreach (var item in PatrolsList.Items)
            {
                ContentPresenter cp = PatrolsList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
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



        private async void ClosePopup_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                //VlcContext.CloseAll();
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

        private void ConfirmEvent_OnClick(object Sender, RoutedEventArgs E)
        {
            try
            {
                // VlcContext.CloseAll();
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

        public void ExpandVideo(VideoStreamingControl.UserControl1 SelectedUC)
        {
            if (fullscreen == false)//e.ClickCount == 2 &&
            {
                flt_canvas_MediaPlayer.Visibility = Visibility.Hidden;
                flt_canvas_MediaPlayer.Children.RemoveAt(0);
            }
            else if (fullscreen == true)//e.ClickCount == 2 &&
            {
                flt_canvas_MediaPlayer.Visibility = Visibility.Visible;
                var light = new VideoStreamingControl.UserControl1();
                light.Width = 300;
                light.Height = 180;
                light.MouseDown += UserControl1_MouseDown;
                light.TouchLeave += UserControl1_TouchLeave;
                // var SelectedUC = e.Source as VideoStreamingControl.UserControl1;
                light.TextExposedInXaml = SelectedUC.TextExposedInXaml;
                flt_canvas_MediaPlayer.Children.Add(light);
            }
            fullscreen = !fullscreen;
        }
        private void UserControl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ExpandVideo(e.Source as VideoStreamingControl.UserControl1);
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
                ExpandVideo(e.Source as VideoStreamingControl.UserControl1);
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
