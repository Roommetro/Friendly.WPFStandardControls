using Codeer.Friendly;
using Codeer.Friendly.Windows;
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
    public class WPFContextMenu
    {
        internal delegate void Clean();
        AppVar _target;

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
            return new WPFContextMenuItem(Target, indices);
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
            AppVar clean = Target.App.Dim();
            var indices = (int[])Target.App[GetType(), "GetIndices"](Target, headerTexts).Core;
            return new WPFContextMenuItem(Target, indices);
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
            var count = (int)Target.App[GetType(), "GetItemCount"](Target).Core;
            var items = new WPFContextMenuItem[count];
            for (int i = 0; i < count; i++) 
            {
                items[i] = new WPFContextMenuItem(Target, new int[] { i });
            }
            return items;
        }

        internal static int GetItemCount(UIElement target)
        {
            Clean cleaner = null;
            try
            {
                var menu = OpenMenu(target, out cleaner);
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

        static int[] GetIndices(UIElement target, string[] headerTexts)
        {
            Clean cleaner = null;
            try
            {
                var menu = OpenMenu(target, out cleaner);
                int[] indices = new int[headerTexts.Length];
                GetIndices(SearcherInTarget.ByType<MenuItem>(TreeUtilityInTarget.VisualTree(menu)), headerTexts, indices, 0);
                return indices;
            }
            finally
            {
                if (cleaner != null) 
                {
                    cleaner();
                }
            }
        }

        static void GetIndices(IEnumerable<MenuItem> items, string[] headerTexts, int[] indices, int index)
        {
            int i = 0;
            foreach (var e in items)
            {
                if (e.Header.ToString() == headerTexts[index])
                {
                    indices[index] = i;
                    if (index == indices.Length - 1)
                    {
                        return;
                    }

                    //次のメニューを開く
                    IInvokeProvider invoker = new MenuItemAutomationPeer(e);
                    invoker.Invoke();

                    foreach (var popup in SearcherInTarget.ByType<Popup>(TreeUtilityInTarget.VisualTree(e)))
                    {
                        GetIndices(SearcherInTarget.ByType<MenuItem>(TreeUtilityInTarget.VisualTree(popup.Child)), headerTexts, indices, index + 1);
                        return;
                    }
                    break;
                }
                i++;
            }
            throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);
        }

        internal static ContextMenu OpenMenu(UIElement target, out Clean cleaner)
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
            target.Focus();
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

            return menu;
        }
    }
}
