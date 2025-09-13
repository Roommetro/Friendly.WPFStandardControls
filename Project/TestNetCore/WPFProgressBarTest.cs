using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

using RM.Friendly.WPFStandardControls;

namespace Test
{
    
    public class WPFProgressBarTest
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
            _target = _app.Type<ProgressBar>()();
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

        [Test]
        public void TestMinimum()
        {
            var progressBar = new WPFProgressBar(_target);
            progressBar.Dynamic().Minimum = (double)10.5;
            Assert.AreEqual(10.5, progressBar.Minimum);
        }

        [Test]
        public void TestMaximum()
        {
            var progressBar = new WPFProgressBar(_target);
            progressBar.Dynamic().Maximum = (double)10.5;
            Assert.AreEqual(10.5, progressBar.Maximum);
        }
    }
}
