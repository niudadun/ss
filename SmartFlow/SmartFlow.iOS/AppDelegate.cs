﻿using System;
using System.Collections.Generic;
using System.Linq;
using SmartFlow.Shared;
using Foundation;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;
using XLabs.Forms.Controls;
using Xamarin.Forms;
using CarouselView.FormsPlugin.iOS;

namespace SmartFlow.iOS
{
    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// This method is invoked when the application has loaded and is ready to run. In this 
        /// method you should instantiate the window, load the UI into it and then make the window
        /// visible.
        ///
        /// You have 17 seconds to return from this method, or iOS will terminate your application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SQLitePCL.Batteries.Init();
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            CarouselViewRenderer.Init();
            DependencyService.Register<ImageButtonRenderer>();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
