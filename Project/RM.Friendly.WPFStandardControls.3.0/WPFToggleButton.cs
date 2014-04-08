using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Primitives.ToggleButton.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Primitives.ToggleButtonのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFToggleButton : WPFControlBase
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
        public WPFToggleButton(AppVar appVar)
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
        public bool? IsChecked { get { return Getter<bool?>("IsChecked"); } }

#if ENG
        /// <summary>
        /// Returns that the control is 3 state.
        /// </summary>
#else
        /// <summary>
        /// 3ステートのトグルボタンであるかを取得します。
        /// </summary>
#endif
        public bool IsThreeState { get { return Getter<bool>("IsThreeState"); } }

#if ENG
        /// <summary>
        /// Sets the control's check state.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="value">Check state.</param>
#else
        /// <summary>
        /// チェック状態を設定します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="value">チェック状態。</param>
#endif
        public void EmulateCheck(bool? value)
        {
            InvokeStatic("EmulateCheck", value);
        }

#if ENG
        /// <summary>
        /// Sets the control's check state.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="value">Check state.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// チェック状態を設定します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="value">チェック状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCheck(bool? value, Async async)
        {
            InvokeStatic("EmulateCheck", async, value);
        }

        private static void EmulateCheck(ToggleButton toggle, bool? value)
        {
            toggle.Focus();
            toggle.IsChecked = value;
        }
    }
}