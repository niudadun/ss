using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using SmartFlow.Droid;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(CustomListView))]
namespace SmartFlow.Droid
{
    /// <summary>
    /// Android Specific Class
    /// This class is used as a wrapper for ListView for Android.
    /// </summary>
    class CustomListView : ListViewRenderer
    {
        private static string TAG = "CustomListView_Android";

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.VerticalScrollBarEnabled = false;
        }
    }
}