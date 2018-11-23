using SmartFlow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.Behaviours
{
    /// <summary>
    /// This class is used to define Base behaviours for the validations needed for input forms like Profile and Declaration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseBehaviour<T> : Behavior<T> where T : BindableObject
    {
        private static string TAG = "BaseBehaviour";

        private bool _isValid = true;

        /// <summary>
        /// Getter Setter for isValid property
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter Setter for message text for a validation error
        /// </summary>
        public string ValidationMessage { get; set; }
    }
}
