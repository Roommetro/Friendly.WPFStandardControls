using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Primitives.ButtonBase.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Primitives.ButtonBaseのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFButtonBase : WPFControlBase
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFButtonBase(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar) { }

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
            EmulateInTarget();
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
            EmulateInTarget(async);
        }

        /// <summary>
        /// クリックです。
        /// </summary>
        /// <param name="button">ボタン。</param>
        static void EmulateClickInTarget(ButtonBase button)
        {
            button.Focus();
            MethodInfo methodInfo = button.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            methodInfo.Invoke(button, new object[] { });
        }
    }
}
