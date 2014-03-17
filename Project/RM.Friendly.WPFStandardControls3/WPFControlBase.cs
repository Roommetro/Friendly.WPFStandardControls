﻿using Codeer.Friendly;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// This is the base class for classes that operate on basic controls in System.Windows.Controls.
    /// </summary>
#else
    /// <summary>
    /// WPFのコントロールを操作するためのクラスの基本クラスです。
    /// </summary>
#endif
    public class WPFControlBase : AppVarWrapper
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
        protected WPFControlBase(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar) { }
    }
}
