using System;
using System.Reflection;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Initialize RM.Friendly.WPFStandardControls.4.
    /// </summary>
#else
    /// <summary>
    /// RM.Friendly.WPFStandardControls.4の初期化。
    /// </summary>
#endif
    public static class WPFStandardControls_4
    {
#if ENG
        /// <summary>
        /// Install RM.Friendly.WPFStandardControls.4.dll to target process.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
#else
        /// <summary>
        /// 対象プロセスにRM.Friendly.WPFStandardControls.4.dllをインジェクションします。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
#endif
        public static void Injection(WindowsAppFriend app)
        {
            WPFStandardControls_3_5.Injection(app);
            string key = typeof(WPFStandardControls_4).ToString();
            object isInit;
            if (!app.TryGetAppControlInfo(key, out isInit))
            {
                WindowsAppExpander.LoadAssembly(app, typeof(WPFStandardControls_4).Assembly);
                ResourcesLocal4.Initialize(app);
                app.AddAppControlInfo(key, true);
            }
        }
    }
}
