using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

using RM.Friendly.WPFStandardControls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Test
{
    
    public class WPFContentControlTest
    {
        WindowsAppFriend _app;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void ContentTest()
        {
            string fullName = new WPFContentControl(_app.Type<Application>().Current.MainWindow).Content.Dynamic().GetType().FullName;
            Assert.AreEqual(typeof(Grid).FullName, fullName);
        }
    }
}
