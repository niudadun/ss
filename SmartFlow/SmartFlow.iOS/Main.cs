using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace SmartFlow.iOS
{
    /// <summary>
    /// Main entry point for iOS application
    /// </summary>
    public class Application
    {
        /// <summary>
        /// First method to be called when app is launched.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
