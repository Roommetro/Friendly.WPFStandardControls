using Codeer.Friendly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.Friendly.Dynamic;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFDatePicker : WPFControlBase4<DatePicker>
    {
        public WPFDatePicker(AppVar appVar)
            : base(appVar) { }

        public DateTime SelectedDate
        {
            get { return Getter<DateTime>("SelectedDate"); }
        }
        public void EmulateChangeSelectedDate(DateTime datetime)
        {
            InvokeStatic("EmulateChangeSelectedDate", datetime);
        }
        public void EmulateChangeSelectedDate(DateTime datetime, Async async)
        {
            InvokeStatic("EmulateChangeSelectedDate", async, datetime);
        }
        static void EmulateChangeSelectedDate(DatePicker datepicker, DateTime datetime)
        {
            datepicker.Focus();
            datepicker.SelectedDate = datetime;
        }
    }
}
