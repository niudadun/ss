using System;
using System.IO;
using Xamarin.Forms;
using SmartFlow.Droid;
using SmartFlow.Shared.Helpers.Interfaces;

[assembly: Dependency(typeof(FileHelper))]
namespace SmartFlow.Droid
{
    /// <summary>
    /// Android Specific Class
    /// This class is used as a wrapper for File Handling.
    /// </summary>
    public class FileHelper : IFileHelper
	{
        private static string TAG = "FileHelper_Android";

        /// <summary>
        /// Gets the Local path of the application based on filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (path != null && path.Length > 0)
            {
                return Path.Combine(path, filename);
            }
            return "";
		}
	}
}
