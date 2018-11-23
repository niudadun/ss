using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.Converters
{
    /// <summary>
    /// This class is used to Trim the data
    /// </summary>
    public class TrimConverter : IValueConverter
    {
        private static string TAG = "TrimConverter";

        /// <summary>
        /// Method to trim the value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = value as string;
            if (s == null)
                return value;
            return s.Trim();
        }

        /// <summary>
        /// Method to convert back the value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = value as string;
            if (s == null)
                return value;
            return s.Trim();
        }
    }
}
