using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFListViewTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<WPFListViewTest>().Init(main._grid);
        }

        [TestCleanup]
        public void TestCleanup()
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

        [TestMethod]
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
    }
}
