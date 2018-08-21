using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

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
            EnsureVisible(index);
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
        /// </summary
#endif
        [UserControlDriverGetter]
        public TItemUserControlDriver SelectedItemDriver => UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(SelectedIndex));
    }
}
