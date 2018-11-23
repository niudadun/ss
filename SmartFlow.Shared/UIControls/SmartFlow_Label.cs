using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.UIControls
{
    public class SmartFlow_Label : Label
    {
        public SmartFlow_Label(string text = null, string classId = null, string automationId = null)
        {
            ClassId = classId;
            Text = text;
            VerticalTextAlignment = TextAlignment.Center;
            HorizontalOptions = LayoutOptions.StartAndExpand;
            Style = (Style)Application.Current.Resources["SmallFont"];
            AutomationId = automationId;
        }
    }


    public class SmartFlow_Validation_Label:Label
    {
        public SmartFlow_Validation_Label(string classId = null, string automationId = null)
        {
            TextColor = Color.Red;
            IsVisible = false;
            ClassId = classId;
            AutomationId = automationId;
        }
    }
}
