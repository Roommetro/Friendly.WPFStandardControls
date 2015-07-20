using Codeer.Friendly;
using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Collection of DependencyObject in target app.
    /// </summary>
#else
    /// <summary>
    /// DependencyObjectのコレクションです。
    /// </summary>
#endif
    public interface IWPFDependencyObjectCollection<out T> where T : DependencyObject
    {
#if ENG
        /// <summary>
        /// Count.
        /// </summary>
#else
        /// <summary>
        /// コレクションの数。
        /// </summary>
#endif
        int Count { get; }

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
        AppVar this[int index] { get; }

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
        AppVar Single();
    }
}
