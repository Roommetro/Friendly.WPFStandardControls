using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (!TestAssistantMode.IsCreatingMode)
            {
                EnsureVisible(index);
            }
            return new WPFListViewItem(this["ItemContainerGenerator"]()["ContainerFromIndex"](index));
        }
    }

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListView.
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">UserControlDriver of item.</typeparam>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListViewに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">アイテムのUserControlDriver</typeparam>
#endif
    public class WPFListView<TItemUserControlDriver> : WPFListView where TItemUserControlDriver : class
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
        /// UserControlDriver of selected item.
        /// </summary>
#else
        /// <summary>
        /// 選択アイテムに割当たるUserControlDriver
        /// </summary>
#endif
        public TItemUserControlDriver SelectedItemDriver
        {
            get
            {
                if (SelectedIndex == -1) return null;
                return UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(this["ItemContainerGenerator"]()["ContainerFromIndex"](SelectedIndex));
            }
        }

#if ENG
        /// <summary>
        /// Active item index.
        /// </summary>
#else
        /// <summary>
        /// アクティブな(キーボードフォーカスを持っている)アイテムのインデックスの取得
        /// </summary>
#endif
        public int ActiveItemIndex => (int)App[typeof(WPFListBox), "GetActiveIndex"](this).Core;
        public int AttentionItemIndex => (int)App[typeof(WPFListBox), "GetAttentionItemIndex"](this).Core;

#if ENG
        /// <summary>
        /// Get item's UserControlDriver.
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <returns>UserControlDriver.</returns>
#else
        /// <summary>
        /// 指定のインデックスのアイテムに割当たったUserControlDriverを取得
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>UserControlDriver</returns>
#endif
        [ItemDriverGetter(ActiveItemKeyProperty = "AttentionItemIndex")]
        public TItemUserControlDriver GetItemDriver(int index)
            => (TestAssistantMode.IsCreatingMode && index == -1) ? null : UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(index));

#if ENG
        /// <summary>
        /// Type for invoke in target
        /// </summary>
        /// <returns>Type for invoke in target.</returns>
#else
        /// <summary>
        /// 対象プロセス内部で実行する型の取得
        /// </summary>
        /// <returns>対象プロセス内部で実行する型</returns>
#endif
        protected override System.Type GetInvokeType() => typeof(WPFListView);
    }
}
