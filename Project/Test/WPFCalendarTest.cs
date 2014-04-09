using System;
using System.Diagnostics;
using Codeer.Friendly.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly.Dynamic;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFCalendarTest
    {
        WindowsAppFriend app;

        dynamic target;

        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));

            dynamic win = app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;

            target = app.Type<Calendar>()();
            grid.Children.Add(target);

            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        private static readonly DateTime TestDateTime = new DateTime(1982, 10, 6);

        [TestMethod]
        public void TestEmulateChangeDate()
        {
            WPFCalendar calendar = new WPFCalendar(target);
            calendar.EmulateChangeDate(TestDateTime);
            DateTime controlDateTime = calendar.SelectedDate ?? DateTime.MinValue;

            Assert.AreEqual(TestDateTime, controlDateTime);

            calendar.EmulateChangeDate(null);
            DateTime? controlDateTime2 = calendar.SelectedDate;
            Assert.AreEqual(null, controlDateTime2);
        }
    }
}
