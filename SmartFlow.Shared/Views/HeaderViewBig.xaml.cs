using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    ///  Header View class
    /// This view contains Big header view used in Main page and Menu Page
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderViewBig : ContentView
    {
        private static string TAG = "HeaderViewBig";

        /// <summary>
        /// HeaderViewBig constructor
        /// </summary>
		public HeaderViewBig ()
		{
			InitializeComponent ();
            SetPageText();
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
       public void SetPageText()
        {
            // Change Request : AS_BC_CLOUDPASS-216
            // Got another change request mail on 18 Jan 2018, to have the same header as before
            
            label5.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_C);
            label6.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_D);
            label7.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_E);
            label8.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_F);            
        }
        
        /// <summary>
        /// Hide the data on the header, as we are using full image to set the info.
        /// Image provided by SGP
        /// </summary>
        public void HideHeaderData()
        {
            IcaLogoName.IsVisible = false;
            label5.IsVisible = false;
            label6.IsVisible = false;
            label7.IsVisible = false;
            label8.IsVisible = false;
        }

        /// <summary>
        /// Make the header background invisible, as we are using same header for two screens.
        /// </summary>
        /// <param name="IsVisible"></param>
        public void HeaderBgImageVisible(bool IsVisible)
        {
            HeaderBackgroundImage.IsVisible = IsVisible;
        }
    }
}