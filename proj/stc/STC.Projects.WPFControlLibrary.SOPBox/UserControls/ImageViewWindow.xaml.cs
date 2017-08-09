using STC.Projects.WPFControlLibrary.SOPBox.Converters;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STC.Projects.WPFControlLibrary.SOPBox.UserControls
{
    /// <summary>
    /// Interaction logic for ImageViewWindow.xaml
    /// </summary>
    public partial class ImageViewWindow : Window
    {
        public ImageViewWindow()
        {
            InitializeComponent();

        }

        public ImageViewWindow(string imgSource)
        {
            InitializeComponent();

            imgChild.Source = new BitmapImage(new Uri(imgSource));

            this.MouseLeftButtonDown += ImageViewWindow_MouseLeftButtonDown;
            imgChild.MouseWheel += imgChild_MouseWheel;

            //this.Owner = Application.Current.MainWindow;
        }

        public ImageViewWindow(string imgSource , bool base64)
        {
            InitializeComponent();

            //imgChild. = ImageSource;
            BitmapImage btm;
            Base64ImageConverter base64Image = new Base64ImageConverter();
            btm = (BitmapImage)base64Image.Convert(imgSource, null, null, null);

            imgChild.Source = btm;

            this.MouseLeftButtonDown += ImageViewWindow_MouseLeftButtonDown;
            imgChild.MouseWheel += imgChild_MouseWheel;

            //this.Owner = Application.Current.MainWindow;
        }

        void imgChild_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double height = imgChild.Height;
            double width = imgChild.Width;

            int delta = e.Delta;
            height += delta;
            width += delta;

            imgChild.Height = height < 150 ? 150 : height;
            imgChild.Width = width < 200 ? 200 : width;
        }

        void ImageViewWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


    }
}
