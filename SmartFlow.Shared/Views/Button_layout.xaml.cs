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
    /// Button layout view
    /// Circle Button View used in language page.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Button_layout : ContentView
	{
        private static string TAG = "Button_layout";

        bool isFirstPage = true;
        bool isLanguage = true;

        /// <summary>
        /// View constructor
        /// </summary>
        public Button_layout()
		{
			InitializeComponent ();

            SetPageText();
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            SetButtonImage(true);
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            SetButtonImage(false);
        }

        private void OnButtonFocused(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            SetButtonImage(btn.IsFocused);
        }

        /// <summary>
        /// Method for changing color/icon of the button when pressed
        /// </summary>
        /// <param name="isPressed"></param>
        void SetButtonImage(bool isPressed)
        {
            if (isPressed)
            {
                language_button.BackgroundColor = Color.FromHex(Shared.Resources.AppResources.blue_color);
                language_button.TextColor = Color.White;
                if (!isLanguage)
                {
                    language_button.Text = "";
                    language_button.Image = (FileImageSource)ImageSource.FromFile(isFirstPage ? "ic_back_arrow_white_right" : "ic_back_arrow_white");
                }
                else
                {
                    language_button.Image = null;
                }
            }
            else
            {
                language_button.BackgroundColor = Color.FromHex(Shared.Resources.AppResources.yellow_color);
                language_button.TextColor = Color.Black;
                if (!isLanguage)
                {
                    language_button.Text = "";
                    language_button.Image = (FileImageSource)ImageSource.FromFile(isFirstPage?"ic_back_arrow_blue_right": "ic_back_arrow_blue");
                }
                else
                {
                    language_button.Image = null;
                }                
            }
        }

        /// <summary>
        /// Method to set the config for the button instance.
        /// Based on this, app decide if button contains text or page arrow.
        /// </summary>
        /// <param name="isFirstpageHere"></param>
        /// <param name="isLanguageHere"></param>
        public void SetIsFirstPage(bool isFirstpageHere, bool isLanguageHere)
        {
            this.isFirstPage = isFirstpageHere;
            this.isLanguage = isLanguageHere;

            if (isLanguage)
            {
                language_button.Image = null;
            }
            else {
                language_button.Text = "";
                language_button.Image = (FileImageSource)ImageSource.FromFile(isFirstPage ? "ic_back_arrow_blue_right" : "ic_back_arrow_blue");
            }
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            //footerText.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.FOOTER_TEXT);
        }
    }
}