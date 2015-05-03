using Codeer.Friendly;
using RM.Friendly.WPFStandardControls.Properties;
using System;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// IEnumerable<DependencyObject> in target app.
    /// </summary>
#else
    /// <summary>
    /// DependencyObjectのコレクションです。
    /// </summary>
#endif
    public class WPFDependencyObjectCollection : IAppVarOwner
    {

#if ENG
        /// <summary>
        /// IEnumerable<DependencyObject> in target app.
        /// </summary>
#else
        /// <summary>
        /// DependencyObjectのコレクションです。
        /// </summary>
#endif
        public AppVar AppVar { get; private set; }

#if ENG
        /// <summary>
        /// Count.
        /// </summary>
#else
        /// <summary>
        /// コレクションの数。
        /// </summary>
#endif
        public int Count { get { return (int)AppVar["Count"]().Core; } }

#if ENG
        /// <summary>
        /// DependencyObject of index in target app .
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>DependencyObject of index in target app .</returns>
#else
        /// <summary>
        /// 対象プロセス内での指定のインデックスのDependencyObject。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>対象プロセス内での指定のインデックスのDependencyObject。</returns>
#endif
        public AppVar this[int index]
        {
            get { return AppVar["[]"](index); }
        }

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
        public WPFDependencyObjectCollection(AppVar appVar)
        {
            AppVar = appVar;
        }

#if ENG
        /// <summary>
        /// Get only one item.
        /// </summary>
        /// <returns></returns>
#else
        /// <summary>
        /// コレクションの要素が一つであることを確認してそれを取得する。
        /// </summary>
        /// <returns></returns>
#endif
        public AppVar Single()
        {
            if (Count != 1)
            {
                throw new NotSupportedException(string.Format(Resources.NotSingle, Count));
            }
            return AppVar["[]"](0);
        }
    }
}
