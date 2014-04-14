using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFCalendar : WPFControlBase4<Calendar>
    {
        public WPFCalendar(AppVar appVar)
            : base(appVar) { }

        public DateTime? SelectedDate
        {
            get { return Getter<DateTime?>("SelectedDate"); }
        }

        public void EmulateChangeDate(DateTime? date)
        {
            StaticAction(EmulateChangeDate, date);
        }

        public void EmulateChangeDate(DateTime? date, Async async)
        {
            StaticAction(EmulateChangeDate, async, date);
        }

        private static void EmulateChangeDate(Calendar calendar, DateTime? date)
        {
            calendar.SelectedDate = date;
        }
    }
}
