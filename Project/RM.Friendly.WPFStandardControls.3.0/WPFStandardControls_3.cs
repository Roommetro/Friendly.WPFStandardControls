using System;
using System.Reflection;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Initialize RM.Friendly.WPFStandardControls.3.
    /// </summary>
#else
    /// <summary>
    /// RM.Friendly.WPFStandardControls.3の初期化。
    /// </summary>
#endif
    public static class WPFStandardControls_3
    {
#if ENG
        /// <summary>
        /// Install RM.Friendly.WPFStandardControls.3.dll to target process.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
#else
        /// <summary>
        /// 対象プロセスにRM.Friendly.WPFStandardControls.3.dllをインジェクションします。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
#endif
        public static void Injection(WindowsAppFriend app)
        {
            string key = typeof(WPFStandardControls_3).FullName;
            object isInit;
            if (!app.TryGetAppControlInfo(key, out isInit))
            {
                WindowsAppExpander.LoadAssembly(app, typeof(WPFStandardControls_3).Assembly);
                ResourcesLocal3.Initialize(app);
                app.AddAppControlInfo(key, true);
            }
        }
    }
}
