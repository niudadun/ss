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
    /// Header View class
    /// This view contains small header view used in mutliple pages
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderView : ContentView
	{
        private static string TAG = "HeaderView";

        /// <summary>
        /// Headerview constructor
        /// </summary>
		public HeaderView ()
		{
			InitializeComponent ();
            SetPageText();
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            // Change Request : AS_BC_CLOUDPASS-216
            // Got another change request mail on 18 Jan 2018, to have the same header as before
            header_line1.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_A);
            header_line2.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.HEADER_TEXT_B);
            //header_line2.IsVisible = false;
        }
    }
}