using System.Windows.Threading;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// InvokeUtility
    /// </summary>
    public static class InvokeUtility
    {
        /// <summary>
        /// DoEvents
        /// </summary>
        public static void DoEvents()
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
