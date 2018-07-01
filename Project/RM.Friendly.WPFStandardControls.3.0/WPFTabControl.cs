using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.TabControl.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.TabControlに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.TabControl")]
    public class WPFTabControl : WPFSelectorCore<TabControl>
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
        public WPFTabControl(AppVar appVar)
            : base(appVar) { }
    }
}
