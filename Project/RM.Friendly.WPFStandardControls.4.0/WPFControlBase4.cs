using System;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// This is the base class for classes that operate on basic controls in .Net4.0 System.Windows.Controls.
    /// </summary>
#else
    /// <summary>
    /// .Net4.0のWPFのコントロールを操作するためのクラスの基本クラスです。
    /// </summary>
#endif
    public class WPFControlBase4<CoreType> : WPFControlBase
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
        protected WPFControlBase4(AppVar appVar)
            : base(appVar)
        {
            Initializer4.Initialize((WindowsAppFriend)appVar.App);
        }

        //AppVarWrapperに移動予定。
        //partialにして、別ファイルにいくつか定義するかなー。
        //Func(戻り値あり)バージョンも定義するか・・・。
        protected void StaticAction(Action<CoreType> a)
        {
            InvokeStatic(a.Method.Name);
        }

        protected void StaticAction<T>(Action<CoreType, T> a, T t)
        {
            InvokeStatic(a.Method.Name, t);
        }

        protected void StaticAction<T1, T2>(Action<CoreType, T1, T2> a, T1 t1, T2 t2)
        {
            InvokeStatic(a.Method.Name, t1, t2);
        }

        protected void StaticAction(Action<CoreType> a, Async async)
        {
            InvokeStatic(a.Method.Name, async);
        }

        protected void StaticAction<T>(Action<CoreType, T> a, Async async, T t)
        {
            InvokeStatic(a.Method.Name, async, t);
        }

        protected void StaticAction<T1, T2>(Action<CoreType, T1, T2> a, Async async, T1 t1, T2 t2)
        {
            InvokeStatic(a.Method.Name, async, t1, t2);
        }
    }
}
