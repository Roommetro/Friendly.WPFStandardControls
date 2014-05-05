using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using System.Diagnostics;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using System.Linq;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Test
{
    [TestClass]
    public class WPFLTabControlTest
    {
        WindowsAppFriend app;

        WPFTabControl tabControl;

        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<TabControl>()();
            grid.Children.Add(target);
            target.ItemsSource = Enumerable.Range(0, 10).Select(i => "value " + i).ToArray();
            tabControl = new WPFTabControl(target);
        }

        [TestCleanup]
        public void TestCleanup() {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void SelectedIndex()
        {
            var index = tabControl.SelectedIndex;
            Assert.AreEqual(-1, (int)index);

            //selector.SelectedIndex = 3;
            tabControl.EmulateChangeSelectedIndex(3);

            index = tabControl.SelectedIndex;
            Assert.AreEqual(3, (int)index);
        }
    }
}
