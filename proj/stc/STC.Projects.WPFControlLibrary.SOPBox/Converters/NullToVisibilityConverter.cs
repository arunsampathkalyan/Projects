using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using STC.Projects.WPFControlLibrary.SOPBox.ServiceLayerReference;

namespace STC.Projects.WPFControlLibrary.SOPBox.Converters
{
    class NullToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null)
            {
                return Visibility.Visible;
            }
            if (!(value is ObservableCollection<PatrolDTO>)) return Visibility.Collapsed;
            return ((ObservableCollection<PatrolDTO>)value).Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
