using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartFlow.Shared.Helpers;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Length Filter for input view.
    /// </summary>
    public class LengthValidationBehaviour : BaseBehaviour<Entry>
    {
        private static string TAG = "LengthValidationBehaviour";

        /// <summary>
        /// Getter Setter for max length
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Overridden method to attach onTextChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        /// <summary>
        /// Method to remove the onTextChanged callback handler from the view binding.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        /// <summary>
        /// Method to check if the entered text length is greater than allowed length
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                // If entry text is bigger than max length, then this function will remove last character from the length
                if (entry.Text.Length > this.MaxLength)
                {
                    string entryText = entry.Text;
                    entryText = entryText.Remove(entryText.Length - 1);
                    entry.Text = entryText;
                }
            }
            catch (Exception e1)
            {
                LogHandler.AddExceptionLog(TAG, "", e1, true);
            }
        }
    }
}
