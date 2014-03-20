using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls.Primitives;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Test
{
    [TestClass]
    public class WPFToggleButtonTest
    {
        WindowsAppFriend _app;
        WPFToggleButton _toggle;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            dynamic checkBox = _app.Type<CheckBox>()();
            main._grid.Children.Add(checkBox);
            _toggle = new WPFToggleButton(_app, checkBox);
        }

        static void AddToggleEvent(ToggleButton toggle)
        {
            toggle.Checked += delegate
            {
                MessageBox.Show("");
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestIsThreeState()
        {
            _toggle.Dynamic().IsThreeState = true;
            Assert.IsTrue(_toggle.IsThreeState);
        }

        [TestMethod]
        public void TestEmulateCheck()
        {
            _toggle.EmulateCheck(true);
            Assert.IsTrue((bool)_toggle.IsChecked);
        }


        [TestMethod]
        public void TestEmulateCheckAsync()
        {
            _app.Type<WPFToggleButtonTest>().AddToggleEvent(_toggle.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(_app);
            _toggle.EmulateCheck(true, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            Assert.IsTrue((bool)_toggle.IsChecked);
        }
    }
}
