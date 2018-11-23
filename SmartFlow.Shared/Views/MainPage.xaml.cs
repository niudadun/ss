﻿//using SmartFlow.Shared.Resources;
using SmartFlow.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    /// MainPage Layout 
    /// View to display options : Select Language, Select Services
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private static string TAG = "MainPage";

        

        /// <summary>
        /// MainPage constructor
        /// </summary>
        public MainPage()
        {            
            InitializeComponent();
            this.Padding = App.PagePadding;

        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            Languages.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.MAIN_PAGE_BTN_SELECT_LANGUAGE);
            Services.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.MAIN_PAGE_BTN_SELECT_SERVICES);
            HeaderViewBig headerViewID = (HeaderViewBig)HeaderViewID;
            headerViewID.SetPageText();
            ElectronicCardLabel.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.ELECTRONIC_CARD);

            // Got Full image with text for the header on main page.
            // Hence we will hide the content and use that image only.
            headerViewID.HideHeaderData();
        }

        /// <summary>
        /// Overridden method called when page comes to foreground
        /// </summary>
        protected override async void OnAppearing()
        {
            SetPageText();
        }


        /// <summary>
        /// Select Language Button Clicked Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectLanguageClicked(object sender, EventArgs e)
        {
            if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.LanguagePage.ToString())
                return;
            App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.LanguagePage.ToString(), false);
        }

        /// <summary>
        /// Select Language Button Pressed Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLanguagePressed(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_select_language_white");
        }

        /// <summary>
        /// Select Language Button Released Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLanguageReleased(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_select_language_blue");
        }

        /// <summary>
        /// Select Language Button Focused Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLanguageFocused(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_select_language_blue");
        }

        /// <summary>
        /// Select Services Button Clicked Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterServiceClicked(object sender, EventArgs e)
        {
            if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.LanguagePage.ToString())
                return;
            // If this is first launch of the app, then we need to launch Tutorial Screen first.
            if (Settings.GetBooleanPreference(Utils.PREFS_KEY_IS_APP_FIRST_LAUNCH))
            {
                if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.MainMenuPageTutorial.ToString())
                    return;
                App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.MainMenuPageTutorial.ToString(), false);
            }
            else
            {
                if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.MainMenuPage.ToString())
                    return;
                App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.MainMenuPage.ToString(), false);
            }
        }

        /// <summary>
        /// Select Services Button Pressed Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnServicesPressed(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_enter_services_white");
        }

        /// <summary>
        /// Select Services Button Released Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnServicesReleased(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_enter_services_blue");
        }

        /// <summary>
        /// Select Services Button Focused Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnServicesFocused(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Image = (FileImageSource)ImageSource.FromFile("ic_enter_services_blue");
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
        /// Dont override here, as on this page we want to just kill the app or minimise the app, as per android default behaviour
        //protected override bool OnBackButtonPressed()
        //{
        //    App.NavigationService.GoBack(false);
        //    return true;
        //}
    }
}