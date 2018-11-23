using SmartFlow.Shared.Helpers.Interfaces;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Respository;
using SmartFlow.Shared.Respository.Interfaces;
using SmartFlow.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartFlow.Shared.Converters;
using SmartFlow.Shared.Helpers;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    /// Profile Info View
    /// When user clicks on any profile item in profile page, this view is generated
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileInfoDialogPage : ContentPage
	{
        private static string TAG = "ProfileInfoDialogPage";

        private int profileIndex;
        int selectedId = -1;
        int selectedDeclarationID = -1;

        //  Navigation Page index : This will tell that on which page the number pad is at and based on that we calculate the views to be shown.
        int NumberPageIndex = 1;

        /// View model for selected profile
        public Profile CurrentProfile;
        public Declaration CurrentDeclaration;
        public List<Profile> ProfileList;
        public List<Declaration> DeclarationsList;

        public BaseViewModel ViewModel;
       

        Enums.EnumMaps Mode;

        /// <summary>
        /// Profile Info page constructor with Profile id and State
        /// </summary>
        public ProfileInfoDialogPage(InfoHolder infoHolder)
        {
            InitializeComponent();
            this.Padding = App.PagePadding;
            Mode = infoHolder.Mode;
            ViewModel = new BaseViewModel();
            if (Mode == Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW)
            {
                selectedId = infoHolder.ProfileId;
                selectedDeclarationID = infoHolder.DeclarationIds[0];
                DeclarationsList = ViewModel.GetDeclarationsBasicInfo().Result;
                BindingContext = ViewModel;
                GetData(true);
            }
            else
            {
                selectedId = infoHolder.ProfileId;
                ProfileList = ViewModel.GetProfilesBasicInfo().Result;
                
                BindingContext = ViewModel;
                GetData(true);
                FooterViewId.SetButtonHandling("").Clicked += OnViewClicked;
            }
            SetPageText();
        }

        /// <summary>
        /// Method to set the text for the view layout
        /// </summary>
        void SetPageText()
        {
            if (Mode == Enums.EnumMaps.PROFILE_LIST)
            {
                BtnGrid.IsVisible = true;
                BtnGridDivider.IsVisible = true;
                BtnGrid1.IsVisible = true;
                BtnGrid2.IsVisible = false;
                BtnUpdate.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.UPDATE_TEXT);
                BtnDelete.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.DELETE_TEXT);
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_LIST_BTN_VIEW));
            }
            else if (Mode == Enums.EnumMaps.MANAGE_DECLARATION)
            {
                BtnGridDivider.IsVisible = true;
                BtnGrid.IsVisible = false;
                BtnGrid1.IsVisible = false;
                BtnGrid2.IsVisible = false;
                BtnUpdate.Text = "";
                BtnDelete.Text = "";
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.EDIT_TEXT));

                InfoGridView.RowDefinitions[2].Height = 0;
            }
            else if (Mode == Enums.EnumMaps.CREATE_DECLARATION)
            {
                // Change request 16 Jan 2018 : Need to remove "Add" profile option from create and manage declarations.
                // Not having any create profile for Create or Manage Declarations scenario

                BtnGridDivider.IsVisible = true;
                BtnGrid.IsVisible = false;
                BtnGrid1.IsVisible = false;
                BtnGrid2.IsVisible = false;
                BtnUpdate.Text = "";
                BtnDelete.Text = "";

                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_LIST_BTN_VIEW));
                InfoGridView.RowDefinitions[2].Height = 0;

                //BtnGrid.IsVisible = true;
                //BtnGridDivider.IsVisible = true;
                //BtnGrid1.IsVisible = false;
                //BtnGrid2.IsVisible = true;
                //FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.PROFILE_LIST_BTN_VIEW));
                //BtnAddPassenger.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.ADD_PASSENGER);
            }
            else if (Mode == Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW)
            {
                BtnGrid.IsVisible = true;
                BtnGridDivider.IsVisible = true;
                BtnGrid1.IsVisible = true;
                BtnGrid2.IsVisible = false;
                BtnUpdate.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.UPDATE_TEXT);
                BtnDelete.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.DELETE_TEXT);
                FooterViewId.SetButtonVisible(false);
            }
        }

        /// <summary>
        /// Update Profile Button Clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdateClicked(object sender, EventArgs e)
        {
            if (Mode == Enums.EnumMaps.MANAGE_DECLARATION)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(ManageDeclarationPage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.MANAGE_DECLARATION), false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.MANAGE_DECLARATION), false);
                    }
                }
            }
            else if (Mode == Enums.EnumMaps.PROFILE_LIST)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(ManageProfilePage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.UPDATE_PROFILE_SELECTION_VIEW), false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.UPDATE_PROFILE_SELECTION_VIEW), false);
                    }
                }
            }
            else if (Mode == Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(CreateDeclarationPage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(),
                                new InfoHolder(selectedId, Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN, new List<int> {selectedDeclarationID }) , false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(),
                                new InfoHolder(selectedId, Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN, new List<int> { selectedDeclarationID }), false);
                    }
                }
            }
        }

        /// <summary>
        /// Delete profile Button Clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void BtnDeleteClicked(object sender, EventArgs e)
        {
            if (Mode == Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW)
            {
                var answer = await DisplayAlert(Shared.Resources.AppResources.delete_declaration, Shared.Resources.AppResources.declaration_permanent_delete,
                      Shared.Resources.AppResources.yes_text, Shared.Resources.AppResources.no_text);
                if (answer)
                {
                    FooterViewId.IsVisible = false;
                    InfoGridView.IsEnabled = false;
                    await ViewModel.DeleteDeclaration(selectedDeclarationID);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.SuccessPage.ToString(), new InfoHolder(-1, Enums.EnumMaps.DELETE_INFO_DECLARATION), false);
                        FooterViewId.IsVisible = true;
                        InfoGridView.IsEnabled = true;
                    });
                }

            }
            else
            {
                var answer = await DisplayAlert(Shared.Resources.AppResources.delete_profile, Shared.Resources.AppResources.profile_permanent_delete,
                    Shared.Resources.AppResources.yes_text, Shared.Resources.AppResources.no_text);
                if (answer)
                {
                    FooterViewId.IsVisible = false;
                    InfoGridView.IsEnabled = false;
                    await ViewModel.DeleteProfile(selectedId);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.SuccessPage.ToString(), new InfoHolder(-1, Enums.EnumMaps.DELETE_PROFILE), false);
                        FooterViewId.IsVisible = true;
                        InfoGridView.IsEnabled = true;
                    });
                }
            }
        }

        /// <summary>
        /// View Profile button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnViewClicked(object sender, EventArgs e)
        {
            if (Mode == Enums.EnumMaps.PROFILE_LIST)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(ManageProfilePage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.VIEW_PROFILE), false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageProfilePage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.VIEW_PROFILE), false);
                    }
                }
            }
            else if (Mode == Enums.EnumMaps.CREATE_DECLARATION)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(CreateDeclarationPage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN), false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN), false);
                    }
                }
            }
            else if (Mode == Enums.EnumMaps.MANAGE_DECLARATION)
            {
                if (Navigation.ModalStack.Count == 0 || Navigation.ModalStack.Last().GetType() != typeof(ManageDeclarationPage))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.GoBack(false);
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.MANAGE_DECLARATION), false);
                        });
                    }
                    else
                    {
                        App.NavigationService.GoBack(false);
                        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ManageDeclarationPage.ToString(), new InfoHolder(selectedId, Enums.EnumMaps.MANAGE_DECLARATION), false);
                    }
                }
            }

        }
        
        /// <summary>
        /// Method to get the profile data from the selected profile
        /// </summary>
        private void GetData(bool NeedToUpdateNavbar)
        {
            if (selectedId != -1)
            {
                if (Mode == Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW)
                {
                    CurrentDeclaration = DeclarationsList.Find(i => i.Id == (selectedDeclarationID));
                    if (CurrentDeclaration != null)
                    {
                        ProfileImage.IsVisible = false;
                        ProfileName.Text = CurrentDeclaration.DeclarationType.ToString() + " : " + DateTime.Parse(CurrentDeclaration.LastUpdatedDate).ToString(Utils.DATE_FORMAT);
                        ProfileId.IsVisible = false;
                    }
                }
                else
                {
                    CurrentProfile = ProfileList.Find(i => i.Id == selectedId);

                    // As number pad moves from 1 onwards, hence we have to add 1 to index, since index starts from 0.
                    profileIndex = ProfileList.IndexOf(CurrentProfile) + 1;

                    if (CurrentProfile != null)
                    {
                        if (CurrentProfile.Image != null)
                        {
                            var stream = new MemoryStream(CurrentProfile.Image);
                            ProfileImage.Source = ImageSource.FromStream(() => stream);
                        }
                        else
                        {
                            ProfileImage.Source = ImageSource.FromFile(Utils.DEFAULT_PROFILE_PIC);
                        }
                        ProfileName.Text = CurrentProfile.Name;
                        ProfileId.Text = "Profile " + profileIndex;
                    }
                    
                    // Since highlight a index can only be done on same page, we dont need to caculate the pageindex again.
                    // Page index only needs to be calculated, if user is coming first time on this screen.
                    if (NeedToUpdateNavbar)
                    {
                        // Calculate NumberPageIndex based on the ProfileIndex

                        double value = (ProfileList.Count - Utils.MaxProfilesOnFirstPage) / (double)Utils.MaxProfilesOnOtherPages;
                        int MaxPages = (int)Math.Ceiling(value) + 1;

                        for (int PageIndex = 1; PageIndex <= MaxPages; PageIndex++)
                        {
                            int MaxProfileIndex = Utils.MaxProfilesOnFirstPage + ((PageIndex - 1) * Utils.MaxProfilesOnOtherPages);
                            int MinProfileIndex = PageIndex == 1 ? 1 : (Utils.MaxProfilesOnFirstPage + ((PageIndex - 1) * Utils.MaxProfilesOnOtherPages - 5));
                            if (profileIndex <= MaxProfileIndex && profileIndex >= MinProfileIndex)
                            {
                                NumberPageIndex = PageIndex;
                                // Since for Manage/Create declaration we dont have "+", and we are not taking that consideraiton here into picture,
                                // Hence we have to check that manually. So in case Profileindex matches the total count and start of the page is from that profileindex,
                                // means in the last page, last bar must have ">" considered here, but actually we can put this number there.
                                // So we will decrease the numpageIndex, so that navigation bar considers that automatically.
                                if ((profileIndex == ProfileList.Count) 
                                    && (profileIndex == MinProfileIndex)
                                    && (Mode == Enums.EnumMaps.MANAGE_DECLARATION || Mode == Enums.EnumMaps.CREATE_DECLARATION))
                                {
                                    NumberPageIndex--;
                                }
                                break;
                            }
                        }
                    }

                    CreateNavBar();
                }
            }            
        }

        /// <summary>
        /// When user click back arrow on number pad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBackPage(object sender, EventArgs e)
        {
            NumberPageIndex--;
            CreateNavBar();
        }

        /// <summary>
        /// When user clicks forward arrow on number pad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavNextPage(object sender, EventArgs e)
        {
            NumberPageIndex++;
            CreateNavBar();
        }


        /// <summary>
        /// Create navigation number pad.
        /// This handles all the views across the pages and take into account the maximum number of profiles supported.
        /// </summary>
        public void CreateNavBar()
        {
            if (ProfileList != null)
            {
                try
                {
                    // Reset the navigation bar to empty text and no delegation methods attached.
                    var profileList = ProfileList.FindAll(i => i != null);

                    FooterViewId.CreateNavBar(true,profileIndex,false, ProfileList.Count, NumberPageIndex, NavNextPage, NavBackPage, OnSelectionForNavBar, OnCreateClicked, Mode);
                }
                catch (Exception e)
                {
                    LogHandler.AddExceptionLog(TAG, "", e, true);
                }
            }
            else
            {
                FooterViewId.SetNavigationBarVisible(false);
            }
        }

        /// <summary>
        /// Overridden method called when page comes to foreground
        /// </summary>
        protected override void OnAppearing()
        {
            //GetProfileData();
        }
        
        private void OnSelectionForNavBar(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int id = Int16.Parse(btn.Text);
            
            selectedId = ProfileList[id - 1].Id;

            // Since when user will select some index, he will be on same page. We dont need to recalculate page index.
            // Just need to set the highlight position 
            GetData(false);
        }

        /// <summary>
        /// Create new profile, which navigate to a profile creation page.
        /// When Create action button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreateClicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.ProfileInfoDialogPage.ToString())
                    {
                        await App.NavigationService.GoBack(false);
                    }
                    await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ProfileCreateOptionsPage.ToString(), false);
                });
            }
            else
            {
                if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.ProfileInfoDialogPage.ToString())
                {
                    App.NavigationService.GoBack(false);
                }
                App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ProfileCreateOptionsPage.ToString(), false);
            }
        }

        /// <summary>
        /// Add Passenger Button Clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddPassengerClicked(object sender, EventArgs e)
        {
            OnCreateClicked(sender, e);
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