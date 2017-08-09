using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper
{
    public class AlertStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Visible;
            if (value is SupervisorNotificationStatus && (Enum.Equals(value, SupervisorNotificationStatus.Approved) || Enum.Equals(value, SupervisorNotificationStatus.Rejected)))
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
