using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Characters only input view.
    /// </summary>
    public class CharacterValidationBehaviour : BaseBehaviour<Entry>
    {
        private static string TAG = "CharacterValidationBehaviour";

        string characterRegex = "";

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public CharacterValidationBehaviour()
        {
            characterRegex = @"^[\w\s{./\\(),'}+:?®©-]+$";
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.VALIDATION_MESSAGE);
        }

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public CharacterValidationBehaviour(bool isName)
        {
            characterRegex = @"[a-zA-Z/'\s]+$";
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.VALIDATION_MESSAGE);
        }

        /// <summary>
        /// Overridden method to attach onTextChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Method to check if the entered text matches the validation pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, characterRegex));
        }

        /// <summary>
        /// Method to remove the onTextChanged callback handler from the view binding.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Entry bindable)
        {        
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

    }
}
