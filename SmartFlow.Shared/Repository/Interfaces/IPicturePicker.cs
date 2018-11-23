using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartFlow.Shared.Repository.Interfaces
{
    /// <summary>
    /// Interface for picture picker
    /// </summary>
    public interface IPicturePicker
    {
        /// Method to get Image stream from selected image
        Task<Stream> GetImageStreamAsync();
    }
}
