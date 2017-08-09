using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace STC.Projects.WPFControlLibrary.SOPBox.Converters
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;

            if (s == null)
                return null;

            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            bi.EndInit();

            return bi;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                
                BitmapImage image = value as BitmapImage;

                encoder.Frames.Add(BitmapFrame.Create(image));
                // Convert Image to byte[]
                encoder.Save(ms);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = System.Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
