using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.UIControls
{
    public class SmartFlow_StackLayout : StackLayout
    {
        public SmartFlow_StackLayout(string classId, bool isEnabled = true)
        {
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.FillAndExpand;
            ClassId = classId;
            Padding = new Thickness(20, 10, 20, 10);
            IsEnabled = isEnabled;
        }
    }

    public class SmartFlow_StackLayout_Menu : StackLayout
    {
        public SmartFlow_StackLayout_Menu()
        {
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Spacing = 1;
        }
    }

}
