using Newtonsoft.Json;
using SmartFlow.Shared.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Services
{
    /// <summary>
    /// Class to define Api functions from IRestService interface
    /// </summary>
    public class RESTService : IRESTService
    {
        private static string TAG = "RESTService";

        HttpClient client;

        /// RestUrl for Api Calls
        public static readonly string RestUrl = "http://localhost:8080/?option=com_workflow&task=smflw_getqs&format=raw&location_Id=2&language_Id=1";

        /// <summary>
        /// RestServices constructor to initiate the object values
        /// </summary>
        public RESTService()
        {
           // var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
           // var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        /// <summary>
        /// Get Server Token
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionSheet> GetServerToken()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var questionSheet = new QuestionSheet();
            //try
            //{
            //    var response = await client.GetAsync(uri);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //        questionSheet = JsonConvert.DeserializeObject<QuestionSheet>(content);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            return questionSheet;
           
        }

        /// <summary>
        /// Get Download codes for all the labels and text information
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionSheet> GetDownloadCodes()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var questionSheet = new QuestionSheet();
           
            return questionSheet;

        }

        /// <summary>
        /// Get config information
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionSheet> GetConfigInfo()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var questionSheet = new QuestionSheet();
           
            return questionSheet;

        }

        /// <summary>
        /// Get Address data
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionSheet> GetAddressData()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var questionSheet = new QuestionSheet();
            
            return questionSheet;

        }
    }
}
