using System;

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
    
    public class WPFToggleButtonTest
    {
        WindowsAppFriend _app;
        WPFToggleButton _toggle;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            dynamic checkBox = _app.Type<CheckBox>()();
            main._grid.Children.Add(checkBox);
            _toggle = new WPFToggleButton(checkBox);
        }

        static void AddToggleEvent(ToggleButton toggle)
        {
            toggle.Checked += delegate
            {
                MessageBox.Show("");
            };
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void TestIsThreeState()
        {
            _toggle.Dynamic().IsThreeState = true;
            Assert.IsTrue(_toggle.IsThreeState);
        }

        [Test]
        public void TestEmulateCheck()
        {
            _toggle.EmulateCheck(true);
            Assert.IsTrue((bool)_toggle.IsChecked);
        }


        [Test]
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
