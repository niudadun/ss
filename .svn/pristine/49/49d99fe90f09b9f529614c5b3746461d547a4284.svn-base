using System;
using System.Collections.Generic;
using System.Text;
using XLabs.Forms.Controls;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define validation behaviour for Picker view
    /// </summary>
    public class PickerValidationBehaviour : BaseBehaviour<ExtendedPicker>
    {
        private static string TAG = "PickerValidationBehaviour";

        /// <summary>
        /// Method to get the error message in case of validation failure.
        /// </summary>
        public PickerValidationBehaviour()
        {
            ValidationMessage = Shared.Helpers.L10n.GetMappedString(Shared.Enums.TextMapping.SELECT_TEXT);
        }

        /// <summary>
        /// Overridden method to attach SelectedIndexChanged callback to the view, so that we can handle the validation scenario when input text is modified.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(ExtendedPicker bindable)
        {
            if (bindable.SelectedIndex == 0)
                IsValid = false;
            bindable.SelectedIndexChanged += HandleSelectionChanged;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Method to check if the picker selection is index 0 or user has actually selected some value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleSelectionChanged(object sender, EventArgs e)
        {
            var picker = (ExtendedPicker)sender;
            if (picker.SelectedIndex == 0)
                IsValid = false;
            else
                IsValid = true;
        }

        /// <summary>
        /// Method to remove the SelectedIndexChanged callback handler from the view binding.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(ExtendedPicker bindable)
        {
            bindable.SelectedIndexChanged -= HandleSelectionChanged;
            base.OnDetachingFrom(bindable);
        }

    }
}
