using SmartFlow.Shared.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Services
{
    /// <summary>
    /// Interface for Api calls
    /// </summary>
    public interface IRESTService
    {
        /// Method to fetch Server Token from server
        Task<QuestionSheet> GetServerToken();

        /// Method to fetch Config Codes from server
        Task<QuestionSheet> GetDownloadCodes();

        /// Method to fetch Config Info from server
        Task<QuestionSheet> GetConfigInfo();

        /// Method to fetch Addresses from server
        Task<QuestionSheet> GetAddressData();        
    }
}
