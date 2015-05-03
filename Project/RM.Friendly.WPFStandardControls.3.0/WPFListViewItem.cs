using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Reflection;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListViewItem.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListViewItemに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFListViewItem : WPFListBoxItemCore<ListViewItem>
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
        public WPFListViewItem(AppVar appVar)
            : base(appVar) { }
    }
}
