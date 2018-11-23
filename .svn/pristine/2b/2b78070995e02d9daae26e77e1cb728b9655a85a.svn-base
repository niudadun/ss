// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SmartFlow.Shared.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
        private static string TAG = "Settings";

        private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion

        /// <summary>
        /// General Settings getter setter for adding values to the preference storage
        /// </summary>
		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}


        /// <summary>
        /// Method to get the value from settings using key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueOrDefault(string key)
        {
           return AppSettings.GetValueOrDefault(key, SettingsDefault);
        }

        /// <summary>
        /// Method to add a value in settings which is mapped to key.
        /// </summary>
        /// <param name="key">Key identifier in settings</param>
        /// <param name="value">Related value</param>
        public static void AddOrUpdateValue(string key,string value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }

        /// <summary>
        /// Method to get the boolean value from settings using key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBooleanPreference(string key)
        {
            return AppSettings.GetValueOrDefault(key, true);
        }

        /// <summary>
        /// Method to add a boolean value in settings which is mapped to key.
        /// </summary>
        /// <param name="key">Key identifier in settings</param>
        /// <param name="value">Related value</param>
        public static void AddBooleanPreference(string key, bool value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }
    }
}