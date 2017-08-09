using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace STC.Projects.WPFControlLibrary.ViolationsMapControl.Converter
{
    class SliderConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = DateTime.Now.Date.AddHours(-((double)value));
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotSupportedException("Not supported");

        }
    }
}
