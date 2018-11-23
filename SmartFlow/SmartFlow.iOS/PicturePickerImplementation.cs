using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SmartFlow.Shared.Repository.Interfaces;
using Xamarin.Forms;
using SmartFlow.iOS;
using System.Threading.Tasks;
using System.IO;
using SmartFlow.Shared.Helpers;

[assembly: Dependency(typeof(PicturePickerImplementation))]
namespace SmartFlow.iOS
{
    /// <summary>
    /// This class is used for picture selection from ImagePicker
    /// </summary>
    public class PicturePickerImplementation : IPicturePicker
    {
        private static string TAG = "PicturePickerImplementation_iOS";

        TaskCompletionSource<Stream> taskCompletionSource;
        UIImagePickerController imagePicker;

        /// <summary>
        /// This method is used to get the image stream from the selected picture.
        /// This image is passed back to the main activity with the stream data
        /// </summary>
        /// <returns></returns>
        public Task<Stream> GetImageStreamAsync()
        {
            try
            {
                // Create and define UIImagePickerController
                imagePicker = new UIImagePickerController
                {
                    SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                    MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
                };

                // Set event handlers
                imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
                imagePicker.Canceled += OnImagePickerCancelled;

                // Present UIImagePickerController;
                UIWindow window = UIApplication.SharedApplication.KeyWindow;
                var viewController = window.RootViewController;
                viewController.PresentModalViewController(imagePicker, true);

                // Return Task object
                taskCompletionSource = new TaskCompletionSource<Stream>();
                return taskCompletionSource.Task;
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
            }
            return null;
        }

        /// <summary>
        /// Callback when image picking is finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            try
            {
                UIImage image = args.EditedImage ?? args.OriginalImage;

                if (image != null)
                {
                    // Convert UIImage to .NET Stream object
                    NSData data = image.AsJPEG(1);
                    Stream stream = data.AsStream();

                    // Set the Stream as the completion of the Task
                    taskCompletionSource.SetResult(stream);
                }
                else
                {
                    taskCompletionSource.SetResult(null);
                }
                imagePicker.DismissModalViewController(true);
            }
            catch (Exception e)
            {
                LogHandler.AddExceptionLog(TAG, "", e, true);
            }
        }

        /// <summary>
        /// Callback when image picker is cancelled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            taskCompletionSource.SetResult(null);
            imagePicker.DismissModalViewController(true);
        }

    }
}