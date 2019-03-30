using Codeer.Friendly.Dynamic;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Test.CaptureTest
{
    [WindowDriver(TypeFullName = "WPFMenu.MainWindow")]
    public class MainWindow_Driver
    {
        public WindowControl Core { get; }
        public WPFMenuBase Menu => new WPFMenuBase(Core.LogicalTree().ByType("System.Windows.Controls.Menu").Single());

        public MainWindow_Driver(WindowControl core)
        {
            Core = core;
        }
    }

    public static class MainWindow_Driver_Extensions
    {
        [WindowDriverIdentify(TypeFullName = "WPFMenu.MainWindow")]
        public static MainWindow_Driver Attach_MainWindow(this WindowsAppFriend app)
            => new MainWindow_Driver(app.WaitForIdentifyFromTypeFullName("WPFMenu.MainWindow"));
    }
}