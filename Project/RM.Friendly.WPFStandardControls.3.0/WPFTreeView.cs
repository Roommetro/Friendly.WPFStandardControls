using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.WPFTreeView.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.WPFTreeViewに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.TreeView")]
    public class WPFTreeView : WPFControlBase<TreeView>
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
        public WPFTreeView(AppVar appVar)
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
        public WPFTreeViewItem GetItem(params string[] headerTexts)
        {
            return new WPFTreeViewItem(InvokeStaticRetAppVar(GetItemInTarget, Ret<TreeViewItem>(), headerTexts));
        }

#if ENG
        /// <summary>
        /// Returns the number of items.
        /// </summary>
#else
        /// <summary>
        /// アイテム数を取得します。
        /// </summary>
#endif
        public int ItemCount
        {
            get { return (int)(this["Items"]()["Count"]().Core); }
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
        public WPFTreeViewItem GetItem(params int[] indices)
        {
            return new WPFTreeViewItem(InvokeStaticRetAppVar(GetItemInTarget, Ret<TreeViewItem>(), indices));
        }

        public WPFTreeViewItem SelectedItem => new WPFTreeViewItem(InvokeStaticRetAppVar(GetSelectedItemInTarget, Ret<TreeViewItem>()));

        static TreeViewItem GetItemInTarget(TreeView tree, string[] headerTexts)
        {
            return GetItemEx(tree, tree, headerTexts, 0);
        }

        static TreeViewItem GetItemInTarget(TreeView tree, int[] indices)
        {
            return GetItemEx(tree, tree, indices, 0);
        }

        static TreeViewItem GetSelectedItemInTarget(TreeView tree) => (TreeViewItem)tree.ItemContainerGenerator.ContainerFromItem(tree.SelectedItem);

        static void ShowNextItem(TreeViewItem item)
        {
            if (item.IsExpanded) return;
            var peer = new TreeViewItemAutomationPeer(item);
            IExpandCollapseProvider expander = peer;
            expander.Expand();
            InvokeUtility.DoEvents();
        }

        static TreeViewItem GetItemEx(TreeView tree, ItemsControl parent, string[] headerTexts, int index)
        {
            for (int i = 0; i < parent.Items.Count; i++)
            {
                var item = GetItemAndScrollIntoView(tree, parent, i);
                var text = HeaderedItemsControlUtility.GetItemText(item);
                if (text == headerTexts[index])
                {
                    if (index == headerTexts.Length - 1)
                    {
                        item.BringIntoView();
                        return item;
                    }
                    else
                    {
                        ShowNextItem(item);
                        return GetItemEx(tree, item, headerTexts, index + 1);
                    }
                }
            }

            throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);
        }

        static TreeViewItem GetItemEx(TreeView tree, ItemsControl parent, int[] headerTexts, int index)
        {
            var targetIndex = headerTexts[index];
            if (parent.Items.Count <= targetIndex) throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);

            var item = GetItemAndScrollIntoView(tree, parent, targetIndex);

            if (index == headerTexts.Length - 1)
            {
                item.BringIntoView();
                return item;
            }
            else
            {
                ShowNextItem(item);
                return GetItemEx(tree, item, headerTexts, index + 1);
            }
        }

        static TreeViewItem GetItemAndScrollIntoView(TreeView tree, ItemsControl parent, int i)
        {
            var box = VisualTreeHelper.GetDescendantBounds(tree);

            var peer = ItemsControlAutomationPeer.CreatePeerForElement(tree);
            var scrollProvider = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
            var direction = ScrollAmount.SmallIncrement;

            bool min1 = false;
            bool max1 = false;
            while (true)
            {
                var item = parent.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                if (item != null)
                {
                    item.BringIntoView();
                    InvokeUtility.DoEvents();
                    var top = item.TranslatePoint(new Point(), tree);
                    if (box.Contains(new Point(box.X, top.Y + item.ActualHeight / 2)))
                    {
                        return item;
                    }
                }

                if (scrollProvider.VerticalScrollPercent == 100)
                {
                    if (max1)
                    {
                        direction = ScrollAmount.SmallDecrement;
                    }
                    else
                    {
                        max1 = true;
                    }
                }
                if (direction == ScrollAmount.SmallDecrement && scrollProvider.VerticalScrollPercent == 0)
                {
                    if (min1)
                    {
                        if (item != null) return item;
                        throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);
                    }
                    else
                    {
                        min1 = true;
                    }
                }

                if (!scrollProvider.VerticallyScrollable)
                {
                    if (item != null) return item;
                }

                scrollProvider.Scroll(ScrollAmount.NoAmount, direction);
                InvokeUtility.DoEvents();
            }
        }

        static TreeViewItem[] GetTreeChildren(DependencyObject control, int index)
        {
            var list = new List<TreeViewItem>();
            if (index != 0)
            {
                var item = control as TreeViewItem;
                if (item != null)
                {
                    list.Add(item);
                    return list.ToArray();
                }
            }
            int count = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(control, i);
                if (child == null) continue;
                list.AddRange(GetTreeChildren(child, index + 1));
            }
            return list.ToArray();
        }

    }

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.WPFTreeView.
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">UserControlDriver of item.</typeparam>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.WPFTreeViewに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="TItemUserControlDriver">アイテムのUserControlDriver</typeparam>
#endif
    public class WPFTreeView<TItemUserControlDriver> : WPFTreeView where TItemUserControlDriver : class
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
        public WPFTreeView(AppVar appVar)
            : base(appVar) { }

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
        public TItemUserControlDriver SelectedItemDriver
        {
            get
            {
                if (this["SelectedItem"]().IsNull) return null;
                return UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(SelectedItem);
            }
        }

#if ENG
        /// <summary>
        /// Get item's UserControlDriver.
        /// </summary>
        /// <param name="headerTexts">目的のアイテムまでのテキストの配列です。</param>
        /// <returns>UserControlDriver.</returns>
#else
        /// <summary>
        /// 指定のインデックスのアイテムに割当たったUserControlDriverを取得
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>UserControlDriver</returns>
#endif
        public TItemUserControlDriver GetItemDriver(params string[] headerTexts)
            => UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(headerTexts));

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
        /// <param name="indices">目的のアイテムまでの各階層でのインデックスの配列です。</param>
        /// <returns>UserControlDriver</returns>
#endif
        public TItemUserControlDriver GetItemDriver(params int[] indices)
            => UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(indices));

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
        protected override Type GetInvokeType() => typeof(WPFTreeView);
    }
}
