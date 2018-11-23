using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using SmartFlow.Shared;
using SmartFlow.Droid;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButton))]
namespace SmartFlow.Droid
{
    /// <summary>
    /// Android Specific Class
    /// This class is implemented, as default Button in Xamarin has text as AllCaps.
    /// To have proper formatting, we are overriding button in this class and handling the caps attribute.
    /// </summary>
    public class CustomButton : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetAllCaps(false);
            }
        }
    }
}
