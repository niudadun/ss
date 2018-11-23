using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using SmartFlow.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButton))]
namespace SmartFlow.iOS
{
    /// <summary>
    /// iOS Specific Class
    /// This class is used as a wrapper for ListView for iOS.
    /// </summary>
    class CustomButton : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            Control.TitleEdgeInsets = new UIEdgeInsets(4, 4, 4, 4);
            Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            Control.TitleLabel.TextAlignment = UITextAlignment.Center;
        }
    }
}