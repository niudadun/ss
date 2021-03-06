﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Empty input view.
    /// </summary>
    public class EmptyValidationBehaviour : BaseBehaviour<Entry>
    {
        private static string TAG = "EmptyValidationBehaviour";

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public EmptyValidationBehaviour()
        {
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.MANDATORY_FIELD_MESSAGE);
        }

        /// <summary>
        /// Overridden method to attach onTextChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            IsValid = !string.IsNullOrWhiteSpace(bindable.Text);
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
            IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
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
