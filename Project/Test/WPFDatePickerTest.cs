using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Codeer.Friendly.Windows.NativeStandardControls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly;

namespace Test
{
    [TestClass]
    public class WPFDatePickerTest : WPFTestBase<DatePicker>
    {
        DateTime TestValue = new DateTime(2000,1,14);

        [TestMethod]
        public void TestEmulateChangeSelectedDate()
        {
            var datepicker = new WPFDatePicker(Target);
            datepicker.EmulateChangeSelectedDate(TestValue);
            Assert.AreEqual(TestValue, datepicker.SelectedDate);
        }

        [TestMethod]
        public void TestEmulateChangeSelectedDateAsync()
        {
            var datepicker = new WPFDatePicker(Target);
            CallRemoteMethod("AttachChangeDateHandler", datepicker);

            datepicker.EmulateChangeSelectedDate(TestValue, new Async());
            ClickNextMessageBox();
            Assert.AreEqual(TestValue, datepicker.SelectedDate);
        }

        static void ChangeSelectedDateEvent(DatePicker datepicker)
        {
            datepicker.SelectedDateChanged += (s, e) => MessageBox.Show("");
        }

        [TestMethod]
        public void TestSelectedDate()
        {
            var datepicker = new WPFDatePicker(Target);
            var testvalue = new DateTime(1970, 5, 2);
            datepicker.Dynamic().SelectedDate = testvalue;
            Assert.AreEqual(testvalue, datepicker.SelectedDate);
        }
        static void AttachChangeDateHandler(DatePicker datepicker)
        {
            EventHandler<SelectionChangedEventArgs> handler = null;
            handler = (s, e) =>
                {
                    MessageBox.Show("");
                    datepicker.SelectedDateChanged -= handler;
                };

            datepicker.SelectedDateChanged += handler;
        }
    }
}
