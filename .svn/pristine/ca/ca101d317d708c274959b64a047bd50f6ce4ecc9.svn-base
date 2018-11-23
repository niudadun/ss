using SmartFlow.Shared.Data.DataModels;
using SmartFlow.Shared.DataTemplates;
using SmartFlow.Shared.Mappers.Db_ViewModel;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Respository.Interfaces;
using SmartFlow.Shared.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFlow.Shared.ViewModels
{
    /// <summary>
    /// Class for handling Profile model and functions to handle database calls.
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        private static string TAG = "ProfileViewModel";
        private Profile _intialProfile = null;
        private Profile_Dbo dbProfile = null;
        

        /// <summary>
        /// Current/Selected profile model
        /// </summary>
        public Profile CurrentProfile {

            get
            {
                if (_intialProfile == null)
                {
                    _intialProfile = ProfileTemplate.CreateProfile();
                }
                return _intialProfile;
            }
            set
            {
                _intialProfile = value;
            }
        }

        /// <summary>
        /// Profile view model construction.
        /// </summary>
        /// <param name="navigate"></param>
        public ProfileViewModel()
        {

        }

        /// <summary>
        /// Profile view model with ID
        /// </summary>
        /// <param name="Id">selected profile id</param>
        public ProfileViewModel(int Id)
        {
            dbProfile = GetProfileById(Id).Result;
            ViewModelMapper.MapProfileToViewModel(dbProfile, CurrentProfile);
        }

        public async Task AddProfile()
        {
            IsLoading = true;
            dbProfile = new Profile_Dbo();
            DBMapper.MapProfileToDB(CurrentProfile, dbProfile, true);
            await _profilesRepository.AddProfileAsync(dbProfile);
            IsLoading = false;
        }
        
        public async Task UpdateProfile()
        {
            IsLoading = true;
            DBMapper.MapProfileToDB(CurrentProfile, dbProfile, true);
            await _profilesRepository.UpdateProfileAsync(dbProfile);
            IsLoading = false;
        }

       

    }
}
