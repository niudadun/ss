using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using SmartFlow.Shared;
using SmartFlow.Droid;
using Android.Graphics;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Label), typeof(CustomLabel))]
namespace SmartFlow.Droid
{
    /// <summary>
    /// Android Specific Class
    /// This class is implemented, as default Button in Xamarin has text as AllCaps.
    /// To have proper formatting, we are overriding button in this class and handling the caps attribute.
    /// </summary>
    public class CustomLabel : LabelRenderer
    {

        List<string> supportedFonts = new List<string>() { "Roboto-Regular", "Roboto-Light" };
        //protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        //{
        //    base.OnElementChanged(e);

        //    if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
        //    {
        //        var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

        //        Control.Typeface = font;
        //    }
        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily) && supportedFonts.Contains(e.NewElement.FontFamily))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".ttf");

                Control.Typeface = font;
            }
        }
    }
}
