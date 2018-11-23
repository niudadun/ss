using SmartFlow.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartFlow.Shared.UIControls
{
    public class SmartFlow_DatePicker : DatePicker
    {
        public SmartFlow_DatePicker(string classId = null, string automationId = null)
        {
            ClassId = classId;
            Format = Utils.DATE_FORMAT;
            AutomationId = automationId;
        }
    }
}
