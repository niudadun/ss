using SmartFlow.Shared.Enums;
using SmartFlow.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace SmartFlow.Shared.UIControls
{
    public class SmartFlow_Button : Button
    {
        public SmartFlow_Button(string text = null, string classId = null, string automationId = null)
        {
            Text = text;
            ClassId = classId;
            TextColor = (Color)Application.Current.Resources["Text_Color"];
            WidthRequest = 80;
            HeightRequest = 80;
            BorderRadius = 0;
            BackgroundColor = (Color)Application.Current.Resources["TextView_Bg"];
            AutomationId = automationId;
        }
    }


    public class SmartFlow_HeaderButton : ImageButton
    {
        public SmartFlow_HeaderButton(string pic = null)
        {
            WidthRequest = 80;
            HeightRequest = 80;
            BorderRadius = 0;
            Orientation = XLabs.Enums.ImageOrientation.ImageCentered;
            Source = ImageSource.FromFile(Utils.DEFAULT_PROFILE_PIC);
            BackgroundColor = (Color)Application.Current.Resources["SmartFlow_Yellow"];
            ClassId = ChapterIdentifiers.Header.ToString();
            AutomationId = Utils.AUTOMATION_ID + "HeaderImage_Button";
        }
    }



    
}
