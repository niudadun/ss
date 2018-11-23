using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartFlow.Shared.Helpers
{
    /// <summary>
    /// Utils class - Contains all constant values
    /// </summary>
    public static class Utils
    {
        private static string TAG = "Utils";

        /// Common Date Format
        public static string CLIENT_DATE_FORMAT = "dd MM yyyy HH:mm";
        // This is used to display the date on Profile/Declaration
        public static string DATE_FORMAT = "dd/MM/yyyy";
        // This is used when submitting the answers to server or QR data
        public static string DATE_FORMAT_DECLARATION = "yyyy/MM/dd";

        public static string AUTOMATION_ID = "AutomationId_";

        public static string DEFAULT_PROFILE_PIC = "ic_profile_pic";
        public static int MAXIMUM_TRIPS_ALLOWED_ONE_TIME = 3;
        public static int MAXIMUM_PROFILES_ALLOWED = 15;

        // These values will remain constant. Please dont change it.
        public static int MaxProfilesOnFirstPage = 7;
        public static int MaxProfilesOnOtherPages = 6;

        /*
         * Keys for preferences
         */
        /// Language Selection Key
        public static string PREFS_KEY_LANGUAGE_SELECTION = "SelectedLanguage";
		// App first launch preference key
        public static string PREFS_KEY_IS_APP_FIRST_LAUNCH = "IsAppFirstLaunch";


        public static string RandomString(int length)
        {
           return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);
        }

        public static DateTime GetDateInFormat(DateTime DateValue, string format = null)
        {
            try
            {
                return DateTime.Parse(DateValue.ToString((format == null || format.Length <= 0) ? Utils.DATE_FORMAT : format));
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
                return DateValue;
            }
        }


        public static Chapter GetChapter(this List<Declaration> Declarations, string chapterClassId)
        {
            foreach (var declaration in Declarations)
            {
                var chapter = declaration.Chapters.Find(i => i.ChapterClassId == chapterClassId);
                if (chapter != null) return chapter;
            }
            return null;
        }

        public static string GetNewTripChapterClassId(this List<Declaration> Declarations)
        {
            return ChapterIdentifiers.Trip.ToString() + " " + Declarations.Count.ToString();
        }

        public static int GetTripIndex(this string TripClassId)
        {
            return Convert.ToInt32(TripClassId.Split(' ')[1]);
        }


        public static string GetTripId(int Id)
        {
            return ChapterIdentifiers.Trip.ToString() + " " + Id.ToString();
        }

        public static List<int> GetDeclarationIds(this List<Declaration> Declarations)
        {
            var Ids = new List<int>();
            Declarations.ForEach(i =>
            {
                Ids.Add(i.Id);
            });
            return Ids;
        }

    }
}
