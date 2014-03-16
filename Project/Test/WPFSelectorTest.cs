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

namespace Test
{
    [TestClass]
    public class WPFSelectorTest
    {
        WindowsAppFriend app;

        dynamic target;

        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            target = app.Type<ListBox>()();
            grid.Children.Add(target);
            target.ItemsSource = Enumerable.Range(0, 100).Select(i => "value " + i).ToArray();
        }

        [TestCleanup]
        public void TestCleanup() {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void SelectedIndexの値を取得して設定できる() {
            var win = app.Type(typeof(Application)).Current.MainWindow;
            var selector = new WPFSelector(app, target);
            var index = selector.SelectedIndex;
            Assert.AreEqual(-1, (int)index);

            selector.SelectedIndex = 3;

            index = selector.SelectedIndex;
            Assert.AreEqual(3, (int)index);
        }
    }
}
