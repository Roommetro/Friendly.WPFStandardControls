using Codeer.Friendly;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ContentControl.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ContentControlに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFContentControl : WPFControlBase<ContentControl>
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
        public WPFContentControl(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Get Content.
        /// </summary>
#else
        /// <summary>
        /// コンテンツを取得します。
        /// </summary>
#endif
        public AppVar Content { get { return this["Content"](); } }
    }
}
