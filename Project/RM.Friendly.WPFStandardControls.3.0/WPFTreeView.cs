using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System;
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
            return HeaderedItemsControlUtility.GetItem<TreeViewItem>(tree, headerTexts, ShowNextItem);
        }

        static TreeViewItem GetItemInTarget(TreeView tree, int[] indices)
        {
            return HeaderedItemsControlUtility.GetItem<TreeViewItem>(tree, indices, ShowNextItem);
        }

        static TreeViewItem GetSelectedItemInTarget(TreeView tree) => (TreeViewItem)tree.ItemContainerGenerator.ContainerFromItem(tree.SelectedItem);

        static void ShowNextItem(TreeViewItem item)
        {
            var peer = new TreeViewItemAutomationPeer(item);
            IExpandCollapseProvider expander = peer;
            expander.Expand();
            InvokeUtility.DoEvents();
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
