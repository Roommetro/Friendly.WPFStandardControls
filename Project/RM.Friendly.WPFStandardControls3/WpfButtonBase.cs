using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFButtonBase : WPFControlBase
    {
        public WPFButtonBase(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar)
        {
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
            App[GetType(), "EmulateClickInTarget"](AppVar);
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
            App[GetType(), "EmulateClickInTarget", async](AppVar);
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
