﻿using SmartFlow.Shared.Behaviours;
using SmartFlow.Shared.Converters;
using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Helpers;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.UIControls;
using SmartFlow.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace SmartFlow.Shared.Views
{
    /// <summary>
    /// Create Declaration Class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDeclarationPage : ContentPage
    {
        Dictionary<string, ScrollView> layouts;
        string currentView;
        private static string TAG = "CreateDeclarationPage";

        //  Navigation Page index : This will tell that on which page the number pad is at and based on that we calculate the views to be shown.
        int NumberPageIndex = 1;

        Enums.EnumMaps Mode;
        DeclarationViewModel ViewModel;

        int TripSelectedId = -1;
        /// <summary>
        /// Create declaration consturctor
        /// </summary>
        public CreateDeclarationPage()
        {
            InitializeComponent();
            this.Padding = App.PagePadding;

            SetDataLayout();
        }

        /// <summary>
        /// Create Declaration Page constructor with Profile id and Mode
        /// </summary>
        public CreateDeclarationPage(InfoHolder infoHolder)
        {
            try
            {
                InitializeComponent();
                this.Padding = App.PagePadding;
                Mode = infoHolder.Mode;
                ViewModel = new DeclarationViewModel(infoHolder);
                SetDataLayout();
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
        }

        void SetDataLayout()
        {
            FooterViewId.GetBackButtonAndRemoveClick().Clicked += OnBackclicked;
            FooterViewId.SetButtonHandling("").Clicked += OnAddClicked;
            var ProfileChapterList = ViewModel.Declarations[0].Profile.Chapters.FindAll(i => (i != null && i.Questions != null && i.Questions.Count > 0));
            var TripChaptersList = ViewModel.Declarations[0].Chapters.FindAll(i => i.ChapterIdentifier == ChapterIdentifiers.Trip);

            ViewModel.Declarations[0].Chapters[0].ChapterClassId = ViewModel.Declarations.GetNewTripChapterClassId();
            ViewModel.Chapters.AddRange(ProfileChapterList);
            ViewModel.Chapters.AddRange(TripChaptersList);

            BindingContext = ViewModel;
            CreateMenuLayout(ViewModel.Chapters);

            layouts = new Dictionary<string, ScrollView>();

            ViewModel.Declarations[0].Profile.Chapters.ForEach(i => { layouts.Add(i.ChapterClassId, CreateDataLayout(i, true)); });

            ViewModel.Declarations[0].Chapters.ForEach(i => { layouts.Add(i.ChapterClassId, CreateDataLayout(i)); });

            var dataLayout = layouts[ChapterIdentifiers.Particulars.ToString()];

            var grid = this.FindByName<Grid>("MainGrid");
            grid.Children.Add(dataLayout);
            Grid.SetColumn(dataLayout, 1);
           
            if (Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                var chapterLayout = layouts[ViewModel.Declarations[0].Chapters[0].ChapterClassId];
                UpdateDropDown(chapterLayout);
                UpdateChapterView();
            }
            SetInformationForMenu();
            currentView = dataLayout.ClassId;
            //else if (Mode == EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN)
            //{
            //    // Change Request : 16 Jan 2018
            //    // User should not be required to click on “Add” button in case of individual trip declaration.
            //    // Only in case when the user wants to add another trip declaration, user should click on Add or the “+” button on the number pad.
            //    // Maximum of three trip declaration data can be submitted by the user, so once three declaration are submitted, the “Add” button should be disabled.
            //    // For the demo since we are only able to submit individual trip information, we should remove the “Add” button.

            //    Mode = EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE;
            //    UpdateChapterView();
            //}
        }


        

        void AddMenuItem(Chapter chapter)
        {
            var grid = this.FindByName<Grid>("MainGrid");
            var menuView = (ScrollView)grid.Children.First(i => i.ClassId == "MenuLayOut");
            var menuLayOut = (StackLayout)menuView.Content;
            var insertIndex = menuLayOut.Children.IndexOf(menuLayOut.Children.First(i => i.ClassId == ChapterIdentifiers.Empty.ToString()));
            var button = new SmartFlow_Button(chapter.Text, chapter.ChapterClassId, Utils.AUTOMATION_ID + chapter.ChapterIdentifier.ToString());
            button.Clicked += GetView;
            menuLayOut.Children.Insert(insertIndex, button);
        }


        void ControlContent(bool declarationEnabled)
        {
            foreach (var i in layouts)
            {
                if (i.Key == ChapterIdentifiers.Particulars.ToString())
                {
                    layouts[i.Key].Content.IsEnabled = false;
                }
                else
                {
                    layouts[i.Key].Content.IsEnabled = declarationEnabled;
                }
            }
        }

        void SetInformationForMenu()
        {
            var dataLayout = layouts[ChapterIdentifiers.Particulars.ToString()];
            var stackLayout = (StackLayout)dataLayout.Content;
            stackLayout.IsEnabled = true;
            stackLayout.BackgroundColor = Color.Transparent;
            stackLayout.Opacity = 1;

            
            if (Mode == Enums.EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE || Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.PREVIEW_TEXT));
                ControlContent(true);
                FooterViewId.SetNavigationBarVisible(true);
                CreateNavBar();
            }
            else if (Mode == EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN)
            {
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.ADD_TEXT));

                // Disabling the view, as this is a selection window
                ControlContent(false);

                // As of now we are not having Group handling. So disable button to add for Group View.
                if (currentView == Enums.ChapterIdentifiers.Particulars.ToString())
                {
                    FooterViewId.SetButtonVisible(false);
                }
                else
                {
                    // Change Request : 16 Jan 2018
                    // No need to show Add on TRIPS, as when user clicks on TRIP, we have to start individual declaration automatically
                    FooterViewId.SetButtonVisible(false);
                }
            }
            else if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                FooterViewId.SetButtonHandling(Helpers.L10n.GetMappedString(Enums.TextMapping.SUBMIT_TEXT));

                // Disabling the view, as this is a selection window
                ControlContent(false);
                FooterViewId.SetNavigationBarVisible(true);
                CreateNavBar();
                UpdateChapterView();
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
            if (ViewModel.Declarations != null)
            {
                try
                {
                    LogHandler.AddLog(TAG, "SELECTED ID : " + TripSelectedId);
                    FooterViewId.CreateNavBar(true, TripSelectedId, true, ViewModel.Declarations.Count, NumberPageIndex, NavNextPage, NavBackPage, OnSelectionForNavBar, OnCreateClicked, Mode);
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
            CreateNavBar();
            GetViewInFront(Utils.GetTripId(TripSelectedId));
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            if (Mode == EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN)
            {
                Mode = EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE;
                UpdateChapterView();
                SetInformationForMenu();
            }
            else if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ViewModel.Declarations.ForEach(i =>
                    {
                        i.DeclarationType = Models.Ede.Enums.DeclarationType.Submitted;
                        i.DeNo = Utils.RandomString(10);
                    });

                    FooterViewId.IsVisible = false;
                    await ViewModel.UpdateDeclaration();

                    await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.SuccessPage.ToString(),
                        new InfoHolder(ViewModel.Declarations[0].ProfileId, Mode, ViewModel.Declarations.GetDeclarationIds()), false);
                    FooterViewId.IsVisible = true;
                });
            }
            else if (Mode == EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE)
            {
                // For preview only, we will do validation checks.
                var isValidated = false;
                foreach (var layout in layouts.Where(i => i.Key != ChapterIdentifiers.Particulars.ToString()))
                {
                    var stackLayout = (StackLayout)layout.Value.Content;
                    isValidated = CheckValidation(stackLayout);
                    if (!isValidated)
                    {
                        GetViewInFront(layout.Key);
                        TripSelectedId = Utils.GetTripIndex(layout.Key);
                        CreateNavBar();
                        break;
                    }
                }
                if (isValidated)
                {
                    SaveDeclaration(false);
                }
            }
            else if (Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                // For preview only, we will do validation checks.
                if (CheckValidation(GetCurrentStackLayout()))
                {
                    SaveDeclaration(false);
                }
            }
        }

        private void UpdateChapterView()
        {
            var grid = this.FindByName<Grid>("MainGrid");
            ScrollView scrollView = (ScrollView)grid.Children[0];
            StackLayout stackLayout = (StackLayout)scrollView.Content;

            foreach (View btn in stackLayout.Children.Where(i => i.ClassId != ChapterIdentifiers.Particulars.ToString() && i.ClassId != ChapterIdentifiers.Header.ToString() && i.ClassId != ChapterIdentifiers.Empty.ToString()))
            {
                if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == Enums.EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
                    ((Button)btn).Text = btn.ClassId + " " + L10n.GetMappedString(TextMapping.PREVIEW_TEXT);
                else
                    ((Button)btn).Text = btn.ClassId;
            }
            
        }

       
        private void OnCreateClicked(object sender, EventArgs e)
        {
            var declaration = ViewModel.InitializeDeclarationTemplate();
            ViewModel.Declarations.Add(declaration);
            declaration.Chapters[0].Text = declaration.Chapters[0].ChapterClassId = ViewModel.Declarations.GetNewTripChapterClassId();
            AddMenuItem(declaration.Chapters[0]);
            layouts.Add(declaration.Chapters[0].ChapterClassId, CreateDataLayout(declaration.Chapters[0]));
            GetViewInFront(declaration.Chapters[0].ChapterClassId);
            TripSelectedId = declaration.Chapters[0].ChapterClassId.GetTripIndex();
            CreateNavBar();
        }

        private void CreateMenuLayout(List<Chapter> Chapters)
        {
            var stackLayout = new SmartFlow_StackLayout_Menu();
            stackLayout.Children.Add(new SmartFlow_HeaderButton());
            foreach (var chapter in Chapters)
            {
                var button = new SmartFlow_Button(chapter.Text, chapter.ChapterClassId, Utils.AUTOMATION_ID + chapter.ChapterIdentifier.ToString());
                button.Clicked += GetView;
                if (chapter.ChapterIdentifier == ChapterIdentifiers.Particulars)
                    button.BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Blue"];
                stackLayout.Children.Add(button);
            }
            for (var i = 0; i < 5; i++)
            {
                stackLayout.Children.Add(new SmartFlow_Button(classId: ChapterIdentifiers.Empty.ToString(), automationId: Utils.AUTOMATION_ID + "Empty_" + i));
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

            var stackLayout = new SmartFlow_StackLayout(chapter.ChapterClassId, !IsReadOnly);

            //Create question list based on view model.
            foreach (var question in chapter.Questions)
            {
                var label = new SmartFlow_Label(question.Text, question.QuestionIdentifier.ToString() + "_label", Utils.AUTOMATION_ID + "label_" + question.QuestionIdentifier.ToString());
                stackLayout.Children.Add(label);
                var validationLabel = new SmartFlow_Validation_Label(question.QuestionIdentifier.ToString() + "_validationlabel", Utils.AUTOMATION_ID + "validationLabel_" + question.QuestionIdentifier.ToString());
                var type = question.Type;
                switch (type)
                {
                    case QuestionType.Date:
                        var datePicker = new SmartFlow_DatePicker(question.QuestionIdentifier.ToString(), Utils.AUTOMATION_ID + question.QuestionIdentifier.ToString());
                        if (question.Config != null)
                        {
                            if (question.Config.MaxDate != null)
                            {
                                datePicker.SetBinding(DatePicker.MaximumDateProperty, new Binding("MaxDate", BindingMode.OneWay, source: question.Config));
                            }
                            if (question.Config.MinDate != null)
                            {
                                datePicker.SetBinding(DatePicker.MinimumDateProperty, new Binding("MinDate", BindingMode.OneWay, source: question.Config));
                            }
                        }
                        datePicker.SetBinding(DatePicker.DateProperty, new Binding("AnswerText", source: question, converter: new Converters.DateTimeConverter()));
                        stackLayout.Children.Add(datePicker);
                        break;
                    case QuestionType.DropDown:

                        var ddList = new List<string>();
                        foreach (var s in question.Answers)
                        {
                            ddList.Add(s.Text);
                        }
                        var dd = new SmartFlow_Picker(ddList, question.QuestionIdentifier.ToString(), Utils.AUTOMATION_ID + question.QuestionIdentifier.ToString());

                        dd.SelectedIndex = question.SelectedAnswerIndex;
                        dd.SetBinding(ExtendedPicker.SelectedIndexProperty, new Binding("SelectedAnswerIndex", source: question));
                        if (question.Config != null && question.Config.IsRequired)
                        {
                            dd.Behaviors.Add(new PickerValidationBehaviour());
                        }
                        stackLayout.Children.Add(validationLabel);
                        stackLayout.Children.Add(dd);

                        if (dd.SelectedIndex <= 0)
                        {
                            dd.IsVisible = question.IsVisible;
                            label.IsVisible = question.IsVisible;
                        }

                        // Add select change listener for every drop down, so that we can hide the validation label if any, when user selects some text from 
                        // drop down
                        if (question.Answers != null && question.Answers.Count > 0)
                            dd.SelectedIndexChanged += SelectedIndexChanged;
                        break;
                    default:
                        var entry = new SmartFlow_Entry(question.QuestionIdentifier.ToString(), Utils.AUTOMATION_ID + question.QuestionIdentifier.ToString());
                        try
                        {
                            entry.SetBinding(Entry.TextProperty, new Binding("AnswerText", source: question, converter: new TrimConverter()));
                            entry.TextChanged += HandleTextChanged;
                        }
                        catch (Exception ex)
                        {
                            LogHandler.AddExceptionLog(TAG, "", ex, true);
                        }
                        if (question.Config != null)
                        {
                            if (question.Config.MaxLength != 0)
                            {
                                entry.Behaviors.Add(new LengthValidationBehaviour()
                                {
                                    MaxLength = question.Config.MaxLength,
                                });
                            }
                            if (question.Config.IsRequired)
                            {
                                entry.Behaviors.Add(new EmptyValidationBehaviour());
                            }
                            if (question.Config.ValidateSpecialCharacters)
                            {
                                entry.Behaviors.Add(new CharacterValidationBehaviour());
                            }
                            if (question.Config.ValidateSpecialCharactersName)
                            {
                                entry.Behaviors.Add(new CharacterValidationBehaviour(true));
                            }
                            if (question.Config.AllowOnlyNumeric)
                            {
                                entry.Keyboard = Keyboard.Numeric;
                                entry.Behaviors.Add(new NumericValidationBehavior());
                            }
                            if (question.Config.ValidateEmailAddress)
                            {
                                entry.Keyboard = Keyboard.Email;
                                entry.Behaviors.Add(new EmailValidationBehavior());
                            }
                            if (question.Config.ValidateContactNumber)
                            {
                                entry.Keyboard = Keyboard.Telephone;
                            }
                        }

                        //if (question.Config != null)
                        //{
                        stackLayout.Children.Add(validationLabel);
                        //}
                        if (entry.Text.Length <= 0)
                        {
                            entry.IsVisible = question.IsVisible;
                            label.IsVisible = question.IsVisible;
                        }
                        stackLayout.Children.Add(entry);
                        break;
                }

            }

            var myView = new ScrollView()
            {
                ClassId = chapter.ChapterClassId,
                Content = stackLayout,
            };
            
            return myView;
        }

        void UpdateDropDown(ScrollView layout)
        {
            var stackLayout = (StackLayout)layout.Content;
            foreach (var s in stackLayout.Children.Where(i => i is ExtendedPicker && i.IsVisible))
            {
                ConditionalMapping(s, stackLayout, layout.ClassId);
            }
        }

        private void GetView(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (Mode == EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE || Mode == EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                if (button.ClassId == ChapterIdentifiers.Particulars.ToString())
                {
                    TripSelectedId = -1;
                }
                else
                {
                    TripSelectedId = button.ClassId.GetTripIndex();
                }
                CreateNavBar();
            }
            else if (Mode == EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN)
            {
                // Change Request : 16 Jan 2018
                //    // User should not be required to click on “Add” button in case of individual trip declaration.
                //    // Only in case when the user wants to add another trip declaration, user should on the “+” button on the number pad.
                //    // Maximum of three trip declaration data can be submitted by the user, so once three declaration are submitted, the “+” button should be disabled.
                //    // For the demo since we are only able to submit individual trip information, we should remove the “+” button.

                //    Mode = EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE;
                //    UpdateChapterView();

                Mode = EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE;
                button.Text = button.ClassId;
                // Since when user clicks TRIP in Selection mode, we have to select the TRIP index 1.
                TripSelectedId = button.ClassId.GetTripIndex();
                SetInformationForMenu();
            }

            GetViewInFront(button.ClassId);
        }

        void UpdateMenuSelectionColor(ScrollView menuView, string chapterClassId)
        {
            var menuLayOut = (StackLayout)menuView.Content;
            foreach (var child in menuLayOut.Children.Where(i => i.ClassId != ChapterIdentifiers.Empty.ToString() && i.ClassId != ChapterIdentifiers.Header.ToString()))
            {
                var btn = (Button)child;
                if (btn.ClassId == chapterClassId)
                {
                    btn.BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Blue"];
                }
                else
                {
                    btn.BackgroundColor = (Color)Application.Current.Resources["TextView_Bg"];
                }
            }
        }

        void GetViewInFront(string chapterClassId)
        {
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
                    UpdateMenuSelectionColor(menuView, chapterClassId);
                }
            }

            grid.Children.RemoveAt(grid.Children.Count() - 1);
            var dataLayout = layouts[chapterClassId];
            grid.Children.Add(dataLayout);
            Grid.SetColumn(dataLayout, 1);
            currentView = chapterClassId;           
        }

        /// <summary>
        /// Method to check if some text has been entered. We will remove validation label for that question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var EntryView = (Entry)sender;

                var grid = this.FindByName<Grid>("MainGrid");
                var scrollView = (ScrollView)grid.Children[1];
                var stackLayout = (StackLayout)scrollView.Content;

                var chapter = ViewModel.Declarations.GetChapter(currentView);
                var Question = chapter.Questions.Find(i => i.QuestionIdentifier.ToString() == EntryView.ClassId);

                // If their is any change in the question selection index, then we should hide the validation label for this question as well.
                var validationLabel = stackLayout.Children.First(k => k is Label && k.ClassId == Question.QuestionIdentifier.ToString() + "_validationlabel");
                validationLabel.IsVisible = false;
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
        }

        /// <summary>
        /// When selection of conditional dropdown is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectedIndexChanged(object sender, EventArgs e)
        {
            var grid = this.FindByName<Grid>("MainGrid");
            var scrollView = (ScrollView)grid.Children.FirstOrDefault(i => i.ClassId == currentView); ;
            var stackLayout = (StackLayout)scrollView.Content;

            ConditionalMapping(sender, stackLayout, currentView);
        }

        /// <summary>
        /// All Conditional questions are displayed based on this method
        /// </summary>
        /// <param name="sender">The sender.</param>
        void ConditionalMapping(object sender,StackLayout stackLayout, string chapterCurrentView)
        {
            try
            {
                var picker = (ExtendedPicker)sender;
               
                var chapter = ViewModel.Declarations.GetChapter(chapterCurrentView);
                var Question = chapter.Questions.Find(i => i.QuestionIdentifier.ToString() == picker.ClassId);

                try
                {
                    // If their is any change in the question selection index, then we should hide the validation label for this question as well.
                    var validationLabel = stackLayout.Children.First(k => k is Label && k.ClassId == Question.QuestionIdentifier.ToString() + "_validationlabel");
                    validationLabel.IsVisible = false;
                }
                catch (Exception e)
                {
                    LogHandler.AddExceptionLog(TAG, "", e, true);
                }

                // Logic below makes sure that for any change in drop down selection index, the questions which are dependent on the answers of this question, should 
                // be traversed again and can be made visible/invisible based on the selected index.

                if (Question != null && Question.Answers != null)
                {
                    var AnswerList = Question.Answers.FindAll(i => (i.ConditionalQuestion != null && i.ConditionalQuestion.Exists(j => j.ConditionQuestion != QuestionIdenfiers.PARTICULARS)));
                    foreach (var answer in AnswerList)
                    {
                        answer.ConditionalQuestion.ForEach(j =>
                        {
                            var validationLabel = stackLayout.Children.First(k => k is Label && k.ClassId == (j.ConditionQuestion.ToString() + "_validationlabel"));
                            var label = stackLayout.Children.First(k => k is Label && k.ClassId == (j.ConditionQuestion.ToString() + "_label"));
                            var editView = stackLayout.Children.First(k => k.ClassId == j.ConditionQuestion.ToString());
                            var conditionalQues = chapter.Questions.Find(n => n.QuestionIdentifier.ToString() == j.ConditionQuestion.ToString());

                            if (answer.Text == picker.SelectedItem.ToString())
                            {
                                // maintain visibility of validation label what ever it is.
                                label.IsVisible = true;
                                editView.IsVisible = true;
                            }
                            else
                            {
                                validationLabel.IsVisible = false;
                                label.IsVisible = false;
                                editView.IsVisible = false;
                                if (conditionalQues.Type == QuestionType.DropDown)
                                    ((ExtendedPicker)editView).SelectedIndex = 0;
                                else if (conditionalQues.Type == QuestionType.Text)
                                    ((Entry)editView).Text = "";
                            }
                        });
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
        }

        /// <summary>
        /// This recursive to be used in case the delegate method fails at any scenario
        /// </summary>
        /// <param name="chapter">The chapter.</param>
        /// <param name="question">The question.</param>
        /// <param name="stackLayout">The stack layout.</param>
        /// <param name="picker">The picker.</param>
        /// <returns></returns>
        bool ConditionalRecursive(Chapter chapter, Question question, StackLayout stackLayout, ExtendedPicker picker)
        {
            List<Answer> AnswerList = question.Answers.FindAll(i => (i.ConditionalQuestion != null && i.ConditionalQuestion.Exists(j => j.ConditionQuestion != QuestionIdenfiers.PARTICULARS)));

            foreach (var answer in AnswerList)
            {
                View label = null;
                View editView = null;
                View validationLabel = null;
                Question conditionalQues = null;
                foreach (var j in answer.ConditionalQuestion)
                {
                    validationLabel = stackLayout.Children.First(k => k is Label && k.ClassId == (j.ConditionQuestion.ToString() + "_validationlabel"));
                    label = stackLayout.Children.First(k => k is Label && k.ClassId == (j.ConditionQuestion.ToString() + "_label"));
                    editView = stackLayout.Children.First(k => k.ClassId == j.ConditionQuestion.ToString());
                    conditionalQues = chapter.Questions.Find(n => n.QuestionIdentifier.ToString() == j.ConditionQuestion.ToString());

                    if (answer.Text == picker.SelectedItem.ToString())
                    {
                        // maintain visibility of validation label what ever it is.
                        label.IsVisible = true;
                        editView.IsVisible = true;
                    }
                    else
                    {
                        validationLabel.IsVisible = false;
                        label.IsVisible = false;
                        editView.IsVisible = false;
                        if (conditionalQues.Type == QuestionType.DropDown)
                            ((ExtendedPicker)editView).SelectedIndex = 0;
                        else if (conditionalQues.Type == QuestionType.Text)
                            ((Entry)editView).Text = "";

                        return ConditionalRecursive(chapter, conditionalQues, stackLayout, picker);
                    }
                }
            }
            return true;
        }

        StackLayout GetCurrentStackLayout()
        {
            var grid = this.FindByName<Grid>("MainGrid");
            var scrollView = (ScrollView)grid.Children[1];
            var stackLayout = (StackLayout)scrollView.Content;
            return stackLayout;
        }

        /// <summary>
        /// Validation checker method.
        /// this method checks all the validation behaviours applied for different views added to the profile details page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Validation<T>(StackLayout stackLayout) where T : BindableObject
        {
            var isValid = true;
            try
            {
                foreach (var i in stackLayout.Children)
                {
                    if (i is T && i.IsVisible)
                    {
                        foreach (var j in i.Behaviors)
                        {
                            var behaviour = j as BaseBehaviour<T>;
                            var label = (SmartFlow_Validation_Label)stackLayout.Children.First(k => k is SmartFlow_Validation_Label && k.ClassId == (i.ClassId + "_validationlabel"));
                            label.IsVisible = false;

                            if (behaviour.IsValid) continue;
                            else
                            {
                                label.IsVisible = true;
                                isValid = false;
                                label.Text = behaviour.ValidationMessage;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }

            return isValid;
        }

        bool CheckValidation(StackLayout stackLayout)
        {
            var IsEntryValidated = Validation<Entry>(stackLayout);
            var IsPickerValidated = Validation<ExtendedPicker>(stackLayout);
            return IsEntryValidated && IsPickerValidated;
        }

        /// <summary>
        /// In case user press back button, then we have to ask user if he wants to save the profile or not. if user clicks cancel, then we have to go back.
        /// Similar thing will happen if user clicks Save, but for that if user clicks cancel, then view will remain on that screen.
        /// </summary>
        /// <param name="IsComingFromBack"></param>
        async Task SaveDeclaration(bool IsComingFromBack)
        {
            bool isDone = false;
            if (Mode == Enums.EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE || Mode == Enums.EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                var answer = await DisplayAlert(Shared.Resources.AppResources.declaration_save_message, Shared.Resources.AppResources.ok_proceed,
                Shared.Resources.AppResources.ok_text, Shared.Resources.AppResources.cancel_text);
                isDone = answer;
                if (answer)
                {
                    // If user clicks Yes to Save declaration, then since its a database call,we should hide all views and only display indicator.
                    // This gives impression to user that something is happening. Kind of a feedback.
                    pageGrid.IsEnabled = false;
                    FooterViewId.IsVisible = false;
                    if (Mode == EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
                    {
                        await ViewModel.UpdateDeclaration();

                    }
                    else
                    {
                        await ViewModel.AddDeclaration();
                    }
                    // If user is coming from back click, then we have to just display Success Save message.
                    // Else user will be coming from Preview button and we have to change the mode and route user to preview view.
                    if (IsComingFromBack)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await App.NavigationService.NavigateModalAsync(Enums.PageEnumsForNavigation.SuccessPage.ToString(), new InfoHolder(-1, Mode), false);

                            pageGrid.IsEnabled = true;
                            FooterViewId.IsVisible = true;
                        });
                    }
                    else
                    {
                        pageGrid.IsEnabled = true;
                        FooterViewId.IsVisible = true;

                        // This is so that we can control the back behaviour from Preview Mode.
                        // We should be knowing where we are going if user clicks back on preview screen
                        if (Mode == EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE)
                        {
                            Mode = EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE;
                        }
                        else if (Mode == EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
                        {
                            Mode = EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN;
                        }

                        SetInformationForMenu();
                    }
                }
                else
                {
                    // If user clicks NO to Save declaration, then we are only refresh db, if we need to unload the pageitself.
                    // Page unload is only done, if user has pressed back and then "no" on the save dialog.
                    // So we show indicator and hide every other view.
                    // Now if user clicks No on the Preview dialog, then we dont have to do anything as we are on same page.
                    if (IsComingFromBack)
                    {
                        pageGrid.IsEnabled = false;
                        FooterViewId.IsVisible = false;
                        //await ViewModel.RefreshDBCommand();
                        FooterViewId.BackBtnCommand.Execute(null);
                        pageGrid.IsEnabled = true;
                        FooterViewId.IsVisible = true;
                    }
                }
            }
        }

        void GoBackToEdit()
        {
            // User clicks back on preview mode, we should know where we are going back to 
            if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE)
            {
                Mode = EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE;
            }
            else if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                Mode = EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN;
            }
            UpdateChapterView();
            SetInformationForMenu();
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
            if (Mode == EnumMaps.CREATE_DECLARATION_SELECTION_SCREEN)
            {
                App.NavigationService.GoBack(false);
            }
            else if (Mode == EnumMaps.DECLARATION_PREVIEW_MODE_SINGLE_PROFILE || Mode == EnumMaps.DECLARATION_PREVIEW_MODE_UPDATE_SCREEN)
            {
                // If user is in preview mode, and clicks back, we have to open edit page with same content, so that user can edit the declaration.
                GoBackToEdit();
            }
            else if (Mode == EnumMaps.CREATE_DECLARATION_SINGLE_PROFILE || Mode == EnumMaps.CREATE_DECLARATION_UPDATE_SCREEN)
            {
                SaveDeclaration(true);
            }
            return true;
        }


    }
}