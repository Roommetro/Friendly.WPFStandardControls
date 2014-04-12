using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFProgressBarTest
    {
        WindowsAppFriend _app;
        WindowControl _mainWindow;

        dynamic _target;

        [TestInitialize]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            _mainWindow = WindowControl.FromZTop(_app);

            dynamic win = _app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            _target = _app.Type<ProgressBar>()();
            grid.Children.Add(_target);

            WindowsAppExpander.LoadAssemblyFromFile(_app, GetType().Assembly.Location);
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

        const double TestValue = 10;

        [TestMethod]
        public void TestEmulateChangeValue()
        {
            var progressBar = new WPFProgressBar(_target);
            progressBar.EmulateChangeValue(TestValue);
            Assert.AreEqual(TestValue, progressBar.Value);
        }

        [TestMethod]
        public void TestEmulateChangeValueAsync()
        {
            var progressBar = new WPFProgressBar(_target);
            _app[GetType(), "ChangeValueEvent"](progressBar.AppVar);
            var async = new Async();
            progressBar.EmulateChangeValue(TestValue, async);
            new NativeMessageBox(_mainWindow.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            Assert.AreEqual(TestValue, progressBar.Value);
        }

        static void ChangeValueEvent(ProgressBar progressBar)
        {
            progressBar.ValueChanged += (s, e) => MessageBox.Show("");
        }

        [TestMethod]
        public void TestMinimum()
        {
            var progressBar = new WPFProgressBar(_target);
            progressBar.Dynamic().Minimum = (double)10.5;
            Assert.AreEqual(10.5, progressBar.Minimum);
        }

        [TestMethod]
        public void TestMaximum()
        {
            var progressBar = new WPFProgressBar(_target);
            progressBar.Dynamic().Maximum = (double)10.5;
            Assert.AreEqual(10.5, progressBar.Maximum);
        }
    }
}
