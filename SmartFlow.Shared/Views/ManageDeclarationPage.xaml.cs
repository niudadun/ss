﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFlow.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Enums;
using XLabs.Forms.Controls;
using SmartFlow.Shared.Behaviours;
using SmartFlow.Shared.Models.Ede.Enums;
using SmartFlow.Shared.Converters;
using System.Globalization;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    /// Declaration Details Page
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageDeclarationPage : ContentPage
    {
        private static string TAG = "ManageDeclarationPage";
        Dictionary<string, ScrollView> layouts;
        string currentView;
        Enums.EnumMaps Mode;
        DeclarationViewModel ViewModel;
        int SelectedProfileId;

        /// <summary>
        /// Boolean to check if we need to reload the declaration list from database
        /// Since we are reloading the list in "OnAppearing", we should only reload when declaration is updated/deleted.
        /// And hence this variable is marked as false, when any view is clicked and only made true back on the "Success" Screen
        /// </summary>
        public static bool IsUpdateList = true;

        /// <summary>
        /// Manage declaration constructor for receiving data object from calling activity.
        /// </summary>
        /// <param name="infoHolder"></param>
        public ManageDeclarationPage(InfoHolder infoHolder)
        {
            try
            {
                InitializeComponent();
                this.Padding = App.PagePadding;

                Mode = infoHolder.Mode;
                ViewModel = new DeclarationViewModel()
                {
                    SelectedProfileId = infoHolder.ProfileId
                };
                SelectedProfileId = infoHolder.ProfileId;
                
                ViewModel.GetDeclartions();

                FooterViewId.GetBackButtonAndRemoveClick().Clicked += OnBackclicked;
                FooterViewId.SetButtonHandling("").Clicked += OnEditClicked;
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.EDIT_TEXT));
                FooterViewId.SetMiddleButtonHandling("QR").Clicked += OnQRButtonClicked;
                InfoPageId.SetLabelInfo("ic_exclamation_blue", Helpers.L10n.GetMappedString(Enums.TextMapping.NO_DECLARATIONS));
                BindingContext = ViewModel;
                SetDataLayout();
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
        }

        /// <summary>
        /// Overridden method called when page comes to foreground
        /// </summary>
        protected override void OnAppearing()
        {
            // We just need to check if current view id is available in the database, if yes, then we do nothing.
            // Else we remove the view from declarations.
            //if (IsUpdateList && currentView != null && currentView.Length > 0)
            //{
            //    //If something has been modified, then only update the declaration view.
            //    // Since only current view can be modified, we have to fetch the data again and refill the chapter information.
            //    ViewModel.GetDeclartions();
            //    if (ViewModel.Declarations != null && ViewModel.Declarations.Count > 0)
            //    {
            //        // This means, declaration has been deleted and we need to remove the view.
            //        if (!ViewModel.Declarations.Exists(i => i.Id == Convert.ToInt32(currentView)))
            //        {
            //            // Remove the classid view from the layout and select first view back.
            //        }
            //        // Update the current view with new data.
            //        else 
            //        {

            //        }

            //        var grid = this.FindByName<Grid>("MainGrid");
            //        foreach (var v in grid.Children)
            //        {
            //            grid.Children.Remove(v);
            //        }

            //        SetDataLayout();
            //    }
            //    else
            //    {
            //        SetDataLayout();
            //    }
            //}
        }

        void SetDataLayout()
        {
            try
            {
                if (ViewModel.Declarations != null && ViewModel.Declarations.Count > 0)
                {
                    MainGrid.IsVisible = true;
                    InfoPageId.IsVisible = false;

                    foreach (DeclarationType val in Enum.GetValues(typeof(DeclarationType)))
                    {
                        var index = 1;
                        foreach (var declaration in ViewModel.Declarations.Where(i => i.DeclarationType == val).OrderBy(c => DateTime.Parse(c.LastUpdatedDate)))
                        {
                            var chapter = new Chapter()
                            {
                                Text = declaration.DeclarationType.ToString() + " " + index + " " + DateTime.Parse(declaration.LastUpdatedDate).ToString(Utils.DATE_FORMAT),
                                Questions = new List<Question>(),
                                ChapterIdentifier = ChapterIdentifiers.Declare,
                                Id = declaration.Id,
                                ChapterDeclarationType = declaration.DeclarationType
                            };
                            chapter.Questions.AddRange(declaration.Profile.Chapters[0].Questions);
                            chapter.Questions.AddRange(declaration.Chapters[0].Questions);
                            ViewModel.Chapters.Add(chapter);
                            index++;
                        }
                    }

                    CreateMenuLayout(ViewModel.Chapters);

                    layouts = new Dictionary<string, ScrollView>();

                    ViewModel.Chapters.ForEach(i =>
                    {
                        layouts.Add(i.Id.ToString(), CreateDataLayout(i, true));
                    });

                    var dataLayout = layouts[ViewModel.Chapters[0].Id.ToString()];
                    var grid = this.FindByName<Grid>("MainGrid");
                    grid.Children.Add(dataLayout);
                    Grid.SetColumn(dataLayout, 1);
                    currentView = dataLayout.ClassId;
                }
                else
                {
                    MainGrid.IsVisible = false;
                    InfoPageId.IsVisible = true;
                }
                SetFooterButtons();
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
        }

        private void CreateMenuLayout(List<Chapter> Chapters)
        {
            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 1,
            };

            var headerButton = new ImageButton()
            {
                // TextColor = (Color)Application.Current.Resources["Text_Color"],
                WidthRequest = 80,
                HeightRequest = 80,
                BorderRadius = 0,
                Orientation = XLabs.Enums.ImageOrientation.ImageCentered,
                Source = ImageSource.FromFile(Utils.DEFAULT_PROFILE_PIC),
                BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Yellow"],
                ClassId = "HeaderButton",
                AutomationId = Utils.AUTOMATION_ID + "HeaderImage_Button",
            };
            stackLayout.Children.Add(headerButton);

            // Having index here to differentiate between the automation ids for Automated test cases.
            var DraftIndex = 1;
            var SubmittedIndex = 1;
            foreach (var chapter in Chapters)
            {
                var button = new Button()
                {
                    ClassId = chapter.Id.ToString(),
                    Text = chapter.Text,
                    TextColor = (Color)Application.Current.Resources["Text_Color"],
                    WidthRequest = 80,
                    HeightRequest = 80,
                    BorderRadius = 0,
                    BackgroundColor = (Color)Application.Current.Resources["TextView_Bg"],
                    // We need different automation ids for each chapter and also it should indicate the type of the chapter.
                    AutomationId = Utils.AUTOMATION_ID + chapter.ChapterDeclarationType.ToString() + "_" + (chapter.ChapterDeclarationType == DeclarationType.Draft ? DraftIndex : SubmittedIndex),
                };
                button.Clicked += GetView;
                if (chapter == Chapters.First())
                    button.BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Blue"];
                stackLayout.Children.Add(button);

                if (chapter.ChapterDeclarationType == DeclarationType.Draft)
                {
                    DraftIndex++;
                }
                else if (chapter.ChapterDeclarationType == DeclarationType.Submitted)
                {
                    SubmittedIndex++;
                }
            }

            for (var i = 0; i < 5; i++)
            {
                var button = new Button()
                {
                    TextColor = (Color)Application.Current.Resources["Text_Color"],
                    AutomationId = Utils.AUTOMATION_ID + "Empty_" + i,
                    WidthRequest = 80,
                    HeightRequest = 80,
                    BorderRadius = 0,
                    BackgroundColor = (Color)Application.Current.Resources["TextView_Bg"],
                };
                stackLayout.Children.Add(button);
            }

            var grid = this.FindByName<Grid>("MainGrid");

            var scrollView = new ScrollView()
            {
                Content = stackLayout,
                ClassId = "MenuLayOut"
            };
            grid.Children.Add(scrollView);
            Grid.SetColumn(scrollView, 0);
        }

        private ScrollView CreateDataLayout(Chapter chapter, bool IsReadOnly = false)
        {

            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ClassId = chapter.Id.ToString(),
                Padding = new Thickness(20, 10, 20, 10),
            };

            //Create question list based on view model.
            foreach (var question in chapter.Questions)
            {
                if (!(question.Type == QuestionType.Text && string.IsNullOrWhiteSpace(question.AnswerText)) && !(question.Type == QuestionType.DropDown && question.SelectedAnswerIndex == 0))
                {
                    var label = new Label()
                    {
                        ClassId = question.QuestionIdentifier.ToString() + "_label",
                        Text = question.Text,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Style = (Style)Application.Current.Resources["SmallFont"],
                        TextColor = Color.Gray,
                        AutomationId = Utils.AUTOMATION_ID + "label_" + question.QuestionIdentifier.ToString(),
                    };
                    stackLayout.Children.Add(label);

                    var AnswerEntry = new Entry()
                    {
                        ClassId = question.QuestionIdentifier.ToString() + "_entry",
                        //Text = (question.Type == QuestionType.Date) ? DateTime.ParseExact(question.AnswerText, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(Utils.DATE_FORMAT) : question.AnswerText,
                        Text = question.AnswerText,
                        AutomationId = Utils.AUTOMATION_ID + "Answer_" + question.QuestionIdentifier.ToString(),
                    };
                    stackLayout.Children.Add(AnswerEntry);
                }
            }

            var dataView = new ScrollView()
            {
                ClassId = chapter.Id.ToString(),
                Content = stackLayout,
            };
            dataView.Content.IsEnabled = !IsReadOnly;
            return dataView;
        }

        private void GetView(object sender, EventArgs e)
        {
            var button = (Button)sender;

            StackLayout menuLayOut = null;
            var grid = this.FindByName<Grid>("MainGrid");
            foreach (var v in grid.Children)
            {
                if (v.ClassId == currentView)
                {
                    layouts[v.ClassId] = (ScrollView)v;
                }
                //Resetting button colors to default Color
                if (v.ClassId == "MenuLayOut")
                {
                    var menuView = (ScrollView)v;
                    menuLayOut = (StackLayout)menuView.Content;
                    foreach (var child in menuLayOut.Children)
                    {
                        if (child.ClassId != "HeaderButton")
                        {
                            var btn = (Button)child;
                            btn.BackgroundColor = (Color)Application.Current.Resources["TextView_Bg"];
                        }
                    }
                }
            }
            grid.Children.RemoveAt(grid.Children.Count() - 1);
            var dataLayout = layouts[button.ClassId];
            grid.Children.Add(dataLayout);
            Grid.SetColumn(dataLayout, 1);
            currentView = button.ClassId;
            button.BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Blue"];
            SetFooterButtons();
        }

        /// <summary>
        /// Back button trigger event.
        /// This button is available in footerView, but the callback handler is registered in this class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnBackclicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        void SetFooterButtons()
        {
            // Manage declarations doesnt contain number bar.

            FooterViewId.SetNavigationBarVisible(false);
            if (ViewModel.Declarations != null && ViewModel.Declarations.Count > 0)
            {
                var declaration = ViewModel.Declarations.Find(i => i.Id == Convert.ToInt32(currentView));

                FooterViewId.SetButtonVisible(!(declaration.DeclarationType == DeclarationType.Submitted));
                FooterViewId.SetMiddleButtonVisible(declaration.DeclarationType == DeclarationType.Submitted);
            }
            else
            {
                FooterViewId.SetButtonVisible(false);
                FooterViewId.SetMiddleButtonVisible(false);
            }
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString())
                return;
            if (true)
            {
                IsUpdateList = false;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ProfileInfoDialogPage.ToString(),
                                new InfoHolder(ViewModel.SelectedProfileId, Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW, new List<int>() { Convert.ToInt32(currentView) }), false);
                    });
                }
                else
                {
                    App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.ProfileInfoDialogPage.ToString(),
                           new InfoHolder(ViewModel.SelectedProfileId, Enums.EnumMaps.MANAGE_DECLARATION_INFO_DIALOG_VIEW, new List<int>() { Convert.ToInt32(currentView) }), false);
                }
            }
            //else
            //{
            //    if (Device.RuntimePlatform == Device.iOS)
            //    {
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            await App.NavigationService.GoBack(false);
            //            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(), new InfoHolder(ViewModel.SelectedProfileId, Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN, Convert.ToInt32(currentView)), false);
            //        });
            //    }
            //    else
            //    {
            //        App.NavigationService.GoBack(false);
            //        App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.CreateDeclarationPage.ToString(), new InfoHolder(ViewModel.SelectedProfileId, Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN, Convert.ToInt32(currentView)), false);
            //    }
            //}

        }

        private void OnQRButtonClicked(object sender, EventArgs e)
        {
            IsUpdateList = false;

            if (App.NavigationService.CurrentPageKey == Enums.PageEnumsForNavigation.QRPage.ToString())
                return;
            var declaration = ViewModel.Declarations.Find(i => i.Id == Convert.ToInt32(currentView));
            InfoHolder InfoHolderObject = new InfoHolder(ViewModel.SelectedProfileId, Mode, new List<int>() { declaration.Id });
            App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.QRPage.ToString(), InfoHolderObject, false);
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