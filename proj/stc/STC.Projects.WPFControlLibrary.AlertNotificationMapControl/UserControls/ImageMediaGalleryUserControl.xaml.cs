using STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ImageMediaGalleryUserControl.xaml
    /// </summary>
    public partial class ImageMediaGalleryUserControl : UserControl
    {
        ImagePopupViewModel vm = null;
        protected bool isDragging;
        private Point clickPosition;

        public delegate void PublishDelegate(object sender, EventArgs e);
        public event PublishDelegate ImageListUpdated;

        public ImageMediaGalleryUserControl()
        {
            InitializeComponent();

            vm = new ImagePopupViewModel();
            if (vm != null)
                vm.ImageMediaSourceUpdated += vm_ImageMediaSourceUpdated;
            DataContext = vm;
        }


        void vm_ImageMediaSourceUpdated(object sender, System.EventArgs e)
        {
            //var handler = ImageListUpdated;
            //if (handler != null)
            //    handler(null, null);

            if (vm.IsVideoStreamingVisible)
            {
                mediaPlayerUC.mePlayer.Source = new Uri(vm.VideoURL);
                mediaPlayerUC.Play_Executed(this, null);
                mediaPlayerUC.Pause_Executed(this, null);
            }
            //vm.ActiveIndex = -1;
            OpenPopup();
        }

        void imgMain_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ChangeImageSize(imgMain, e.Delta);
        }

        void borderImageViwer_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            vm.ActiveIndex -= 1;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            vm.ActiveIndex += 1;
        }

        private void btnClosePopups_Click(object sender, RoutedEventArgs e)
        {
            if (vm.IsVideoStreamingVisible)
            {
                mediaPlayerUC.Stop_Executed(this, null);
                CloseUserControl();
            }
            vm.ActiveIndex = -1;
            gridImageSection.Visibility = Visibility.Collapsed;
            this.Visibility = Visibility.Collapsed;
        }

        private async void CloseUserControl()
        {
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyUserControlClose");
            sb.Begin();
            await Task.Delay(1000);
            this.Visibility = Visibility.Collapsed;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ChangeImageSize(imgMain, -120);
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ChangeImageSize(imgMain, 120);
        }

        private void btnDefaultSize_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultImageSize();
        }

        private void SetDefaultImageSize()
        {
            imgMain.Height = 580;
            imgMain.Width = 900;
        }

        private void ChangeImageSize(Image img, int deltaSize)
        {
            double height = img.ActualHeight;
            double width = img.ActualWidth;

            int delta = deltaSize;
            height += delta;
            width += delta;

            ValidateMinimumSizeForImage(img, height, width, 100, 150);
        }

        private void ValidateMinimumSizeForImage(Image img, double height, double width, double minHeight, double minWidth)
        {
            img.Height = height < minHeight ? minHeight : height;
            img.Width = width < minWidth ? minWidth : width;
        }

        private void listViewImageTray_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewImageTray.SelectedIndex != -1)
            {
                gridImageSection.Visibility = Visibility.Visible;
                SetDefaultImageSize();
            }
        }




        private async void OpenPopup()
        {
            this.Visibility = Visibility.Visible;
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyUserControlOpen");
            //if (sb != null && sb.Children.Count == 1)
            //{
            //    var dAOpen = sb.Children[0] as DoubleAnimation;
            //    if (dAOpen != null)
            //    {
            //        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            //        dAOpen.From = desktopWorkingArea.Right + 20;
            //        dAOpen.To = desktopWorkingArea.Right - (this.Width + 20);


            //    }

            //}
            sb.Begin();
            await Task.Delay(1000);

            //CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            //OnCloseCanvas(canvasEventArgs);
        }


    }
}
