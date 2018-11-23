using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Helpers.Interfaces;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFlow.Shared
{
    /// <summary>
    /// Translation class to handle all langauge related functions
    /// </summary>
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci = null;
        const string ResourceId = "SmartFlow.Shared.Resources.AppResources";
        /// <summary>
        /// set up language culture based on IOS or Android platform.
        /// </summary>
        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo();
                if (!string.IsNullOrEmpty(Settings.GetValueOrDefault(Utils.PREFS_KEY_LANGUAGE_SELECTION)))
                {
                    ci = new CultureInfo(Settings.GetValueOrDefault(Utils.PREFS_KEY_LANGUAGE_SELECTION));
                }
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager temp = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = temp.GetString(Text, ci);
            if (translation == null)
            {
//#if DEBUG
//                throw new ArgumentException(
//                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
//                    "Text");
//#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
//#endif
            }
            return translation;
        }
    }
}
