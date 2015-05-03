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
    /// Provides operations on controls of type System.Windows.Controls.ListBoxItem.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxItemに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFListBoxItemCore<T> : WPFControlBase<T> where T: ListBoxItem
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
        public WPFListBoxItemCore(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Get selection state.
        /// </summary>
#else
        /// <summary>
        /// 選択状態であるかを取得。
        /// </summary>
#endif
        public bool IsSelected { get { return Getter<bool>("IsSelected"); } }

#if ENG
        /// <summary>
        /// Change Selected.
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
#endif
        public void EmulateChangeSelected(bool isSelected)
        {
            InvokeStatic(EmulateChangeSelected, isSelected);
        }

#if ENG
        /// <summary>
        /// Change Selected.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelected(bool isSelected, Async async)
        {
            InvokeStatic(EmulateChangeSelected, async, isSelected);
        }

        static void EmulateChangeSelected(T item, bool isSelected)
        {
            item.Focus();
            item.IsSelected = isSelected;
        }
    }

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListBoxItem.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxItemに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFListBoxItem : WPFListBoxItemCore<ListBoxItem>
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
        public WPFListBoxItem(AppVar appVar)
            : base(appVar) { }
    }
}
