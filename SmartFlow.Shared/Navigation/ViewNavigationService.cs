using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartFlow.Shared.Helpers;

namespace SmartFlow.Shared.Navigation
{
    /// <summary>
    /// Class for defining all the functions declared in Navigation Service Interface
    /// </summary>
    public class ViewNavigationService : INavigationService
    {
        private static string TAG = "ViewNavigationService";

        private readonly object _sync = new object();
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Stack<NavigationPage> _navigationPageStack =
            new Stack<NavigationPage>();
        private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        /// <summary>
        /// Check if page key is already in stack.
        /// </summary>
        /// <param name="pageKey">Page key</param>
        /// <param name="pageType">Page Type</param>
        public void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        /// <summary>
        /// initialise root page in stack.
        /// </summary>
        /// <param name="rootPageKey">The key of root page</param>
        /// <returns></returns>
        public Page SetRootPage(string rootPageKey)
        {
            var rootPage = GetPage(rootPageKey);
            NavigationPage.SetHasNavigationBar(rootPage, false);
            _navigationPageStack.Clear();
            var mainPage = new NavigationPage(rootPage);
            NavigationPage.SetHasNavigationBar(mainPage, false);
            _navigationPageStack.Push(mainPage);
            return mainPage;
        }

        /// <summary>
        /// construct the key of current page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = CurrentNavigationPage.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        /// <summary>
        /// Navigate to previous page.
        /// </summary>
        /// <param name="animated">set up default animation to true.</param>
        /// <returns></returns>
        public async Task GoBack(bool animated = true)
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                 await CurrentNavigationPage.PopAsync(animated);
                 return;
            }

            if (_navigationPageStack.Count > 1)
            {
                
                _navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync(animated);
                return;
            }
            await CurrentNavigationPage.PopAsync(animated);
        }

        /// <summary>
        /// Navigation async Task without any params
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        /// <summary> 
        /// Navigation async Task with params passed from previous page
        /// </summary>
        /// <param name="pageKey">key of target page</param>
        /// <param name="parameter">Object is about to pass to target page.</param>
        /// <param name="animated">animation flag</param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);
            try
            {
                _navigationPageStack.Push(modalNavigationPage);
            }
            catch (Exception ex)
            {
                LogHandler.AddExceptionLog(TAG, "", ex, true);
            }
            
            
        }

        /// <summary>
        /// Navigation async Task without any params
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            await NavigateAsync(pageKey, null, animated);
        }

        /// <summary> 
        /// Navigation async Task with params passed from previous page
        /// </summary>
        /// <param name="pageKey">key of target page</param>
        /// <param name="parameter">Object is about to pass to target page.</param>
        /// <param name="animated">animation flag</param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        /// <summary>
        /// Get the page based on provided page key.
        /// </summary>
        /// <param name="pageKey">the key of page to get</param>
        /// <param name="parameter">object that want to be passed to target page.</param>
        /// <returns></returns>
        private Page GetPage(string pageKey, object parameter = null)
        {

            lock (_sync)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(string.Format(Resources.AppResources.PageError, pageKey));
                }

                var type = _pagesByKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[]
                    {
                    };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Length == 1
                                       && p[0].ParameterType == parameter.GetType();
                            });

                    parameters = new[]
                    {
                    parameter
                };
                }

                if (constructor == null)
                {
                    throw new InvalidOperationException(string.Format(Resources.AppResources.PageError1, pageKey));
                }

                var page = constructor.Invoke(parameters) as Page;
                return page;
            }
        }
    }
}
