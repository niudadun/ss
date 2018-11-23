using SmartFlow.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListView))]
namespace SmartFlow.iOS
{
    /// <summary>
    /// iOS Specific Class
    /// This class is used as a wrapper for ListView for iOS.
    /// </summary>
    class CustomListView : ListViewRenderer
    {
        private static string TAG = "CustomListView_iOS";

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.ShowsVerticalScrollIndicator = false;
        }
    }
}
