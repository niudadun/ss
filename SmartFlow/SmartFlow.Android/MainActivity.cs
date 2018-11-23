using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SmartFlow.Shared;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.Permissions;
using SmartFlow.Shared.Helpers;

namespace SmartFlow.Droid
{
    /// <summary>
    /// Main Activity of Android
    /// This will initiate the entry point, app configuration and initialisations
    /// </summary>
    [Activity(Label = "EDE", Icon = "@drawable/ica_logo", Theme = "@style/splashscreen", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static string TAG = "MainActivity";

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;
		//  RequestCode for BioSDK MRZ capture
        public static readonly int PickBioSDK = 2000;

        /// <summary>
        /// First method to start for the activity
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {
            ImageCircleRenderer.Init();
            Xamarin.Forms.Init();
            CarouselViewRenderer.Init();
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;
            //base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

			// We have to download the licence for the biosdk, so when app is initiated, 
			// we are doing that, so that while actual MRZ capture, app can directly go to MRZ screen
			// rather than waiting for the licence to load.
            SendBroadcast(new Intent("filter.fetchBiSDKLicence"));
        }

        /// <summary>
        /// Setter Getter method for Image Picker
        /// </summary>
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        /// <summary>
        /// This method is called when some result/data is passed back from child activity to this activity
        /// </summary>
        /// <param name="requestCode">Code to determine which child activity result has been passed back</param>
        /// <param name="resultCode">Code to check what is the result status (OK, CANCELLED)</param>
        /// <param name="intent">Data information which is passed back</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    try
                    {
                        Android.Net.Uri uri = intent.Data;
                        Stream stream = ContentResolver.OpenInputStream(uri);

                        // Set the Stream as the completion of the Task
                        PickImageTaskCompletionSource.SetResult(stream);
                    }
                    catch (Exception e)
                    {
                        LogHandler.AddExceptionLog(TAG, "", e, true);
                    }
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
            else if (requestCode == PickBioSDK)
            {
			// MRZ data return from BioSDK. Saving it in prefs to be used later
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    try
                    {
                        String dob = intent.GetStringExtra("DOB");
                        String expiry = intent.GetStringExtra("PEX");
                        String name = intent.GetStringExtra("Name");
                        String passportNumber = intent.GetStringExtra("Number");
                        byte[] bitmapImage = intent.GetByteArrayExtra("Image");

                        var str = Convert.ToBase64String(bitmapImage);
                        
                        LogHandler.AddLog("SMARTDOCAPP", "MRX INFO DOB :     " + dob);

                        LogHandler.AddLog("SMARTDOCAPP", "MRX INFO EXPIRY :     " + expiry);

                        LogHandler.AddLog("SMARTDOCAPP", "MRX INFO NAME :    " + name);

                        LogHandler.AddLog("SMARTDOCAPP", "MRX INFO NUMBER :    " + passportNumber);
                        LogHandler.AddLog("SMARTDOCAPP", "MRX INFO Image :    " + str);

                        Settings.AddOrUpdateValue(Utils.PREF_KEY_BIOSDK_DOB, dob);
                        Settings.AddOrUpdateValue(Utils.PREF_KEY_BIOSDK_EXPIRY, expiry);
                        Settings.AddOrUpdateValue(Utils.PREF_KEY_BIOSDK_NAME, name);
                        Settings.AddOrUpdateValue(Utils.PREF_KEY_BIOSDK_PASSPORT_NUMBER, passportNumber);
                        Settings.AddOrUpdateValue(Utils.PREF_KEY_BIOSDK_IMAGE, str);
                        

                        PickImageTaskCompletionSource.SetResult(null);
                    }
                    catch (Exception e)
                    {
                        LogHandler.AddExceptionLog(TAG, "", e, true);
                    }
                }
            }
        }

        /// <summary>
        /// Response callback for Permissions dialog
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="permissions"></param>
        /// <param name="grantResults"></param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

