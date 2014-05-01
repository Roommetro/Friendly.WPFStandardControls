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
    /// Provides operations on controls of type System.Windows.Controls.MenuItem.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.MenuItemに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFMenuItem : WPFControlBase<MenuItem>
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
        public WPFMenuItem(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the control's check state.
        /// </summary>
#else
        /// <summary>
        /// チェック状態を取得します。
        /// </summary>
#endif
        public bool IsChecked { get { return Getter<bool>("IsChecked"); } }

#if ENG
        /// <summary>
        /// Returns that item is checkable.
        /// </summary>
#else
        /// <summary>
        /// チェック可能であるかを取得します。
        /// </summary>
#endif
        public bool IsCheckable { get { return Getter<bool>("IsCheckable"); } }


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
        /// Get Visual inside MenuItem on VisualTree.
        /// </summary>
        /// <param name="typeFullName">Type full name.</param>
        /// <returns>AppVar corresponding to a Visual. </returns>
#else
        /// <summary>
        /// VisualTree上でMenuItemの内側にあるVisualを取得します。
        /// ButtonやRadioButtonなどです。
        /// </summary>
        /// <param name="typeFullName">取得したいVisual要素のタイプフルネーム。</param>
        /// <returns>Visual要素に対応したAppVar。</returns>
#endif
        public AppVar GetCoreElement(string typeFullName)
        {
            return InvokeStaticRetAppVar(GetCoreElement, Ret<Visual>(), typeFullName);
        }

#if ENG
        /// <summary>
        /// Performs a click.
        /// </summary>
#else
        /// <summary>
        /// クリックです。
        /// </summary>
#endif
        public void EmulateClick()
        {
            InvokeStatic(EmulateClick);
        }

#if ENG
        /// <summary>
        /// Performs a click.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// クリックです。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateClick(Async async)
        {
            InvokeStatic(EmulateClick, async);
        }

        static void EmulateClick(MenuItem item)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            item.Focus();
            invoker.Invoke();
            InvokeUtility.DoEvents();
        }

        static Visual GetCoreElement(MenuItem item, string typeFullName)
        {
            var element = HeaderedItemsControlUtility.GetCoreElement(item, typeFullName);
            if (element == null)
            {
                throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundElement);
            }
            return element;
        }
    }
}
