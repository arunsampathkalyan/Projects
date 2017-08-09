using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace STC.Projects.WPFControlLibrary.SOPBox.Converters
{
  public  class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null)
            {
                return value;
            }
            var doubleValue = 0.0;
            return double.TryParse(value.ToString(), out doubleValue) ? System.Convert.ToInt32(doubleValue) : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
