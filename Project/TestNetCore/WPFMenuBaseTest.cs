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
    
    public class WPFMenuBaseTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFMenuBaseTest>().Init(main._grid);
        }

        void ResetConnection()
        {
            int id = _app.ProcessId;
            _app.Dispose();
            _app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFMenuBaseTest>().Init(main._grid);
        }

        static WPFMenuBaseTestControl Init(Grid grid)
        {
            WPFMenuBaseTestControl ctrl = new WPFMenuBaseTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void GetMenuItemStringTest()
        {
            var menu = new WPFMenuBase(_ctrl._menu);
            var item = menu.GetItem("1", "1-0", "1-0-1");
            item.EmulateClick();
            Assert.AreEqual("1-0-1", (string)_ctrl.executeCommand);
        }

        [Test]
        public void GetMenuItemIntTest()
        {
            var menu = new WPFMenuBase(_ctrl._menu);
            var item = menu.GetItem(1, 0, 1);
            item.EmulateClick();
            Assert.AreEqual("1-0-1", (string)_ctrl.executeCommand);
        }

        [Test]
        public void GetMenuItemNotFoundTest()
        {
            TestUtility.TestExceptionMessage(() =>
            {
                ResetConnection();
                WPFMenuBase menu = new WPFMenuBase(_ctrl._menu);
                menu.GetItem(3);
            },
                "The desire item was not found.",
                "指定のアイテムは見つかりませんでした。");
        }
    }
}
