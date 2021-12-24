using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.UserControl.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.UserControl。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.UserControl")]
    public class WPFUserControl : WPFContentControlCore<UserControl>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFUserControl(AppVar appVar)
            : base(appVar) { }
    }
}
