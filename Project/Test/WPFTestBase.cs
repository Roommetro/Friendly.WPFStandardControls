using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Test
{
    public class WPFTestBase<T> where T : Control
    {
        WindowsAppFriend _app;
        WindowControl _mainWindow;

        dynamic _target;

        protected dynamic Target
        {
            get { return _target; }
        }

        [TestInitialize]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            _mainWindow = WindowControl.FromZTop(_app);

            dynamic win = _app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            _target = _app.Type<T>()();
            grid.Children.Add(_target);

            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_app != null)
            {
                _app.Dispose();
                Process process = Process.GetProcessById(_app.ProcessId);
                process.CloseMainWindow();
                _app = null;
            }
        }

        protected void CallRemoteMethod(string methodName, WPFControlBase<T> control)
        {
            _app[GetType(), methodName](control.AppVar);
        }

        protected void ClickNextMessageBox()
        {
            new NativeMessageBox(_mainWindow.WaitForNextModal()).EmulateButtonClick("OK");
        }

    }
}
