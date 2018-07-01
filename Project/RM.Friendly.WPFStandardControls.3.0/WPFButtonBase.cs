using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
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
    /// TypeがSystem.Windows.Controls.Primitives.ButtonBaseに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.Primitives.ButtonBase")]
    public class WPFButtonBase : WPFControlBase<ButtonBase>
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
        public WPFButtonBase(AppVar appVar)
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
        /// <param name="button">ボタン。</param>
        static void EmulateClick(ButtonBase button)
        {
            button.Focus();
            MethodInfo methodInfo = button.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            methodInfo.Invoke(button, new object[] { });
        }
    }
}
