using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using SmartFlow.Shared.Helpers;

namespace SmartFlow.Shared.Converters
{
    /// <summary>
    /// This class is used for converting Image stream to image object, which can be set in an image view.
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        private static string TAG = "ImageConverter";

        /// <summary>
        /// Method used for converting stream to image.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            try
            {
                if (value != null)
                {
                    byte[] imageAsBytes = (byte[])value;
                    var stream = new MemoryStream(imageAsBytes);
                    retSource = ImageSource.FromStream(() => stream);
                }
                else
                {
                    retSource = ImageSource.FromFile(Utils.DEFAULT_PROFILE_PIC);
                }
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
            }
            return retSource;
        }

        /// <summary>
        /// Method to convert back the image object to target type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
