using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ContextMenu.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ContextMenuに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.ContextMenu", DriverMappingEnabled = false)]
    public class WPFContextMenu : IAppVarOwner
    {
        internal delegate void Clean();
        AppVar _target;

#if ENG
        /// <summary>
        /// Open the menu by key?
        /// </summary>
#else
        /// <summary>
        /// キーでメニューを開くか
        /// </summary>
#endif
        public bool OpenByKey { get; set; } = true;

#if ENG
        /// <summary>
        /// Focus element at opening.
        /// </summary>
#else
        /// <summary>
        /// メニューを開くときにフォーカスがある要素。
        /// </summary>
#endif
        public AppVar Target
        {
            get { return _target; }
            set
            {
                _target = value;
                WPFStandardControls_3.Injection((WindowsAppFriend)_target.App);
            }
        }

#if ENG
        /// <summary>
        /// ContextMenu.
        /// </summary>
#else
        /// <summary>
        /// コンテキストメニュー
        /// </summary>
#endif
        public AppVar AppVar => Target.App[typeof(WPFContextMenu), "GetContextMenu"](Target);

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
        public WPFContextMenuItem GetItem(params int[] indices)
        {
            return new WPFContextMenuItem(Target, OpenByKey, ToObjectArray(indices));
        }

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
        public WPFContextMenuItem GetItem(params string[] headerTexts)
        {
            return new WPFContextMenuItem(Target, OpenByKey, ToObjectArray(headerTexts));
        }

        static object[] ToObjectArray<T>(T[] src)
        {
            var dst = new object[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dst[i] = src[i];
            }
            return dst;
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
            var count = (int)Target.App[typeof(WPFContextMenu), "GetItemCount"](Target, OpenByKey).Core;
            var items = new WPFContextMenuItem[count];
            for (int i = 0; i < count; i++) 
            {
                items[i] = new WPFContextMenuItem(Target, OpenByKey, new object[] { i });
            }
            return items;
        }

        internal static int GetItemCount(UIElement target, bool openByKey)
        {
            Clean cleaner = null;
            try
            {
                var menu = OpenMenu(target, openByKey, out cleaner);
                int count = 0;
                foreach (var e in SearcherInTarget.ByType<MenuItem>(TreeUtilityInTarget.VisualTree(menu))) 
                {
                    count++;
                }
                return count;
            }
            finally
            {
                if (cleaner != null)
                {
                    cleaner();
                }
            }
        }

        static ContextMenu GetContextMenu(UIElement target)
        {
            var tree = TreeUtilityInTarget.VisualTree(target, TreeRunDirection.Ancestors);
            foreach (var e in tree)
            {
                var f = e as FrameworkElement;
                if (f != null && f.ContextMenu != null)
                {
                    return f.ContextMenu;
                }
            }
            throw new NotSupportedException();
        }

        internal static ContextMenu OpenMenu(UIElement target, bool openByKey, out Clean cleaner)
        {
            cleaner = null;

            FrameworkElement owner = null;
            var tree = TreeUtilityInTarget.VisualTree(target, TreeRunDirection.Ancestors);
            foreach (var e in tree)
            {
                var f = e as FrameworkElement;
                if (f != null && f.ContextMenu != null)
                {
                    owner = f;
                    break;
                }
            }
            if (owner == null)
            {
                throw new NotSupportedException();
            }
            var menu = owner.ContextMenu;

            if (openByKey)
            {
                if (!HasFocus(target))
                {
                    target.Focus();
                }
                SendInputEx.SendKey(System.Windows.Forms.Keys.Apps);
            }
            else
            {
                int count = menu.CommandBindings.Count;

                foreach (var e in TreeUtilityInTarget.VisualTree(target, TreeRunDirection.Ancestors))
                {
                    var u = e as UIElement;
                    if (u != null && u.CommandBindings != null)
                    {
                        foreach (CommandBinding command in u.CommandBindings)
                        {
                            menu.CommandBindings.Add(command);
                        }
                    }
                }
                if (!HasFocus(target))
                {
                    target.Focus();
                }
                menu.IsOpen = true;
                InvokeUtility.DoEvents();

                //数を元に戻す
                cleaner = () =>
                {
                    while (count < menu.CommandBindings.Count)
                    {
                        menu.CommandBindings.RemoveAt(menu.CommandBindings.Count - 1);
                    }
                    menu.IsOpen = false;
                };
            }

            return menu;
        }

        static bool HasFocus(Visual visual)
        {
            foreach (var e in TreeUtilityInTarget.VisualTree(visual))
            {
                var uielement = e as UIElement;
                if (uielement == null) continue;
                if (uielement.IsFocused) return true;
            }
            return false;
        }
    }
}
