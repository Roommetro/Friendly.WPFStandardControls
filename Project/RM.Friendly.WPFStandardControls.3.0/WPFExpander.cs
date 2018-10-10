using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Reflection;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Expander.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Expanderに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.Expander")]
    public class WPFExpander : WPFControlBase<Expander>
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
        public WPFExpander(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the control's expanded state.
        /// </summary>
#else
        /// <summary>
        /// 展開状態を取得します。
        /// </summary>
#endif
        public bool IsExpanded
        {
            get { return Getter<bool>("IsExpanded"); }
        }

#if ENG
        /// <summary>
        /// Open.
        /// </summary>
#else
        /// <summary>
        /// 開きます。
        /// </summary>
#endif
        public void EmulateOpen()
        {
            App[typeof(WPFExpander), "EmulateChangeExpanded"](this, true);
        }

#if ENG
        /// <summary>
        /// Open.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 開きます。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateOpen(Async async)
        {
            App[typeof(WPFExpander), "EmulateChangeExpanded", async](this, true);
        }

#if ENG
        /// <summary>
        /// Close.
        /// </summary>
#else
        /// <summary>
        /// 閉じます。
        /// </summary>
#endif
        public void EmulateClose()
        {
            App[typeof(WPFExpander), "EmulateChangeExpanded"](this, false);
        }

#if ENG
        /// <summary>
        /// Close.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 閉じます。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateClose(Async async)
        {
            App[typeof(WPFExpander), "EmulateChangeExpanded", async](this, false);
        }


        static void EmulateChangeExpanded(Expander expander, bool isExpanded)
        {
            expander.Focus();
            expander.IsExpanded = isExpanded;
        }
    }
}
