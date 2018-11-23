using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Models;
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
    /// QRPage Page View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRPage : ContentPage
    {
        private static string TAG = "QRPage";

        InfoHolder InfoHolderObject;

        //  Navigation Page index : This will tell that on which page the number pad is at and based on that we calculate the views to be shown.
        int NumberPageIndex = 1;

        int TripSelectedId = 1;
        /// <summary>
        /// State from which this page is called.
        /// </summary>
        Enums.EnumMaps Mode;
        List<Declaration> SelectedDeclarations;
        DeclarationViewModel ViewModel;

        /// <summary>
        /// Success Page constructor
        /// </summary>
        /// 
        public QRPage()
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            SetPageText();
            
            FooterViewId.SetBackButtonVisible(false);
            FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.NEXT_TEXT)).Clicked += OnDoneClicked;
        }

        /// <summary>
        /// Success Page constructor with state info
        /// </summary>
        /// 
        public QRPage(InfoHolder infoHolder)
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            SetPageText();

            FooterViewId.SetBackButtonVisible(false);
            FooterViewId.SetButtonHandling("").Clicked += OnDoneClicked;

            Mode = infoHolder.Mode;
            InfoHolderObject = infoHolder;

            FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.NEXT_TEXT));

            GetDeclarationsData();
        }

        void UpdateData()
        {
            if (Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                QrImage.Source = (FileImageSource)ImageSource.FromFile("QR");
                dNoText.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.DE_NO) + " : " + SelectedDeclarations[TripSelectedId-1].DeNo;
                NameText.Text = Helpers.L10n.GetMappedString(Enums.TextMapping.NAME) + " : " + SelectedDeclarations[TripSelectedId - 1].Profile.Name;
                TripDateText.Text = "Trip" + " : " + SelectedDeclarations[TripSelectedId - 1].LastUpdatedDate;
                
                CreateNavBar();
            }
        }

        void GetDeclarationsData()
        {
            ViewModel = new DeclarationViewModel()
            {
                SelectedProfileId = InfoHolderObject.ProfileId
            };

            ViewModel.GetDeclartions();

            foreach (int id in InfoHolderObject.DeclarationIds)
            {
                SelectedDeclarations.Add(ViewModel.Declarations.Find(i=>i.Id == id));
            }

            UpdateData();
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
            if (SelectedDeclarations != null)
            {
                try
                {
                    LogHandler.AddLog(TAG, "SELECTED ID : " + TripSelectedId);
                    FooterViewId.CreateNavBar(true, TripSelectedId, true, SelectedDeclarations.Count, NumberPageIndex, NavNextPage, NavBackPage, OnSelectionForNavBar, null, Mode);
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

        private void OnSelectionForNavBar(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            TripSelectedId = Int16.Parse(btn.Text);
            UpdateData();
        }

        /// <summary>
        /// Method to set the text/properties for the view layout
        /// </summary>
        void SetPageText()
        {
           
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
            if (Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.LanguageSelectionDialogPage.ToString(), new InfoHolder(-1,Enums.EnumMaps.SUBMIT_ANOTHER_DECLARATION), false);
                });
            }
            else if (Mode == Enums.EnumMaps.MANAGE_DECLARATION)
            {
                App.NavigationService.GoBack(false);
            }
            return true;
        }
    }
}