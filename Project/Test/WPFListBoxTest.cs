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
using NotInstallProject;

namespace Test
{
    [TestClass]
    public class WPFListBoxTest
    {
        WindowsAppFriend app;

        WPFListBox listBox;

        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<ListBox>()();
            grid.Children.Add(target);
            target.ItemsSource = Enumerable.Range(0, 100).Select(i => "value " + i).ToArray();
            listBox = new WPFListBox(target);
        }

        [TestCleanup]
        public void TestCleanup() {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void SelectedIndex()
        {
            var index = listBox.SelectedIndex;
            Assert.AreEqual(-1, (int)index);

            //selector.SelectedIndex = 3;
            listBox.EmulateChangeSelectedIndex(3);

            index = listBox.SelectedIndex;
            Assert.AreEqual(3, (int)index);
        }

        static void AddSelectionEvent(ListBox list)
        {
            list.SelectionChanged += delegate
            {
                MessageBox.Show("");
            };
        }

        [TestMethod]
        public void SelectedIndexAsync()
        {
            app.Type<WPFListBoxTest>().AddSelectionEvent(listBox.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            listBox.EmulateChangeSelectedIndex(3, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            int index = listBox.SelectedIndex;
            Assert.AreEqual(3, index);
        }

        [TestMethod]
        public void EnsureVisible()
        {
            listBox.EnsureVisible(99);
            dynamic item = listBox.Dynamic().ItemContainerGenerator.ContainerFromIndex(99);
            Assert.IsFalse((bool)app.Type<object>().ReferenceEquals(null, item));
        }

        [TestMethod]
        public void GetItemEmulateChangeSelected()
        {
            var item = listBox.GetItem(99);
            item.EmulateChangeSelected(false);
            Assert.IsFalse(item.IsSelected);
            item.EmulateChangeSelected(true);
            Assert.IsTrue(item.IsSelected);
        }

        [TestMethod]
        public void GetItemEmulateChangeSelectedAsync()
        {
            WindowControl windowControl = WindowControl.FromZTop(app);
            var item = listBox.GetItem(99);
            app.Type(GetType()).MessageBoxEvent(item);
            var a = new Async();
            item.EmulateChangeSelected(true, a);
            Assert.IsTrue(item.IsSelected);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            a.WaitForCompletion();
        }

        [TestMethod]
        public void SelectedItemDriverTest()
        {
            var listEx = new WPFListBox<ItemControlDriver>(listBox.AppVar);
            listEx.EmulateChangeSelectedIndex(0);
            Assert.AreEqual("value 0", listEx.SelectedItemDriver.Text.Text);
        }

        [TestMethod]
        public void GetItemDriverTest()
        {
            var listEx = new WPFListBox<ItemControlDriver>(listBox.AppVar);
            listEx.EmulateChangeSelectedIndex(0);
            Assert.AreEqual("value 1", listEx.GetItemDriver(1).Text.Text);
        }

        static void MessageBoxEvent(ListBoxItem item)
        {
            item.Selected += delegate
            {
                MessageBox.Show("");
            };
        }
    }
}
