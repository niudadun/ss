using SmartFlow.Shared.Data.DataModels;
using SmartFlow.Shared.Mappers.Db_ViewModel;
using SmartFlow.Shared.Models;
using SmartFlow.Shared.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartFlow.Shared.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
       
        protected bool _isLoading;
        public List<Profile> Profiles { get; set; }
        public List<Declaration> Declarations { get; set; }
        protected readonly IProfilesRepository _profilesRepository;
        protected readonly IDeclarationsRepository _declarationsRepository;

        public BaseViewModel()
        {
            _isLoading = IsLoading;
            _profilesRepository = App._profilesRepository;
            _declarationsRepository = App._declarationsRepository;
        }
        /// <summary>
        /// Control button enabled or not.
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }

            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }

        public async void GetProfiles()
        {
            IsLoading = true;
            var db_Profiles = await _profilesRepository.GetProfilesAsync();
            Profiles = new List<Profile>();
            ViewModelMapper.MapProfilesToViewModel(db_Profiles.ToList(), Profiles);
            Profiles = Profiles.OrderBy(c => c.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
            IsLoading = false;
        }


        public async void GetDeclartions()
        {
            IsLoading = true;
            var db_Declarations = await _declarationsRepository.GetDeclarationsAsync();
            Declarations = new List<Declaration>();
            ViewModelMapper.MapDeclarationsToViewModel(db_Declarations.ToList(), Declarations);
            IsLoading = false;
        }

        public async Task DeleteDeclaration(object id)
        {
            IsLoading = true;
            await _declarationsRepository.RemoveDeclarationAsync(Convert.ToInt32(id));
            IsLoading = false;
        }

        public async Task DeleteProfile(object id)
        {
            IsLoading = true;
            await _profilesRepository.RemoveProfileAsync(Convert.ToInt32(id));
            IsLoading = false;
        }


        public async Task<List<Profile>> GetProfilesBasicInfo()
        {
            IsLoading = true;
            var db_Profiles = await _profilesRepository.GetProfilesAsync();
            var profiles = new List<Profile>();
            ViewModelMapper.MapProfilesBasicToViewModel(db_Profiles.ToList(), profiles);
            profiles = profiles.OrderBy(c => c.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
            IsLoading = false;
            return profiles;
        }

        public async Task<List<Declaration>> GetDeclarationsBasicInfo()
        {
            IsLoading = true;
            var db_declarations = await _declarationsRepository.GetDeclarationsAsync();
            var declarations = new List<Declaration>();
            ViewModelMapper.MapDeclarationsBasicToViewModel(db_declarations.ToList(), declarations);
            IsLoading = false;
            return declarations;
        }

        public async Task<Profile_Dbo> GetProfileById(int ProfileId)
        {
            IsLoading = true;
            var dbProfile = await _profilesRepository.GetProfileByIdAsync(ProfileId);
            IsLoading = false;
            return dbProfile;
        }
        
        /// <summary>
        /// Gets the refresh database command.
        /// </summary>
        /// <value>
        /// The refresh database command.
        /// </value>
        public async Task RefreshDBCommand()
        {
            IsLoading = true;
            await _profilesRepository.RefreshDbContext();
            IsLoading = false;
        }
        /// <summary>
        /// Method called when any property is changed for the profile list
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Callback for any property change
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
