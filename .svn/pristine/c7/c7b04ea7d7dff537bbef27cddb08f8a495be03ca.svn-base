using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SmartFlow.Shared.Helpers
{
    /// <summary>
    /// Loghandler class
    /// This class contains all functions related to logging
    /// </summary>
    public class LogHandler
    {
        /// <summary>
        /// Log method for normal logging
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void AddLog(String tag, String message)
        {
            Console.WriteLine(tag + " : " + message);
        }

        /// <summary>
        /// Log method to handle exception message and if needed, report to 3rd party analytics
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        /// <param name="logExternally"></param>
        public static void AddExceptionLog(String tag, String message, Exception e, bool logExternally)
        {

            Console.WriteLine(tag + " : " + e.StackTrace);
        }

        /// <summary>
        /// Log method based on xamarin's Debug mode.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void AddLogToDebug(String tag, String message)
        {
            Debug.WriteLine(tag + " : " + message);
        }
    }
}
