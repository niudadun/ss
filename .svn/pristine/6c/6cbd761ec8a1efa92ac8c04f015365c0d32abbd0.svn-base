using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Resources;
using SmartFlow.Shared.ViewModels;
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
    /// Profile Create Options View
    /// Options : Scan Passport, Manual Entry
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileCreateOptionsPage : ContentPage
    {
        private static string TAG = "ProfileCreateOptionsPage";
        
        Enums.EnumMaps Mode;
        int SelectedProfileID = -1;
        /// <summary>
        /// Profile Create page constructor.
        /// </summary>
        public ProfileCreateOptionsPage()
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            Mode = Enums.EnumMaps.CREATE_PROFILE;
            SelectedProfileID = -1;
            SetPageText();            
        }

        /// <summary>
        /// Profile Create page constructor with Profile id and Mode
        /// </summary>
        public ProfileCreateOptionsPage(InfoHolder infoHolder)
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            Mode = infoHolder.Mode;
            SelectedProfileID = infoHolder.ProfileId;
            
            SetPageText();
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            ScanButton.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.SCAN_TEXT);
            ManualButton.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.MANUAL_TEXT);
        }

        /// <summary>
        /// Scan Passport Button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScanClicked(object sender, EventArgs e)
        {
            //App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ProfilesList.ToString(), false);
        }

        /// <summary>
        /// Manual entry button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterManualClicked(object sender, EventArgs e)
        {
            if (Mode == Enums.EnumMaps.UPDATE_PROFILE_SELECTION_VIEW)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.NavigationService.GoBack(false);
                        await App.NavigationService.GoBack(false);
                        await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(SelectedProfileID, Enums.EnumMaps.UPDATE_PROFILE), false);
                    });
                }
                else
                {
                    App.NavigationService.GoBack(false);
                    App.NavigationService.GoBack(false);
                    App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(SelectedProfileID, Enums.EnumMaps.UPDATE_PROFILE), false);
                }
            }
            else if (Mode == Enums.EnumMaps.CREATE_PROFILE)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.NavigationService.GoBack(false);
                        await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(-1, Enums.EnumMaps.CREATE_PROFILE), false);
                    });
                }
                else
                {
                    App.NavigationService.GoBack(false);
                    App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(-1, Enums.EnumMaps.CREATE_PROFILE), false);
                }
            }
        }

        /// <summary>
        /// Scan Passport Button Pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScanPressed(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_id_white");
        }

        /// <summary>
        /// Scan Passport Button Released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScanReleased(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_id_blue");
        }

        /// <summary>
        /// Scan Passport Button Focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScanFocused(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_id_white");
        }

        /// <summary>
        /// Manual Entry Button pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnManualPressed(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_edit_white");
        }

        /// <summary>
        /// Manual Entry Button Released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnManualReleased(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_edit_blue");
        }

        /// <summary>
        /// Manual Entry Button Focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnManualFocused(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_edit_white");
        }


        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// Handled default back navigation of android to Custom Navigation.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            App.NavigationService.GoBack(false);
            return true;
        }

    }
}