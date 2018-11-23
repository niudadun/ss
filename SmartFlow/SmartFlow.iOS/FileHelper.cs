using System;
using System.IO;
using Xamarin.Forms;
using SmartFlow.Shared.Helpers.Interfaces;
using SmartFlow.Shared.Helpers;
using SmartFlow.iOS;

[assembly: Dependency(typeof(FileHelper))]
namespace SmartFlow.iOS
{
    /// <summary>
    /// iOS Specific Class
    /// This class is used as a wrapper for File Handling.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        private static string TAG = "FileHelper_iOS";

        /// <summary>
        /// Gets the Local path of the application based on filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFilePath(string filename)
        {
            try
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

                if (!Directory.Exists(libFolder))
                {
                    Directory.CreateDirectory(libFolder);
                }

                return Path.Combine(libFolder, filename);
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
            }
            return "";
        }
    }
}
