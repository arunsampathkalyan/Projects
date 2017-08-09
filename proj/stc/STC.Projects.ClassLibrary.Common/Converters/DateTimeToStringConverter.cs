using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace STC.Projects.ClassLibrary.Common.Converters
{


    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Date":
                        return ((DateTime)value).ToString("dd/MM/yyyy");

                    case "DateTime":
                        {
                            if (Utility.GetLang() == "en")
                                return ((DateTime)value).ToString("d/M/yyyy H:mm:ss");
                            else
                                return ((DateTime)value).ToString("yyyy/M/d H:mm:ss");
                        }
                }
            }
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
