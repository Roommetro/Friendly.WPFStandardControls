using Codeer.Friendly;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{


#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxに対応した操作を提供します。
    /// </summary>
#endif
    public partial class WPFListBox : WPFSelector<ListBox>
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
        public WPFListBox(AppVar appVar)
            : base(appVar) { }
    }
}
