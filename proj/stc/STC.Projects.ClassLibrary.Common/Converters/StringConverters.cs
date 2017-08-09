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
    public class StringEnglishToArabicSelectorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Utility.GetLang() == "ar")
                return (value != null && value.Count() > 0 && value[0] != null ? value[0].ToString() : "");
            else
                return (value != null && value.Count() > 1 && value[1] != null ? value[1].ToString() : "");

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringAppendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (parameter != null)
            {
                string stringFormat = (Utility.GetLang() == "ar") ? "%{0:#,0.##}" : "{0:#,0.##}%";
                switch (parameter.ToString())
                {
                    case "Percentage":
                        {
                            double tempValue = (((double)value) < 1) ? (((double)value) * 100) : ((double)value);


                            //if (Utility.GetLang() == "ar")
                            //{
                            //    if (((double)value) < 1)
                            //        return string.Format("%{0:#,0.##}", (((double)value) * 100));
                            //    return string.Format("%{0:#,0.##}", ((double)value));
                            //}
                            //else
                            //{
                            //    if (((double)value) < 1)
                            //        return string.Format("{0:#,0.##}%", (((double)value) * 100));
                            //    return string.Format("{0:#,0.##}%", ((double)value));
                            //}

                            //string.Format("{0:#,0.##%}" - Internally multiplies to 100

                            return string.Format(stringFormat, tempValue);
                        }

                    case "ActualPercentage":
                        {
                            //if (Utility.GetLang() == "ar")
                            //    return string.Format("%{0}", ((double)value));
                            //else
                            //    return string.Format("{0}%", ((double)value));

                            return string.Format(stringFormat, ((double)value));
                        }

                }
                return value.ToString() + parameter.ToString();
            }

            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Status":
                        {

                            if (Utility.GetLang() == "ar")
                            {

                                return (((Boolean)value) == false) ? "مفعل" : "غير مفعل";
                            }
                            else
                            {

                                return (((Boolean)value) == false) ? "Enabled" : "Disabled";
                            }

                        }

                    case "IsDeleted":
                        {
                            if (Utility.GetLang() == "ar")
                            {

                                return (((Boolean)value) == true) ? "غير مفعل" : "مفعل";
                            }
                            else
                            {

                                return (((Boolean)value) == true) ? "Disabled" : "Enabled";
                            }
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


    public class BoolToColorConverter : IValueConverter
    {
        SolidColorBrush redBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("Green");
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Status":
                        {

                            return ((((Boolean)value) == false) ? ((SolidColorBrush)new BrushConverter().ConvertFromString("Green")) : ((SolidColorBrush)new BrushConverter().ConvertFromString("Red")));

                        }

                    case "IsDeleted":
                        {

                            return ((((Boolean)value) == false) ? ((SolidColorBrush)new BrushConverter().ConvertFromString("Green")) : ((SolidColorBrush)new BrushConverter().ConvertFromString("Red")));

                        }

                }

            }

            return redBrush;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ChartValuetoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value.ToString();
            double myValue;
            if (double.TryParse(val, out myValue))
            {
                return Math.Abs(myValue) < 0.001 ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
