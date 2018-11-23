using SmartFlow.Shared.Helpers;
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
    /// Success Page View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveSuccess : ContentPage
    {
        private static string TAG = "SaveSuccess";

        InfoHolder InfoHolderObject;
        /// <summary>
        /// State from which this page is called.
        /// </summary>
        Enums.EnumMaps Mode;

        /// <summary>
        /// Success Page constructor
        /// </summary>
        /// 
        public SaveSuccess()
        {
            InitializeComponent();
            this.Padding = App.PagePadding;
            
            FooterViewId.SetBackButtonVisible(false);
            FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.DONE_TEXT)).Clicked += OnDoneClicked;

            InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_CREATED_SUCCESSFULLY));
            SetPageText();
        }

        /// <summary>
        /// Success Page constructor with state info
        /// </summary>
        /// 
        public SaveSuccess(InfoHolder infoHolder)
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            InfoHolderObject = infoHolder;
            Mode = infoHolder.Mode;

            FooterViewId.SetBackButtonVisible(false);
            FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.DONE_TEXT)).Clicked += OnDoneClicked;
            SetPageText();
        }

        /// <summary>
        /// Method to set the text/properties for the view layout
        /// </summary>
        void SetPageText()
        {
            if (Mode == Enums.EnumMaps.CREATE_PROFILE)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_CREATED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.UPDATE_PROFILE)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_UPDATED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.DECLARATION_SAVED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.DELETE_PROFILE)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_DELETED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.DECLARATION_SUBMITTED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.DECLARATION_UPDATED_SUCCESSFULLY));
            }
            else if (Mode == Enums.EnumMaps.TOC_SCREEN)
            {
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.NEXT_TEXT));
                LabelTextSucess.IsVisible = true;
                LabelTextScroll.IsVisible = true;
                InfoPageId.IsVisible = false;
                LabelTextSucess.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.TOC_TEXT);
            }
            else if (Mode == Enums.EnumMaps.DELETE_INFO_DECLARATION)
            {
                InfoPageId.SetLabelInfo("ic_success_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.DECLARATION_DELETED_SUCCESSFULLY));
            }
        }

        /// <summary>
        /// Done Action button clicked.
        /// based on the state from which this page is called, app will decide how many pages are required to be popped out,
        /// so that profile list can be visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoneClicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
                IOSBackPress();
            else
                OnBackButtonPressed();
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
            if (Mode == Enums.EnumMaps.CREATE_PROFILE)
            {
                ProfilesList.IsUpdateProfileList = true;
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }
            else if (Mode == Enums.EnumMaps.UPDATE_PROFILE)
            {
                ProfilesList.IsUpdateProfileList = true;
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }
            else if(Mode == Enums.EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE)
            {
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }
            else if(Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }
            else if (Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                ManageDeclarationPage.IsUpdateList = true;
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
                App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.QRPage.ToString(), InfoHolderObject, false);                
            }
            else if (Mode == Enums.EnumMaps.DELETE_PROFILE)
            {
                ProfilesList.IsUpdateProfileList = true;
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }
            else if (Mode == Enums.EnumMaps.TOC_SCREEN)
            {
                // As we are launching TOC screen on press of NO Submit Another, and hence we have to go back to "Main menu page"         
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);

                App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.LanguageSelectionDialogPage.ToString(), new InfoHolder(-1,Enums.EnumMaps.CONFIRMATION_SCREEN), false);
            }
            else if (Mode == Enums.EnumMaps.DELETE_INFO_DECLARATION)
            {
                ManageDeclarationPage.IsUpdateList = true;
                App.NavigationService.GoBack(false);
                App.NavigationService.GoBack(false);
            }

            return true;
        }


        private void IOSBackPress()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Mode == Enums.EnumMaps.CREATE_PROFILE)
                {
                    //await App.NavigationService.Remove(2);
                     App.NavigationService.GoBack(false);
                     App.NavigationService.GoBack(false);
                }
                else if (Mode == Enums.EnumMaps.UPDATE_PROFILE)
                {
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                }
                else if (Mode == Enums.EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE)
                {
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                }
                else if (Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
                {
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                }
                else if (Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
                {
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.QRPage.ToString(), InfoHolderObject, false);
                }
                else if (Mode == Enums.EnumMaps.DELETE_PROFILE)
                {
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                }
                else if (Mode == Enums.EnumMaps.TOC_SCREEN)
                {
                    // As we are launching TOC screen on press of NO Submit Another, and hence we have to go back to "Main menu page"         
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);
                    await App.NavigationService.GoBack(false);

                    await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.LanguageSelectionDialogPage.ToString(), new InfoHolder(-1, Enums.EnumMaps.CONFIRMATION_SCREEN), false);
                }
                else if (Mode == Enums.EnumMaps.DELETE_INFO_DECLARATION)
                {
                    App.NavigationService.GoBack(false);
                    App.NavigationService.GoBack(false);
                }
            });
        }
    }
}