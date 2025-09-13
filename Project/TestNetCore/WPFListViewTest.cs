using System;

using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly;
using Codeer.Friendly.Windows.NativeStandardControls;
using NotInstallProject;

namespace Test
{
    
    public class WPFListViewTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFListViewTest>().Init(main._grid);
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        void ResetConnection()
        {
            int id = _app.ProcessId;
            _app.Dispose();
            _app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFListViewTest>().Init(main._grid);
        }

        static WPFListViewTestControl Init(Grid grid)
        {
            WPFListViewTestControl ctrl = new WPFListViewTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [Test]
        public void TestSelectedIndex()
        {
            var listView = new WPFListView(_ctrl.listView);
            var index = listView.SelectedIndex;
            Assert.AreEqual(-1, (int)index);

            //selector.SelectedIndex = 3;
            listView.EmulateChangeSelectedIndex(3);

            index = listView.SelectedIndex;
            Assert.AreEqual(3, (int)index);
        }

        [Test]
        public void GetItemEmulateChangeSelected()
        {
            var listView = new WPFListView(_ctrl.listView);
            var item = listView.GetItem(99);
            item.EmulateChangeSelected(false);
            Assert.IsFalse(item.IsSelected);
            item.EmulateChangeSelected(true);
            Assert.IsTrue(item.IsSelected);
        }

        [Test]
        public void GetItemEmulateChangeSelectedAsync()
        {
            var listView = new WPFListView(_ctrl.listView);
            WindowControl windowControl = WindowControl.FromZTop(_app);
            var item = listView.GetItem(99);
            _app.Type(GetType()).MessageBoxEvent(item);
            var a = new Async();
            item.EmulateChangeSelected(true, a);
            Assert.IsTrue(item.IsSelected);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            a.WaitForCompletion();
        }

        [Test]
        public void SelectedItemDriverTest()
        {
            var listEx = new WPFListView<ItemControlDriver>(_ctrl.listView);
            listEx.EmulateChangeSelectedIndex(0);
            Assert.AreEqual("0", listEx.SelectedItemDriver.Text.Text);
        }

        [Test]
        public void GetItemDriverTest()
        {
            var listEx = new WPFListView<ItemControlDriver>(_ctrl.listView);
            listEx.EmulateChangeSelectedIndex(0);
            Assert.AreEqual("1", listEx.GetItemDriver(1).Text.Text);
        }

        static void MessageBoxEvent(ListViewItem item)
        {
            item.Selected += delegate
            {
                MessageBox.Show("");
            };
        }
    }
}
