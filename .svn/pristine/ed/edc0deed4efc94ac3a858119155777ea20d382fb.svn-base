using ImageCircle.Forms.Plugin.Abstractions;
using SmartFlow.Shared.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFlow.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    /// Profile List Item View 
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileListItemView : ViewCell
	{
        private static string TAG = "ProfileListItemView";

        /// <summary>
        /// Profile List item view constructor
        /// </summary>
        public ProfileListItemView()
		{
			InitializeComponent ();

            SetPageText();
        }

        /// <summary>
        /// Method to set the text/properties for the view layout
        /// </summary>
        void SetPageText()
        {
            ProfileImage.SetBinding(CircleImage.SourceProperty, "Image", converter: new ImageConverter());
            ProfileCount.SetBinding(Label.TextProperty, new Binding("Index", stringFormat: "Profile {0}"));
            ProfileName.SetBinding(Label.TextProperty, "Name");
            //ProfileCount.IsVisible = false;
        }
    }
}