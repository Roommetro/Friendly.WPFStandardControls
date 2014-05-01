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
        /// メニューアイテムを取得します。
        /// </summary>
        /// <param name="headerTexts">目的のメニューアイテムまでのヘッダテキストの配列です。</param>
        /// <returns>メニューアイテム。</returns>
#else
        /// <summary>
        /// Get MenuItem.
        /// </summary>
        /// <param name="headerTexts">The array of header text to the target menu item. </param>
        /// <returns>MenuItem.</returns>
#endif
        public WPFMenuItem GetMenuItem(params string[] headerTexts)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetMenuItemInTarget, Ret<MenuItem>(), headerTexts));
        }

#if ENG
        /// <summary>
        /// Get MenuItem.
        /// </summary>
        /// <param name="indices">The array of index to the target menu item. </param>
        /// <returns>Get MenuItem.</returns>
#else
        /// <summary>
        /// メニューアイテムを取得します。
        /// </summary>
        /// <param name="indices">目的のメニューアイテムまでのメニューインデックスの配列です。</param>
        /// <returns>メニューアイテム。</returns>
#endif
        public WPFMenuItem GetMenuItem(params int[] indices)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetMenuItemInTarget, Ret<MenuItem>(), indices));
        }

        static MenuItem GetMenuItemInTarget(MenuBase menu, string[] headerTexts)
        {
            GetMenuItemDelegate<string> getMenuItem = (visual, headerText) =>
            {
                IsMatchDelegate isMatch = (item) => (item.Header.ToString() == headerText);
                NextDelegate next = null;
                next = (v) => GetMenuItem(v, isMatch, next);
                return GetMenuItem(visual, isMatch, next);
            };
            return GetMenuItemInTarget(menu, headerTexts, getMenuItem);
        }

        static MenuItem GetMenuItemInTarget(MenuBase menu, int[] indices)
        {
            GetMenuItemDelegate<int> getMenuItem = (visual, index) =>
            {
                int currentIndex = 0;
                IsMatchDelegate isMatch = (v) => (currentIndex++ == index);
                NextDelegate next = null;
                next = (v) => GetMenuItem(v, isMatch, next);
                return GetMenuItem(visual, isMatch, next);
            };
            return GetMenuItemInTarget(menu, indices, getMenuItem);
        }

        delegate MenuItem GetMenuItemDelegate<T>(Visual v, T param);
        delegate bool IsMatchDelegate(MenuItem v);
        delegate MenuItem NextDelegate(Visual v);

        static MenuItem GetMenuItemInTarget<T>(MenuBase menu, T[] indices, GetMenuItemDelegate<T> getMenuItem)
        {
            Visual v = menu;
            for (int i = 0; i < indices.Length; i++)
            {
                var item = getMenuItem(v, indices[i]);
                if (item == null)
                {
                    throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundMenuItem);
                }
                if (i == indices.Length - 1)
                {
                    return item;
                }
                IInvokeProvider invoker = new MenuItemAutomationPeer(item);
                invoker.Invoke();
                v = item;
            }
            return null;
        }

        static MenuItem GetMenuItem(Visual visual, IsMatchDelegate isMatch, NextDelegate next)
        {
            foreach (var element in VisualTreeUtility.GetChildren(visual))
            {
                Visual o = element;
                var menuItem = o as MenuItem;
                if (menuItem != null)
                {
                    if (isMatch(menuItem))
                    {
                        return menuItem;
                    }
                }
                else
                {
                    var popup = o as Popup;
                    if (popup != null)
                    {
                        o = popup.Child as Visual;
                    }
                    var nextMenuItem = next(o);
                    if (nextMenuItem != null)
                    {
                        return nextMenuItem;
                    }
                }
            }
            return null;
        }
    }
}
