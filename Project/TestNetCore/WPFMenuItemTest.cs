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
    
    public class WPFMenuItemTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFMenuItemTest>().Init(main._grid);
        }

        void ResetConnection()
        {
            int id = _app.ProcessId;
            _app.Dispose();
            _app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFMenuItemTest>().Init(main._grid);
        }

        static WPFMenuItemTestControl Init(Grid grid)
        {
            WPFMenuItemTestControl ctrl = new WPFMenuItemTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void EmulteClickTest()
        {
            WPFMenuItem item = new WPFMenuItem(_ctrl._menuItem);
            item.EmulateClick();
            Assert.IsTrue((bool)_ctrl.menuItemClicked);
        }

        [Test]
        public void EmulteClickTestAsync()
        {
            WPFMenuItem item = new WPFMenuItem(_ctrl._menuItemMessage);
            Async async = new Async();
            WindowControl windowControl = WindowControl.FromZTop(_app);
            item.EmulateClick(async);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
        }

        [Test]
        public void TextTest()
        {
            WPFMenuItem item = new WPFMenuItem(_ctrl._menuItem);
            Assert.AreEqual("MenuItem", item.Text);
        }

        [Test]
        public void GetCoreElementTest()
        {
            WPFMenuBase menu = new WPFMenuBase(_ctrl._menu);
            var item = menu.GetItem(2);
            var button = new WPFButtonBase(item.GetCoreElement(typeof(Button).FullName));
            button.EmulateClick();
            Assert.IsTrue((bool)_ctrl.menuButtonClicked);
        }

        [Test]
        public void GetCoreElementNotFoundTest()
        {
            TestUtility.TestExceptionMessage(() => {
                ResetConnection();
                WPFMenuBase menu = new WPFMenuBase(_ctrl._menu);
                var item = menu.GetItem(2);
                item.GetCoreElement("X");
            },
                "The desire Visual element was not found.",
                "指定のVisual要素は見つかりませんでした。");
        }

    }
}
