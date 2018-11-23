using SmartFlow.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFlow.Shared.Helpers
{
    /// <summary>
    /// InfoHolder class
    /// This class contains the state and profile id of the selected profile.
    /// It is used when Details page/Success page is called with a particular details page state.
    /// So rather than passing two separate params, we use this class to pass data
    /// </summary>
    public class InfoHolder
    {
        public int ProfileId { get; private set; }
        public string LanguageID { get; private set; }
        public Enums.EnumMaps Mode { get; private set; }
        public List<int> DeclarationIds { get; private set; }



        /// <summary>
        ///  InfoHolder parametrized Constructor
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="Mode"></param>
        public InfoHolder(int profileId, Enums.EnumMaps mode)
        {
            ProfileId = profileId;
            Mode = mode;
        }
		

        /// <summary>
        ///  InfoHolder parametrized Constructor
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="whichInfo"></param>
        public InfoHolder(string languageID, Enums.EnumMaps mode)
        {
            LanguageID = languageID;
            Mode = mode;
        }

        /// <summary>
        /// InfoHolder parametrized Constructor
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="whichInfo"></param>
        /// <param name="name"></param>
        /// <param name="dateInfo"></param>
        /// <param name="deNoInfo"></param>
        /// <param name="tripIDInfo"></param>
        public InfoHolder(int profileId, Enums.EnumMaps mode, List<int> declarationIds)
        {
            ProfileId = profileId;
            Mode = mode;
            DeclarationIds = declarationIds;
        }
       
    }
}
