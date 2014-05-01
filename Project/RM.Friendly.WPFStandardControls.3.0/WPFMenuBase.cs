using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Primitives.MenuBase.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Primitives.MenuBaseに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFMenuBase : WPFControlBase<MenuBase>
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
        public WPFMenuBase(AppVar appVar)
            : base(appVar) { }

#if ENG        
        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="headerTexts">The array of text to the target item. </param>
        /// <returns>Item.</returns>
#else
        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="headerTexts">目的のアイテムまでのテキストの配列です。</param>
        /// <returns>アイテム。</returns>
#endif
        public WPFMenuItem GetItem(params string[] headerTexts)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetItemInTarget, Ret<MenuItem>(), headerTexts));
        }

#if ENG
        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="indices">The array of index to the target item. </param>
        /// <returns>Item.</returns>
#else
        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="indices">目的のアイテムまでの各階層でのインデックスの配列です。</param>
        /// <returns>アイテム。</returns>
#endif
        public WPFMenuItem GetItem(params int[] indices)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetItemInTarget, Ret<MenuItem>(), indices));
        }

        static MenuItem GetItemInTarget(MenuBase menu, string[] headerTexts)
        {
            return HeaderedItemsControlUtility.GetItem<MenuItem>(menu, headerTexts, ShowNextItem);
        }

        static MenuItem GetItemInTarget(MenuBase menu, int[] indices)
        {
            return HeaderedItemsControlUtility.GetItem<MenuItem>(menu, indices, ShowNextItem);
        }

        static void ShowNextItem(MenuItem item)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            invoker.Invoke();
        }
    }
}
