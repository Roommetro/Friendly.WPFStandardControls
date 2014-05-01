using System.Windows.Threading;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class InvokeUtility
    {
        internal static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrames), frame);
            Dispatcher.PushFrame(frame);
        }

        static object ExitFrames(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }
    }
}
