using Codeer.Friendly;
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
    public class WPFTabControl : WPFSelector<TabControl>
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
