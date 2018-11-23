using SmartFlow.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.Converters
{
    /// <summary>
    /// DateTime converter for converting date string to given format
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        #region IValueConverter implementation

        /// <summary>
        /// Convert date to given options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            return DateTime.ParseExact(value.ToString(), Utils.DATE_FORMAT,culture);
        }

        /// <summary>
        ///  Convert date to given options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var datetime = (DateTime)value;
            return datetime.ToString(Utils.DATE_FORMAT);
        }

        #endregion
    }
}
