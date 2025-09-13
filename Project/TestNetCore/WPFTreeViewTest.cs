using System;

using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Test
{
    
    public class WPFTreeViewTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFTreeViewTest>().Init(main._grid);
        }

        void ResetConnection()
        {
            int id = _app.ProcessId;
            _app.Dispose();
            _app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFTreeViewTest>().Init(main._grid);
        }

        static WPFTreeViewTestControl Init(Grid grid)
        {
            WPFTreeViewTestControl ctrl = new WPFTreeViewTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void GetItemStringTest()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem("1", "1-0", "1-0-1");
            Assert.AreEqual("1-0-1", item.Text);
        }

        [Test]
        public void GetItemIntTest()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1, 0, 1);
            Assert.AreEqual("1-0-1", item.Text);
        }

        [Test]
        public void GetItemStringTest2()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem("1").GetItem("1-0", "1-0-1");
            Assert.AreEqual("1-0-1", item.Text);
        }

        [Test]
        public void GetItemIntTest2()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1).GetItem(0, 1);
            Assert.AreEqual("1-0-1", item.Text);
        }

        [Test]
        public void SelectedItem()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1, 0, 1);
            item.EmulateChangeSelected(true);
            Assert.AreEqual("1-0-1", tree.SelectedItem.Text);
        }

        [Test]
        public void GetItemNotFoundTest()
        {
            TestUtility.TestExceptionMessage(() =>
            {
                ResetConnection();
                WPFTreeView tree = new WPFTreeView(_ctrl._tree);
                tree.GetItem(3);
            },
                "The desire item was not found.",
                "指定のアイテムは見つかりませんでした。");
        }
    }
}
