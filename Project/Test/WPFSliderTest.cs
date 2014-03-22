using System;
using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;

namespace Test
{
    [TestClass]
    public class WPFSliderTest
    {
        WindowsAppFriend app;
        WindowControl mainWindow;

        dynamic target;

        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            mainWindow = WindowControl.FromZTop(app);

            dynamic win = app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            target = app.Type<Slider>()();
            grid.Children.Add(target);

            WindowsAppExpander.LoadAssemblyFromFile(app, GetType().Assembly.Location);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        const double TestValue = 10;
        [TestMethod]
        public void TestEmulateChangeValue()
        {
            WPFSlider slider = new WPFSlider(target);
            slider.EmulateChangeValue(TestValue);
            Assert.AreEqual(TestValue, slider.Value);
        }

        [TestMethod]
        public void TestEmulateChangeValueAsync()
        {
            WPFSlider slider = new WPFSlider(target);

            app[GetType(), "ChangeValueEvent"](slider.AppVar);
            Async async = new Async();
            slider.EmulateChangeValue(TestValue, async);
            new NativeMessageBox(mainWindow.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            Assert.AreEqual(TestValue, slider.Value);
        }

        static void ChangeValueEvent(Slider slider)
        {
            slider.ValueChanged += (s, e) =>
            {
                MessageBox.Show("");
            };
        }
    }
}
