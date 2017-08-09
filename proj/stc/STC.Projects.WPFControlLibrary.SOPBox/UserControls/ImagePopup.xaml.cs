using STC.Projects.ClassLibrary.Common;
using STC.Projects.WPFControlLibrary.SOPBox.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for ImagePopup.xaml
    /// </summary>
    public partial class ImagePopup : UserControl
    {
        ImagePopupViewModel vm = null;
        protected bool isDragging;
        private Point clickPosition;

        public ImagePopup()
        {
            InitializeComponent();
            //this.MouseLeftButtonDown += ImagePopup_MouseLeftButtonDown;
            //this.MouseLeftButtonUp += ImagePopup_MouseLeftButtonUp;
            //this.MouseMove += ImagePopup_MouseMove;
            vm = new ImagePopupViewModel();
            if (vm != null)
                vm.ImageMediaSourceUpdated += vm_ImageMediaSourceUpdated;
            DataContext = vm;
        }

        void ImagePopup_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var draggableControl = sender as UserControl;

            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
            }
        }

        void ImagePopup_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as UserControl;
            clickPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();
        }

        void ImagePopup_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = sender as UserControl;
            draggable.ReleaseMouseCapture();
        }

        void vm_ImageMediaSourceUpdated(object sender, System.EventArgs e)
        {
            OpenPopup();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
            this.Visibility = Visibility.Collapsed;

            //CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            //OnCloseCanvas(canvasEventArgs);
        }

        private async void OpenPopup()
        {
            this.Visibility = Visibility.Visible;
            Storyboard sb = new Storyboard();
            sb = (Storyboard)TryFindResource("MyStoryboardOpen");
            sb.Begin();
            await Task.Delay(1000);

            //CanvasEventArgs canvasEventArgs = new CanvasEventArgs();

            //OnCloseCanvas(canvasEventArgs);
        }

        private void btnImagePopupClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender as Button != null && ((((sender as Button).Parent as Grid).Parent as Border).Parent as System.Windows.Controls.Primitives.Popup != null))
                {
                    ((((sender as Button).Parent as Grid).Parent as Border).Parent as System.Windows.Controls.Primitives.Popup).IsOpen = false;
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender as Image != null && (sender as Image).DataContext as String != null)
            {
                ImageViewWindow childWnd = new ImageViewWindow((sender as Image).DataContext.ToString());
                childWnd.Show();

                //Image container = (Image)sender;
               
                childWnd.Activate();
                childWnd.Focus();
                childWnd.Topmost = true;

                
               // this.AddChild(childWnd);

                //childWnd.BringIntoView();
            }

        }
    }
}
