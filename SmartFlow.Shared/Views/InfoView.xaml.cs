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
    /// Info View to display a label and text view.
    /// Example : Success screen view
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoView : ContentView
	{
        private static string TAG = "InfoView";

        /// <summary>
        /// InfoView constructor
        /// </summary>
        public InfoView()
		{
			InitializeComponent ();

            SetPageText();
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            //footerText.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.FOOTER_TEXT);
        }

        /// <summary>
        /// Method to set the text and image for the layout
        /// </summary>
        public void SetLabelInfo(String textButton, String textLabel)
        {
            InfoButtonName.Image = (FileImageSource)ImageSource.FromFile(textButton);
            LabelText.Text = textLabel;
        }
    }
}