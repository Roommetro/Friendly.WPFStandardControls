using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;

using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;

namespace Test
{
    
    public class WPFSliderTest
    {
        WindowsAppFriend _app;
        WindowControl _mainWindow;

        dynamic _target;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            _mainWindow = WindowControl.FromZTop(_app);

            dynamic win = _app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            _target = _app.Type<Slider>()();
            grid.Children.Add(_target);

            WindowsAppExpander.LoadAssemblyFromFile(_app, GetType().Assembly.Location);

        }

        [TearDown]
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

        const double TestValue = 10;
        [Test]
        public void TestEmulateChangeValue()
        {
            var slider = new WPFSlider(_target);
            slider.EmulateChangeValue(TestValue);
            Assert.AreEqual(TestValue, slider.Value);
        }

        [Test]
        public void TestEmulateChangeValueAsync()
        {
            var slider = new WPFSlider(_target);

            _app[GetType(), "ChangeValueEvent"](slider.AppVar);
            var async = new Async();
            slider.EmulateChangeValue(TestValue, async);
            new NativeMessageBox(_mainWindow.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            Assert.AreEqual(TestValue, slider.Value);
        }

        static void ChangeValueEvent(Slider slider)
        {
            slider.ValueChanged += (s, e) => MessageBox.Show("");
        }

        [Test]
        public void TestMinimum()
        {
            var slider = new WPFSlider(_target);
            slider.Dynamic().Minimum = (double)10.5;
            Assert.AreEqual(10.5, slider.Minimum);
        }

        [Test]
        public void TestMaximum()
        {
            var slider = new WPFSlider(_target);
            slider.Dynamic().Maximum = (double)10.5;
            Assert.AreEqual(10.5, slider.Maximum);
        }
    }
}
