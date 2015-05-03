using System;
using System.Reflection;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Initialize RM.Friendly.WPFStandardControls.3.5.
    /// </summary>
#else
    /// <summary>
    /// RM.Friendly.WPFStandardControls.3.5の初期化。
    /// </summary>
#endif
    public static class WPFStandardControls_3_5
    {
#if ENG
        /// <summary>
        /// Install RM.Friendly.WPFStandardControls.3.5.dll to target process.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
#else
        /// <summary>
        /// 対象プロセスにRM.Friendly.WPFStandardControls.3.5.dllをインジェクションします。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
#endif
        public static void Injection(WindowsAppFriend app)
        {
            WPFStandardControls_3.Injection(app);
            string key = typeof(WPFStandardControls_3_5).FullName;
            object isInit;
            if (!app.TryGetAppControlInfo(key, out isInit))
            {
                WindowsAppExpander.LoadAssembly(app, typeof(WPFStandardControls_3_5).Assembly);
                app.AddAppControlInfo(key, true);
            }
        }
    }
}
