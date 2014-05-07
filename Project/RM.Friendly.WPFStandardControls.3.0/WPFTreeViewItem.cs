using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Reflection;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.TreeViewItem.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.TreeViewItemに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFTreeViewItem : WPFControlBase<TreeViewItem>
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
        public WPFTreeViewItem(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Get expanding state.
        /// </summary>
#else
        /// <summary>
        /// 展開状態であるかを取得。
        /// </summary>
#endif
        public bool IsExpanded { get { return Getter<bool>("IsExpanded"); } }

#if ENG
        /// <summary>
        /// Get selection state.
        /// </summary>
#else
        /// <summary>
        /// 選択状態であるかを取得。
        /// </summary>
#endif
        public bool IsSelected { get { return Getter<bool>("IsSelected"); } }

#if ENG
        /// <summary>
        /// Get item text.
        /// </summary>
#else
        /// <summary>
        /// アイテムのテキストを取得します。
        /// </summary>
#endif
        public string Text { get { return (string)AppVar.App[typeof(HeaderedItemsControlUtility), "GetItemText"](AppVar).Core; } }

#if ENG
        /// <summary>
        /// Get Visual inside TreeViewItem on VisualTree.
        /// </summary>
        /// <param name="typeFullName">Type full name.</param>
        /// <returns>AppVar corresponding to a Visual. </returns>
#else
        /// <summary>
        /// VisualTree上でTreeViewItemの内側にあるVisualを取得します。
        /// ButtonやRadioButtonなどです。
        /// </summary>
        /// <param name="typeFullName">取得したいVisual要素のタイプフルネーム。</param>
        /// <returns>Visual要素に対応したAppVar。</returns>
#endif
        public AppVar GetCoreElement(string typeFullName)
        {
            return InvokeStaticRetAppVar(GetCoreElement, Ret<Visual>(), typeFullName);
        }

        static Visual GetCoreElement(TreeViewItem item, string typeFullName)
        {
            var element = VisualTreeUtility.GetCoreElement(item, typeFullName);
            if (element == null)
            {
                throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundElement);
            }
            return element;
        }

#if ENG
        /// <summary>
        /// Change Expanded.
        /// </summary>
        /// <param name="isExpanded">Item's expanding state.</param>
#else
        /// <summary>
        /// 展開状態を変更します。
        /// </summary>
        /// <param name="isExpanded">展開状態。</param>
#endif
        public void EmulateChangeExpanded(bool isExpanded)
        {
            InvokeStatic(EmulateChangeExpanded, isExpanded);
        }

#if ENG
        /// <summary>
        /// Change Expanded.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="isExpanded">Item's expanding state.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 展開状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="isExpanded">展開状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeExpanded(bool isExpanded, Async async)
        {
            InvokeStatic(EmulateChangeExpanded, async, isExpanded);
        }

        static void EmulateChangeExpanded(TreeViewItem item, bool isExpanded)
        {
            item.Focus();
            item.IsExpanded = isExpanded;
        }

#if ENG
        /// <summary>
        /// Change Selected.
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
#endif
        public void EmulateChangeSelected(bool isSelected)
        {
            InvokeStatic(EmulateChangeSelected, isSelected);
        }

#if ENG
        /// <summary>
        /// Change Selected.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelected(bool isSelected, Async async)
        {
            InvokeStatic(EmulateChangeSelected, async, isSelected);
        }

        static void EmulateChangeSelected(TreeViewItem item, bool isSelected)
        {
            item.Focus();
            item.IsSelected = isSelected;
        }
    }
}
