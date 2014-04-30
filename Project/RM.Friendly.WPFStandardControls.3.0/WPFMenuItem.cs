using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Reflection;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

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

        /// <summary>
        /// クリックです。
        /// </summary>
        /// <param name="item">メニューアイテム。</param>
        static void EmulateClick(MenuItem item)
        {
            IInvokeProvider invoker = new MenuItemAutomationPeer(item);
            item.Focus();
            invoker.Invoke();
            InvokeUtility.DoEvents();
        }
    }
}
