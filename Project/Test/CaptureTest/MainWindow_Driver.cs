using Codeer.Friendly.Dynamic;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Test.CaptureTest
{
    [WindowDriver(TypeFullName = "TestTargetCore.MainWindow")]
    public class MainWindow_Driver
    {
        public WindowControl Core { get; }
        public WPFTextBox _text => new WPFTextBox(Core.Dynamic()._text);

        public MainWindow_Driver(WindowControl core)
        {
            Core = core;
        }
    }

    public static class MainWindow_Driver_Extensions
    {
        [WindowDriverIdentify(TypeFullName = "TestTargetCore.MainWindow")]
        public static MainWindow_Driver Attach_MainWindow(this WindowsAppFriend app)
            => new MainWindow_Driver(app.WaitForIdentifyFromTypeFullName("TestTargetCore.MainWindow"));
    }
}