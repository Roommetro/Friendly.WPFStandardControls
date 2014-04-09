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
    public class WPFControlBase4 : WPFControlBase
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

        protected AppVar InvokeStatic(Action a)
        {
            return InvokeStatic(a.Method.Name);
        }

        protected AppVar InvokeStatic<T>(Action<T> a, T t)
        {
            return InvokeStatic(a.Method.Name, t);
        }

        protected AppVar InvokeStatic<T1, T2>(Action<T1, T2> a, T1 t1, T2 t2)
        {
            return InvokeStatic(a.Method.Name, t1, t2);
        }

        protected AppVar InvokeStatic(Action a, Async async)
        {
            return InvokeStatic(a.Method.Name, async);
        }

        protected AppVar InvokeStatic<T>(Action<T> a, Async async, T t)
        {
            return InvokeStatic(a.Method.Name, async, t);
        }

        protected AppVar InvokeStatic<T1, T2>(Action<T1, T2> a, Async async, T1 t1, T2 t2)
        {
            return InvokeStatic(a.Method.Name, async, t1, t2);
        }
    }
}
