using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace STC.Projects.ClassLibrary.Common.Converters
{


    public class HorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            HorizontalAlignment alignment;
            //if (value == null) return string.Empty;

            alignment = (Utility.GetLang() == "ar") ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Inverse":
                        {
                            alignment = (Utility.GetLang() == "ar") ? HorizontalAlignment.Left : HorizontalAlignment.Right;
                            break;
                        }
                    case "Right":
                        {
                            alignment = HorizontalAlignment.Right;
                            break;
                        }

                    case "Left":
                        {
                            alignment = HorizontalAlignment.Left;
                            break;
                        }
                }
            }

            return alignment;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public class FlowDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FlowDirection direction;
            //if (value == null) return string.Empty;

            direction = (Utility.GetLang() == "ar") ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Inverse":
                        {
                            direction = (Utility.GetLang() == "ar") ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
                            break;
                        }
                    case "RightToLeft":
                        {
                            direction = FlowDirection.RightToLeft;
                            break;
                        }

                    case "LeftToRight":
                        {
                            direction = FlowDirection.LeftToRight;
                            break;
                        }
                }
            }

            return direction;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
