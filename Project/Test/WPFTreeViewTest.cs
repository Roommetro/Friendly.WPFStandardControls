using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class WPFTreeViewTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
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

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void GetItemStringTest()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem("1", "1-0", "1-0-1");
            Assert.AreEqual("1-0-1", item.Text);
        }

        [TestMethod]
        public void GetItemIntTest()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1, 0, 1);
            Assert.AreEqual("1-0-1", item.Text);
        }

        [TestMethod]
        public void GetItemStringTest2()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem("1").GetItem("1-0", "1-0-1");
            Assert.AreEqual("1-0-1", item.Text);
        }

        [TestMethod]
        public void GetItemIntTest2()
        {
            var tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1).GetItem(0, 1);
            Assert.AreEqual("1-0-1", item.Text);
        }

        [TestMethod]
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
