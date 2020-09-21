using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Reflection;
using System.Windows.Documents;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Documents.Hyperlink.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Documents.Hyperlinkに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Documents.Hyperlink")]
    public class WPFHyperlink : IAppVarOwner
    {
#if ENG
        /// <summary>
        /// Variable manipulation object within the target application.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション変数操作クラスです。
        /// </summary>
#endif
        public AppVar AppVar { get; }

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
        public WPFHyperlink(AppVar appVar)
            => AppVar = appVar;
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
            AppVar.App[typeof(WPFHyperlink), "EmulateClick"](this);
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
            AppVar.App[typeof(WPFHyperlink), "EmulateClick", async](this);
        }

        static void EmulateClick(Hyperlink hyperLink)
        {
            hyperLink.Focus();
            MethodInfo methodInfo = hyperLink.GetType().GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            methodInfo.Invoke(hyperLink, new object[] { });
        }
    }
}
