using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFControlBaseTest
    {
        WindowsAppFriend app;

        dynamic target;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            dynamic win = app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            target = app.Type<ListBox>()();
            grid.Children.Add(target);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestVisibility()
        {
            var win = app.Type(typeof(Application)).Current.MainWindow;
            var selector = new WPFSelector(app, target);
            Assert.AreEqual(Visibility.Visible, selector.Visibility);
            selector.Dynamic().Visibility = Visibility.Hidden;
            Assert.AreEqual(Visibility.Hidden, selector.Visibility);
        }

        [TestMethod]
        public void TestIsEnabled()
        {
            var win = app.Type(typeof(Application)).Current.MainWindow;
            var selector = new WPFSelector(app, target);
            Assert.IsTrue(selector.IsEnabled);
            selector.Dynamic().IsEnabled = false;
            Assert.IsFalse(selector.IsEnabled);
        }
    }
}
