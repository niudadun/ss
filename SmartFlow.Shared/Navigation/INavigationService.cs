using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Navigation
{
    /// <summary>
    /// Interface for custom navigation service.
    /// All pages are defined via pageKey and enum page value.
    /// </summary>
    public interface INavigationService
    {
        /// Get Current Page
        string CurrentPageKey { get; }
        /// Configure the pagekey with enum page value
        void Configure(string pageKey, Type pageType);
        /// Navigate to previous screen method
        Task GoBack(bool animated = true);
        /// Method to Navigate to another screen
        Task NavigateModalAsync(string pageKey, bool animated = true);
        /// Method to navigate to another screen with some param information passed from previous screen
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);
        /// Method to Navigate to another screen
        Task NavigateAsync(string pageKey, bool animated = true);
        /// Method to navigate to another screen with some param information passed from previous screen
        Task NavigateAsync(string pageKey, object parameter, bool animated = true);
    }
}
