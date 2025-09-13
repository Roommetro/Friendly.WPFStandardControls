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
using NotInstallProject;

namespace Test
{
    
    public class WPFTreeViewItemTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFTreeViewItemTest>().Init(main._grid);
        }

        void ResetConnection()
        {
            int id = _app.ProcessId;
            _app.Dispose();
            _app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFTreeViewItemTest>().Init(main._grid);
        }

        static WPFTreeViewItemTestControl Init(Grid grid)
        {
            WPFTreeViewItemTestControl ctrl = new WPFTreeViewItemTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void SelectedTest()
        {
            WPFTreeViewItem item0 = new WPFTreeViewItem(_ctrl._item0);
            item0.EmulateChangeSelected(true);
            Assert.IsTrue(item0.IsSelected);
            WPFTreeViewItem item0_0 = new WPFTreeViewItem(_ctrl._item0_0);
            item0_0.EmulateChangeSelected(true);
            Assert.IsFalse(item0.IsSelected);
            Assert.IsTrue(item0_0.IsSelected);
        }
        
        [Test]
        public void ExpandedTest()
        {
            WPFTreeViewItem item0 = new WPFTreeViewItem(_ctrl._item0);
            item0.EmulateChangeExpanded(true);
            Assert.IsTrue(item0.IsExpanded);
        }

        [Test]
        public void EmulteChangeExpandedAsyncTest()
        {
            WPFTreeViewItem item = new WPFTreeViewItem(_ctrl._item1);
            Async async = new Async();
            WindowControl windowControl = WindowControl.FromZTop(_app);
            item.EmulateChangeExpanded(true, async);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
        }

        [Test]
        public void TextTest()
        {
            WPFTreeViewItem item = new WPFTreeViewItem(_ctrl._item0);
            Assert.AreEqual("item0", item.Text);
        }

        [Test]
        public void GetCoreElementTest()
        {
            WPFTreeView tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1);
            var button = new WPFButtonBase(item.GetCoreElement(typeof(Button).FullName));
            button.EmulateClick();
            Assert.IsTrue((bool)_ctrl.treeButtonClicked);
        }

        [Test]
        public void HeaderContentTest()
        {
            WPFTreeView tree = new WPFTreeView(_ctrl._tree);
            var item = tree.GetItem(1);
            var button = new WPFButtonBase(item.HeaderContent.VisualTree().ByType<Button>().Single());
            button.EmulateClick();
            Assert.IsTrue((bool)_ctrl.treeButtonClicked);
        }

        [Test]
        public void SelectedItemDriverTest()
        {
            var tree = new WPFTreeView<ItemControlDriver>(_ctrl._tree);
            WPFTreeViewItem item0 = new WPFTreeViewItem(_ctrl._item0);
            item0.EmulateChangeSelected(true);
            Assert.AreEqual("item0", tree.SelectedItemDriver.Text.Text);
        }

        [Test]
        public void GetItemDriverTest()
        {
            var tree = new WPFTreeView<ItemControlDriver>(_ctrl._tree);
            WPFTreeViewItem item0 = new WPFTreeViewItem(_ctrl._item0);
            item0.EmulateChangeSelected(true);
            Assert.AreEqual("item0", tree.GetItemDriver("item0").Text.Text);
        }

        [Test]
        public void GetCoreElementNotFoundTest()
        {
            TestUtility.TestExceptionMessage(() => {
                ResetConnection();
                WPFTreeView tree = new WPFTreeView(_ctrl._tree);
                var item = tree.GetItem(1);
                item.GetCoreElement("X");
            },
                "The desire Visual element was not found.",
                "指定のVisual要素は見つかりませんでした。");
        }
    }
}
