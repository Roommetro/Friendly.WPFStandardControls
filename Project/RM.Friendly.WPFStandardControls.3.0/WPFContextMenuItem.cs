using Codeer.Friendly;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of System.Windows.Controls.ContextMenu's item.
    /// </summary>
#else
    /// <summary>
    /// System.Windows.Controls.ContextMenuのアイテムに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFContextMenuItem
    {
        AppVar _target;
        object[] _indices;
        bool _openByKey;
#if ENG
        /// <summary>
        /// Returns the control's check state.
        /// </summary>
#else
        /// <summary>
        /// チェック状態を取得します。
        /// </summary>
#endif
        public bool IsChecked { get { using (var item = GetItem()) return item.Item.IsChecked; } }

#if ENG
        /// <summary>
        /// Returns that item is checkable.
        /// </summary>
#else
        /// <summary>
        /// チェック可能であるかを取得します。
        /// </summary>
#endif
        public bool IsCheckable { get { using (var item = GetItem()) return item.Item.IsCheckable; } }

#if ENG
        /// <summary>
        /// Get item text.
        /// </summary>
#else
        /// <summary>
        /// アイテムのテキストを取得します。
        /// </summary>
#endif
        public string Text { get { using (var item = GetItem()) return item.Item.Text; } }

#if ENG
        /// <summary>
        /// Returns true if the item is set to visible.
        /// </summary>
#else
        /// <summary>
        /// 表示/非表示を取得します。
        /// </summary>
#endif
        public Visibility Visibility { get { using (var item = GetItem()) return item.Item.Visibility; } }

#if ENG
        /// <summary>
        /// Returns true if the control is enabled.
        /// </summary>
#else
        /// <summary>
        /// 活性/非活性を取得します。
        /// </summary>
#endif
        public bool IsEnabled { get { using (var item = GetItem()) return item.Item.IsEnabled; } }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target">Focus element at opening.</param>
        /// <param name="indices">The array of index to the target item. </param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="target">コンテキストメニューを開くときにフォーカスの当たっている要素。</param>
        /// <param name="indices">目的のアイテムまでの各階層でのインデックスの配列です。</param>
#endif
        internal WPFContextMenuItem(AppVar target, bool openByKey, object[] indices)
        {
            _target = target;
            _indices = indices;
            _openByKey = openByKey;
        }

#if ENG
        /// <summary>
        /// Get items.
        /// </summary>
        /// <returns>All items.</returns>
#else
        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <returns>アイテム。</returns>
#endif
        public WPFContextMenuItem[] GetItems()
        {
            
            using (var item = GetItem())
            {
                var count = (int)item.Item.App[typeof(WPFContextMenuItem), "GetItemCount"](item.Item).Core;
                var items = new WPFContextMenuItem[count];
                for (int i = 0; i < count; i++)
                {
                    var next = new List<object>(_indices);
                    next.Add(i);
                    items[i] = new WPFContextMenuItem(_target, _openByKey, next.ToArray());
                }
                return items;
            }
        }

        static int GetItemCount(MenuItem item)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            invoker.Invoke();
            foreach (var p in SearcherInTarget.ByType<Popup>(TreeUtilityInTarget.VisualTree(item)))
            {
                int count = 0;
                foreach (var e in SearcherInTarget.ByType<MenuItem>(TreeUtilityInTarget.VisualTree(p.Child)))
                {
                    count++;
                }
                return count;
            }
            return 0;
        }

#if ENG
        /// <summary>
        /// Performs a click.
        /// </summary>
#else
        /// <summary>
        /// クリックです。
        /// </summary>
#endif
        public void EmulateClick()
        {
            var item = GetItem();
            _target.App[typeof(InvokeUtility), "DoEvents"]();
            _target.App[typeof(WPFContextMenuItem), "EmulateClick"](item.Item, item.Clean);
        }

#if ENG
        /// <summary>
        /// Performs a click.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// クリックです。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateClick(Async async)
        {
            var item = GetItem();
            _target.App[typeof(WPFContextMenuItem), "EmulateClick", async](item.Item, item.Clean);
        }

        static void EmulateClick(MenuItem item, WPFContextMenu.Clean clean)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            item.Focus();
            invoker.Invoke();
            InvokeUtility.DoEvents();
            if (clean != null) clean();
        }

        class DynamicMenuItem : IDisposable
        {
            internal WPFMenuItem Item { get; set; }
            internal AppVar Clean { get; set; }
            public void Dispose()
            {
                if (Clean != null && !Clean.IsNull)
                {
                    Clean["Invoke"]();
                    Clean = null;
                }
            }
        }

        DynamicMenuItem GetItem()
        {
            AppVar clean = _target.App.Dim();
            var item = _target.App[typeof(WPFContextMenuItem), "GetItemInTarget"](_target, _openByKey, _indices, clean);
            return new DynamicMenuItem() { Item = new WPFMenuItem(item), Clean = clean };
        }

        static MenuItem GetItemInTarget(UIElement target, bool openByKey, object[] indices, out WPFContextMenu.Clean cleaner)
        {
            var menu = WPFContextMenu.OpenMenu(target, openByKey, out cleaner);
            var item = HeaderedItemsControlUtility.GetItem<MenuItem>(menu, indices, ShowNextItem);
            return item;
        }

        static void ShowNextItem(MenuItem item)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            invoker.Invoke();
        }
    }
}
