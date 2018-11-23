using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Email input view.
    /// </summary>
    public class EmailValidationBehavior : BaseBehaviour<Entry>
    {
        private static string TAG = "EmailValidationBehavior";

        /// <summary>
        /// Regular expression for Email pattern
        /// </summary>
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        //static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumericValidationBehavior), false);

        //public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        //public bool IsValid
        //{
        //    get { return (bool)base.GetValue(IsValidProperty); }
        //    private set { base.SetValue(IsValidPropertyKey, value); }
        //}

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public EmailValidationBehavior()
        {
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.INVALID_EMAIL);
        }

        /// <summary>
        /// Overridden method to attach onTextChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        /// <summary>
        /// Method to check if the entered text matches the validation pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        /// <summary>
        /// Method to remove the onTextChanged callback handler from the view binding.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
