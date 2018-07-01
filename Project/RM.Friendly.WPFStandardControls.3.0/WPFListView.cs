﻿using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListView.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListViewに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.ListView")]
    public class WPFListView : WPFListBoxCore<ListView>
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
        public WPFListView(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Getitem.
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <returns>Item.</returns>
#else
        /// <summary>
        /// アイテムの取得。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>アイテム。</returns>
#endif
        public WPFListViewItem GetItem(int index)
        {
            EnsureVisible(index);
            return new WPFListViewItem(this["ItemContainerGenerator"]()["ContainerFromIndex"](index));
        }
    }
}
