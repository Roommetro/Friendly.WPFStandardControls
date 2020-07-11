using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFListBoxCore<T> : WPFSelectorCore<T>
        where T : ListBox
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
        public WPFListBoxCore(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// It is made for the item of the appointed index to be in a visible state. 
        /// A visible state is in the state to which creation of ListBoxItem was attained on VisualTree. 
        /// </summary>
        /// <param name="index">Item index.</param>
#else
        /// <summary>
        /// 指定のインデックスのアイテムが可視状態になるようにします。
        /// 可視状態とはListBoxItemがVisualTree上で作成可能になった状態です。
        /// </summary>
        /// <param name="index">インデックス。</param>
#endif
        virtual public void EnsureVisible(int index)
        {
            InvokeStatic(EnsureVisible, index);
        }

        static void EnsureVisible(ListBox listBox, int index)
        {
            listBox.Focus();
            listBox.ScrollIntoView(listBox.Items[index]);
            listBox.UpdateLayout();
            if (listBox.ItemContainerGenerator.ContainerFromIndex(index) == null)
            {
                ListBoxAutomationPeer peer = new ListBoxAutomationPeer(listBox);
                var scroll = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
                scroll.SetScrollPercent(scroll.HorizontalScrollPercent, 0);
                listBox.UpdateLayout();
                while (listBox.ItemContainerGenerator.ContainerFromIndex(index) == null)
                {
                    scroll.Scroll(ScrollAmount.NoAmount, ScrollAmount.LargeIncrement);
                    listBox.UpdateLayout();
                }
            }
        }

        static int GetActiveIndex(ListBox list)
        {
            DependencyObject focusedElement = null;
            if (list.IsKeyboardFocusWithin)
            {
                focusedElement = Keyboard.FocusedElement as DependencyObject;
            }
            else if (list.IsMouseCaptureWithin)
            {
                focusedElement = Mouse.Captured as DependencyObject;
            }
            if (focusedElement == null) return -1;

            var focusedVisual = (List<DependencyObject>)TreeUtilityInTarget.VisualTree(focusedElement, TreeRunDirection.Ancestors);
            for (var i = 0; i < focusedVisual.Count; i++)
            {
                var item = focusedVisual[i] as ListBoxItem;
                if (item == null) continue;

                i++;
                for (; i < focusedVisual.Count; i++)
                {
                    var findedListBox = focusedVisual[i] as ListBox;
                    if (findedListBox == null) continue;

                    //ListBox in ListBox
                    if (findedListBox != list) break;

                    return list.ItemContainerGenerator.IndexFromContainer(item);
                }
            }
            return -1;
        }
    }

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.ListBox")]
    public class WPFListBox : WPFListBoxCore<ListBox>
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
        public WPFListBox(AppVar appVar)
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
        public WPFListBoxItem GetItem(int index)
        {
            if (!TestAssistantMode.IsCreatingMode)
            {
                EnsureVisible(index);
            }
            return new WPFListBoxItem(this["ItemContainerGenerator"]()["ContainerFromIndex"](index));
        }
    }

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ListBox.
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">UserControlDriver of item.</typeparam>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ListBoxに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">アイテムのUserControlDriver</typeparam>
#endif
    public class WPFListBox<TItemUserControlDriver> : WPFListBox where TItemUserControlDriver : class
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
        public WPFListBox(AppVar appVar) : base(appVar) { }

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
        [UserControlDriverGetter(ActiveItemKeyProperty = "ActiveItemIndex")]
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
        protected override Type GetInvokeType() => typeof(WPFListBox);
    }
}
