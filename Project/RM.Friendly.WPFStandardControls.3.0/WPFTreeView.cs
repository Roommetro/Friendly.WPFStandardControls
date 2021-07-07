using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
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

#if ENG
        /// <summary>
        /// Selected Item.
        /// </summary>
#else
        /// <summary>
        /// 選択中のアイテム
        /// </summary>
#endif
        public WPFTreeViewItem SelectedItem => new WPFTreeViewItem(InvokeStaticRetAppVar(GetSelectedItemInTarget, Ret<TreeViewItem>()));

        static TreeViewItem GetItemInTarget(TreeView tree, string[] headerTexts)
        {
            return GetItemEx(tree, tree, headerTexts, 0);
        }

        static TreeViewItem GetItemInTarget(TreeView tree, int[] indices)
        {
            return GetItemEx(tree, tree, indices, 0);
        }

        static TreeViewItem GetSelectedItemInTarget(TreeView tree)
        {
            foreach (var e in TreeUtilityInTarget.VisualTree(tree))
            {
                var item = e as TreeViewItem;
                if (item != null && item.DataContext == tree.SelectedItem) return item;
            }
            return null;
        }

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

        static TreeViewItem GetItemEx(TreeView tree, ItemsControl parent, int[] indices, int index)
        {
            var targetIndex = indices[index];
            if (parent.Items.Count <= targetIndex) throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);

            var item = GetItemAndScrollIntoView(tree, parent, targetIndex);

            if (index == indices.Length - 1)
            {
                item.BringIntoView();
                return item;
            }
            else
            {
                ShowNextItem(item);
                return GetItemEx(tree, item, indices, index + 1);
            }
        }

        static TreeViewItem GetItemAndScrollIntoView(TreeView treeView, ItemsControl parent, int i)
        {
            var isVirtualized = VirtualizingStackPanel.GetIsVirtualizing(treeView);
            return isVirtualized
                ? GetItemAndScrollIntoViewForVirtualized(treeView, parent, i)
                : GetItemAndScrollIntoViewForNonVirtualized(treeView, parent, i);
        }

        static TreeViewItem GetItemAndScrollIntoViewForNonVirtualized(TreeView treeView, ItemsControl parent, int i)
        {
            // 仮想化されていないので常にとれる
            var container = (TreeViewItem)parent.ItemContainerGenerator.ContainerFromIndex(i);
            container.BringIntoView();
            InvokeUtility.DoEvents();
            return container;
        }

        static TreeViewItem GetItemAndScrollIntoViewForVirtualized(TreeView tree, ItemsControl parent, int i)
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
                    // 時々スクロールしすぎるあるらしい。item.DataContextがDisconnectedになってる時はもう一回やりなおす。
                    if(item.DataContext == WPFTreeViewReflection.GetDisconnectedSource())
                    {
                        continue;
                    }
                    var top = item.TranslatePoint(new Point(), tree);
                    // TreeViewItemのActualHeightはItemsHost(子要素)を含んだ高さを返すので、ItemsHostを除いた高さで評価する
                    if (box.Contains(new Point(box.X, top.Y + (item.ActualHeight - GetItemsHostHeight(item)) / 2)))
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

                var now = DateTime.Now;
                var current = scrollProvider.VerticalScrollPercent;
                scrollProvider.Scroll(ScrollAmount.NoAmount, direction);
                InvokeUtility.DoEvents();
                while (DateTime.Now - now < TimeSpan.FromSeconds(1)
                    && current == scrollProvider.VerticalScrollPercent)
                {
                    InvokeUtility.DoEvents();
                }
            }
        }

        static double GetItemsHostHeight(TreeViewItem item)
        {
            return WPFTreeViewReflection.GetItemsHostFor(item)?.ActualHeight ?? 0;
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

        static int[] GetActiveIndices(TreeView tree)
        {
            DependencyObject focusedElement = null;
            if (tree.IsKeyboardFocusWithin)
            {
                focusedElement = Keyboard.FocusedElement as DependencyObject;
            }
            else if (tree.IsMouseCaptureWithin)
            {
                focusedElement = Mouse.Captured as DependencyObject;
            }
            if (focusedElement == null) return new int[0];

            var focusedVisual = (List<DependencyObject>)TreeUtilityInTarget.VisualTree(focusedElement, TreeRunDirection.Ancestors);
            for (var i = 0; i < focusedVisual.Count; i++)
            {
                var item = focusedVisual[i] as TreeViewItem;
                if (item == null) continue;

                var targetItemIndex = i;
                i++;
                for (; i < focusedVisual.Count; i++)
                {
                    var findedTreeView = focusedVisual[i] as TreeView;
                    if (findedTreeView == null) continue;

                    //TreeView in TreeView
                    if (findedTreeView != tree) break;

                    //Get Keys.
                    var indices = new List<int>();
                    for (int j = targetItemIndex; j < i; j++)
                    {
                        var focusedItem = focusedVisual[j] as TreeViewItem;
                        if (focusedItem == null) continue;
                        indices.Add(GetIndex(focusedItem, focusedVisual, j, i));
                    }
                    indices.Reverse();
                    return indices.ToArray();
                }
            }
            return new int[0];
        }

        static int GetIndex(TreeViewItem item, List<DependencyObject> focusedVisual, int itemIndex, int treeIndex)
        {
            for (int j = itemIndex + 1; j < treeIndex; j++)
            {
                var parent = focusedVisual[j] as TreeViewItem;
                if (parent != null)
                {
                    return parent.ItemContainerGenerator.IndexFromContainer(item);
                }
            }
            return ((TreeView)focusedVisual[treeIndex]).ItemContainerGenerator.IndexFromContainer(item);
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
        /// </summary>
#endif
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
        /// Active item key.
        /// </summary>
#else
        /// <summary>
        /// アクティブな(キーボードフォーカスを持っている)アイテムのキーの取得
        /// </summary>
#endif
        public int[] ActiveItemIndices => (int[])App[typeof(WPFTreeView), "GetActiveIndices"](this).Core;

#if ENG
        /// <summary>
        /// Get item's UserControlDriver.
        /// </summary>
        /// <param name="headerTexts">The array of text to the target item. </param>
        /// <returns>UserControlDriver.</returns>
#else
        /// <summary>
        /// 指定のインデックスのアイテムに割当たったUserControlDriverを取得
        /// </summary>
        /// <param name="headerTexts">目的のアイテムまでのテキストの配列です。</param>
        /// <returns>UserControlDriver</returns>
#endif
        public TItemUserControlDriver GetItemDriver(params string[] headerTexts)
            => UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(headerTexts));

#if ENG
        /// <summary>
        /// Get item's UserControlDriver.
        /// </summary>
        /// <param name="indices">Item index.</param>
        /// <returns>UserControlDriver.</returns>
#else
        /// <summary>
        /// 指定のインデックスのアイテムに割当たったUserControlDriverを取得
        /// </summary>
        /// <param name="indices">目的のアイテムまでの各階層でのインデックスの配列です。</param>
        /// <returns>UserControlDriver</returns>
#endif
        [ItemDriverGetter(ActiveItemKeyProperty = "ActiveItemIndices")]
        public TItemUserControlDriver GetItemDriver(params int[] indices)
            => (TestAssistantMode.IsCreatingMode && indices.Length == 0) ? null : UserControlDriverUtility.AttachDriver<TItemUserControlDriver>(GetItem(indices));

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

#if ENG
        /// <summary>
        /// Call during capture.
        /// AppVar of Capture Code Generator running in the target process comes over.
        /// </summary>
        /// <param name="captureCodeGenerator">Capture Code Generator.</param>
#else
        /// <summary>
        /// キャプチャ中に呼ばれる。
        /// 対象プロセス内で動作するCapture Code Generator の AppVar が渡ってくる。
        /// </summary>
        /// <param name="captureCodeGenerator">Capture Code Generator.</param>
#endif
        [CaptureCodeGeneratorCustom]
        public virtual void CustomCaptureCodeGenerator(AppVar captureCodeGenerator)
            => captureCodeGenerator["IsTextKey"](false);
    }
}
