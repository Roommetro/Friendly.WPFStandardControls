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
    //@@@メニューベースにするか？


#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Menu.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Menuに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFMenu : WPFControlBase<Menu>
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
        public WPFMenu(AppVar appVar)
            : base(appVar) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerTexts"></param>
        /// <returns></returns>
        public WPFMenuItem GetMenuItem(params string[] headerTexts)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetMenuItemCore, Ret<MenuItem>(), headerTexts));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        public WPFMenuItem GetMenuItem(params int[] indices)
        {
            return new WPFMenuItem(InvokeStaticRetAppVar(GetMenuItemCore, Ret<MenuItem>(), indices));
        }

        static MenuItem GetMenuItemCore(Menu menu, string[] headerTexts)
        {
            Visual v = menu;
            for (int i = 0; i < headerTexts.Length; i++ )
            {
                var item = GetMenuItem(v, headerTexts[i]);
                if (item == null)
                {
                    throw new NotSupportedException();//見つからなかった。
                }
                if (i == headerTexts.Length - 1)
                {
                    return item;
                }
                IInvokeProvider invoker = new MenuItemAutomationPeer(item);
                invoker.Invoke();
                v = item;
            }
            return null;
        }

        static MenuItem GetMenuItem(Visual visual, string headerText)
        {
            int count = VisualTreeHelper.GetChildrenCount(visual);
            for (int i = 0; i < count; i++)
            {
                object o = VisualTreeHelper.GetChild(visual, i);
                var menuItem = o as MenuItem;
                if (menuItem != null)
                {
                    if (menuItem.Header.ToString() == headerText)
                    {
                        return menuItem;
                    }
                    continue;
                }
                else
                {
                    var popup = o as Popup;
                    if (popup != null)
                    {
                        o = popup.Child;
                    }
                    var next = o as Visual;
                    if (next != null)
                    {
                        var nextMenuItem = GetMenuItem(next, headerText);
                        if (nextMenuItem != null)
                        {
                            return nextMenuItem;
                        }
                    }
                }
            }
            return null;
        }


        static MenuItem GetMenuItemCore(Menu menu, int[] indices)
        {
            Visual v = menu;
            for (int i = 0; i < indices.Length; i++)
            {
                int currentIndex = 0;
                var item = GetMenuItem(v, ref currentIndex, indices[i]);
                if (item == null)
                {
                    throw new NotSupportedException();//見つからなかった。
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

        static MenuItem GetMenuItem(Visual visual, ref int currentIndex, int index)
        {
            int count = VisualTreeHelper.GetChildrenCount(visual);
            for (int i = 0; i < count; i++)
            {
                object o = VisualTreeHelper.GetChild(visual, i);
                var menuItem = o as MenuItem;
                if (menuItem != null)
                {
                    if (currentIndex == index)
                    {
                        return menuItem;
                    }
                    currentIndex++;
                }
                else
                {
                    var popup = o as Popup;
                    if (popup != null)
                    {
                        o = popup.Child;
                    }
                    var next = o as Visual;
                    if (next != null)
                    {
                        var nextMenuItem = GetMenuItem(next, ref currentIndex, index);
                        if (nextMenuItem != null)
                        {
                            return nextMenuItem;
                        }
                    }
                }
            }
            return null;
        }
    }
}
