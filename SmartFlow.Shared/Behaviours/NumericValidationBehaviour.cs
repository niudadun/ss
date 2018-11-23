using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Numbers Only for input view.
    /// </summary>
    public class NumericValidationBehavior : BaseBehaviour<Entry>
    {
        private static string TAG = "NumericValidationBehavior";

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public NumericValidationBehavior()
        {
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.NUMBERS_ONLY_MESSAGE);
        }

        /// <summary>
        /// Overridden method to attach onTextChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Method to remove the onTextChanged callback handler from the view binding.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Method to check if the entered text matches the validation pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            double result;
            IsValid = double.TryParse(e.NewTextValue, out result);
        }
    }
}
