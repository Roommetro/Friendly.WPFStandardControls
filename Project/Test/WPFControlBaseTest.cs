using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.Grasp;

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
            var selector = new WPFListBox(target);
            Assert.AreEqual(Visibility.Visible, selector.Visibility);
            selector.Dynamic().Visibility = Visibility.Hidden;
            Assert.AreEqual(Visibility.Hidden, selector.Visibility);
        }

        [TestMethod]
        public void TestIsEnabled()
        {
            var selector = new WPFListBox(target);
            Assert.IsTrue(selector.IsEnabled);
            selector.Dynamic().IsEnabled = false;
            Assert.IsFalse(selector.IsEnabled);
        }

        [TestMethod]
        public void TestActivate()
        {
            var win = new WindowControl(app.Type(typeof(Application)).Current.MainWindow);
            var newWin = app.Type<Window>()();
            newWin.Show();
            var selector = new WPFListBox(target);
            selector.Activate();

            Assert.AreEqual(WindowControl.FromZTop(app).Handle, win.Handle);
            Assert.IsTrue((bool)selector.Dynamic().IsFocused);
            newWin.Close();
        }

        [TestMethod]
        public void TestSize()
        {
            var selector = new WPFListBox(target);
            Size size = selector.Dynamic().RenderSize;
            Assert.AreEqual(new System.Drawing.Size((int)size.Width, (int)size.Height), selector.Size);
        }

        [TestMethod]
        public void TestPointToScreen()
        {
            var selector = new WPFListBox(target);
            Point size = selector.Dynamic().PointToScreen(new Point());
            Assert.AreEqual(new System.Drawing.Point((int)size.X, (int)size.Y), selector.PointToScreen(new System.Drawing.Point()));
        }
    }
}
