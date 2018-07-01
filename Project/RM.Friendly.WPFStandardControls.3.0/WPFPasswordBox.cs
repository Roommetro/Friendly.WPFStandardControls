using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.PasswordBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.PasswordBoxに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.PasswordBox")]
    public class WPFPasswordBox : WPFControlBase<PasswordBox>
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
        public WPFPasswordBox(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the control's password.
        /// </summary>
#else
        /// <summary>
        /// パスワードを取得します。
        /// </summary>
#endif
        public string Password
        {
            get { return Getter<string>("Password"); }
        }

#if ENG
        /// <summary>
        /// Sets the control's password.
        /// </summary>
        /// <param name="password">Password to use.</param>
#else
        /// <summary>
        /// パスワードを変更します。
        /// </summary>
        /// <param name="password">パスワード。</param>
#endif
        public void EmulateChangePassword(string password)
        {
            InvokeStatic(EmulateChangePassword, password);
        }

#if ENG
        /// <summary>
        /// Sets the control's password.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="password">Password to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// パスワードを変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="password">パスワード。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangePassword(string password, Async async)
        {
            InvokeStatic(EmulateChangePassword, async, password);
        }

        private static void EmulateChangePassword(PasswordBox passwordBox, string value)
        {
            passwordBox.Focus();
            passwordBox.Password = value;
        }
    }
}
