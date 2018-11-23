﻿using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using SmartFlow.Shared.Helpers.Interfaces;
using SmartFlow.Droid.Locale_Android;
using SmartFlow.Shared.Helpers;

[assembly:Dependency(typeof(Locale_Android))]

namespace SmartFlow.Droid.Locale_Android
{
    /// <summary>
    /// Android Specific Class
    /// This class is used for mapping of Language ids between Xamarin and Android, as some langauge codes are different between
    /// both platforms. 
    /// </summary>
	public class Locale_Android : ILocale
	{
        private static string TAG = "Locale_Android";

        /// <summary>
        /// Set the Culture of the app
        /// </summary>
        /// <param name="ci"></param>
        public void SetLocale(CultureInfo ci)
		{
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

            LogHandler.AddLog(TAG, "CurrentCulture set: " + ci.Name);
		}

        /// <summary>
        /// Get the culture info of the app.
        /// If their is no culture info available, then default English is set as culture.
        /// </summary>
        /// <returns>CultureInfo</returns>
		public CultureInfo GetCurrentCultureInfo()
		{
			var netLanguage = "en";
			var androidLocale = Java.Util.Locale.Default;
			netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));

            //TODO : This gets called a lot - try/catch can be expensive so consider caching or something
            System.Globalization.CultureInfo ci = null;
			try
			{
				ci = new System.Globalization.CultureInfo(netLanguage);
			}
			catch (CultureNotFoundException e1)
			{
                LogHandler.AddExceptionLog(TAG, "", e1,true);

                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
				{
					var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    LogHandler.AddLog(TAG, netLanguage + " failed, trying " + fallback + " (" + e1.Message + ")");

					ci = new System.Globalization.CultureInfo(fallback);
				}
				catch (CultureNotFoundException e2)
				{
                    LogHandler.AddExceptionLog(TAG, "", e2, true);
                    LogHandler.AddLog(TAG, netLanguage + " couldn't be set, using 'en' (" + e2.Message + ")");

                    // iOS language not valid .NET culture, falling back to English
                    ci = new System.Globalization.CultureInfo("en");
				}
			}

			return ci;
		}

        /// <summary>
        /// Converts Android language code to .net Language code.
        /// </summary>
        /// <param name="androidLanguage"></param>
        /// <returns>.Net Language String</returns>
		string AndroidToDotnetLanguage(string androidLanguage)
		{
            LogHandler.AddLog(TAG, "Android Language:" + androidLanguage);

			var netLanguage = androidLanguage;

			//Certain languages need to be converted to CultureInfo equivalent
			switch (androidLanguage)
			{
				case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
				case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
				case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
					netLanguage = "ms"; // closest supported
					break;
				case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET 
					netLanguage = "id-ID"; // correct code for .NET
					break;
				case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
					netLanguage = "de-CH"; // closest supported
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}

            LogHandler.AddLog(TAG, ".NET Language/Locale:" + netLanguage);
			return netLanguage;
		}

        /// <summary>
        /// Converts .net Language code to Android language code
        /// </summary>
        /// <param name="platCulture"></param>
        /// <returns>Android Language Code string</returns>
		string ToDotnetFallbackLanguage(PlatformCulture platCulture)
		{
            LogHandler.AddLog(TAG, ".NET Fallback Language:" + platCulture.LanguageCode);

            // use the first part of the identifier (two chars, usually);
            var netLanguage = platCulture.LanguageCode; 

			switch (platCulture.LanguageCode)
			{
				case "gsw":
					netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}

            LogHandler.AddLog(TAG, ".NET Fallback Language/Locale:" + netLanguage + " (application-specific)");
			return netLanguage;
		}
	}

}

