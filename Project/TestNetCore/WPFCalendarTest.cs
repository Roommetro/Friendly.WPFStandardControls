using System;
using System.Diagnostics;
using Codeer.Friendly.Windows;

using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly.Dynamic;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly;

namespace Test
{
    
    public class WPFCalendarTest : WPFTestBase<Calendar>
    {
        private static readonly DateTime TestDateTime = new DateTime(1982, 10, 6);

        [Test]
        public void TestEmulateChangeDate()
        {
            WPFCalendar calendar = new WPFCalendar(Target);

            TestDateTimeSet(calendar, TestDateTime);

            TestDateTimeSet(calendar, null);
        }

        private static void TestDateTimeSet(WPFCalendar calendar, DateTime? value)
        {
            calendar.EmulateChangeDate(value);
            DateTime? controlDateTime = calendar.SelectedDate;

            Assert.AreEqual(value, controlDateTime);
        }

        [Test]
        public void TestEmulateChangeDateAsync()
        {
            WPFCalendar calendar = new WPFCalendar(Target);

            TestDateTimeSetAsync(calendar, TestDateTime);

            TestDateTimeSetAsync(calendar, null);
        }

        private void TestDateTimeSetAsync(WPFCalendar calendar, DateTime? value)
        {
            CallRemoteMethod("AttachChangeDateHandler", calendar);
            calendar.EmulateChangeDate(value, new Async());

            ClickNextMessageBox();

            Assert.AreEqual(calendar.SelectedDate, value);
        }

        static void AttachChangeDateHandler(Calendar calendar)
        {
            EventHandler<SelectionChangedEventArgs> handler = null;
            handler = (s, e) =>
                {
                    MessageBox.Show("");
                    calendar.SelectedDatesChanged -= handler;
                };

            calendar.SelectedDatesChanged += handler;
        }
    }
}
